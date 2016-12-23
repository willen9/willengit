using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    public class VendorDAL : IVendor
    {
        DataTable IVendor.SelectVendor(BusinessLayer.Models.Vendor vendor, int Page,string condition1,string condition2)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select vendor.* from vendor where 1=1 ";
            if (vendor != null)
            {
                if (!string.IsNullOrEmpty(vendor.Company))
                {
                    sqlStr += " and vendor.Company=@Company ";
                }
                if (!string.IsNullOrEmpty(vendor.UserGroup))
                {
                    sqlStr += " and vendor.UserGroup=@UserGroup ";
                }
                if (!string.IsNullOrEmpty(vendor.Creator))
                {
                    sqlStr += " and vendor.Creator=@Creator ";
                }
                string departmentIdParameterValue1 = "";
                string departmentIdParameterValue2 = "";
                if (!string.IsNullOrEmpty(vendor.VendorId))
                {
                    if (!string.IsNullOrEmpty(condition1))
                    {
                        if (condition1 == "like1")
                        {
                            sqlStr += " and vendor.VendorId like @VendorId ";
                            departmentIdParameterValue1 = "%" + vendor.VendorId;
                        }
                        else if (condition1 == "like2")
                        {
                            sqlStr += " and vendor.VendorId like @VendorId ";
                            departmentIdParameterValue1 = vendor.VendorId +"%";
                        }
                        else if (condition1 == "like3")
                        {
                            sqlStr += " and vendor.VendorId like @VendorId ";
                            departmentIdParameterValue1 = "%" + vendor.VendorId +"%";
                        }
                        else
                        {
                            sqlStr += " and vendor.VendorId " + condition1 + " @VendorId ";
                            departmentIdParameterValue1 = vendor.VendorId;
                        }
                    }
                    else
                    {
                        sqlStr += " and vendor.VendorId=@VendorId ";
                        departmentIdParameterValue1 = vendor.VendorId;
                    }
                }
                if (!string.IsNullOrEmpty(vendor.VendorName))
                {
                    if (!string.IsNullOrEmpty(condition2))
                    {
                        if (condition2 == "like1")
                        {
                            sqlStr += " and vendor.VendorName like @VendorName ";
                            departmentIdParameterValue2 = "%" + vendor.VendorName;
                        }
                        else if (condition2 == "like2")
                        {
                            sqlStr += " and vendor.VendorName like @VendorName ";
                            departmentIdParameterValue2 = vendor.VendorName + "%";
                        }
                        else if (condition2 == "like3")
                        {
                            sqlStr += " and vendor.VendorName like @VendorName ";
                            departmentIdParameterValue2 = "%" + vendor.VendorName + "%";
                        }
                        else
                        {
                            sqlStr += " and vendor.VendorName " + condition2 + " @VendorName ";
                            departmentIdParameterValue2 = vendor.VendorName;
                        }
                    }
                    else
                    {
                        sqlStr += " and vendor.VendorName like @VendorName ";
                        departmentIdParameterValue2 = "%" + vendor.VendorName + "%";
                    }
                }
                if(Page!=0)
                {
                    sqlStr += " limit @Page1,@Page2";
                }
                
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Company", DbType.String, vendor.Company),
                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, vendor.UserGroup),
                    DataAccessMySQL.CreateParameter("Creator", DbType.String, vendor.Creator),
                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, departmentIdParameterValue1),
                    DataAccessMySQL.CreateParameter("VendorName", DbType.String,departmentIdParameterValue2),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        public bool IsModuleId(string vendorid)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from vendor where VendorId=@VendorId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("VendorId",DbType.String,vendorid)
                }).ToString();

            if (int.Parse(count) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        string IVendor.UpdateVendor(BusinessLayer.Models.Vendor vendor, string AddressId, string txtAddress1, string txtAddress2, string txtAddressContact, string txtAddressTelNo, string txtAddressFaxNo, string txtAddressRemark)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = dataAccessMySql.CreateDbTransaction();
            if (int.Parse(
              dataAccessMySql.ExecuteScalar("select count(*) from Vendor where VendorId=@VendorId", tran,
                  new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)
                    }).ToString()) <= 0)
            {
                return "1";
            }
            if (int.Parse(
             dataAccessMySql.ExecuteScalar("select count(*) from Vendor where VendorName=@VendorName and VendorId!=@VendorId", tran,
                 new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorName", DbType.String, vendor.VendorName),
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)
                    }).ToString()) > 0)
            {
                return "2";
            }
            //if (!string.IsNullOrEmpty(vendor.Contact))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from VendorContact where VendorId=@VendorId and ContactId=@ContactId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId),
            //                DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendor.Contact)
            //            }).ToString()) <= 0)
            //    {
            //        return "3";
            //    }
            //}
            //if (!string.IsNullOrEmpty(vendor.AddressSName))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId),
            //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendor.AddressSName)
            //            }).ToString()) <= 0)
            //    {
            //        return "4";
            //    }
            //}
            //if (!string.IsNullOrEmpty(vendor.Address))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId),
            //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendor.Address)
            //            }).ToString()) <= 0)
            //    {
            //        return "5";
            //    }
            //}
            dataAccessMySql.ExecuteNonQuery(
                "update Vendor set VendorName=@VendorName,VendorFName=@VendorFName,ModiDate=@ModiDate,CurrencyId=@CurrencyId,TaxRate=@TaxRate,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,AddressSName=@AddressSName,Address=@Address,Remark=@Remark where VendorId=@VendorId",
                tran, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("VendorName", DbType.String,vendor.VendorName),
                    DataAccessMySQL.CreateParameter("VendorFName", DbType.String, vendor.VendorFName),
                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, vendor.CurrencyId??""),
                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String, vendor.TaxRate),
                    DataAccessMySQL.CreateParameter("Contact", DbType.String, vendor.Contact),
                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, vendor.TelNo),
                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, vendor.FaxNo),
                    DataAccessMySQL.CreateParameter("Email", DbType.String, vendor.Email),
                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, vendor.AddressSName),
                    DataAccessMySQL.CreateParameter("Address", DbType.String, vendor.Address),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, vendor.Remark),
                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)
                });

            dataAccessMySql.ExecuteNonQuery("Delete from VendorAddress where VendorId=@VendorId", tran,new DbParameter[]
                {
                     DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)});
                 
            if (txtAddress1 != "" && AddressId != null)
            {
                string[] strAddressId = AddressId.Split(',');
                string[] strtxtAddress1 = txtAddress1.Split(',');
                string[] strtxtAddress2 = txtAddress2.Split(',');
                string[] strtxtAddressContact = txtAddressContact.Split(',');
                string[] strtxtAddressTelNo = txtAddressTelNo.Split(',');
                string[] strtxtAddressFaxNo = txtAddressFaxNo.Split(',');
                string[] strtxtAddressRemark = txtAddressRemark.Split(',');

                for (int i = 0; i < strAddressId.Length; i++)
                {
                    if (string.IsNullOrEmpty(strAddressId[i]))
                    {
                        continue;
                    }
                    dataAccessMySql.ExecuteNonQuery("insert into VendorAddress(VendorId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo,Remark) values(@VendorId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Remark)", tran,
                    new DbParameter[]
                    {
                          DataAccessMySQL.CreateParameter("VendorId", DbType.String,vendor.VendorId),
                          DataAccessMySQL.CreateParameter("AddressId", DbType.String,strAddressId[i].ToString()),
                          DataAccessMySQL.CreateParameter("AddressSName", DbType.String, strtxtAddress1[i].ToString()),
                          DataAccessMySQL.CreateParameter("Address", DbType.String, strtxtAddress2[i].ToString()),
                          DataAccessMySQL.CreateParameter("Contact", DbType.String, strtxtAddressContact[i].ToString()),
                          DataAccessMySQL.CreateParameter("TelNo", DbType.String,strtxtAddressTelNo[i].ToString()),
                          DataAccessMySQL.CreateParameter("FaxNo", DbType.String,strtxtAddressFaxNo[i].ToString()),
                          DataAccessMySQL.CreateParameter("Remark", DbType.String,strtxtAddressRemark[i].ToString())
                    });
                }
            }
            tran.Commit();
            return "0";
        }

        bool IVendor.DeleteVendor(BusinessLayer.Models.Vendor vendor)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = dataAccessMySql.CreateDbTransaction();
            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from Vendor where VendorId=@VendorId",tran,
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from Vendor where  VendorId=@VendorId", tran,
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)
                });

            dataAccessMySql.ExecuteNonQuery("Delete from VendorAddress where VendorId=@VendorId", tran, new DbParameter[]
             {
                     DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)});

            dataAccessMySql.ExecuteNonQuery("delete from VendorContact where  VendorId=@VendorId",tran,
            new DbParameter[]
            {
                      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)
            });

            tran.Commit();
            return true;
        }

        string IVendor.InsertVendor(BusinessLayer.Models.Vendor vendor,string AddressId, string txtAddress1, string txtAddress2, string txtAddressContact, string txtAddressTelNo, string txtAddressFaxNo, string txtAddressRemark)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = dataAccessMySql.CreateDbTransaction();

            if (int.Parse(
                     dataAccessMySql.ExecuteScalar("select count(*) from vendor where VendorId=@VendorId", tran,
                         new DbParameter[]
                         {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId)
                         }).ToString()) > 0)
                {
                    return "1";
                }
                if (int.Parse(
                  dataAccessMySql.ExecuteScalar("select count(*) from vendor where VendorName=@VendorName", tran,
                      new DbParameter[]
                        {
                        DataAccessMySQL.CreateParameter("VendorName", DbType.String, vendor.VendorName)
                        }).ToString()) > 0)
                {
                    return "2";
                }
                //if (!string.IsNullOrEmpty(vendor.Contact))
                //{
                //    if (int.Parse(
                //        dataAccessMySql.ExecuteScalar(
                //            "select count(*) from VendorContact where VendorId=@VendorId and ContactId=@ContactId",
                //            new DbParameter[]
                //            {
                //                DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId),
                //                DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendor.Contact)
                //            }).ToString()) <= 0)
                //    {
                //        return "3";
                //    }
                //}
                //if (!string.IsNullOrEmpty(vendor.AddressSName))
                //{
                //    if (int.Parse(
                //        dataAccessMySql.ExecuteScalar(
                //            "select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
                //            new DbParameter[]
                //            {
                //                DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId),
                //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendor.AddressSName)
                //            }).ToString()) <= 0)
                //    {
                //        return "4";
                //    }
                //}
                //if (!string.IsNullOrEmpty(vendor.Address))
                //{
                //    if (int.Parse(
                //        dataAccessMySql.ExecuteScalar(
                //            "select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
                //            new DbParameter[]
                //            {
                //                DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId),
                //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendor.Address)
                //            }).ToString()) <= 0)
                //    {
                //        return "5";
                //    }
                //}
                dataAccessMySql.ExecuteNonQuery(
                    "insert into vendor(CreateDate,VendorId,VendorName,VendorFName,CurrencyId,TaxRate,Contact,TelNo,FaxNo,Email,AddressSName,Address,Remark) values(@CreateDate,@VendorId,@VendorName,@VendorFName,@CurrencyId,@TaxRate,@Contact,@TelNo,@FaxNo,@Email,@AddressSName,@Address,@Remark)", tran,
                    new DbParameter[]
                    {
                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendor.VendorId),
                    DataAccessMySQL.CreateParameter("VendorName", DbType.String, vendor.VendorName),
                    DataAccessMySQL.CreateParameter("VendorFName", DbType.String, vendor.VendorFName),
                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, vendor.CurrencyId??""),
                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String, vendor.TaxRate),
                    DataAccessMySQL.CreateParameter("Contact", DbType.String, vendor.Contact),
                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, vendor.TelNo),
                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, vendor.FaxNo),
                    DataAccessMySQL.CreateParameter("Email", DbType.String, vendor.Email),
                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, vendor.AddressSName),
                    DataAccessMySQL.CreateParameter("Address", DbType.String, vendor.Address),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, vendor.Remark)
                    });

                //dataAccessMySql.ExecuteNonQuery("insert into VendorAddress(VendorId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo,Remark) values(@VendorId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Remark)",
                //new DbParameter[]
                //{
                //      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                //      DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId),
                //      DataAccessMySQL.CreateParameter("AddressSName", DbType.String, vendorAddress.AddressSName),
                //      DataAccessMySQL.CreateParameter("Address", DbType.String, vendorAddress.Address),
                //      DataAccessMySQL.CreateParameter("Contact", DbType.String, vendorAddress.Contact),
                //      DataAccessMySQL.CreateParameter("TelNo", DbType.String, vendorAddress.TelNo),
                //      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, vendorAddress.FaxNo),
                //      DataAccessMySQL.CreateParameter("Remark", DbType.String, vendorAddress.Remark)
                //});

                if (txtAddress1 != "" && AddressId != null)
                {
                    string[] strAddressId = AddressId.Split(',');
                    string[] strtxtAddress1 = txtAddress1.Split(',');
                    string[] strtxtAddress2 = txtAddress2.Split(',');
                    string[] strtxtAddressContact = txtAddressContact.Split(',');
                    string[] strtxtAddressTelNo = txtAddressTelNo.Split(',');
                    string[] strtxtAddressFaxNo = txtAddressFaxNo.Split(',');
                    string[] strtxtAddressRemark = txtAddressRemark.Split(',');

                    for (int i = 0; i < strAddressId.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strAddressId[i]))
                        {
                            continue;
                        }
                        dataAccessMySql.ExecuteNonQuery("insert into VendorAddress(VendorId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo,Remark) values(@VendorId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Remark)", tran,
                        new DbParameter[]
                        {
                          DataAccessMySQL.CreateParameter("VendorId", DbType.String,vendor.VendorId),
                          DataAccessMySQL.CreateParameter("AddressId", DbType.String,strAddressId[i].ToString()),
                          DataAccessMySQL.CreateParameter("AddressSName", DbType.String, strtxtAddress1[i].ToString()),
                          DataAccessMySQL.CreateParameter("Address", DbType.String, strtxtAddress2[i].ToString()),
                          DataAccessMySQL.CreateParameter("Contact", DbType.String, strtxtAddressContact[i].ToString()),
                          DataAccessMySQL.CreateParameter("TelNo", DbType.String,strtxtAddressTelNo[i].ToString()),
                          DataAccessMySQL.CreateParameter("FaxNo", DbType.String,strtxtAddressFaxNo[i].ToString()),
                          DataAccessMySQL.CreateParameter("Remark", DbType.String,strtxtAddressRemark[i].ToString())
                        });
                    }
                }
            tran.Commit();
            return "0";
 
        }

        DataTable IVendor.SearchDetailVendor(string col, string condition, string value)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string sqlStr = @"select Vendor.* from Vendor where 1=1  ";
            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";
                if (conditionArray[i] == "like%")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }
                dblen++;
            }
            if (strCondition != "")
            {
                return dataAccessMySql.ExecuteDataTable(sqlStr + strCondition, dbParameters);
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
        
        string IVendor.GetVendorName(string VendorId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            object VendorName = dbMySQL.ExecuteScalar("select VendorName from Vendor where VendorId=@VendorId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("VendorId",DbType.String,VendorId)
                });
            if (VendorName != null)
            {
                return "YES-" + VendorName.ToString();
            }
            else
            {
                return "NO-廠商不存在!";
            }

        }

        bool IVendor.ImportFile(string path, string type, out string msg)
        {
            msg = "";
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dataAccessMySql.CreateDbTransaction();
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    //StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("Big5"));
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    bool isFirstRow = true;
                    while (sr.Peek() > -1)
                    {
                        string[] valueArray = sr.ReadLine().Split(',');
                        if (isFirstRow)
                        {
                            if (type == "B")
                            {
                                if (valueArray.Length < 11)
                                {
                                    msg = "匯入檔欄位數量不正確";
                                    return false;
                                }
                            }
                            if (type == "A")
                            {
                                if (valueArray.Length < 6)
                                {
                                    msg = "匯入檔欄位數量不正確";
                                    return false;
                                }
                            }
                            if (type == "C")
                            {
                                if (valueArray.Length < 8)
                                {
                                    msg = "匯入檔欄位數量不正確";
                                    return false;
                                }
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (type == "A")
                        {
                            if (int.Parse(
                                dataAccessMySql.ExecuteScalar(
                                    "select count(*) from Vendor where VendorId=@VendorId",
                                    tran,
                                    new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim())
                                    }).ToString()) <= 0)
                            {
                                msg = "廠商代號：" + valueArray[0] + "不存在";
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                                return false;
                            }
                            if (int.Parse(
                                dataAccessMySql.ExecuteScalar(
                                    "select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
                                    tran,
                                    new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim()),
                                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, valueArray[1].Trim())
                                    }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery("update VendorAddress set AddressSName=@AddressSName,Address=@Address,TelNo=@TelNo,FaxNo=@FaxNo,ModiDate=@ModiDate where VendorId=@VendorId and AddressId=@AddressId",
                                tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressId", DbType.String, valueArray[1].Trim())
                                });
                            }
                            else
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                    "insert into VendorAddress(CreateDate,VendorId,AddressId,AddressSName,Address,TelNo,FaxNo) values(@CreateDate,@VendorId,@AddressId,@AddressSName,@Address,@TelNo,@FaxNo)",
                                   tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressId", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[5].Trim())
                                });
                            }
                        }
                        if (type == "B")
                        {
                            if (int.Parse(
                               dataAccessMySql.ExecuteScalar(
                                   "select count(*) from Vendor where VendorId=@VendorId",
                                   tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0])
                                    }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery("update Vendor set VendorName=@VendorName,VendorFName=@VendorFName,CurrencyId=@CurrencyId,TaxRate=@TaxRate,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,AddressSName=@AddressSName,Address=@Address,ModiDate=@ModiDate where VendorId=@VendorId",
                                tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("VendorName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("VendorFName", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String,valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String,valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[7].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[8].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[9].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[10].Trim()),
                                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim())
                                });
                            }
                            else
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                    "insert into Vendor(CreateDate,VendorId,VendorName,VendorFName,CurrencyId,TaxRate,Contact,TelNo,FaxNo,Email,AddressSName,Address) values(@CreateDate,@VendorId,@VendorName,@VendorFName,@CurrencyId,@TaxRate,@Contact,@TelNo,@FaxNo,@Email,@AddressSName,@Address)",
                                   tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("VendorName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("VendorFName", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String,valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String,valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[7].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[8].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[9].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[10].Trim())
                                });
                            }
                        }
                        if (type == "C")
                        {
                            if (int.Parse(
                                dataAccessMySql.ExecuteScalar(
                                    "select count(*) from Vendor where VendorId=@VendorId",
                                    tran,
                                    new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim())
                                    }).ToString()) <= 0)
                            {
                                msg = "廠商代號：" + valueArray[0] + "不存在";
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                                return false;
                            }
                            if (int.Parse(
                                dataAccessMySql.ExecuteScalar(
                                    "select count(*) from VendorContact where VendorId=@VendorId and ContactId=@ContactId",
                                    tran,
                                    new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim()),
                                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, valueArray[1].Trim())
                                    }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery("update VendorContact set Contact=@Contact,Department=@Department,Occupation=@Occupation,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,ModiDate=@ModiDate where VendorId=@VendorId and ContactId=@ContactId",
                                tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Department", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("Occupation", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[7].Trim()),
                                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("ContactId", DbType.String, valueArray[1].Trim())
                                });
                            }
                            else
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                    "insert into VendorContact(CreateDate,VendorId,ContactId,Contact,Department,Occupation,TelNo,FaxNo,Email) values(@CreateDate,@VendorId,@ContactId,@Contact,@Department,@Occupation,@TelNo,@FaxNo,@Email)",
                                   tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("ContactId", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Department", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("Occupation", DbType.String, valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[7].Trim())
                                });
                            }
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

        DataTable IVendor.GetAddressInfo(BusinessLayer.Models.VendorAddress vendorAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from VendorAddress where 1=1 ";
            if (vendorAddress != null)
            {
                if (!string.IsNullOrEmpty(vendorAddress.VendorId))
                {
                    sqlStr += " and VendorId=@VendorId ";
                }
                if (!string.IsNullOrEmpty(vendorAddress.AddressId))
                {
                    sqlStr += " and AddressId=@AddressId ";
                }
                if (!string.IsNullOrEmpty(vendorAddress.AddressSName))
                {
                    sqlStr += " and AddressSName like @AddressSName ";
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                    DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId),
                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, "%" + vendorAddress.AddressSName + "%")
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        DataTable IVendor.GetContactInfo(BusinessLayer.Models.VendorContact vendorContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from VendorContact where 1=1 ";
            if (vendorContact != null)
            {
                if (!string.IsNullOrEmpty(vendorContact.VendorId))
                {
                    sqlStr += " and VendorId=@VendorId ";
                }
                if (!string.IsNullOrEmpty(vendorContact.ContactId))
                {
                    sqlStr += " and ContactId=@ContactId ";
                }
                if (!string.IsNullOrEmpty(vendorContact.Contact))
                {
                    sqlStr += " and Contact like @Contact ";
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorContact.VendorId),
                    DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendorContact.ContactId),
                    DataAccessMySQL.CreateParameter("Contact", DbType.String, "%" + vendorContact.Contact + "%")
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        bool IVendor.SaveNewContact(BusinessLayer.Models.VendorContact vendorContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from VendorContact where VendorId=@VendorId and ContactId=@ContactId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorContact.VendorId),
                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendorContact.ContactId)
                    }).ToString()) <= 0)
            {
                dataAccessMySql.ExecuteNonQuery("insert into VendorContact(VendorId,ContactId,Contact,Department,Occupation,TelNo,FaxNo,Email,Remark) values(@VendorId,@ContactId,@Contact,@Department,@Occupation,@TelNo,@FaxNo,@Email,@Remark)",
                new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorContact.VendorId),
                      DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendorContact.ContactId),
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, vendorContact.Contact),
                      DataAccessMySQL.CreateParameter("Department", DbType.String, vendorContact.Department),
                      DataAccessMySQL.CreateParameter("Occupation", DbType.String, vendorContact.Occupation),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, vendorContact.TelNo),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, vendorContact.FaxNo),
                      DataAccessMySQL.CreateParameter("Email", DbType.String, vendorContact.Email),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, vendorContact.Remark)
                });
            }
            else
            {
                return false;
            }

            return true;
        }

        bool IVendor.DeleteContact(BusinessLayer.Models.VendorContact vendorContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from VendorContact where VendorId=@VendorId and ContactId=@ContactId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorContact.VendorId),
                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendorContact.ContactId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from VendorContact where  VendorId=@VendorId and ContactId=@ContactId",
                new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorContact.VendorId),
                      DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendorContact.ContactId)
                });
            return true;
        }

        bool IVendor.SaveContact(BusinessLayer.Models.VendorContact vendorContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from VendorContact where VendorId=@VendorId and ContactId=@ContactId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorContact.VendorId),
                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendorContact.ContactId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            else
            {
                dataAccessMySql.ExecuteNonQuery("update VendorContact set Contact=@Contact,Department=@Department,Occupation=@Occupation,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,Remark=@Remark where  VendorId=@VendorId and ContactId=@ContactId",
               new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, vendorContact.Contact),
                      DataAccessMySQL.CreateParameter("Department", DbType.String, vendorContact.Department),
                      DataAccessMySQL.CreateParameter("Occupation", DbType.String, vendorContact.Occupation),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, vendorContact.TelNo),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, vendorContact.FaxNo),
                      DataAccessMySQL.CreateParameter("Email", DbType.String, vendorContact.Email),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, vendorContact.Remark),
                      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorContact.VendorId),
                      DataAccessMySQL.CreateParameter("ContactId", DbType.String, vendorContact.ContactId)
                });
            }

            return true;
        }

        bool IVendor.DeleteAddress(BusinessLayer.Models.VendorAddress vendorAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from VendorAddress where  VendorId=@VendorId and AddressId=@AddressId",
                new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                      DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId)
                });
            return true;
        }

        bool IVendor.SaveAddress(BusinessLayer.Models.VendorAddress vendorAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            else
            {
                dataAccessMySql.ExecuteNonQuery("update VendorAddress set AddressSName=@AddressSName,Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,Remark=@Remark where  VendorId=@VendorId and AddressId=@AddressId",
               new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("AddressSName", DbType.String, vendorAddress.AddressSName),
                      DataAccessMySQL.CreateParameter("Address", DbType.String, vendorAddress.Address),
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, vendorAddress.Contact),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, vendorAddress.TelNo),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, vendorAddress.FaxNo),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, vendorAddress.Remark),
                      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                      DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId)
                });
            }

            return true;
        }

        bool IVendor.SaveNewAddress(BusinessLayer.Models.VendorAddress vendorAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from VendorAddress where VendorId=@VendorId and AddressId=@AddressId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId)
                    }).ToString()) <= 0)
            {
                //dataAccessMySql.ExecuteNonQuery("insert into VendorAddress(VendorId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo,Remark) values(@VendorId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Remark)",
                //new DbParameter[]
                //{
                //      DataAccessMySQL.CreateParameter("VendorId", DbType.String, vendorAddress.VendorId),
                //      DataAccessMySQL.CreateParameter("AddressId", DbType.String, vendorAddress.AddressId),
                //      DataAccessMySQL.CreateParameter("AddressSName", DbType.String, vendorAddress.AddressSName),
                //      DataAccessMySQL.CreateParameter("Address", DbType.String, vendorAddress.Address),
                //      DataAccessMySQL.CreateParameter("Contact", DbType.String, vendorAddress.Contact),
                //      DataAccessMySQL.CreateParameter("TelNo", DbType.String, vendorAddress.TelNo),
                //      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, vendorAddress.FaxNo),
                //      DataAccessMySQL.CreateParameter("Remark", DbType.String, vendorAddress.Remark)
                //});
            }
            else
            {
                return false;
            }

            return true;
        }

        public DataTable SelectVendorList(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select vendor.* from vendor where 1=1 ";

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            string strCondition = "";
            DbParameter[] dbParameters = new DbParameter[colArray.Length + 1];
            int dblen = 1;
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
                dblen++;
            }

            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);



            DataTable dtCustomer = null;
            if (strCondition != "")
            {

                dtCustomer = dbMySQL.ExecuteDataTable(sqlStr + strCondition, dbParameters);
            }
            else
            {
                dtCustomer = dbMySQL.ExecuteDataTable(sqlStr, dbParameters);
            }

            return dtCustomer;
        }
    }
}