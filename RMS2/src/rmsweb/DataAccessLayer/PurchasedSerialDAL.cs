using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class PurchasedSerialDAL : IPurchasedSerialDAL
    {
        public bool AddPurchasedSerial(PurchasedSerial purchasedSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from PurchasedSerial where 
                    ProductNo=@ProductNo and SerialNo=@SerialNo and VendorId=@VendorId and PurchaseDate=@PurchaseDate",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,purchasedSerial.ProductNo),
                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,purchasedSerial.SerialNo),
                        DataAccessMySQL.CreateParameter("VendorId",DbType.String,purchasedSerial.VendorId),
                        DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,purchasedSerial.PurchaseDate)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into PurchasedSerial (Company,UserGroup,Creator,CreateDate,
                        ProductNo,SerialNo,VendorId,PurchaseDate,SettingCode,SourceOrderType,SourceOrderNo,SourceOrderItemId,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @ProductNo,@SerialNo,@VendorId,@PurchaseDate,@SettingCode,@SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@Remark)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,purchasedSerial.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,purchasedSerial.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,purchasedSerial.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,purchasedSerial.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,purchasedSerial.SerialNo),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,purchasedSerial.VendorId),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,purchasedSerial.PurchaseDate),
                            DataAccessMySQL.CreateParameter("SettingCode",DbType.String,purchasedSerial.SettingCode),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,purchasedSerial.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,purchasedSerial.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,purchasedSerial.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,purchasedSerial.Remark)
                        });
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "廠商代號已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool UpdatePurchasedSerial(PurchasedSerial purchasedSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update PurchasedSerial set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,
                        SettingCode=@SettingCode,SourceOrderType=@SourceOrderType,SourceOrderNo=@SourceOrderNo,SourceOrderItemId=@SourceOrderItemId,Remark=@Remark where 
                        ProductNo=@ProductNo and SerialNo=@SerialNo and VendorId=@VendorId and PurchaseDate=@PurchaseDate",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,purchasedSerial.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,purchasedSerial.UserGroup),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,purchasedSerial.Creator),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,purchasedSerial.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,purchasedSerial.SerialNo),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,purchasedSerial.VendorId),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,purchasedSerial.PurchaseDate),
                            DataAccessMySQL.CreateParameter("SettingCode",DbType.String,purchasedSerial.SettingCode),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,purchasedSerial.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,purchasedSerial.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,purchasedSerial.SourceOrderItemId),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,purchasedSerial.Remark)
                        });
                msg = "";
                return true;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool DelPurchasedSerial(PurchasedSerial purchasedSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"delete from PurchasedSerial  where 
                        ProductNo=@ProductNo and SerialNo=@SerialNo and VendorId=@VendorId and PurchaseDate=@PurchaseDate",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,purchasedSerial.ProductNo),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,purchasedSerial.SerialNo),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,purchasedSerial.VendorId),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,purchasedSerial.PurchaseDate)
                        });
                msg = "";
                return true;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public List<PurchasedSerial> SearchPurchasedSerial(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<PurchasedSerial> objPurchasedSerial = new List<PurchasedSerial>();

            string sql = @"select s.*,v.VendorName,p.ProductName from PurchasedSerial as s
                left join vendor as v on v.VendorId=s.VendorId
                left join product as p on p.ProductNo=s.ProductNo where 1=1";

            string strCondition = "";
            DbParameter[] dbParameters = new DbParameter[colArray.Length - 1];
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";
                if (conditionArray[i] == "like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }
            }


            DataTable dtPurchasedSerial = null;
            if (strCondition != "")
            {
                dtPurchasedSerial = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtPurchasedSerial = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtPurchasedSerial.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPurchasedSerial.Rows)
                {
                    PurchasedSerial obj = new PurchasedSerial();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.VendorId = dr["VendorId"].ToString();
                    obj.VendorName = dr["VendorName"].ToString();
                    obj.PurchaseDate = dr["PurchaseDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["PurchaseDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.SettingCode = dr["SettingCode"].ToString() == "Y" ? "Y:已設定" : "N:未設定";
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.SourceOrderItemId = dr["SourceOrderItemId"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objPurchasedSerial.Add(obj);
                }
            }

            return objPurchasedSerial;
        }

        public PurchasedSerial GetPurchasedSerialInfo(PurchasedSerial purchasedSerial)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            PurchasedSerial obj = null;

            DataTable dtPurchasedSerial = dbMySQL.ExecuteDataTable(@"select s.*,v.VendorName,p.ProductName,
                pr.ProductType as ProductTypeId1Name from PurchasedSerial as s
                left join vendor as v on v.VendorId=s.VendorId
                left join product as p on p.ProductNo=s.ProductNo
                left join ProductCategory as pr on pr.ProductCategoryType='1' and pr.ProductTypeId=p.ProductTypeId1 where 
                s.ProductNo=@ProductNo and s.SerialNo=@SerialNo and s.VendorId=@VendorId and s.PurchaseDate=@PurchaseDate",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,purchasedSerial.ProductNo),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,purchasedSerial.SerialNo),
                    DataAccessMySQL.CreateParameter("VendorId",DbType.String,purchasedSerial.VendorId),
                    DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,purchasedSerial.PurchaseDate)
                });

            if (dtPurchasedSerial.Rows.Count > 0)
            {
                obj = new PurchasedSerial();
                obj.ProductNo = dtPurchasedSerial.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtPurchasedSerial.Rows[0]["ProductName"].ToString();
                obj.ProductTypeId1Name = dtPurchasedSerial.Rows[0]["ProductTypeId1Name"].ToString();
                obj.SerialNo = dtPurchasedSerial.Rows[0]["SerialNo"].ToString();
                obj.VendorId = dtPurchasedSerial.Rows[0]["VendorId"].ToString();
                obj.VendorName = dtPurchasedSerial.Rows[0]["VendorName"].ToString();
                obj.PurchaseDate = dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString() == "" ? "" : DateTime.ParseExact(dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.SettingCode = dtPurchasedSerial.Rows[0]["SettingCode"].ToString() == "Y" ? "Y:已設定" : "N:未設定";
                obj.SourceOrderType = dtPurchasedSerial.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtPurchasedSerial.Rows[0]["SourceOrderNo"].ToString();
                obj.SourceOrderItemId = dtPurchasedSerial.Rows[0]["SourceOrderItemId"].ToString();
                obj.Remark = dtPurchasedSerial.Rows[0]["Remark"].ToString();
            }

            return obj;
        }

        public bool ImportFile(string path, PurchasedSerial purchasedSerial)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dataAccessMySql.CreateDbTransaction();
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    bool isFirstRow = true;
                    while (sr.Peek() > -1)
                    {
                        string[] valueArray = sr.ReadLine().Split(',');
                        if (isFirstRow)
                        {
                            if (valueArray.Length < 5)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }

                        //判斷品號是否存在
                        if (int.Parse(dataAccessMySql.ExecuteScalar("select count(*) from Product where ProductNo=@ProductNo", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0].Trim()),
                            }).ToString()) == 0)
                        {
                            continue;
                        }

                        //判斷廠商代號是否存在
                        if (int.Parse(dataAccessMySql.ExecuteScalar("select count(*) from Vendor where VendorId=@VendorId", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[2].Trim()),
                            }).ToString()) == 0)
                        {
                            continue;
                        }
                        //判斷主鍵是否重複
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from PurchasedSerial where ProductNo=@ProductNo and SerialNo=@SerialNo and VendorId=@VendorId and PurchaseDate=@PurchaseDate",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("SerialNo", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("PurchaseDate", DbType.String, valueArray[3].Trim().Replace("/",""))
                                }).ToString()) ==0)
                        {
                            string[] SourceOrderNo = valueArray[4].Split('-');
                            dataAccessMySql.ExecuteNonQuery(
                               @"insert into PurchasedSerial(ProductNo,SerialNo,VendorId,PurchaseDate,SourceOrderType,SourceOrderNo,SourceOrderItemId,SettingCode,Company,UserGroup,Creator,CreateDate) values
                                (@ProductNo,@SerialNo,@VendorId,@PurchaseDate,@SourceOrderType,@SourceOrderNo,@SourceOrderItemId,'N',@Company,@UserGroup,@Creator,@CreateDate)",
                              tran,
                               new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String,valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("SerialNo", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("PurchaseDate", DbType.String, valueArray[3].Trim().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("SourceOrderType", DbType.String, SourceOrderNo[0].Trim()),
                                    DataAccessMySQL.CreateParameter("SourceOrderNo", DbType.String, SourceOrderNo[1].Trim()),
                                    DataAccessMySQL.CreateParameter("SourceOrderItemId", DbType.String, SourceOrderNo[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Company", DbType.String, purchasedSerial.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, purchasedSerial.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator", DbType.String, purchasedSerial.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd"))
                                });
                        }
                    }
                    tran.Commit();
                    sr.Close();
                    sr.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        public bool IsSerialNo(string SerialNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from PurchasedSerial where SerialNo=@SerialNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,SerialNo)
                }).ToString())>0)
            {
                return false;
            }
            return true;

        }

        public bool AddWarranty(string WarrantyId, Warranty_D warranty_D,out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                string[] WarrantyIdArray = WarrantyId.Split(',');

                string[] valueInfo = null;

                for (int i = 0; i < WarrantyIdArray.Length; i++)
                {
                    valueInfo = WarrantyIdArray[i].Split('#');

                    DataTable dtPurchasedSerial = dbMySQL.ExecuteDataTable(@"select s.*,v.VendorName from PurchasedSerial as s 
                        left join vendor as v on v.VendorId=s.VendorId where s.ProductNo=@ProductNo 
                        and s.SerialNo=@SerialNo and s.VendorId=@VendorId and s.PurchaseDate=@PurchaseDate", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,valueInfo[2].ToString()),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,valueInfo[3].ToString().Replace("/",""))
                        });
                    if(dtPurchasedSerial!=null&&dtPurchasedSerial.Rows.Count>0)
                    {
                        dbMySQL.ExecuteNonQuery(@"insert into Warranty_D (Company,UserGroup,Creator,CreateDate,ProductNo,SerialNo,
                            ChangeDate,ChangeOrderType,ChangeOrderNo,ChangeOrderItemId,TargetId,TargetName,DataDate,DataTime,
                            WarrantyPeriod,WarrantySDate,WarrantyEDate,WarrantyType,Remark) values (@Company,@UserGroup,@Creator,@CreateDate,@ProductNo,@SerialNo,
                            @ChangeDate,@ChangeOrderType,@ChangeOrderNo,@ChangeOrderItemId,@TargetId,@TargetName,@DataDate,@DataTime,
                            @WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@WarrantyType,@Remark)", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_D.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_D.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_D.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,dtPurchasedSerial.Rows[0]["SourceOrderType"].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,dtPurchasedSerial.Rows[0]["SourceOrderNo"].ToString()),
                                DataAccessMySQL.CreateParameter("ChangeOrderItemId",DbType.String,dtPurchasedSerial.Rows[0]["SourceOrderItemId"].ToString()),
                                DataAccessMySQL.CreateParameter("TargetId",DbType.String,dtPurchasedSerial.Rows[0]["VendorId"].ToString()),
                                DataAccessMySQL.CreateParameter("TargetName",DbType.String,dtPurchasedSerial.Rows[0]["VendorName"].ToString()),
                                DataAccessMySQL.CreateParameter("DataDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("DataTime",DbType.String,DateTime.Now.ToString("HHmmss")),
                                DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,warranty_D.WarrantyPeriod),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString().Replace("/","")),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,DateTime.ParseExact(dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString().Replace("/",""), "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(int.Parse(warranty_D.WarrantyPeriod)).AddDays(-1).ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("WarrantyType",DbType.String,"1"),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,warranty_D.Remark)
                            });

                        if(int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from Warranty_H where ProductNo=@ProductNo and SerialNo=@SerialNo",tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                            }).ToString())==0)
                        {
                            dbMySQL.ExecuteNonQuery(@"insert into Warranty_H (Company,UserGroup,Creator,CreateDate,ProductNo,SerialNo,
                                LastWarrantyType,LastWarrantyRemark,LastWarrantySDate,LastWarrantyEDate) values (@Company,@UserGroup,
                                @Creator,@CreateDate,@ProductNo,@SerialNo,@LastWarrantyType,@LastWarrantyRemark,@LastWarrantySDate,
                                @LastWarrantyEDate)", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,warranty_D.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,warranty_D.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,warranty_D.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                                    DataAccessMySQL.CreateParameter("LastWarrantyType",DbType.String,"1"),
                                    DataAccessMySQL.CreateParameter("LastWarrantyRemark",DbType.String,warranty_D.Remark),
                                    DataAccessMySQL.CreateParameter("LastWarrantySDate",DbType.String,dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("LastWarrantyEDate",DbType.String,DateTime.ParseExact(dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString().Replace("/",""), "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(int.Parse(warranty_D.WarrantyPeriod)).AddDays(-1).ToString("yyyyMMdd"))
                                });
                        }else
                        {
                            dbMySQL.ExecuteNonQuery(@"update Warranty_H set Modifier=@Modifier,ModiDate=@ModiDate,
                                LastWarrantyType=@LastWarrantyType,LastWarrantyRemark=@LastWarrantyRemark,
                                LastWarrantySDate=@LastWarrantySDate,LastWarrantyEDate=@LastWarrantyEDate where 
                                ProductNo=@ProductNo and SerialNo=@SerialNo", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("Modifier",DbType.String,warranty_D.Creator),
                                    DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                                    DataAccessMySQL.CreateParameter("LastWarrantyType",DbType.String,"1"),
                                    DataAccessMySQL.CreateParameter("LastWarrantyRemark",DbType.String,warranty_D.Remark),
                                    DataAccessMySQL.CreateParameter("LastWarrantySDate",DbType.String,dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString().Replace("/","")),
                                    DataAccessMySQL.CreateParameter("LastWarrantyEDate",DbType.String,DateTime.ParseExact(dtPurchasedSerial.Rows[0]["PurchaseDate"].ToString().Replace("/",""), "yyyyMMdd", CultureInfo.InvariantCulture).AddMonths(int.Parse(warranty_D.WarrantyPeriod)).AddDays(-1).ToString("yyyyMMdd"))
                                });
                        }

                        dbMySQL.ExecuteNonQuery(@"update PurchasedSerial set SettingCode='Y' where ProductNo=@ProductNo 
                        and SerialNo = @SerialNo and VendorId = @VendorId and PurchaseDate = @PurchaseDate", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,valueInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,valueInfo[2].ToString()),
                            DataAccessMySQL.CreateParameter("PurchaseDate",DbType.String,valueInfo[3].ToString().Replace("/",""))
                        });
                    }

                }

                tran.Commit();
                msg = "";
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                msg = ex.Message;
                return false;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }
    }
}