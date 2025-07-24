using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MasterMech
{
    class CustomerClass
    {
		public int lnCustNo;        // NOT NULL,
		public string lsCustFName;  //NOT NULL,
		public string lsCustLName;  //NOT NULL,
		public string lsCustMobNo;  //NOT NULL,
		public string lsCustEmail;  // NOT NULL,
		public string lsCustSts;    // NOT NULL,
		public string CustType;       // NOT NULL,
     	public string lsCustStAddr;
		public string lsCustArAddr;
		public string lsCustCity;
		public string lsCustState;
		public string CustPinCode;
		public string lsCustCountry;
		public string lsCustGSTNo;
		public DateTime dCustlLastVisit;
		public string lsCustRemarks;
		public DateTime dCreated;
		public string lsCreatedBy;
		public DateTime? dModified;
		public string lsModifiedBy;
		public char Deleted;
		public DateTime dDeletedOn;
		public string lsDeletedBy;

		private string ConnStr;
		private string iCustNo;

		public CustomerClass()
		{
			
		}

		public CustomerClass(int CustNo, string CustFName, string CustLName, string CustMobNo, string CustEmail, string CustSts, string CCustType)
		{
			lnCustNo = CustNo;
			lsCustFName = CustFName;
			lsCustLName = CustLName;
			lsCustMobNo = CustMobNo;
			lsCustEmail = CustEmail;
			lsCustSts = CustSts;
			CustType = CCustType;
		}
		public CustomerClass(string isConStr, string InCustNo)
		{
			ConnStr = isConStr;
			iCustNo = InCustNo;
        }


		public bool SaveCCustomer(string isConStr, int InCustNo, bool ibNewMode)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					if (ibNewMode)
					{
						string lsQuery = "INSERT INTO Customer( CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity,CustState,";
						       lsQuery += "CustPinCode,CustCountry,CustGSTNo,CustlLastVisit,CustRemarks,Created,CreatedBy,Deleted)";
						       lsQuery += " VALUES ( @CustFName, @CustLName, @CustMobNo, @CustEmail, @CustSts, @CustType, @CustStAddr, @CustArAddr, @CustCity,@CustState,";
						       lsQuery += "@CustPinCode,@CustCountry,@CustGSTNo,@CustlLastVisit,@CustRemarks,@Created,@CreatedBy,@Deleted)";

						using (SqlCommand cmd = new SqlCommand(lsQuery))
						{
							cmd.Connection = lObjCon;
							cmd.CommandType = CommandType.Text;

							
							cmd.Parameters.AddWithValue("@CustNo", SqlDbType.Int).Value = InCustNo;
							cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = lsCustFName;
							cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = lsCustLName;
							cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = lsCustMobNo;
							cmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = lsCustEmail;
							cmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = lsCustSts;
							cmd.Parameters.AddWithValue("@CustType", SqlDbType.Char).Value = CustType; 
							cmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.VarChar).Value = (object)lsCustStAddr ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.VarChar).Value = (object)lsCustArAddr ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = (object)lsCustCity ?? DBNull.Value;

							cmd.Parameters.AddWithValue("@CustState", SqlDbType.VarChar).Value = (object)lsCustState ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.Char).Value = (object)CustPinCode ?? DBNull.Value; 
							cmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = (object)lsCustCountry ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = (object)lsCustGSTNo ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@CustlLastVisit", SqlDbType.DateTime).Value = DateTime.Now;
							cmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = (object)lsCustRemarks ?? DBNull.Value; ;
							cmd.Parameters.AddWithValue("@Created", SqlDbType.DateTime).Value = DateTime.Now;
							cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
							cmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'N';
							/*cmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = dModified;
							cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;

							
							cmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.VarChar).Value = DateTime.Now;
							cmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.Char).Value = 'N';*/


							lObjCon.Open();
							cmd.ExecuteNonQuery();
							lObjCon.Close();
							return true;
						}
					}
					else
					{
						string lsQuery = "UPDATE Customer SET CustFName=@CustFName, CustLName=@CustLName, CustMobNo=@CustMobNo, CustEmail=@CustEmail, CustSts=@CustSts, CustType=@CustType, CustStAddr=@CustStAddr, CustArAddr=@CustArAddr, CustCity=@CustCity,CustState=@CustState,";
						       lsQuery += "CustPinCode=@CustPinCode,CustCountry=@CustCountry,CustGSTNo=@CustGSTNo,CustRemarks=@CustRemarks";
						       lsQuery += " ,Modified=@Modified, ModifiedBy=@ModifiedBy WHERE CustNo=@CustNo";

						using (SqlCommand cmd = new SqlCommand(lsQuery))
						{
								cmd.Connection = lObjCon;
								cmd.CommandType = CommandType.Text;

								cmd.Parameters.AddWithValue("@CustNo", SqlDbType.Int).Value = InCustNo;
								cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = lsCustFName;
								cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = lsCustLName;
								cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = lsCustMobNo;
								cmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = lsCustEmail;
								cmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = lsCustSts;
								cmd.Parameters.AddWithValue("@CustType", SqlDbType.Char).Value = CustType; 
								cmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.VarChar).Value = (object)lsCustStAddr ?? DBNull.Value;
								cmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.VarChar).Value = (object)lsCustArAddr ?? DBNull.Value;
								cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = (object)lsCustCity ?? DBNull.Value;

								cmd.Parameters.AddWithValue("@CustState", SqlDbType.VarChar).Value = (object)lsCustState ?? DBNull.Value;
								cmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.Char).Value = (object)CustPinCode ?? DBNull.Value; 
								cmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = (object)lsCustCountry ?? DBNull.Value;
								cmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = (object)lsCustGSTNo ?? DBNull.Value;
								
								cmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = (object)lsCustRemarks ?? DBNull.Value; ;
								cmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
								cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
								

							lObjCon.Open();
							cmd.ExecuteNonQuery();
							lObjCon.Close();
							return true;
						}
					}
				}
			}
			catch (SqlException ex)
			{
				MasterMechUtil.ShowError(ex.Message);
				return false;
			}
		}


		public bool SearchCustDlt(string isConStr, string isSearchUserID,List<CustomerClass> oObjCustDlt)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQryText = "SELECT CustNo, CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity,CustState,";
						   lsQryText += "CustPinCode,CustCountry,CustGSTNo,CustRemarks FROM Customer";
					       lsQryText += " WHERE Deleted ='N' AND CustFName LIKE @CustFName ";

					SqlCommand cmd = new SqlCommand(lsQryText, lObjCon);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = "%" + isSearchUserID + "%";
					
					lObjCon.Open();
					using (SqlDataReader lObjSDR = cmd.ExecuteReader())
					{
						if (lObjSDR.HasRows)
						{
							while (lObjSDR.Read())
							{
								// These values are not being checked for being null as by table design these columns can not be null
								lnCustNo = (int)lObjSDR["CustNo"];
								lsCustFName = lObjSDR["CustFName"].ToString();
								lsCustLName = lObjSDR["CustLName"].ToString();
								lsCustMobNo = lObjSDR["CustMobNo"].ToString();
								lsCustEmail = lObjSDR["CustEmail"].ToString();
								lsCustSts = lObjSDR["CustSts"].ToString();
								//CustType = char.Parse (lObjSDR["CustType"]);
								lsCustStAddr = (string)((object)lObjSDR["CustStAddr"].ToString() ?? DBNull.Value);
								lsCustArAddr = (string)((object)lObjSDR["CustArAddr"].ToString() ?? DBNull.Value);
								lsCustCity = (string)((object)lObjSDR["CustCity"].ToString() ?? DBNull.Value);
								lsCustState = (string)((object)lObjSDR["CustState"].ToString() ?? DBNull.Value);
								//CustPinCode = (char)((object)lObjSDR["CustPinCode"].ToString() ?? DBNull.Value);
								lsCustCountry = (string)((object)lObjSDR["CustCountry"].ToString() ?? DBNull.Value);
								lsCustGSTNo = (string)((object)lObjSDR["CustGSTNo"].ToString() ?? DBNull.Value);
								lsCustRemarks = (string)((object)lObjSDR["CustRemarks"].ToString() ?? DBNull.Value);



								oObjCustDlt.Add(new CustomerClass(lnCustNo, lsCustFName, lsCustLName, lsCustMobNo, lsCustEmail, lsCustSts, CustType));
							}
						}
						else
						{
							lObjCon.Close();
						}
					}
				}
			}
			catch (SqlException ex)
			{
				MasterMechUtil.ShowError(ex.Message);
			}
			return true;
		}


		public bool Load(string isConStr, int iCustNo)
		{
			bool lbDataFound = false;

			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "SELECT CustNo, CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr,CustCity,CustState,";
				lsQuery += "CustPinCode,CustCountry,CustGSTNo,CustRemarks, Created, CreatedBy, Modified, ModifiedBy FROM Customer WHERE CustNo = @CustNo AND Deleted='N'";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@CustNo", SqlDbType.VarChar).Value = iCustNo;

						lObjCon.Open();
						using (SqlDataReader lObjSDR = cmd.ExecuteReader())
						{
							if (lObjSDR.HasRows)
							{
								while (lObjSDR.Read())
								{
									lnCustNo = iCustNo;
									lsCustFName = lObjSDR["CustFName"].ToString();
									lsCustLName = lObjSDR["CustLName"].ToString();
									lsCustMobNo = lObjSDR["CustMobNo"].ToString();
                 					lsCustEmail = lObjSDR["CustEmail"].ToString();
									lsCustSts = lObjSDR["CustSts"].ToString();
									CustType =  lObjSDR["CustType"].ToString();
									lsCustStAddr = DBNull.Value.Equals(lObjSDR["CustStAddr"]) ? null : lObjSDR["CustStAddr"].ToString();
									lsCustArAddr = DBNull.Value.Equals(lObjSDR["CustArAddr"]) ? null : lObjSDR["CustArAddr"].ToString();
									lsCustCity = DBNull.Value.Equals(lObjSDR["CustCity"]) ? null : lObjSDR["CustCity"].ToString();
									lsCustState= DBNull.Value.Equals(lObjSDR["CustState"]) ? null : lObjSDR["CustState"].ToString();
									CustPinCode = DBNull.Value.Equals(lObjSDR["CustPinCode"]) ? null : lObjSDR["CustPinCode"].ToString();
									lsCustCountry = DBNull.Value.Equals(lObjSDR["CustCountry"]) ? null : lObjSDR["CustCountry"].ToString();
									lsCustGSTNo = DBNull.Value.Equals(lObjSDR["CustGSTNo"]) ? null : lObjSDR["CustGSTNo"].ToString();
									lsCustRemarks = DBNull.Value.Equals(lObjSDR["CustRemarks"]) ? null : lObjSDR["CustRemarks"].ToString();
									dCreated = (DateTime)lObjSDR["Created"];
									lsCreatedBy = DBNull.Value.Equals(lObjSDR["CreatedBy"]) ? null : lObjSDR["CreatedBy"].ToString();
									dModified = DBNull.Value.Equals(lObjSDR["Modified"]) ? (DateTime?)null : (DateTime)lObjSDR["Modified"];
									lsModifiedBy = DBNull.Value.Equals(lObjSDR["ModifiedBy"]) ? null : lObjSDR["ModifiedBy"].ToString();
								}
								lbDataFound = true;
							}
							else
								lbDataFound = false;
						}
						lObjCon.Close();
						return lbDataFound;
					}
				}

				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
					return false;
				}
			}
		}


		public bool Delete(string isConStr, int iCustNo)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQuery = "UPDATE Customer SET Deleted ='Y', DeletedOn=@DeletedOn, DeletedBy = @DeletedBy WHERE CustNo=@CustNo";

					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;
						cmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.DateTime).Value = DateTime.Now;
						cmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
						cmd.Parameters.AddWithValue("@CustNo", SqlDbType.VarChar).Value = iCustNo;

						lObjCon.Open();
						cmd.ExecuteNonQuery();
						lObjCon.Close();
						return true;
					}
				}
			}
			catch (SqlException ex)
			{
				MasterMechUtil.ShowError(ex.Message);
				return false;
			}
		}


		public bool SearchAdvance(string isConStr, string isSearchUserID,string isSearchLast,string isSearchCity, List<CustomerClass> oObjCustDlt)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQryText = "SELECT CustNo, CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity,CustState,";
						   lsQryText += "CustPinCode,CustCountry,CustGSTNo,CustRemarks FROM Customer";
					       lsQryText += " WHERE Deleted ='N' AND CustFName LIKE @CustFName  AND CustLName LIKE @CustLName AND CustCity LIKE @CustCity";

					SqlCommand cmd = new SqlCommand(lsQryText, lObjCon);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = "%" + isSearchUserID + "%";
					cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = "%" + isSearchLast + "%";
					cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = "%" + isSearchCity + "%";
					lObjCon.Open();
					using (SqlDataReader lObjSDR = cmd.ExecuteReader())
					{
						if (lObjSDR.HasRows)
						{
							while (lObjSDR.Read())
							{
								// These values are not being checked for being null as by table design these columns can not be null
								lnCustNo = (int)lObjSDR["CustNo"];
								lsCustFName = lObjSDR["CustFName"].ToString();
								lsCustLName = lObjSDR["CustLName"].ToString();
								lsCustMobNo = lObjSDR["CustMobNo"].ToString();
								lsCustEmail = lObjSDR["CustEmail"].ToString();
								lsCustSts = lObjSDR["CustSts"].ToString();
								CustType =  lObjSDR["CustType"].ToString();
								lsCustStAddr = (string)((object)lObjSDR["CustStAddr"].ToString() ?? DBNull.Value);
								lsCustArAddr = (string)((object)lObjSDR["CustArAddr"].ToString() ?? DBNull.Value);
								lsCustCity = (string)((object)lObjSDR["CustCity"].ToString() ?? DBNull.Value);
								lsCustState = (string)((object)lObjSDR["CustState"].ToString() ?? DBNull.Value);
								CustPinCode = lObjSDR["CustPinCode"].ToString();
								lsCustCountry = (string)((object)lObjSDR["CustCountry"].ToString() ?? DBNull.Value);
								lsCustGSTNo = (string)((object)lObjSDR["CustGSTNo"].ToString() ?? DBNull.Value);
								lsCustRemarks = (string)((object)lObjSDR["CustRemarks"].ToString() ?? DBNull.Value);



								oObjCustDlt.Add(new CustomerClass(lnCustNo, lsCustFName, lsCustLName, lsCustMobNo, lsCustEmail, lsCustSts, CustType));
							}
						}
						else
						{
							lObjCon.Close();
						}
					}
				}
			}
			catch (SqlException ex)
			{
				MasterMechUtil.ShowError(ex.Message);
			}
			return true;
		}


		public bool SearchCustMobile(string isConStr, string isSearchUserID, List<CustomerClass> oObjCustDlt)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQryText = "SELECT CustNo, CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity,CustState,";
					       lsQryText += "CustPinCode,CustCountry,CustGSTNo,CustRemarks FROM Customer";
					       lsQryText += " WHERE Deleted ='N' AND CustMobNo LIKE @CustMobNo ";

					SqlCommand cmd = new SqlCommand(lsQryText, lObjCon);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = "%" + isSearchUserID + "%";

					lObjCon.Open();
					using (SqlDataReader lObjSDR = cmd.ExecuteReader())
					{
						if (lObjSDR.HasRows)
						{
							while (lObjSDR.Read())
							{
								// These values are not being checked for being null as by table design these columns can not be null
								lnCustNo = (int)lObjSDR["CustNo"];
								lsCustFName = lObjSDR["CustFName"].ToString();
								lsCustLName = lObjSDR["CustLName"].ToString();
								lsCustMobNo = lObjSDR["CustMobNo"].ToString();
								lsCustEmail = lObjSDR["CustEmail"].ToString();
								lsCustSts = lObjSDR["CustSts"].ToString();
								//CustType = char.Parse (lObjSDR["CustType"]);
								lsCustStAddr = (string)((object)lObjSDR["CustStAddr"].ToString() ?? DBNull.Value);
								lsCustArAddr = (string)((object)lObjSDR["CustArAddr"].ToString() ?? DBNull.Value);
								lsCustCity = (string)((object)lObjSDR["CustCity"].ToString() ?? DBNull.Value);
								lsCustState = (string)((object)lObjSDR["CustState"].ToString() ?? DBNull.Value);
								//CustPinCode = (char)((object)lObjSDR["CustPinCode"].ToString() ?? DBNull.Value);
								lsCustCountry = (string)((object)lObjSDR["CustCountry"].ToString() ?? DBNull.Value);
								lsCustGSTNo = (string)((object)lObjSDR["CustGSTNo"].ToString() ?? DBNull.Value);
								lsCustRemarks = (string)((object)lObjSDR["CustRemarks"].ToString() ?? DBNull.Value);



								oObjCustDlt.Add(new CustomerClass(lnCustNo, lsCustFName, lsCustLName, lsCustMobNo, lsCustEmail, lsCustSts, CustType));
							}
						}
						else
						{
							lObjCon.Close();
						}
					}
				}
			}
			catch (SqlException ex)
			{
				MasterMechUtil.ShowError(ex.Message);
			}
			return true;
		}

		public bool Save(SqlCommand iobjcmd)
        {
			if(lnCustNo == 0)
            {
                try
                {
					string lsQryText = "INSERT INTO Customer ( CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity,CustState,";
						   lsQryText += "CustPinCode,CustCountry,CustGSTNo,CustlLastVisit,CustRemarks,Created,CreatedBy,Deleted) OUTPUT INSERTED.CustNo";
					       lsQryText += " VALUES ( @CustFName, @CustLName, @CustMobNo, @CustEmail, @CustSts, @CustType, @CustStAddr, @CustArAddr, @CustCity,@CustState,";
						   lsQryText += "@CustPinCode,@CustCountry,@CustGSTNo,@CustlLastVisit,@CustRemarks,@Created,@CreatedBy,@Deleted)";

					iobjcmd.CommandText = lsQryText;
					iobjcmd.CommandType = CommandType.Text;



					iobjcmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = lsCustFName;
					iobjcmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = lsCustLName;
					iobjcmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = lsCustMobNo;
					iobjcmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = lsCustEmail;
					iobjcmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = lsCustSts;
					iobjcmd.Parameters.AddWithValue("@CustType", SqlDbType.Char).Value = CustType;
					iobjcmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.VarChar).Value = (object)lsCustStAddr ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.VarChar).Value = (object)lsCustArAddr ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = (object)lsCustCity ?? DBNull.Value;

					iobjcmd.Parameters.AddWithValue("@CustState", SqlDbType.VarChar).Value = (object)lsCustState ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.Char).Value = (object)CustPinCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = (object)lsCustCountry ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = (object)lsCustGSTNo ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustlLastVisit", SqlDbType.DateTime).Value = DateTime.Now;
					iobjcmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = (object)lsCustRemarks ?? DBNull.Value; ;
					iobjcmd.Parameters.AddWithValue("@Created", SqlDbType.DateTime).Value = DateTime.Now;
					iobjcmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
					iobjcmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'N';

					lnCustNo = (int)iobjcmd.ExecuteScalar();
					iobjcmd.Parameters.Clear();
				}

				catch(SqlException ex)
                {
					MasterMechUtil.ShowError(ex.Message);
					return false;
                }
            }
			else
            {
				try
				{
					string lsQuery = "UPDATE Customer SET CustFName=@CustFName, CustLName=@CustLName, CustMobNo=@CustMobNo, CustEmail=@CustEmail, CustSts=@CustSts, CustType=@CustType, CustStAddr=@CustStAddr, CustArAddr=@CustArAddr, CustCity=@CustCity,CustState=@CustState,";
						   lsQuery += "CustPinCode=@CustPinCode,CustCountry=@CustCountry,CustGSTNo=@CustGSTNo,CustRemarks=@CustRemarks";
					       lsQuery += " ,Modified=@Modified, ModifiedBy=@ModifiedBy WHERE CustNo=@CustNo";

					iobjcmd.CommandText = lsQuery;
					iobjcmd.CommandType = CommandType.Text;


					iobjcmd.Parameters.AddWithValue("@CustNo", SqlDbType.Int).Value = lnCustNo;
					iobjcmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = lsCustFName;
					iobjcmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = lsCustLName;
					iobjcmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = lsCustMobNo;
					iobjcmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = lsCustEmail;
					iobjcmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = lsCustSts;
					iobjcmd.Parameters.AddWithValue("@CustType", SqlDbType.Char).Value = CustType;
					iobjcmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.VarChar).Value = (object)lsCustStAddr ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.VarChar).Value = (object)lsCustArAddr ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = (object)lsCustCity ?? DBNull.Value;

					iobjcmd.Parameters.AddWithValue("@CustState", SqlDbType.VarChar).Value = (object)lsCustState ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.Char).Value = (object)CustPinCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = (object)lsCustCountry ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = (object)lsCustGSTNo ?? DBNull.Value;

					iobjcmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = (object)lsCustRemarks ?? DBNull.Value; ;
					iobjcmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
					iobjcmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;

					iobjcmd.ExecuteNonQuery();
					iobjcmd.Parameters.Clear();
				}
				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
					return false;
				}

			}
			return true;
        }

		public bool updatedLastVisit(SqlCommand iobjcmd)
		{
			try
			{
				string lsQuery = "UPDATE Customer SET CustLastVisit = @CustLastVisit where CustNo = @CustNo";

				iobjcmd.CommandText = lsQuery;
				iobjcmd.CommandType = CommandType.Text;

				iobjcmd.Parameters.AddWithValue("@CustNo", SqlDbType.VarChar).Value = lnCustNo;
				iobjcmd.Parameters.AddWithValue("@CustLastVisit", SqlDbType.DateTime).Value = (object)dCustlLastVisit ?? DBNull.Value;

				iobjcmd.Parameters.Clear();
				return true;
			}
			catch(SqlException ex)
            {
				MasterMechUtil.ShowError(ex.Message);
				return false;
            }
		}

	}

}
