using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class Contract_HDAL : IContract_HDAL
    {
        public List<Contract_H> GetContract_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Contract_H> objContract_H = new List<Contract_H>();

            string sql = @"SELECT h.ContractType,o.OrderSName,h.ContractNo,h.CustomerId,c.CustomerName,
                c.CustomerType,ConfirmedDate,h.ContractSDate,h.ContractEDate,h.Confirmed FROM contract_h as h
                left join OrderCategory as o on o.OrderType=h.ContractType
                left join Customer as c on c.CustomerId=h.CustomerId where 1=1";

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

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

            DataTable dtContract_H = null;
            if (strCondition != "")
            {
                dtContract_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtContract_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtContract_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtContract_H.Rows)
                {
                    Contract_H obj = new Contract_H();
                    obj.ContractType = dr["ContractType"].ToString();       //合約單別
                    obj.OrderSName = dr["OrderSName"].ToString();           //合約單別名稱
                    obj.ContractNo = dr["ContractNo"].ToString();           //合約單號
                    obj.CustomerId = dr["CustomerId"].ToString();           //客戶代號
                    obj.CustomerName = dr["CustomerName"].ToString();       //客戶簡稱
                    obj.CustomerType = dr["CustomerType"].ToString();       //客戶型態
                    obj.Confirmed = dr["Confirmed"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //合約日期
                    obj.ContractSDate = dr["ContractSDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ContractSDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //合約起日
                    obj.ContractEDate = dr["ContractEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ContractEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");      //合約迄日
                    objContract_H.Add(obj);
                }
            }

            return objContract_H;

        }

        public List<Contract_H> GetContractH(Contract_H contract_H, string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Contract_H> objContract_H = new List<Contract_H>();

            string sql = @"SELECT h.ContractType,o.OrderSName,h.ContractNo,h.Version,h.CustomerId,c.CustomerName,
                c.CustomerType,ConfirmedDate,h.ContractSDate,h.ContractEDate
				FROM contract_h as h
                left join OrderCategory as o on o.OrderType=h.ContractType
                left join Customer as c on c.CustomerId=h.CustomerId where
                DATEDIFF(NOW(),CAST(ContractEDate as datetime)) <= 0 and h.Confirmed='Y'";

            //if(!string.IsNullOrEmpty(contract_H.ContractType))
            //{
            //    sql += " and h.ContractType=@ContractType";
            //}

            //if (!string.IsNullOrEmpty(contract_H.ContractNo))
            //{
            //    sql += " and h.ContractNo=@ContractNo";
            //}

            //if (!string.IsNullOrEmpty(contract_H.CustomerId))
            //{
            //    sql += " and h.CustomerId=@CustomerId";
            //}

            //if (Page != 0)
            //{
            //    sql += " limit @Page1,@Page2";
            //}

            //DataTable dtContract_H = dbMySQL.ExecuteDataTable(sql,new DbParameter[]{
            //    DataAccessMySQL.CreateParameter("ContractType",DbType.String,"%"+contract_H.ContractType+"%"),
            //    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,"%"+contract_H.ContractNo+"%"),
            //    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,contract_H.CustomerId),
            //    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
            //    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
            //});  

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            string strCondition = "";

            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len + 1];
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


            DataTable dtContract_H = null;
            if (strCondition != "")
            {

                dtContract_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtContract_H = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }



            if (dtContract_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtContract_H.Rows)
                {
                    Contract_H obj = new Contract_H();
                    obj.ContractType = dr["ContractType"].ToString();       //合約單別
                    obj.OrderSName = dr["OrderSName"].ToString();           //合約單別名稱
                    obj.ContractNo = dr["ContractNo"].ToString();           //合約單號
                    obj.Version = dr["Version"].ToString();                 //版次
                    obj.CustomerId = dr["CustomerId"].ToString();           //客戶代號
                    obj.CustomerName = dr["CustomerName"].ToString();       //客戶簡稱
                    obj.CustomerType = dr["CustomerType"].ToString();       //客戶型態
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //合約日期
                    obj.ContractSDate = dr["ContractSDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ContractSDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //合約起日
                    obj.ContractEDate = dr["ContractEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ContractEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");      //合約迄日
                    objContract_H.Add(obj);
                }
            }

            return objContract_H;

        }

        public bool AddContract_H(Contract_H contract_H, string strProductNo, string strProductName,
            string strQTY, string strUnit, string strWarrantyPeriod, string strWarrantySDate,
            string strWarrantyEDate, string strRoutineServiceFreq, string strRemark, string strProductSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from contract_h where ContractType=@ContractType and ContractNo=@ContractNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into contract_h (Company,UserGroup,Creator,CreateDate,
                        ContractType,ContractNo,OrderDate,ConfirmedDate,Version,RoutineServiceFreq,
                        D2DServiceFreq,Discount,ContractPrice,WarrantyPeriod,ContractSDate,ContractEDate,
                        Renewal,Confirmed,ConfirmedMan,Remark,CustomerId,AddressSName,Address,Contact,Tel,Fax,Email) values (
                        @Company,@UserGroup,@Creator,@CreateDate,
                        @ContractType,@ContractNo,@OrderDate,@ConfirmedDate,@Version,@RoutineServiceFreq,
                        @D2DServiceFreq,@Discount,@ContractPrice,@WarrantyPeriod,@ContractSDate,@ContractEDate,
                        @Renewal,@Confirmed,@ConfirmedMan,@Remark,@CustomerId,@AddressSName,@Address,@Contact,@Tel,@Fax,@Email)", tran,
                        new DbParameter[]{
                             DataAccessMySQL.CreateParameter("Company",DbType.String,contract_H.Company.Trim()),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contract_H.UserGroup.Trim()),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,contract_H.Creator.Trim()),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType.Trim()),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo.Trim()),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,contract_H.OrderDate.Trim()),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,contract_H.ConfirmedDate.Trim()),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,contract_H.Version.Trim()),
                            DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.Double,contract_H.RoutineServiceFreq),
                            DataAccessMySQL.CreateParameter("D2DServiceFreq",DbType.Double,contract_H.D2DServiceFreq),
                            DataAccessMySQL.CreateParameter("Discount",DbType.Double,contract_H.Discount),
                            DataAccessMySQL.CreateParameter("ContractPrice",DbType.Double,contract_H.ContractPrice),
                            DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.Double,contract_H.WarrantyPeriod),
                            DataAccessMySQL.CreateParameter("ContractSDate",DbType.String,contract_H.ContractSDate.Trim()),
                            DataAccessMySQL.CreateParameter("ContractEDate",DbType.String,contract_H.ContractEDate.Trim()),
                            DataAccessMySQL.CreateParameter("Renewal",DbType.String,contract_H.Renewal.Trim()),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,contract_H.Confirmed.Trim()),
                            DataAccessMySQL.CreateParameter("ConfirmedMan",DbType.String,contract_H.ConfirmedMan.Trim()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,contract_H.Remark.Trim()),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,contract_H.CustomerId.Trim()),
                            DataAccessMySQL.CreateParameter("AddressSName",DbType.String,contract_H.AddressSName.Trim()),
                            DataAccessMySQL.CreateParameter("Address",DbType.String,contract_H.Address.Trim()),
                            DataAccessMySQL.CreateParameter("Contact",DbType.String,contract_H.Contact.Trim()),
                            DataAccessMySQL.CreateParameter("Tel",DbType.String,contract_H.Tel.Trim()),
                            DataAccessMySQL.CreateParameter("Fax",DbType.String,contract_H.Fax.Trim()),
                            DataAccessMySQL.CreateParameter("Email",DbType.String,contract_H.Email.Trim())
                        });

                    string sql = "";

                    if (strProductNo != "")
                    {
                        string[] strProductNoArray = strProductNo.Split(',');
                        string[] strProductNameArray = strProductName.Split(',');
                        string[] strQTYArray = strQTY.Split(',');
                        string[] strUnitArray = strUnit.Split(',');
                        string[] strWarrantyPeriodArray = strWarrantyPeriod.Split(',');
                        string[] strWarrantySDateArray = strWarrantySDate.Split(',');
                        string[] strWarrantyEDateArray = strWarrantyEDate.Split(',');
                        string[] strRoutineServiceFreqArray = strRoutineServiceFreq.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');
                        string[] strProductSerialArray = strProductSerial.Split(',');

                        sql = @"insert into contract_productd (Company,UserGroup,Creator,
                        CreateDate,ContractType,ContractNo,ItemId,ProductNo,ProductName,QTY,
                        Unit,WarrantyPeriod,WarrantySDate,WarrantyEDate,RoutineServiceFreq,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@ContractType,@ContractNo,@ItemId,@ProductNo,@ProductName,@QTY,
                        @Unit,@WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@RoutineServiceFreq,@Remark)";

                        string sqlSerial = @"insert into Contract_ProductSerial (Company,UserGroup,Creator,
                        CreateDate,ContractType,ContractNo,ItemId,ProductNo,SerialNo,IsClosed) values 
                        (@Company,@UserGroup,@Creator,
                        @CreateDate,@ContractType,@ContractNo,@ItemId,@ProductNo,@SerialNo,@IsClosed)";
                        string itemid = "";
                        for (int i = 0; i < strProductNoArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strProductNoArray[i]))
                            {
                                continue;
                            }
                            itemid = (i + 1).ToString("0000");
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,contract_H.Company.Trim()),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contract_H.UserGroup.Trim()),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,contract_H.Creator.Trim()),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType.Trim()),
                                DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo.Trim()),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,itemid.Trim()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,strWarrantyPeriodArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,strWarrantySDateArray[i].ToString()==""?"":strWarrantySDateArray[i].ToString().Replace("/","").Trim()),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,strWarrantyEDateArray[i].ToString()==""?"":strWarrantyEDateArray[i].ToString().Replace("/","").Trim()),
                                DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.String,strRoutineServiceFreqArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString().Trim())
                            });

                            string[] strSerialArray = strProductSerialArray[i].ToString().Split('#');
                            for (int y = 0; y < strSerialArray.Length; y++)
                            {
                                if (string.IsNullOrEmpty(strSerialArray[y]))
                                {
                                    continue;
                                }


                                if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from contract_productserial 
                                where ContractType=@ContractType and ContractNo=@ContractNo and ItemId=@ItemId
                                and SerialNo=@SerialNo", tran,
                                    new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType),
                                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,itemid),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialArray[y].ToString())
                                }).ToString())==0)
                                {
                                    dbMySQL.ExecuteNonQuery(sqlSerial, tran, new DbParameter[]{
                                        DataAccessMySQL.CreateParameter("Company",DbType.String,contract_H.Company.Trim()),
                                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contract_H.UserGroup.Trim()),
                                        DataAccessMySQL.CreateParameter("Creator",DbType.String,contract_H.Creator.Trim()),
                                        DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType.Trim()),
                                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo.Trim()),
                                        DataAccessMySQL.CreateParameter("ItemId",DbType.String,itemid.Trim()),
                                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString().Trim()),
                                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialArray[y].ToString().Trim()),
                                        DataAccessMySQL.CreateParameter("IsClosed",DbType.String,"N")
                                    });
                                }
                            }
                        }
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

        public Contract_H Contract_HInfo(string ContractType, string ContractNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Contract_H contract_H = new Contract_H();

            DataTable dtContract_H = dbMySQL.ExecuteDataTable(@"select h.*,o.OrderSName,
                e.EmployeeName as ConfirmedManName,c.CustomerName as CustomerName,
                cu.Contact as ContactName from contract_h as h
                left join ordercategory as o on o.OrderType=h.ContractType
                left join employee as e on e.EmployeeId=h.ConfirmedMan
                left join customer as c on c.CustomerId=h.CustomerId 
                left join CustomerContact as cu on cu.CustomerId=h.CustomerId and cu.ContactId=h.Contact 
                where h.ContractType=@ContractType and h.ContractNo=@ContractNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo)
                });

            if (dtContract_H != null && dtContract_H.Rows.Count > 0)
            {
                contract_H.ContractType = dtContract_H.Rows[0]["ContractType"].ToString();       //合約單別
                contract_H.OrderSName = dtContract_H.Rows[0]["OrderSName"].ToString();           //合約單別名稱
                contract_H.ContractNo = dtContract_H.Rows[0]["ContractNo"].ToString();           //合約單號
                contract_H.OrderDate = dtContract_H.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtContract_H.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");           //單據日期
                contract_H.ConfirmedDate = dtContract_H.Rows[0]["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dtContract_H.Rows[0]["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");           //合約日期
                contract_H.Version = dtContract_H.Rows[0]["Version"].ToString();           //版次
                contract_H.RoutineServiceFreq = double.Parse(dtContract_H.Rows[0]["RoutineServiceFreq"].ToString() == "" ? "0" : dtContract_H.Rows[0]["RoutineServiceFreq"].ToString());           //定保次數
                contract_H.D2DServiceFreq = double.Parse(dtContract_H.Rows[0]["D2DServiceFreq"].ToString() == "" ? "0" : dtContract_H.Rows[0]["D2DServiceFreq"].ToString());           //外出服務
                contract_H.Discount = double.Parse(dtContract_H.Rows[0]["Discount"].ToString() == "" ? "0" : dtContract_H.Rows[0]["Discount"].ToString());           //零件折扣
                contract_H.ContractPrice = double.Parse(dtContract_H.Rows[0]["ContractPrice"].ToString() == "" ? "0" : dtContract_H.Rows[0]["ContractPrice"].ToString());           //合約費用
                contract_H.WarrantyPeriod = double.Parse(dtContract_H.Rows[0]["WarrantyPeriod"].ToString() == "" ? "0" : dtContract_H.Rows[0]["WarrantyPeriod"].ToString());           //保固期限
                contract_H.ContractSDate = dtContract_H.Rows[0]["ContractSDate"].ToString() == "" ? "" : DateTime.ParseExact(dtContract_H.Rows[0]["ContractSDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //合約起日
                contract_H.ContractEDate = dtContract_H.Rows[0]["ContractEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtContract_H.Rows[0]["ContractEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");      //合約迄日
                contract_H.Renewal = dtContract_H.Rows[0]["Renewal"].ToString();           //續約否
                contract_H.Confirmed = dtContract_H.Rows[0]["Confirmed"].ToString();           //確認碼
                contract_H.ConfirmedMan = dtContract_H.Rows[0]["ConfirmedMan"].ToString();       //確認人員
                contract_H.ConfirmedManName = dtContract_H.Rows[0]["ConfirmedManName"].ToString();       //確認人員名稱
                contract_H.Remark = dtContract_H.Rows[0]["Remark"].ToString();       //備註
                contract_H.CustomerId = dtContract_H.Rows[0]["CustomerId"].ToString();       //客戶代號
                contract_H.CustomerName = dtContract_H.Rows[0]["CustomerName"].ToString();       //客戶名稱
                contract_H.AddressSName = dtContract_H.Rows[0]["AddressSName"].ToString();       //地址簡稱
                contract_H.Address = dtContract_H.Rows[0]["Address"].ToString();       //地址
                contract_H.Contact = dtContract_H.Rows[0]["Contact"].ToString();       //聯絡人
                contract_H.ContactName = dtContract_H.Rows[0]["ContactName"].ToString();       //聯絡人名稱
                contract_H.Tel = dtContract_H.Rows[0]["Tel"].ToString();       //電話
                contract_H.Fax = dtContract_H.Rows[0]["Fax"].ToString();       //傳真
                contract_H.Email = dtContract_H.Rows[0]["Email"].ToString();       //E-mail

                return contract_H;
            }
            else
            {
                return null;
            }
        }

        public bool DelContract_H(string ContractType, string ContractNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                dbMySQL.ExecuteNonQuery("delete from contract_h where ContractType=@ContractType and ContractNo=@ContractNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo)
                    });

                dbMySQL.ExecuteNonQuery("delete from contract_productd where ContractType=@ContractType and ContractNo=@ContractNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo)
                    });

                dbMySQL.ExecuteNonQuery("delete from contract_productserial where ContractType=@ContractType and ContractNo=@ContractNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo)
                    });
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
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

        public bool UpdateContract_H(Contract_H contract_H, string strProductNo, string strProductName,
            string strQTY, string strUnit, string strWarrantyPeriod, string strWarrantySDate,
            string strWarrantyEDate, string strRoutineServiceFreq, string strRemark,string strProductSerial, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                dbMySQL.ExecuteNonQuery(@"update contract_h set 
                    Modifier=@Modifier,ModiDate=@ModiDate,OrderDate=@OrderDate,
                    Version=@Version,RoutineServiceFreq=@RoutineServiceFreq,
                    D2DServiceFreq=@D2DServiceFreq,Discount=@Discount,ContractPrice=@ContractPrice,
                    WarrantyPeriod=@WarrantyPeriod,ContractSDate=@ContractSDate,ContractEDate=@ContractEDate,
                    Renewal=@Renewal,Remark=@Remark,CustomerId=@CustomerId,
                    AddressSName=@AddressSName,Address=@Address,Contact=@Contact,Tel=@Tel,Fax=@Fax,Email=@Email where
                    ContractType=@ContractType and ContractNo=@ContractNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,contract_H.Modifier.Trim()),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType.Trim()),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo.Trim()),
                        DataAccessMySQL.CreateParameter("OrderDate",DbType.String,contract_H.OrderDate.Trim()),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,contract_H.Version.Trim()),
                        DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.Double,contract_H.RoutineServiceFreq),
                        DataAccessMySQL.CreateParameter("D2DServiceFreq",DbType.Double,contract_H.D2DServiceFreq),
                        DataAccessMySQL.CreateParameter("Discount",DbType.Double,contract_H.Discount),
                        DataAccessMySQL.CreateParameter("ContractPrice",DbType.Double,contract_H.ContractPrice),
                        DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.Double,contract_H.WarrantyPeriod),
                        DataAccessMySQL.CreateParameter("ContractSDate",DbType.String,contract_H.ContractSDate.Trim()),
                        DataAccessMySQL.CreateParameter("ContractEDate",DbType.String,contract_H.ContractEDate.Trim()),
                        DataAccessMySQL.CreateParameter("Renewal",DbType.String,contract_H.Renewal.Trim()),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,contract_H.Remark.Trim()),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,contract_H.CustomerId.Trim()),
                        DataAccessMySQL.CreateParameter("AddressSName",DbType.String,contract_H.AddressSName.Trim()),
                        DataAccessMySQL.CreateParameter("Address",DbType.String,contract_H.Address.Trim()),
                        DataAccessMySQL.CreateParameter("Contact",DbType.String,contract_H.Contact.Trim()),
                        DataAccessMySQL.CreateParameter("Tel",DbType.String,contract_H.Tel.Trim()),
                        DataAccessMySQL.CreateParameter("Fax",DbType.String,contract_H.Fax.Trim()),
                        DataAccessMySQL.CreateParameter("Email",DbType.String,contract_H.Email.Trim())
                    });

                dbMySQL.ExecuteNonQuery("delete from contract_productd where ContractType=@ContractType and ContractNo=@ContractNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo)
                    });

                dbMySQL.ExecuteNonQuery("delete from contract_productserial where ContractType=@ContractType and ContractNo=@ContractNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo)
                    });

                string sql = "";

                if (strProductNo != "")
                {
                    string[] strProductNoArray = strProductNo.Split(',');
                    string[] strProductNameArray = strProductName.Split(',');
                    string[] strQTYArray = strQTY.Split(',');
                    string[] strUnitArray = strUnit.Split(',');
                    string[] strWarrantyPeriodArray = strWarrantyPeriod.Split(',');
                    string[] strWarrantySDateArray = strWarrantySDate.Split(',');
                    string[] strWarrantyEDateArray = strWarrantyEDate.Split(',');
                    string[] strRoutineServiceFreqArray = strRoutineServiceFreq.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');
                    string[] strProductSerialArray = strProductSerial.Split(',');

                    sql = @"insert into contract_productd (ContractType,ContractNo,ItemId,ProductNo,ProductName,QTY,
                        Unit,WarrantyPeriod,WarrantySDate,WarrantyEDate,RoutineServiceFreq,Remark) values (@ContractType,@ContractNo,@ItemId,@ProductNo,@ProductName,@QTY,
                        @Unit,@WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@RoutineServiceFreq,@Remark)";

                    string sqlSerial = @"insert into Contract_ProductSerial (ContractType,ContractNo,ItemId,ProductNo,SerialNo,IsClosed) values 
                        (@ContractType,@ContractNo,@ItemId,@ProductNo,@SerialNo,@IsClosed)";
                    string itemid = "";
                    for (int i = 0; i < strProductNoArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strProductNoArray[i]))
                        {
                            continue;
                        }
                        itemid = (i + 1).ToString("0000");
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                //DataAccessMySQL.CreateParameter("Company",DbType.String,contract_H.Company.Trim()),
                                //DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contract_H.UserGroup.Trim()),
                                //DataAccessMySQL.CreateParameter("Creator",DbType.String,contract_H.Creator.Trim()),
                                //DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType.Trim()),
                                DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo.Trim()),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,itemid.Trim()),
                                DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("ProductName",DbType.String,strProductNameArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Unit",DbType.String,strUnitArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,strWarrantyPeriodArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,strWarrantySDateArray[i].ToString()==""?"":strWarrantySDateArray[i].ToString().Replace("/","").Trim()),
                                DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,strWarrantyEDateArray[i].ToString()==""?"":strWarrantyEDateArray[i].ToString().Replace("/","").Trim()),
                                DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.String,strRoutineServiceFreqArray[i].ToString().Trim()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString().Trim())
                            });

                        string[] strSerialArray = strProductSerialArray[i].ToString().Split('#');
                        for (int y = 0; y < strSerialArray.Length; y++)
                        {
                            if (string.IsNullOrEmpty(strSerialArray[y]))
                            {
                                continue;
                            }


                            if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from contract_productserial 
                                where ContractType=@ContractType and ContractNo=@ContractNo and ItemId=@ItemId
                                and SerialNo=@SerialNo", tran,
                                new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType),
                                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,itemid),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialArray[y].ToString())
                            }).ToString()) == 0)
                            {
                                dbMySQL.ExecuteNonQuery(sqlSerial, tran, new DbParameter[]{
                                        //DataAccessMySQL.CreateParameter("Company",DbType.String,contract_H.Company.Trim()),
                                        //DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contract_H.UserGroup.Trim()),
                                        //DataAccessMySQL.CreateParameter("Creator",DbType.String,contract_H.Creator.Trim()),
                                        //DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType.Trim()),
                                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo.Trim()),
                                        DataAccessMySQL.CreateParameter("ItemId",DbType.String,itemid.Trim()),
                                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString().Trim()),
                                        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialArray[y].ToString().Trim()),
                                        DataAccessMySQL.CreateParameter("IsClosed",DbType.String,"N")
                                    });
                            }
                        }
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

        public bool TypeContract_H(Contract_H contract_H,out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            try
            {
                dbMySQL.ExecuteNonQuery(@"update contract_h set
                    Modifier=@Modifier,ModiDate=@ModiDate,Confirmed=@Confirmed,ConfirmedMan=@ConfirmedMan where
                    ContractType=@ContractType and ContractNo=@ContractNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contract_H.ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contract_H.ContractNo),
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,contract_H.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,contract_H.Confirmed),
                        DataAccessMySQL.CreateParameter("ConfirmedMan",DbType.String,contract_H.ConfirmedMan),
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

        public string GetSupportAplOrderNo(string OrderType)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select CodeMode,SerialNrCodeLength,AutoConfirm from OrderCategory where OrderType=@OrderType",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("OrderType",DbType.String,OrderType)
                });

            if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
            {
                string ManuscriptNum = "";
                if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "1")
                {
                    ManuscriptNum = DateTime.Now.ToString("yyyyMMdd");
                    object ManuscripNo = dbMySQL.ExecuteScalar("SELECT MAX(CodeNumber) FROM syscodenumbers WHERE CodeNumber like @CodeNumber and CodeType=@CodeType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, ManuscriptNum+"%"),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });
                    string OrderNo = "";
                    string codeNum = "000000000001";
                    if (ManuscripNo == null || ManuscripNo.ToString() == "")
                    {
                        OrderNo = ManuscriptNum + codeNum.Substring(codeNum.Length - int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()), int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                    }
                    else
                    {
                        if (dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString() != "0")
                        {
                            OrderNo = ManuscriptNum + (int.Parse(ManuscripNo.ToString().Substring(8)) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
                        }
                        else
                        {
                            return "None-由於當前單別的單號格式是：yyyy/MM/dd，已使用，請換單別！";
                        }

                    }
                    dbMySQL.ExecuteScalar("insert into syscodenumbers (CodeType,CodeNumber) values (@CodeType,@CodeNumber)",
                       new DbParameter[]{
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, OrderNo),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    return "YES-" + OrderNo + "-" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                }
                //else
                //{
                //    return "NO--" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                //}
                if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "2")
                {
                    ManuscriptNum = DateTime.Now.ToString("yyyyMM");
                    object ManuscripNo = dbMySQL.ExecuteScalar("SELECT MAX(CodeNumber) FROM syscodenumbers WHERE CodeNumber like @CodeNumber and CodeType=@CodeType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, ManuscriptNum+"%"),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    string OrderNo = "";
                    string codeNum = "000000000001";
                    if (ManuscripNo == null || ManuscripNo.ToString() == "")
                    {
                        OrderNo = ManuscriptNum + codeNum.Substring(codeNum.Length - int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()), int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                    }
                    else
                    {
                        if (dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString() != "0")
                        {
                            OrderNo = ManuscriptNum + (int.Parse(ManuscripNo.ToString().Substring(6)) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
                        }
                        else
                        {
                            return "None-由於當前單別的單號格式是：yyyy/MM，已使用，請換單別！";
                        }
                    }

                    dbMySQL.ExecuteScalar("insert into syscodenumbers (CodeType,CodeNumber) values (@CodeType,@CodeNumber)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, OrderNo),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    return "YES-" + OrderNo + "-" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                }
                //else
                //{
                //    return "NO--" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                //}
                if (dtOrderCategory.Rows[0]["CodeMode"].ToString() == "3")
                {
                    object ManuscripNo = dbMySQL.ExecuteScalar("SELECT MAX(CodeNumber) FROM syscodenumbers WHERE CodeNumber like @CodeNumber and CodeType=@CodeType",
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, ManuscriptNum+"%"),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    string OrderNo = "";
                    string codeNum = "000000000001";
                    if (ManuscripNo == null || ManuscripNo.ToString() == "")
                    {
                        OrderNo = codeNum.Substring(codeNum.Length - int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()), int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString()));
                    }
                    else
                    {
                        OrderNo = (int.Parse(ManuscripNo.ToString()) + 1).ToString("000000000000".Substring(0, int.Parse(dtOrderCategory.Rows[0]["SerialNrCodeLength"].ToString())));
                    }

                    dbMySQL.ExecuteScalar("insert into syscodenumbers (CodeType,CodeNumber) values (@CodeType,@CodeNumber)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("CodeNumber", DbType.String, OrderNo),
                            DataAccessMySQL.CreateParameter("CodeType", DbType.String, OrderType)
                        });

                    return "YES-" + OrderNo + "-" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();

                }
                //else
                //{
                return "NO--" + dtOrderCategory.Rows[0]["AutoConfirm"].ToString();
                //}
            }
            else
            {
                return "None-單別不存在！";
            }

        }

        public bool AddServiceArrange(ServiceArrange_H serviceArrange_H,string strchk,out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                string[] SupportIdArray = strchk.Split(',');

                string[] valueInfo = null;

                string[] orderInfo = null;
                for (int i = 0; i < SupportIdArray.Length; i++)
                {
                    valueInfo = SupportIdArray[i].Split('-');

                    orderInfo = GetSupportAplOrderNo(serviceArrange_H.SerArrangeOrderType).Split('-');

                    if (orderInfo[0].ToString() == "None")
                    {
                        msg = "單別不存在!";
                        return false;
                    }

                    if (orderInfo[0].ToString() == "NO")
                    {
                        msg = "單別編碼方式屬於手動填寫，請換單別!";
                        return false;
                    }

                    DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,serviceArrange_H.SerArrangeOrderType)
                        });
                    string AutoConfirm = "N";
                    if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
                    {
                        if (dtOrderCategory.Rows[0]["AutoConfirm"].ToString() == "Y")
                        {
                            AutoConfirm = "Y";

                            serviceArrange_H.Confirmed = "Y";
                            serviceArrange_H.Confirmor = serviceArrange_H.Creator;
                            serviceArrange_H.ConfirmedDate = DateTime.Now.ToString("yyyyMMdd");
                        }
                    }

                    if (AutoConfirm == "N")
                    {
                        serviceArrange_H.Confirmed = "N";
                        serviceArrange_H.Confirmor = "";
                        serviceArrange_H.ConfirmedDate = "";
                    }

                    DataTable dtContract = dbMySQL.ExecuteDataTable(@"select ps.ContractType,ps.ContractNo,ps.ItemId,
                        ps.ProductNo,pd.ProductName,ps.SerialNo,h.ConfirmedDate,
                        pd.RoutineServiceFreq,pd.WarrantySDate,pd.WarrantyEDate,
                        pd.WarrantyPeriod from Contract_ProductSerial as ps
                        left join Contract_ProductD as pd on pd.ContractType=ps.ContractType
                        and pd.ContractNo=ps.ContractNo and pd.ItemId=ps.ItemId
                        left join Contract_H as h on h.ContractType=ps.ContractType
                        and h.ContractNo=ps.ContractNo where ps.ContractType=@ContractType 
                        and ps.ContractNo=@ContractNo and ps.IsClosed='N'", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,valueInfo[0].ToString()),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,valueInfo[1].ToString()),
                        });
                    if (dtContract != null && dtContract.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dtContract.Rows)
                        {
                            dbMySQL.ExecuteNonQuery(@"insert into ServiceArrange_H (Company,UserGroup,Creator,CreateDate,
                        SerArrangeOrderType,SerArrangeOrderNo,OrderDate,ConfirmedDate,CustomerId,
                        Version,SourceOrderType,SourceOrderNo,SourceOrderItemId,ProductNo,ProductName,SerialNo,
                        SaleDate,RoutineServiceFreq,WarrantyPeriod,WarrantySDate,WarrantyEDate,Confirmed,Confirmor) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@SerArrangeOrderType,@SerArrangeOrderNo,
                        @OrderDate,@ConfirmedDate,@CustomerId,
                        @Version,@SourceOrderType,@SourceOrderNo,@SourceOrderItemId,@ProductNo,@ProductName,@SerialNo,
                        @SaleDate,@RoutineServiceFreq,@WarrantyPeriod,@WarrantySDate,@WarrantyEDate,@Confirmed,@Confirmor)", tran,
                            new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,serviceArrange_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,serviceArrange_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,serviceArrange_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,serviceArrange_H.SerArrangeOrderType),
                            DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,orderInfo[1].ToString()),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,serviceArrange_H.ConfirmedDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dr["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,"0000"),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dr["ContractType"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dr["ContractNo"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderItemId",DbType.String,dr["ItemId"].ToString()),
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dr["ProductNo"].ToString()),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,dr["ProductName"].ToString()),
                            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,dr["SerialNo"].ToString()),
                            DataAccessMySQL.CreateParameter("SaleDate",DbType.String,dr["ConfirmedDate"].ToString()),
                            DataAccessMySQL.CreateParameter("RoutineServiceFreq",DbType.String,dr["RoutineServiceFreq"].ToString()),
                            DataAccessMySQL.CreateParameter("WarrantyPeriod",DbType.String,dr["WarrantyPeriod"].ToString()),
                            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,dr["WarrantySDate"].ToString()),
                            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,dr["WarrantyEDate"].ToString()),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,serviceArrange_H.Confirmed),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,serviceArrange_H.Confirmor)
                            });
                        }
                        
                    }

                }
                msg = "";
                tran.Commit();
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