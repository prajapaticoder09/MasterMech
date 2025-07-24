using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MasterMech
{
	class Item
	{
		public int ItemNo;
		public string ItemDesc;
		public string ItemType;
		public string ItemCatg;
		public float ItemPrice;
		public string ItemUOM;
		public string ItemSts;
		public float? CGSTRate;
		public float? SGSTRate;
		public float? IGSTRate;
		public string UPCCode;
		public string HSNCode;
		public string SACCode;
		public float? QtyHand;
		public float? ReOrderQty;
		public float? ReOrderLvl;
		public int? NoOfParts;
		public string ItemRemarks;
		public DateTime? Created;
		public string CreatedBy;
		public DateTime? Modified;
		public string ModifiedBy;
		public char Deleted;
		public DateTime? DeletedOn;
		public string DeletedBy;

		private string ConnStr;
		private int iItemNo;


		public Item()
		{

		}

		public Item(int lnItemNo, string lsItemDesc, string lsItemType, string lsItemCatg, float fItemPrice, string lsItemUOM, string lsItemSts) 
			
		{
			ItemNo = lnItemNo;
			ItemDesc = lsItemDesc;
			ItemType = lsItemType;
			ItemCatg = lsItemCatg;
			ItemPrice = fItemPrice;
			ItemUOM = lsItemUOM;
			ItemSts = lsItemSts;
			
			
		}

		public Item(string isConStr, int ItemNo)
		{
			ConnStr = isConStr;
			iItemNo = ItemNo;

		}

		public bool Save(string isConStr, int ItemNo, bool ibNewMode)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					if (ibNewMode)
					{
						string lsQuery = "INSERT INTO ItemMaster( ItemDesc, ItemType, ItemCatg, ItemPrice, ItemUOM, ItemSts, CGSTRate, SGSTRate, IGSTRate,";
						       lsQuery += "UPCCode,HSNCode,SACCode,QtyHand,ReOrderQty,ReOrderLvl,NoOfParts,ItemRemarks,Created,CreatedBy,Deleted) ";

						       lsQuery += "VALUES ( @ItemDesc, @ItemType, @ItemCatg, @ItemPrice, @ItemUOM, @ItemSts, @CGSTRate, @SGSTRate, @IGSTRate,";
						       lsQuery += "@UPCCode,@HSNCode,@SACCode,@QtyHand,@ReOrderQty,@ReOrderLvl,@NoOfParts,@ItemRemarks,@Created,@CreatedBy,@Deleted)";

						using (SqlCommand cmd = new SqlCommand(lsQuery))
						{
							cmd.Connection = lObjCon;
							cmd.CommandType = CommandType.Text;

							cmd.Parameters.AddWithValue("@ItemNo", SqlDbType.Int).Value = ItemNo;
							cmd.Parameters.AddWithValue("@ItemDesc", SqlDbType.VarChar).Value = ItemDesc;
							cmd.Parameters.AddWithValue("@ItemType", SqlDbType.VarChar).Value = ItemType;
							cmd.Parameters.AddWithValue("@ItemCatg", SqlDbType.VarChar).Value = ItemCatg;
							cmd.Parameters.AddWithValue("@ItemPrice", SqlDbType.Float).Value = ItemPrice;
							cmd.Parameters.AddWithValue("@ItemUOM", SqlDbType.VarChar).Value = ItemUOM;
							cmd.Parameters.AddWithValue("@ItemSts", SqlDbType.VarChar).Value = ItemSts;
							cmd.Parameters.AddWithValue("@CGSTRate", SqlDbType.Float).Value = (object)CGSTRate ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@SGSTRate", SqlDbType.Float).Value = (object)SGSTRate ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@IGSTRate", SqlDbType.Float).Value = (object)IGSTRate ?? DBNull.Value;

							cmd.Parameters.AddWithValue("@UPCCode", SqlDbType.VarChar).Value = (object)UPCCode ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@HSNCode", SqlDbType.VarChar).Value = (object)HSNCode ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@SACCode", SqlDbType.VarChar).Value = (object)SACCode ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@QtyHand", SqlDbType.Float).Value = (object)QtyHand ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@ReOrderQty", SqlDbType.Float).Value = (object)ReOrderQty ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@ReOrderLvl", SqlDbType.Float).Value = (object)ReOrderLvl ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@NoOfParts", SqlDbType.Int).Value = (object)NoOfParts ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@ItemRemarks", SqlDbType.VarChar).Value = (object)ItemRemarks ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@Created", SqlDbType.DateTime).Value = DateTime.Now;
							cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
							cmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'N';


							lObjCon.Open();
							cmd.ExecuteNonQuery();
							lObjCon.Close();
							return true;
						}
					}
					else
					{
						string lsQuery = "UPDATE ItemMaster SET  ItemDesc=@ItemDesc, ItemType=@ItemType, ItemCatg=@ItemCatg, ItemPrice=@ItemPrice, ItemUOM=@ItemUOM, ItemSts=@ItemSts,";
						lsQuery += "CGSTRate=@CGSTRate, SGSTRate=@SGSTRate, IGSTRate=@IGSTRate,UPCCode=@UPCCode,HSNCode=@HSNCode,SACCode=@SACCode,";
						lsQuery += "QtyHand=@QtyHand,ReOrderQty=@ReOrderQty,ReOrderLvl=@ReOrderLvl,NoOfParts=@NoOfParts,ItemRemarks=@ItemRemarks,";
						lsQuery += "Modified=@Modified,ModifiedBy=@ModifiedBy WHERE ItemNo=@ItemNo";

						using (SqlCommand cmd = new SqlCommand(lsQuery))
						{
							cmd.Connection = lObjCon;
							cmd.CommandType = CommandType.Text;

							cmd.Parameters.AddWithValue("@ItemNo", SqlDbType.Int).Value = ItemNo;
							cmd.Parameters.AddWithValue("@ItemDesc", SqlDbType.VarChar).Value = ItemDesc;
							cmd.Parameters.AddWithValue("@ItemType", SqlDbType.VarChar).Value = ItemType;
							cmd.Parameters.AddWithValue("@ItemCatg", SqlDbType.VarChar).Value = ItemCatg;
							cmd.Parameters.AddWithValue("@ItemPrice", SqlDbType.Float).Value = ItemPrice;
							cmd.Parameters.AddWithValue("@ItemUOM", SqlDbType.VarChar).Value = ItemUOM;
							cmd.Parameters.AddWithValue("@ItemSts", SqlDbType.VarChar).Value = ItemSts;
							cmd.Parameters.AddWithValue("@CGSTRate", SqlDbType.Float).Value = (object)CGSTRate ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@SGSTRate", SqlDbType.Float).Value = (object)SGSTRate ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@IGSTRate", SqlDbType.Float).Value = (object)IGSTRate ?? DBNull.Value;

							cmd.Parameters.AddWithValue("@UPCCode", SqlDbType.VarChar).Value = (object)UPCCode ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@HSNCode", SqlDbType.VarChar).Value = (object)HSNCode ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@SACCode", SqlDbType.VarChar).Value = (object)SACCode ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@QtyHand", SqlDbType.Float).Value = (object)QtyHand ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@ReOrderQty", SqlDbType.Float).Value = (object)ReOrderQty ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@ReOrderLvl", SqlDbType.Float).Value = (object)ReOrderLvl ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@NoOfParts", SqlDbType.Int).Value = (object)NoOfParts ?? DBNull.Value;
							cmd.Parameters.AddWithValue("@ItemRemarks", SqlDbType.VarChar).Value = (object)ItemRemarks ?? DBNull.Value;
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


		public bool SearchItems(string isConStr, string isSearchUserID, List<Item> oObjUserDtls)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQryText = "SELECT ItemNo, ItemDesc, ItemType, ItemCatg, ItemPrice,ItemUOM,ItemSts,CGSTRate,SGSTRate,IGSTRate,UPCCode,HSNCode,SACCode,QtyHand,";
					       lsQryText += "ReOrderQty,ReOrderLvl,NoOfParts FROM ItemMaster ";
					       lsQryText += "WHERE Deleted ='N' AND ItemDesc LIKE  @ItemDesc";

					SqlCommand cmd = new SqlCommand(lsQryText, lObjCon);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@ItemDesc", SqlDbType.VarChar).Value = "%" + isSearchUserID + "%";
					lObjCon.Open();
					using (SqlDataReader lObjSDR = cmd.ExecuteReader())
					{
						if (lObjSDR.HasRows)
						{
							while (lObjSDR.Read())
							{
								// These values are not being checked for being null as by table design these columns can not be null
								ItemNo = (int)lObjSDR["ItemNo"];
								ItemDesc = lObjSDR["ItemDesc"].ToString();
								ItemType = lObjSDR["ItemType"].ToString();
								ItemCatg = lObjSDR["ItemCatg"].ToString();
								ItemPrice = Convert.ToSingle( lObjSDR["ItemPrice"]);
								ItemUOM = lObjSDR["ItemUOM"].ToString();
								ItemSts = lObjSDR["ItemSts"].ToString();
								CGSTRate = DBNull.Value.Equals(lObjSDR["CGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["CGSTRate"]);
								SGSTRate = DBNull.Value.Equals(lObjSDR["SGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["SGSTRate"]);
								IGSTRate = DBNull.Value.Equals(lObjSDR["IGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["IGSTRate"]);
								UPCCode = lObjSDR["UPCCode"].ToString();
								HSNCode = lObjSDR["HSNCode"].ToString();
								SACCode = lObjSDR["SACCode"].ToString();

								QtyHand = DBNull.Value.Equals(lObjSDR["QtyHand"]) ? (float?)null : Convert.ToSingle(lObjSDR["QtyHand"]);
								ReOrderQty = DBNull.Value.Equals(lObjSDR["ReOrderQty"]) ? (float?)null : Convert.ToSingle(lObjSDR["ReOrderQty"]);
								ReOrderLvl = DBNull.Value.Equals(lObjSDR["ReOrderLvl"]) ? (float?)null : Convert.ToSingle(lObjSDR["ReOrderLvl"]);
								NoOfParts = DBNull.Value.Equals(lObjSDR["NoOfParts"]) ? (int?)null : (int)lObjSDR["NoOfParts"];


								oObjUserDtls.Add(new Item(ItemNo, ItemDesc, ItemType, ItemCatg, ItemPrice, ItemUOM, ItemSts)); 
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


		public bool Load(string isConStr, int inItemNo)
		{
			bool lbDataFound = false;

			using (SqlConnection lObjCon = new SqlConnection(isConStr))
			{

				string lsQuery = "SELECT ItemNo, ItemDesc,ItemType, ItemCatg, ItemPrice,ItemUOM,ItemSts,CGSTRate,SGSTRate,IGSTRate,UPCCode,HSNCode,SACCode,QtyHand,";
				       lsQuery += "ReOrderQty,ReOrderLvl,NoOfParts,ItemRemarks,Created,CreatedBy,Modified,ModifiedBy FROM ItemMaster ";
				       lsQuery += "WHERE Deleted ='N' AND ItemNo LIKE  @ItemNo";
				try
				{
					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;

						cmd.Parameters.AddWithValue("@ItemNo", SqlDbType.VarChar).Value = inItemNo;

						lObjCon.Open();
						using (SqlDataReader lObjSDR = cmd.ExecuteReader())
						{
							if (lObjSDR.HasRows)
							{
								while (lObjSDR.Read())
								{
									ItemNo = inItemNo;
									ItemDesc = lObjSDR["ItemDesc"].ToString();
									ItemType = lObjSDR["ItemType"].ToString();
									ItemCatg = lObjSDR["ItemCatg"].ToString();
									ItemPrice = Convert.ToSingle (lObjSDR["ItemPrice"]);
									ItemUOM = lObjSDR["ItemUOM"].ToString();
									ItemSts = DBNull.Value.Equals(lObjSDR["ItemSts"]) ? null : lObjSDR["ItemSts"].ToString();
									CGSTRate = DBNull.Value.Equals(lObjSDR["CGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["CGSTRate"]);
									SGSTRate = DBNull.Value.Equals(lObjSDR["SGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["SGSTRate"]);
									IGSTRate = DBNull.Value.Equals(lObjSDR["IGSTRate"]) ? (float?)null : Convert.ToSingle(lObjSDR["IGSTRate"]);
									UPCCode = DBNull.Value.Equals(lObjSDR["UPCCode"]) ? null : lObjSDR["UPCCode"].ToString();
									HSNCode = DBNull.Value.Equals(lObjSDR["HSNCode"]) ? null : lObjSDR["HSNCode"].ToString();
									SACCode = DBNull.Value.Equals(lObjSDR["SACCode"]) ? null : lObjSDR["SACCode"].ToString();
									QtyHand = DBNull.Value.Equals(lObjSDR["QtyHand"]) ? (float?)null : Convert.ToSingle(lObjSDR["QtyHand"]);
									ReOrderQty = DBNull.Value.Equals(lObjSDR["ReOrderQty"]) ? (float?)null : Convert.ToSingle(lObjSDR["ReOrderQty"]);
									ReOrderLvl = DBNull.Value.Equals(lObjSDR["ReOrderLvl"]) ? (float?)null : Convert.ToSingle( lObjSDR["ReOrderLvl"]);
									NoOfParts = DBNull.Value.Equals(lObjSDR["NoOfParts"]) ? (int?)null : (int)lObjSDR["NoOfParts"];
									ItemRemarks = DBNull.Value.Equals(lObjSDR["ItemRemarks"]) ? null : lObjSDR["ItemRemarks"].ToString();
									Created = (DateTime)lObjSDR["Created"];
									CreatedBy = DBNull.Value.Equals(lObjSDR["CreatedBy"]) ? null : lObjSDR["CreatedBy"].ToString();
									Modified = DBNull.Value.Equals(lObjSDR["Modified"]) ? (DateTime?)null : (DateTime)lObjSDR["Modified"];
									ModifiedBy = DBNull.Value.Equals(lObjSDR["ModifiedBy"]) ? null : lObjSDR["ModifiedBy"].ToString();

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


		public bool Delete(string isConStr, int iItemNo)
		{
			try
			{
				using (SqlConnection lObjCon = new SqlConnection(isConStr))
				{
					string lsQuery = "UPDATE ItemMaster SET Deleted ='Y', DeletedOn=@DeletedOn, DeletedBy = @DeletedBy WHERE ItemNo=@ItemNo";

					using (SqlCommand cmd = new SqlCommand(lsQuery))
					{
						cmd.Connection = lObjCon;
						cmd.CommandType = CommandType.Text;
						cmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.DateTime).Value = DateTime.Now;
						cmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
						cmd.Parameters.AddWithValue("@ItemNo", SqlDbType.VarChar).Value = iItemNo;

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
