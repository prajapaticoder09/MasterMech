using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace MasterMech
{
    class InvoiceItem
    {
		public int? InvoiceItemSNo;
		public int? InvoiceSNo;
		public int ItemNo;
		public string ItemDesc;
		public string ItemType;
		public string ItemCatg;
		public float? ItemPrice;
		public string ItemUOM;
		public string ItemSts;
	    public float? CGSTRate;
		public float? SGSTRate;
		public float? IGSTRate;
		public string UPCCode;
		public string HSNCode;
		public string SACCode;
		public float? Qty;
		public float? SGSTAmount;
		public float? CGSTAmount;
		public float? IGSTAmount;
		public float? DiscountAmount;
		public float? TotalAmount;
		public DateTime Created;
		public string CreatedBy;
		public DateTime Modified;
		public string ModifiedBy;
		public char Deleted;
		public DateTime DeletedOn;
		public string DeletedBy;

		// Supporting propeties
		private string ConnStr;
		private string UserID;

		public InvoiceItem()
        {

        }
		public InvoiceItem(string isConStr, string isUserID)
		{
			ConnStr = isConStr;
			UserID = isUserID;
		}

		public InvoiceItem(int lnItemNo,string lsItemDesc,float? lnItemPrice,string lsUOM,float? lnQty, float? lnDiscountAmount, float? lnCGSTRate, float? lnCGSTAmount,
			float? lnSGSTRate, float? lnSGSTAmount, float? lnIGSTRate, float? lnIGSTAmount, float? lnTotalAmount, string lsUPCCode, string lsHSNCode, string lsSACCode, string lsItemType, string lsItemCatg, string lsitemSts)
        {
			ItemNo = lnItemNo;
			ItemDesc = lsItemDesc;
			ItemPrice = lnItemPrice;
			ItemUOM = lsUOM;
			Qty = lnQty;
			DiscountAmount = lnDiscountAmount;
			CGSTRate = lnCGSTRate;
			CGSTAmount = lnCGSTAmount;
			SGSTRate = lnSGSTRate;
			SGSTAmount = lnSGSTAmount;
			IGSTRate = lnIGSTRate;
			IGSTAmount = lnIGSTAmount;
			TotalAmount = lnTotalAmount;
			UPCCode = lsUPCCode;
			HSNCode = lsHSNCode;
			SACCode = lsSACCode;
			ItemType = lsItemType;
			ItemCatg = lsItemCatg;
			ItemSts = lsitemSts;
		}

		

		public bool Save(SqlCommand iobjcmd)
		{

			if (InvoiceItemSNo == null)
			{
				try
				{
					string lsQryText = "INSERT INTO [InvoiceItem" + MasterMechUtil.sFY + "]";
					lsQryText += "(InvoiceSNo, ItemNo, ItemDesc, ItemType, ItemCatg, ItemPrice, ItemUOM, ItemSts, CGSTRate, SGSTRate, IGSTRate, UPCCode,";
					lsQryText += "HSNCode, SACCode, Qty, SGSTAmount, CGSTAmount, IGSTAmount, DiscountAmount, TotalAmount, Created, CreatedBy,Deleted)";

					lsQryText += " VALUES(@InvoiceSNo,@ItemNo,@ItemDesc,@ItemType,@ItemCatg,@ItemPrice,@ItemUOM,@ItemSts,@CGSTRate,@SGSTRate,@IGSTRate,@UPCCode,";
					lsQryText += "@HSNCode,@SACCode,@Qty,@SGSTAmount,@CGSTAmount,@IGSTAmount,@DiscountAmount,@TotalAmount,@Created,@CreatedBy,@Deleted)";

					iobjcmd.CommandText = lsQryText;
					iobjcmd.CommandType = CommandType.Text;



					iobjcmd.Parameters.AddWithValue("@InvoiceSNo", SqlDbType.VarChar).Value = InvoiceSNo;
					iobjcmd.Parameters.AddWithValue("@ItemNo", SqlDbType.VarChar).Value = ItemNo;
					iobjcmd.Parameters.AddWithValue("@ItemDesc", SqlDbType.VarChar).Value = ItemDesc;
					iobjcmd.Parameters.AddWithValue("@ItemType", SqlDbType.VarChar).Value = ItemType;
					iobjcmd.Parameters.AddWithValue("@ItemCatg", SqlDbType.VarChar).Value = ItemCatg;
					iobjcmd.Parameters.AddWithValue("@ItemPrice", SqlDbType.Char).Value = ItemPrice;
					iobjcmd.Parameters.AddWithValue("@ItemUOM", SqlDbType.VarChar).Value = ItemUOM;
					iobjcmd.Parameters.AddWithValue("@ItemSts", SqlDbType.VarChar).Value = ItemSts;
					iobjcmd.Parameters.AddWithValue("@CGSTRate", SqlDbType.VarChar).Value = (object)CGSTRate ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@SGSTRate", SqlDbType.VarChar).Value = (object)SGSTRate ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@IGSTRate", SqlDbType.Char).Value = (object)IGSTRate ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@UPCCode", SqlDbType.VarChar).Value = (object)UPCCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@HSNCode", SqlDbType.VarChar).Value = (object)HSNCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@SACCode", SqlDbType.DateTime).Value = (object)SACCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@Qty", SqlDbType.VarChar).Value = (object)Qty ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@SGSTAmount", SqlDbType.DateTime).Value = (object)SGSTAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CGSTAmount", SqlDbType.VarChar).Value = (object)CGSTAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@IGSTAmount", SqlDbType.Char).Value = (object)IGSTAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@DiscountAmount", SqlDbType.VarChar).Value = (object)DiscountAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@TotalAmount", SqlDbType.VarChar).Value = (object)TotalAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@Created", SqlDbType.DateTime).Value = DateTime.Now;
					iobjcmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
					iobjcmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'N';

					iobjcmd.ExecuteNonQuery();
					iobjcmd.Parameters.Clear();
				}

				catch (SqlException ex)
				{
					MasterMechUtil.ShowError(ex.Message);
					return false;
				}
			}
			else
            {
				try
				{
					string lsQryText = "UPDATE [InvoiceItem" + MasterMechUtil.sFY + "] SET";
					lsQryText += " ItemNo=@ItemNo, ItemDesc=@ItemDesc, ItemType=@ItemType, ItemCatg=@ItemCatg, ItemPrice=@ItemPrice, ItemUOM=@ItemUOM, ItemSts=@ItemSts, CGSTRate=@CGSTRate, SGSTRate=@SGSTRate, IGSTRate=@IGSTRate, UPCCode=@UPCCode,";
					lsQryText += "HSNCode=@HSNCode, SACCode=@SACCode, Qty=@Qty, SGSTAmount=@SGSTAmount, CGSTAmount=@CGSTAmount, IGSTAmount=@IGSTAmount, DiscountAmount=@DiscountAmount, TotalAmount=@TotalAmount,Modified=@Modified,ModifiedBy=@ModifiedBy where InvoiceSNo=@InvoiceSNo";

					iobjcmd.CommandText = lsQryText;
					iobjcmd.CommandType = CommandType.Text;



					iobjcmd.Parameters.AddWithValue("@InvoiceSNo", SqlDbType.VarChar).Value = InvoiceSNo;
					iobjcmd.Parameters.AddWithValue("@ItemNo", SqlDbType.VarChar).Value = ItemNo;
					iobjcmd.Parameters.AddWithValue("@ItemDesc", SqlDbType.VarChar).Value = ItemDesc;
					iobjcmd.Parameters.AddWithValue("@ItemType", SqlDbType.VarChar).Value = ItemType;
					iobjcmd.Parameters.AddWithValue("@ItemCatg", SqlDbType.VarChar).Value = ItemCatg;
					iobjcmd.Parameters.AddWithValue("@ItemPrice", SqlDbType.Char).Value = ItemPrice;
					iobjcmd.Parameters.AddWithValue("@ItemUOM", SqlDbType.VarChar).Value = ItemUOM;
					iobjcmd.Parameters.AddWithValue("@ItemSts", SqlDbType.VarChar).Value = ItemSts;
					iobjcmd.Parameters.AddWithValue("@CGSTRate", SqlDbType.VarChar).Value = (object)CGSTRate ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@SGSTRate", SqlDbType.VarChar).Value = (object)SGSTRate ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@IGSTRate", SqlDbType.Char).Value = (object)IGSTRate ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@UPCCode", SqlDbType.VarChar).Value = (object)UPCCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@HSNCode", SqlDbType.VarChar).Value = (object)HSNCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@SACCode", SqlDbType.DateTime).Value = (object)SACCode ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@Qty", SqlDbType.VarChar).Value = (object)Qty ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@SGSTAmount", SqlDbType.DateTime).Value = (object)SGSTAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@CGSTAmount", SqlDbType.VarChar).Value = (object)CGSTAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@IGSTAmount", SqlDbType.Char).Value = (object)IGSTAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@DiscountAmount", SqlDbType.VarChar).Value = (object)DiscountAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@TotalAmount", SqlDbType.VarChar).Value = (object)TotalAmount ?? DBNull.Value;
					iobjcmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
					iobjcmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
					//iobjcmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'N';

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



		public bool SearchInvoiceItem(string isConStr, int isSearchUserID, List<InvoiceItem> oObjInvoice)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{

					string lsQryText = "SELECT ItemNo, ItemDesc,ItemType, ItemCatg, ItemPrice, ItemUOM, ItemSts, CGSTRate, SGSTRate, IGSTRate, UPCCode,";
					lsQryText += "HSNCode, SACCode, Qty, SGSTAmount, CGSTAmount, IGSTAmount, DiscountAmount, TotalAmount FROM  [InvoiceItem" + MasterMechUtil.sFY + "]";
					lsQryText += " WHERE Deleted ='N' AND InvoiceSNo LIKE @InvoiceSNo ";

					SqlCommand cmd = new SqlCommand(lsQryText, lObjCon);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@InvoiceSNo", SqlDbType.VarChar).Value = "%" + isSearchUserID + "%";

					lObjCon.Open();
					using (SqlDataReader lObjSDR = cmd.ExecuteReader())
					{
						if (lObjSDR.HasRows)
						{
							while (lObjSDR.Read())
							{
								// These values are not being checked for being null as by table design these columns can not be null
								
								//InvoiceSNo = (int)lObjSDR["InvoiceSNo"];
								ItemNo = (int)lObjSDR["ItemNo"];
								ItemDesc = DBNull.Value.Equals(lObjSDR["ItemDesc"]) ? null : (lObjSDR["ItemDesc"]).ToString(); 
								ItemType = DBNull.Value.Equals(lObjSDR["ItemType"]) ? null : (lObjSDR["ItemType"]).ToString();
								ItemCatg = DBNull.Value.Equals(lObjSDR["ItemCatg"]) ? null : (lObjSDR["ItemCatg"]).ToString();
								ItemPrice = DBNull.Value.Equals(lObjSDR["ItemPrice"]) ? (float?)null : Convert.ToSingle(lObjSDR["ItemPrice"]);
								ItemUOM = DBNull.Value.Equals(lObjSDR["ItemUOM"]) ? null : (lObjSDR["ItemUOM"]).ToString();
								ItemSts = DBNull.Value.Equals(lObjSDR["ItemSts"]) ? null : (lObjSDR["ItemSts"]).ToString();
								CGSTRate = DBNull.Value.Equals(lObjSDR["CGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["CGSTRate"]);
								IGSTRate = DBNull.Value.Equals(lObjSDR["IGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["IGSTRate"]);
								SGSTRate = DBNull.Value.Equals(lObjSDR["SGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["SGSTRate"]);
								UPCCode = DBNull.Value.Equals(lObjSDR["UPCCode"]) ? null : (lObjSDR["UPCCode"]).ToString();
								HSNCode = DBNull.Value.Equals(lObjSDR["HSNCode"]) ? null : (lObjSDR["HSNCode"]).ToString();
								SACCode = DBNull.Value.Equals(lObjSDR["SACCode"]) ? null : (lObjSDR["SACCode"]).ToString();
								Qty = DBNull.Value.Equals(lObjSDR["Qty"]) ? (float?)null : Convert.ToSingle(lObjSDR["Qty"]);
								SGSTAmount = DBNull.Value.Equals(lObjSDR["SGSTAmount"]) ? (float?)null : Convert.ToSingle(lObjSDR["SGSTAmount"]);
								IGSTAmount = DBNull.Value.Equals(lObjSDR["IGSTAmount"]) ? (float?)null : Convert.ToSingle(lObjSDR["IGSTAmount"]);
								CGSTAmount = DBNull.Value.Equals(lObjSDR["CGSTAmount"]) ? (float?)null : Convert.ToSingle(lObjSDR["CGSTAmount"]);
								DiscountAmount = DBNull.Value.Equals(lObjSDR["DiscountAmount"]) ? (float?)null : Convert.ToSingle(lObjSDR["DiscountAmount"]);
								TotalAmount = DBNull.Value.Equals(lObjSDR["TotalAmount"]) ? (float?)null : Convert.ToSingle(lObjSDR["TotalAmount"]);





								oObjInvoice.Add(new InvoiceItem(ItemNo, ItemDesc,(float?)ItemPrice,ItemUOM,(float?)Qty,DiscountAmount,CGSTRate,CGSTAmount,
									SGSTRate,SGSTAmount,IGSTRate,IGSTAmount,TotalAmount,UPCCode,HSNCode,SACCode,ItemType,ItemCatg,ItemSts));
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


		public bool DeletedInvoice(SqlCommand iobjcmd)
        {
			try
			{


				string lsQuery = "UPDATE [InvoiceItem" + MasterMechUtil.sFY + "] SET Deleted ='Y', DeletedOn=@DeletedOn, DeletedBy = @DeletedBy WHERE InvoiceSNo = @InvoiceSNo";

				iobjcmd.CommandText = lsQuery;
				iobjcmd.CommandType = CommandType.Text;

				iobjcmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.DateTime).Value = DateTime.Now;
				iobjcmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
				iobjcmd.Parameters.AddWithValue("@InvoiceSNo", SqlDbType.VarChar).Value = InvoiceSNo;


				iobjcmd.ExecuteNonQuery();
				iobjcmd.Parameters.Clear();
			}
			catch (SqlException ex)
			{
				MasterMechUtil.ShowError(ex.Message);
				return false;
			}
			return true;

		}
	}
}
