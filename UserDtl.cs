using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace MasterMech
{
	class UserDtl
	{
		public string sUserID;
		public string sPwd;
		public string sUserName;
		public string sMobNo;
		public string sEmailID;
		public string sUserType;
		public DateTime? dLastLoginTime;
		public DateTime? dLastPwdChangeTime;
		public string sRemarks;
		public DateTime? dCreated;
		public string sCreatedBy;
		public DateTime? dModified;
		public string sModifiedBy;
		public string SDeleted;
		public DateTime? dDeletedOn;
		public string sDeletedBy;

		private string ConnStr;
		private string UserID;

		public UserDtl()
		{

		}

		public UserDtl(string isUserID, string isUserName, string isMobNo, string isEmailID, string isUserType)
		{
			sUserID = isUserID;
			sUserName = isUserName;
			sMobNo = isMobNo;
			sEmailID = isEmailID;
			sUserType = isUserType;
		}

		public UserDtl(string isConStr,string isUserId)
        {
			ConnStr = isConStr;
			UserID = isUserId;

		}
		public bool Load(string isConStr, string isUserID)
		{
			bool lbDataFound = false;

			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "SELECT Pwd,UserName,MobNo, EmailID, UserType, LastLoginTime, LastPwdChangeTime,";
				lsQuery += " Remarks, Created, CreatedBy, Modified, ModifiedBy FROM UserDtl WHERE UserID = @UserID AND Deleted='N'";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = isUserID;

						lObjCon.Open();
						using (SqlDataReader lObjSDR = cmd.ExecuteReader())
						{
							if (lObjSDR.HasRows)
							{
								while (lObjSDR.Read())
								{
									sUserID = isUserID;
									sUserName = DBNull.Value.Equals(lObjSDR["UserName"]) ? null : lObjSDR["UserName"].ToString();
									sPwd = DBNull.Value.Equals(lObjSDR["Pwd"]) ? null : lObjSDR["Pwd"].ToString();
									sMobNo = DBNull.Value.Equals(lObjSDR["MobNo"]) ? null : lObjSDR["MobNo"].ToString();
									
									sEmailID = DBNull.Value.Equals(lObjSDR["EmailID"]) ? null : lObjSDR["EmailID"].ToString();
									sUserType = DBNull.Value.Equals(lObjSDR["UserType"]) ? null : lObjSDR["UserType"].ToString();
									dLastLoginTime = DBNull.Value.Equals(lObjSDR["LastLoginTime"]) ? (DateTime?)null : (DateTime)lObjSDR["LastLoginTime"];
									dLastPwdChangeTime = DBNull.Value.Equals(lObjSDR["LastPwdChangeTime"]) ? (DateTime?)null : (DateTime)lObjSDR["LastPwdChangeTime"];
									sRemarks = DBNull.Value.Equals(lObjSDR["Remarks"]) ? null : lObjSDR["Remarks"].ToString();
									dCreated = (DateTime)lObjSDR["Created"];
									sCreatedBy = DBNull.Value.Equals(lObjSDR["CreatedBy"]) ? null : lObjSDR["CreatedBy"].ToString();
									dModified = DBNull.Value.Equals(lObjSDR["Modified"]) ? (DateTime?)null : (DateTime)lObjSDR["Modified"];
									sModifiedBy = DBNull.Value.Equals(lObjSDR["ModifiedBy"]) ? null : lObjSDR["ModifiedBy"].ToString();
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

		public bool ValidUserID(string isConStr)
		{
			int lnUserCount = 0;

			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "SELECT COUNT(*) as USERCOUNT FROM UserDtl WHERE UserID = @UserID";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = sUserID;

						lObjCon.Open();
						using (SqlDataReader lObjSDR = cmd.ExecuteReader())
						{
							if (lObjSDR.HasRows)
							{
								while (lObjSDR.Read())
								{
									lnUserCount = (int)lObjSDR["USERCOUNT"];
								}
							}
						}
						lObjCon.Close();
					}
				}

				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
				}
			}

			if (lnUserCount == 0)
				return false;
			else
				return true;
		}

		public bool ValidLogin(string isConStr)
		{
			bool lbValidUser = false;
			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "SELECT UserType FROM UserDtl WHERE UserID = @UserID AND Pwd=@Pwd";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = sUserID;
						cmd.Parameters.AddWithValue("@Pwd", SqlDbType.VarChar).Value = sPwd;

						lObjCon.Open();
						using (SqlDataReader lObjSDR = cmd.ExecuteReader())
						{
							if (lObjSDR.HasRows)
							{
								while (lObjSDR.Read())
								{
									sUserType = lObjSDR["UserType"].ToString();
									lbValidUser = true;
								}
							}
						}
						lObjCon.Close();
					}
				}

				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
				}
			}

			return lbValidUser;
		}

		public bool UpdateLoginTime(string isConStr, string isUserID)
		{
			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "UPDATE UserDtl SET LastLoginTime=@LastLoginTime WHERE UserID = @UserID AND Deleted='N'";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@LastLoginTime", SqlDbType.DateTime).Value = DateTime.Now;
						cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = isUserID;

						lObjCon.Open();
						cmd.ExecuteNonQuery();
						lObjCon.Close();
						return true;
					}
				}

				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
					return false;
				}
			}
		}

		public bool UpdateUserGeneralInfo(string isConStr, string isUserID)
		{
			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "UPDATE UserDtl SET UserName=@UserName,MobNo=@MobNo,EmailID=@EmailID,Modified=@Modified,ModifiedBy=@ModifiedBy WHERE UserID = @UserID AND Deleted='N'";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = sUserName; ;
						cmd.Parameters.AddWithValue("@MobNo", SqlDbType.VarChar).Value = sMobNo;
						cmd.Parameters.AddWithValue("@EmailID", SqlDbType.VarChar).Value = sEmailID;
						cmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
						cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = isUserID;
						cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = isUserID;

						lObjCon.Open();
						cmd.ExecuteNonQuery();
						lObjCon.Close();
						return true;
					}
				}

				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
					return false;
				}
			}
		}

		public bool UpdatePassword(string isConStr, string isUserID, string isPWD)
		{
			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "UPDATE UserDtl SET Pwd=@Pwd,LastPwdChangeTime=@LastPwdChangeTime,Modified=@Modified,ModifiedBy=@ModifiedBy WHERE UserID = @UserID AND Deleted='N'";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@Pwd", SqlDbType.VarChar).Value = isPWD; ;
						cmd.Parameters.AddWithValue("@LastPwdChangeTime", SqlDbType.DateTime).Value = DateTime.Now;
						cmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
						cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = isUserID;
						cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = isUserID;

						lObjCon.Open();
						cmd.ExecuteNonQuery();
						lObjCon.Close();
						return true;
					}
				}

				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
					return false;
				}
			}

		}

		public bool Save(string isConStr, string isUserID, bool ibNewMode)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					if (ibNewMode)
					{
						string lsQuery = "INSERT INTO UserDtl(UserID, Pwd, UserName, MobNo, EmailID, UserType, Remarks, Created, CreatedBy, Deleted) ";
						lsQuery += " VALUES (@UserID, @Pwd, @UserName, @MobNo, @EmailID, @UserType, @Remarks,  @Created, @CreatedBy, @Deleted)";

						using (SqlCommand cmd = new SqlCommand(lsQuery))
						{
							cmd.Connection = lObjCon;
							cmd.CommandType = CommandType.Text;

							cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = sUserID;
							cmd.Parameters.AddWithValue("@Pwd", SqlDbType.VarChar).Value = sPwd;
							cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = sUserName;
							cmd.Parameters.AddWithValue("@MobNo", SqlDbType.VarChar).Value = sMobNo;
							cmd.Parameters.AddWithValue("@EmailID", SqlDbType.VarChar).Value = sEmailID;
							cmd.Parameters.AddWithValue("@UserType", SqlDbType.VarChar).Value = sUserType;
							cmd.Parameters.AddWithValue("@Remarks", SqlDbType.VarChar).Value = sRemarks;
							cmd.Parameters.AddWithValue("@Created", SqlDbType.DateTime).Value = DateTime.Now;
							cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = isUserID;
							cmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'N';

							lObjCon.Open();
							cmd.ExecuteNonQuery();
							lObjCon.Close();
							return true;
						}
					}
					else
					{
						string lsQuery	= "UPDATE UserDtl SET Pwd=@Pwd, UserName=@UserName, MobNo=@MobNo, EmailID=@EmailID, UserType=@UserType,";
						lsQuery			+= "Remarks=@Remarks, Modified=@Modified, ModifiedBy=@ModifiedBy WHERE UserID=@UserID";

						using (SqlCommand cmd = new SqlCommand(lsQuery))
						{
							cmd.Connection = lObjCon;
							cmd.CommandType = CommandType.Text;

							cmd.Parameters.AddWithValue("@Pwd", SqlDbType.VarChar).Value = sPwd;
							cmd.Parameters.AddWithValue("@UserName", SqlDbType.VarChar).Value = sUserName;
							cmd.Parameters.AddWithValue("@MobNo", SqlDbType.VarChar).Value = sMobNo;
							cmd.Parameters.AddWithValue("@EmailID", SqlDbType.VarChar).Value = sEmailID;
							cmd.Parameters.AddWithValue("@UserType", SqlDbType.VarChar).Value = sUserType;
							cmd.Parameters.AddWithValue("@Remarks", SqlDbType.VarChar).Value = sRemarks;
							cmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
							cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = isUserID;
							cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = sUserID;

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

		public bool SearchUser(string isConStr, string isUserID,string isSearchUserID, List<UserDtl> oObjUserDtls)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQryText = "SELECT UserID, UserName, MobNo, EmailID, UserType FROM UserDtl ";
					lsQryText += "WHERE Deleted ='N' AND UserID LIKE  @UserID";

					SqlCommand cmd = new SqlCommand(lsQryText, lObjCon);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = "%" + isSearchUserID + "%";
					lObjCon.Open();
					using (SqlDataReader lObjSDR = cmd.ExecuteReader())
					{
						if (lObjSDR.HasRows)
						{
							while (lObjSDR.Read())
							{
								// These values are not being checked for being null as by table design these columns can not be null
								sUserID = lObjSDR["UserID"].ToString();
								sUserName = lObjSDR["UserName"].ToString();
								sMobNo = lObjSDR["MobNo"].ToString();

								sEmailID =(string) ((object)lObjSDR["EmailID"].ToString() ?? DBNull.Value);
								sUserType = (string)((object)lObjSDR["UserType"].ToString() ?? DBNull.Value);

								oObjUserDtls.Add(new UserDtl(sUserID, sUserName, sMobNo, sEmailID, sUserType));
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

		public bool DeleteUserParam()
        {
            using (SqlConnection lObjCon = new SqlConnection(ConnStr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        string lsQryText = "DELETE FROM UserDtl WHERE UserID =  @sUserID"; 
                        

                        lObjCon.Open();
                        cmd.Connection = lObjCon;
                        cmd.CommandText = lsQryText;
                        cmd.CommandType = CommandType.Text;
						cmd.Parameters.AddWithValue("@sUserID", SqlDbType.VarChar).Value = sUserID;
                        cmd.ExecuteNonQuery();
                    }

                    catch(SqlException ex)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

		public bool Delete(string isConStr, string isUserID)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQuery = "UPDATE UserDtl SET Deleted ='Y', DeletedOn=@DeletedOn, DeletedBy = @DeletedBy WHERE UserID=@UserID";

					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;
						cmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.DateTime).Value = DateTime.Now;
						cmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = isUserID;
						cmd.Parameters.AddWithValue("@UserID", SqlDbType.VarChar).Value = sUserID;

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

	}


}
