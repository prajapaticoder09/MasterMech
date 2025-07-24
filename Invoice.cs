using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MasterMech
{
    class Invoice
    {
        //Invoice Table Field
        public string InvoiceNo;
        public int? InvoiceSNo;
        public DateTime InvoiceDate;
        public string InvoiceSts;
        public CustomerClass InvoiceCustomer;
        public string VehicleRegNo;
        public string VehicleModel;
        public string ChassisNo;
        public string EngineNo;
        public int? Mileage;
        public string ServiceType;
        public string ServiceAssoName;
        public string ServiceAssoMobNo;
        public double? PartsTotal;
        public double? LabourTotal;
        public double? PartsCGSTTotal;
        public double? LabourCGSTTotal;
        public double? PartsSGSTTotal;
        public double? LabourSGSTTotal;
        public double? PartsIGSTTotal;
        public double? LabourIGSTTotal;
        public double? TotalSGST;
        public double? TotalCGST;
        public double? TotalIGST;
        public double? TotalTax;
        public double? TotalAmount;
        public double? GrandTotal;
        public double? DiscountAmount;
        public double? InvoiceTotal;
        public string InvoiceRemarks;
        public DateTime? Created;
        public string CreatedBy;
        public DateTime? Modified;
        public string ModifiedBy;
        public char Deleted;
        public DateTime? DeletedOn;
        public string DeletedBy;


        // Invoice Item in the Invoice
        public List<InvoiceItem> InvoiceItem;

        // Supporting propeties
        private string ConnStr;
        private string UserID;

        public Invoice()
        {
            InvoiceCustomer = new CustomerClass();
            InvoiceItem = new List<InvoiceItem>();
        }


        public Invoice(string isConStr, string isUserID)
        {
            ConnStr = isConStr;
            UserID = isUserID;
            InvoiceCustomer = new CustomerClass(isConStr, isUserID);
            InvoiceItem = new List<InvoiceItem>();
        }

        public Invoice(int CustNo, string CustFName, string CustLName, string CustMobNo, string CustEmail, string CustSts, string CCustType, string lnInvoiceNo, int lnInvoiceSNo, string lsInvoiceSts)
        {
             InvoiceCustomer = new CustomerClass();
           InvoiceCustomer.lnCustNo = CustNo;
            InvoiceCustomer.lsCustFName = CustFName;
            InvoiceCustomer.lsCustLName = CustLName;
            InvoiceCustomer.lsCustMobNo = CustMobNo;
            InvoiceCustomer.lsCustEmail = CustEmail;
            InvoiceCustomer.lsCustSts = CustSts;
            InvoiceCustomer.CustType = CCustType;
            InvoiceNo = lnInvoiceNo;
            InvoiceSNo = lnInvoiceSNo;
            InvoiceSts = lsInvoiceSts;
        }

        public bool Save()
        {
            using (SqlConnection lobjCon = new SqlConnection(ConnStr))
            {
                lobjCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = lobjCon;

                SqlTransaction InvoiceTrans;
                InvoiceTrans = lobjCon.BeginTransaction("InvoiceTrans");
                cmd.Transaction = InvoiceTrans;

                if (InvoiceCustomer.lnCustNo == 0)
                {
                    if (!InvoiceCustomer.Save(cmd))
                    {
                        InvoiceTrans.Rollback();
                        lobjCon.Close();
                        return false;
                    }
                }
                else
                {
                    if (!InvoiceCustomer.updatedLastVisit(cmd))
                    {
                        InvoiceTrans.Rollback();
                        lobjCon.Close();
                        return false;
                    }
                }


                if (InvoiceSNo == null)// New Invoice ,Insert Invocie Values into the Invoice table
                {
                    try
                    {
                        string lsQryText = "INSERT INTO [Invoice" + MasterMechUtil.sFY + "]";

                        lsQryText += "(InvoiceDate, InVoiceSts, CustNo, CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr, CustCity,";
                        lsQryText += "CustState, CustPinCode, CustCountry, CustGSTNo, CustlLastVisit, CustRemarks, VehicleRegNo, VehicleModel, ChassisNo, EngineNo,";
                        lsQryText += "Mileage, ServiceType, ServiceAssoName, ServiceAssoMobNo, PartsTotal, LabourTotal, PartsCGSTTotal, LabourCGSTTotal, PartsSGSTTotal,";
                        lsQryText += " LabourSGSTTotal, PartsIGSTTotal, LabourIGSTTotal, TotalSGST, TotalCGST, TotalIGST, TotalTax, TotalAmount, GrandTotal, DiscountAmount,";
                        lsQryText += " InvoiceTotal, InvoiceRemarks, Created,CreatedBy ,Deleted) OUTPUT INSERTED.InvoiceSNo";

                        lsQryText += " VALUES (@InvoiceDate,@InVoiceSts,@CustNo,@CustFName,@CustLName,@CustMobNo,@CustEmail,@CustSts,@CustType,@CustStAddr,@CustArAddr,@CustCity,";
                        lsQryText += "@CustState,@CustPinCode,@CustCountry,@CustGSTNo,@CustlLastVisit,@CustRemarks,@VehicleRegNo,@VehicleModel,@ChassisNo,@EngineNo,";
                        lsQryText += "@Mileage,@ServiceType,@ServiceAssoName,@ServiceAssoMobNo,@PartsTotal,@LabourTotal,@PartsCGSTTotal,@LabourCGSTTotal,@PartsSGSTTotal,";
                        lsQryText += "@LabourSGSTTotal,@PartsIGSTTotal,@LabourIGSTTotal,@TotalSGST,@TotalCGST,@TotalIGST,@TotalTax,@TotalAmount,@GrandTotal,@DiscountAmount,";
                        lsQryText += "@InvoiceTotal,@InvoiceRemarks,@Created,@CreatedBy,@Deleted)";

                        
                            cmd.CommandText = lsQryText;
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.AddWithValue("@InvoiceDate", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.AddWithValue("@InvoiceSts", SqlDbType.VarChar).Value = "SAVED";
                            cmd.Parameters.AddWithValue("@CustNo", SqlDbType.VarChar).Value = InvoiceCustomer.lnCustNo;
                            cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustFName;
                            cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustLName;
                            cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustMobNo;
                            cmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustEmail;
                            cmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustSts;
                            cmd.Parameters.AddWithValue("@CustType", SqlDbType.VarChar).Value = InvoiceCustomer.CustType;
                            cmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustStAddr ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustArAddr ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustCity ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@CustState", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustState ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.VarChar).Value = InvoiceCustomer.CustPinCode ;
                            cmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustCountry ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustGSTNo ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@CustlLastVisit", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustRemarks ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@VehicleRegNo", SqlDbType.VarChar).Value = (object)VehicleRegNo ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@VehicleModel", SqlDbType.VarChar).Value = (object)VehicleModel ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@ChassisNo", SqlDbType.VarChar).Value = (object)ChassisNo ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@EngineNo", SqlDbType.VarChar).Value = (object)EngineNo ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@Mileage", SqlDbType.VarChar).Value = (object)Mileage ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@ServiceType", SqlDbType.VarChar).Value = (object)ServiceType ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@ServiceAssoName", SqlDbType.VarChar).Value = (object)ServiceAssoName ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@ServiceAssoMobNo", SqlDbType.VarChar).Value = (object)ServiceAssoMobNo ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@PartsTotal", SqlDbType.VarChar).Value = (object)PartsTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@LabourTotal", SqlDbType.VarChar).Value = (object)LabourTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@PartsCGSTTotal", SqlDbType.VarChar).Value = (object)PartsCGSTTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@LabourCGSTTotal", SqlDbType.VarChar).Value = (object)LabourCGSTTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@PartsSGSTTotal", SqlDbType.VarChar).Value = (object)PartsSGSTTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@LabourSGSTTotal", SqlDbType.VarChar).Value = (object)LabourSGSTTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@PartsIGSTTotal", SqlDbType.VarChar).Value = (object)PartsIGSTTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@LabourIGSTTotal", SqlDbType.VarChar).Value = (object)LabourIGSTTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@TotalSGST", SqlDbType.VarChar).Value = (object)TotalSGST ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@TotalCGST", SqlDbType.VarChar).Value = (object)TotalCGST ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@TotalIGST", SqlDbType.VarChar).Value = (object)TotalIGST ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@TotalTax", SqlDbType.VarChar).Value = (object)TotalTax ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@TotalAmount", SqlDbType.VarChar).Value = (object)TotalAmount ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@GrandTotal", SqlDbType.VarChar).Value = (object)GrandTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@DiscountAmount", SqlDbType.VarChar).Value = (object)DiscountAmount ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@InvoiceTotal", SqlDbType.VarChar).Value = (object)InvoiceTotal ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@InvoiceRemarks", SqlDbType.VarChar).Value = (object)InvoiceRemarks ?? DBNull.Value;
                            cmd.Parameters.AddWithValue("@Created", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.AddWithValue("@CreatedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
                            cmd.Parameters.AddWithValue("@Deleted", SqlDbType.Char).Value = 'N';





                            InvoiceSNo = (int)cmd.ExecuteScalar();
                            cmd.Parameters.Clear();

                            foreach (InvoiceItem lobjInvoiceItem in InvoiceItem)
                            {
                              lobjInvoiceItem.InvoiceSNo = InvoiceSNo;
                              if (!lobjInvoiceItem.Save(cmd))
                              {
                                InvoiceTrans.Rollback();
                                lobjCon.Close();
                                return false;
                              }
                            }
                        InvoiceTrans.Commit();
                            lobjCon.Close();

                        

                    }
                    catch (SqlException ex)
                    {
                        MasterMechUtil.ShowError(ex.Message);
                        InvoiceTrans.Rollback();
                        lobjCon.Close();
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        string lsQuery = "UPDATE [Invoice" + MasterMechUtil.sFY + "] SET  CustNo=@CustNo, CustFName=@CustFName, CustLName=@CustLName, CustMobNo=@CustMobNo, CustEmail=@CustEmail,";
                        lsQuery += " CustSts=@CustSts, CustType=@CustType, CustStAddr=@CustStAddr, CustArAddr=@CustArAddr, CustCity=@CustCity,";
                        lsQuery += "CustState=@CustState, CustPinCode=@CustPinCode, CustCountry=@CustCountry, CustGSTNo=@CustGSTNo, CustRemarks=@CustRemarks, VehicleRegNo=@VehicleRegNo, VehicleModel=@VehicleModel, ChassisNo=@ChassisNo, EngineNo=@EngineNo,";
                        lsQuery += "Mileage=@Mileage, ServiceType=@ServiceType, ServiceAssoName=@ServiceAssoName, ServiceAssoMobNo=@ServiceAssoMobNo, PartsTotal=@PartsTotal, LabourTotal=@LabourTotal, PartsCGSTTotal=@PartsCGSTTotal, LabourCGSTTotal=@LabourCGSTTotal, PartsSGSTTotal=@PartsSGSTTotal,";
                        lsQuery += "LabourSGSTTotal=@LabourSGSTTotal, PartsIGSTTotal=@PartsIGSTTotal, LabourIGSTTotal=@LabourIGSTTotal, TotalSGST=@TotalSGST, TotalCGST=@TotalCGST, TotalIGST=@TotalIGST, TotalTax=@TotalTax, TotalAmount=@TotalAmount, GrandTotal=@GrandTotal, DiscountAmount=@DiscountAmount,";
                        lsQuery += "InvoiceTotal=@InvoiceTotal, InvoiceRemarks=@InvoiceRemarks,Modified=@Modified,ModifiedBy=@ModifiedBy where InvoiceSNo = @InvoiceSNo ";
                        
                        cmd.CommandText = lsQuery;
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@InvoiceSNo", SqlDbType.DateTime).Value = InvoiceSNo;
                        cmd.Parameters.AddWithValue("@CustNo", SqlDbType.VarChar).Value = InvoiceCustomer.lnCustNo;
                        cmd.Parameters.AddWithValue("@CustFName", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustFName;
                        cmd.Parameters.AddWithValue("@CustLName", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustLName;
                        cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustMobNo;
                        cmd.Parameters.AddWithValue("@CustEmail", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustEmail;
                        cmd.Parameters.AddWithValue("@CustSts", SqlDbType.VarChar).Value = InvoiceCustomer.lsCustSts;
                        cmd.Parameters.AddWithValue("@CustType", SqlDbType.VarChar).Value = InvoiceCustomer.CustType;
                        cmd.Parameters.AddWithValue("@CustStAddr", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustStAddr ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@CustArAddr", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustArAddr ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@CustCity", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustCity ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@CustState", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustState ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@CustPinCode", SqlDbType.VarChar).Value = InvoiceCustomer.CustPinCode;
                        cmd.Parameters.AddWithValue("@CustCountry", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustCountry ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@CustGSTNo", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustGSTNo ?? DBNull.Value;
                       // cmd.Parameters.AddWithValue("@CustlLastVisit", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.AddWithValue("@CustRemarks", SqlDbType.VarChar).Value = (object)InvoiceCustomer.lsCustRemarks ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@VehicleRegNo", SqlDbType.VarChar).Value = (object)VehicleRegNo ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@VehicleModel", SqlDbType.VarChar).Value = (object)VehicleModel ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@ChassisNo", SqlDbType.VarChar).Value = (object)ChassisNo ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@EngineNo", SqlDbType.VarChar).Value = (object)EngineNo ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@Mileage", SqlDbType.VarChar).Value = (object)Mileage ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@ServiceType", SqlDbType.VarChar).Value = (object)ServiceType ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@ServiceAssoName", SqlDbType.VarChar).Value = (object)ServiceAssoName ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@ServiceAssoMobNo", SqlDbType.VarChar).Value = (object)ServiceAssoMobNo ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@PartsTotal", SqlDbType.VarChar).Value = (object)PartsTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@LabourTotal", SqlDbType.VarChar).Value = (object)LabourTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@PartsCGSTTotal", SqlDbType.VarChar).Value = (object)PartsCGSTTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@LabourCGSTTotal", SqlDbType.VarChar).Value = (object)LabourCGSTTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@PartsSGSTTotal", SqlDbType.VarChar).Value = (object)PartsSGSTTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@LabourSGSTTotal", SqlDbType.VarChar).Value = (object)LabourSGSTTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@PartsIGSTTotal", SqlDbType.VarChar).Value = (object)PartsIGSTTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@LabourIGSTTotal", SqlDbType.VarChar).Value = (object)LabourIGSTTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@TotalSGST", SqlDbType.VarChar).Value = (object)TotalSGST ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@TotalCGST", SqlDbType.VarChar).Value = (object)TotalCGST ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@TotalIGST", SqlDbType.VarChar).Value = (object)TotalIGST ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@TotalTax", SqlDbType.VarChar).Value = (object)TotalTax ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@TotalAmount", SqlDbType.VarChar).Value = (object)TotalAmount ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@GrandTotal", SqlDbType.VarChar).Value = (object)GrandTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@DiscountAmount", SqlDbType.VarChar).Value = (object)DiscountAmount ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@InvoiceTotal", SqlDbType.VarChar).Value = (object)InvoiceTotal ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@InvoiceRemarks", SqlDbType.VarChar).Value = (object)InvoiceRemarks ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@Modified", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;

                        //if (InvoiceCustomer.lnCustNo != 0)
                        //{
                        //    if (!InvoiceCustomer.Save(cmd))
                        //    {
                        //        InvoiceTrans.Rollback();
                        //        lobjCon.Close();
                        //        return false;
                        //    }
                        //}

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        foreach (InvoiceItem lobjInvoiceItem in InvoiceItem)
                        {
                            lobjInvoiceItem.InvoiceSNo = InvoiceSNo;
                            if (!lobjInvoiceItem.Save(cmd))
                            {
                                InvoiceTrans.Rollback();
                                lobjCon.Close();
                                return false;
                            }
                        }

                        InvoiceTrans.Commit();
                        lobjCon.Close();
                    }
                    catch (SqlException ex)
                    {
                        MasterMechUtil.ShowError(ex.Message);
                        return false;
                    }
                }
            }
            return true;
        }



        public bool SearchInvoice(string isConStr, string isSearchMob, List<Invoice> oObjInvoice)
        {
            try
            {
                using (SqlConnection lObjCon = new SqlConnection(isConStr))
                {

                    string lsQryText = "SELECT  InnvoiceNo, InvoiceSNo,InvoiceSts, CustNo, CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType FROM  [Invoice" + MasterMechUtil.sFY + "]";
                    lsQryText += " WHERE Deleted ='N' AND CustMobNo LIKE @CustMobNo ";

                    SqlCommand cmd = new SqlCommand(lsQryText, lObjCon);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CustMobNo", SqlDbType.VarChar).Value = "%" + isSearchMob + "%";

                    lObjCon.Open();
                    using (SqlDataReader lObjSDR = cmd.ExecuteReader())
                    {
                        if (lObjSDR.HasRows)
                        {
                            while (lObjSDR.Read())
                            {
                                // These values are not being checked for being null as by table design these columns can not be null
                                InvoiceNo = lObjSDR["InnvoiceNo"].ToString();
                               InvoiceSNo = (int)lObjSDR["InvoiceSNo"];
                               InvoiceSts = lObjSDR["InvoiceSts"].ToString();
                                InvoiceCustomer.lnCustNo = (int)lObjSDR["CustNo"];
                                InvoiceCustomer.lsCustFName = lObjSDR["CustFName"].ToString();
                                InvoiceCustomer.lsCustLName = lObjSDR["CustLName"].ToString();
                                InvoiceCustomer.lsCustMobNo = lObjSDR["CustMobNo"].ToString();
                                InvoiceCustomer.lsCustEmail = lObjSDR["CustEmail"].ToString();
                                InvoiceCustomer.lsCustSts = lObjSDR["CustSts"].ToString();
                                InvoiceCustomer.CustType = lObjSDR["CustType"].ToString();
                               


                                /*lsCustStAddr = (string)((object)lObjSDR["CustStAddr"].ToString() ?? DBNull.Value);
                                lsCustArAddr = (string)((object)lObjSDR["CustArAddr"].ToString() ?? DBNull.Value);
                                lsCustCity = (string)((object)lObjSDR["CustCity"].ToString() ?? DBNull.Value);
                                lsCustState = (string)((object)lObjSDR["CustState"].ToString() ?? DBNull.Value);
                                //CustPinCode = (char)((object)lObjSDR["CustPinCode"].ToString() ?? DBNull.Value);
                                lsCustCountry = (string)((object)lObjSDR["CustCountry"].ToString() ?? DBNull.Value);
                                lsCustGSTNo = (string)((object)lObjSDR["CustGSTNo"].ToString() ?? DBNull.Value);
                                lsCustRemarks = (string)((object)lObjSDR["CustRemarks"].ToString() ?? DBNull.Value);*/



                                oObjInvoice.Add(new Invoice(InvoiceCustomer.lnCustNo, InvoiceCustomer.lsCustFName, InvoiceCustomer.lsCustLName, InvoiceCustomer.lsCustMobNo, InvoiceCustomer.lsCustEmail, InvoiceCustomer.lsCustSts, InvoiceCustomer.CustType, InvoiceNo, (int)InvoiceSNo, InvoiceSts));
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



        public bool Load(string isConStr, int? InINvoiceNo)
        {
            bool lbDataFound = false;

            using (SqlConnection lObjCon = new SqlConnection(isConStr))
            {

                string lsQuery = "SELECT  InnvoiceNo,InvoiceSNo,InvoiceDate,InvoiceSts, InvoiceRemarks, CustNo, CustFName, CustLName, CustMobNo, CustEmail, CustSts, CustType, CustStAddr, CustArAddr,CustCity,CustState,";
                lsQuery += "CustPinCode,CustCountry,CustGSTNo,CustlLastVisit,CustRemarks, Created, CreatedBy, Modified, ModifiedBy,VehicleRegNo,PartsTotal,LabourTotal,";
                lsQuery += "PartsCGSTTotal,PartsSGSTTotal,PartsIGSTTotal,LabourCGSTTotal,LabourSGSTTotal,LabourIGSTTotal,TotalAmount,TotalSGST,TotalCGST,TotalIGST,";
                lsQuery += "TotalTax,DiscountAmount,GrandTotal,VehicleModel,ChassisNo,EngineNo,Mileage,ServiceType,ServiceAssoName,ServiceAssoMobNo FROM [Invoice" + MasterMechUtil.sFY + "] WHERE InvoiceSNo = @InvoiceSNo AND Deleted='N'";
                try
                {
                    using (SqlCommand cmd = new SqlCommand(lsQuery))
                    {
                        cmd.Connection = lObjCon;
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@InvoiceSNo", SqlDbType.VarChar).Value = InINvoiceNo;

                        lObjCon.Open();
                        using (SqlDataReader lObjSDR = cmd.ExecuteReader())
                        {
                            if (lObjSDR.HasRows)
                            {
                                while (lObjSDR.Read())
                                {
                                    InvoiceSNo = InINvoiceNo;
                                    InvoiceNo = lObjSDR["InnvoiceNo"].ToString();
                                    InvoiceSts = lObjSDR["InvoiceSts"].ToString();
                                    InvoiceDate = (DateTime)lObjSDR["InvoiceDate"];
                                    InvoiceRemarks = DBNull.Value.Equals(lObjSDR["InvoiceRemarks"]) ? null : lObjSDR["InvoiceRemarks"].ToString();
                                    InvoiceCustomer.lnCustNo = (int)lObjSDR["CustNo"];
                                    InvoiceCustomer.lsCustFName = lObjSDR["CustFName"].ToString();
                                    InvoiceCustomer.lsCustLName = lObjSDR["CustLName"].ToString();
                                    InvoiceCustomer.lsCustMobNo = lObjSDR["CustMobNo"].ToString();
                                    InvoiceCustomer.lsCustEmail = lObjSDR["CustEmail"].ToString();
                                    InvoiceCustomer.lsCustSts = lObjSDR["CustSts"].ToString();
                                    InvoiceCustomer.CustType = lObjSDR["CustType"].ToString();
                                    InvoiceCustomer.lsCustStAddr = DBNull.Value.Equals(lObjSDR["CustStAddr"]) ? null : lObjSDR["CustStAddr"].ToString();
                                    InvoiceCustomer.lsCustArAddr = DBNull.Value.Equals(lObjSDR["CustArAddr"]) ? null : lObjSDR["CustArAddr"].ToString();
                                    InvoiceCustomer.lsCustCity = DBNull.Value.Equals(lObjSDR["CustCity"]) ? null : lObjSDR["CustCity"].ToString();
                                    InvoiceCustomer.lsCustState = DBNull.Value.Equals(lObjSDR["CustState"]) ? null : lObjSDR["CustState"].ToString();
                                    InvoiceCustomer.CustPinCode = DBNull.Value.Equals(lObjSDR["CustPinCode"]) ? null : lObjSDR["CustPinCode"].ToString();
                                    InvoiceCustomer.lsCustCountry = DBNull.Value.Equals(lObjSDR["CustCountry"]) ? null : lObjSDR["CustCountry"].ToString();
                                    InvoiceCustomer.lsCustGSTNo = DBNull.Value.Equals(lObjSDR["CustGSTNo"]) ? null : lObjSDR["CustGSTNo"].ToString();
                                    InvoiceCustomer.dCustlLastVisit = (DateTime)lObjSDR["CustlLastVisit"];
                                    InvoiceCustomer.lsCustRemarks = DBNull.Value.Equals(lObjSDR["CustRemarks"]) ? null : lObjSDR["CustRemarks"].ToString();
                                    Created = (DateTime)lObjSDR["Created"];
                                    CreatedBy = DBNull.Value.Equals(lObjSDR["CreatedBy"]) ? null : lObjSDR["CreatedBy"].ToString();
                                    Modified = DBNull.Value.Equals(lObjSDR["Modified"]) ? (DateTime?)null : (DateTime)lObjSDR["Modified"];
                                    ModifiedBy = DBNull.Value.Equals(lObjSDR["ModifiedBy"]) ? null : lObjSDR["ModifiedBy"].ToString();
                                    PartsTotal = DBNull.Value.Equals(lObjSDR["PartsTotal"]) ? null : (double?)lObjSDR["PartsTotal"];
                                    PartsSGSTTotal = DBNull.Value.Equals(lObjSDR["PartsSGSTTotal"]) ? null : (double?)lObjSDR["PartsSGSTTotal"];
                                    PartsIGSTTotal = DBNull.Value.Equals(lObjSDR["PartsIGSTTotal"]) ? null : (double?)lObjSDR["PartsIGSTTotal"];
                                    PartsCGSTTotal = DBNull.Value.Equals(lObjSDR["PartsCGSTTotal"]) ? null : (double?)lObjSDR["PartsCGSTTotal"];
                                    LabourTotal = DBNull.Value.Equals(lObjSDR["LabourTotal"]) ? null : (double?)lObjSDR["LabourTotal"];
                                    LabourSGSTTotal = DBNull.Value.Equals(lObjSDR["LabourSGSTTotal"]) ? null : (double?)lObjSDR["LabourSGSTTotal"];
                                    LabourIGSTTotal = DBNull.Value.Equals(lObjSDR["LabourIGSTTotal"]) ? null : (double?)lObjSDR["LabourIGSTTotal"];
                                    LabourCGSTTotal = DBNull.Value.Equals(lObjSDR["LabourCGSTTotal"]) ? null : (double?)lObjSDR["LabourCGSTTotal"];
                                    TotalAmount = DBNull.Value.Equals(lObjSDR["TotalAmount"]) ? null : (double?)lObjSDR["TotalAmount"];
                                    TotalCGST = DBNull.Value.Equals(lObjSDR["TotalCGST"]) ? null : (double?)lObjSDR["TotalCGST"];
                                    TotalIGST = DBNull.Value.Equals(lObjSDR["TotalIGST"]) ? null : (double?)lObjSDR["TotalIGST"];
                                    TotalSGST = DBNull.Value.Equals(lObjSDR["TotalSGST"]) ? null : (double?)lObjSDR["TotalSGST"];
                                    DiscountAmount = DBNull.Value.Equals(lObjSDR["DiscountAmount"]) ? null : (double?)lObjSDR["DiscountAmount"];
                                    TotalTax = DBNull.Value.Equals(lObjSDR["TotalTax"]) ? null : (double?)lObjSDR["TotalTax"];
                                    GrandTotal = DBNull.Value.Equals(lObjSDR["GrandTotal"]) ? null : (double?)lObjSDR["GrandTotal"];
                                    VehicleRegNo = DBNull.Value.Equals(lObjSDR["VehicleRegNo"]) ? null : lObjSDR["VehicleRegNo"].ToString();
                                    VehicleModel = DBNull.Value.Equals(lObjSDR["VehicleModel"]) ? null : lObjSDR["VehicleModel"].ToString();
                                    ChassisNo = DBNull.Value.Equals(lObjSDR["ChassisNo"]) ? null : lObjSDR["ChassisNo"].ToString();
                                    EngineNo = DBNull.Value.Equals(lObjSDR["EngineNo"]) ? null : lObjSDR["EngineNo"].ToString();
                                    Mileage = DBNull.Value.Equals(lObjSDR["Mileage"]) ? null : (int?)lObjSDR["Mileage"];
                                    ServiceType = DBNull.Value.Equals(lObjSDR["ServiceType"]) ? null : lObjSDR["ServiceType"].ToString();
                                    ServiceAssoName = DBNull.Value.Equals(lObjSDR["ServiceAssoName"]) ? null : lObjSDR["ServiceAssoName"].ToString();
                                    ServiceAssoMobNo = DBNull.Value.Equals(lObjSDR["ServiceAssoMobNo"]) ? null : lObjSDR["ServiceAssoMobNo"].ToString();

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

        public bool Delete(string isConStr, int? lnInvoiceSNo)
        {
            using (SqlConnection lobjCon = new SqlConnection(isConStr))
            {
                lobjCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = lobjCon;

                SqlTransaction InvoiceTrans;
                InvoiceTrans = lobjCon.BeginTransaction("InvoiceTrans");
                cmd.Transaction = InvoiceTrans;

                try
                {
                    string lsQuery = "UPDATE [Invoice" + MasterMechUtil.sFY + "] SET Deleted ='Y', DeletedOn=@DeletedOn, DeletedBy = @DeletedBy WHERE InvoiceSNo = @InvoiceSNo";

                    cmd.CommandText = lsQuery;
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@DeletedOn", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.AddWithValue("@DeletedBy", SqlDbType.VarChar).Value = MasterMechUtil.sUserID;
                    cmd.Parameters.AddWithValue("@InvoiceSNo", SqlDbType.VarChar).Value = lnInvoiceSNo;


                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    InvoiceItem lobjInvoiceItem = new InvoiceItem(MasterMechUtil.ConnStr,MasterMechUtil.sUserID);
                        lobjInvoiceItem.InvoiceSNo = InvoiceSNo;
                        if (!lobjInvoiceItem.DeletedInvoice(cmd))
                        {
                            InvoiceTrans.Rollback();
                            lobjCon.Close();
                            return false;
                        }
                  

                    InvoiceTrans.Commit();
                    lobjCon.Close();
                }
                catch (SqlException ex)
                {
                    MasterMechUtil.ShowError(ex.Message);
                   
                    return false;
                }
                
            }
            return true;


        }


    }
}
