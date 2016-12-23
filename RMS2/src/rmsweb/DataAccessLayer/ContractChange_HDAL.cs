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
    public class ContractChange_HDAL : IContractChange_HDAL
    {
        public bool AddContractChange_H(ContractChange_H contractChange_H, string strItemId, string strProductNo,
            string strSerialNo, string strNewIsClosed, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtContract_H = dbMySQL.ExecuteDataTable(@"select * from Contract_H where ContractType=@ContractType and ContractNo=@ContractNo",tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo)
                    });
                if (dtContract_H != null && dtContract_H.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery(@"update Contract_H set Company=@Company,UserGroup=@UserGroup,
                        Modifier=@Modifier,ModiDate=@ModiDate,Version=@Version where
                        ContractType=@ContractType and ContractNo=@ContractNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,contractChange_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contractChange_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,contractChange_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtContract_H.Rows[0]["Version"].ToString())+1).ToString("0000"))
                        });

                    dbMySQL.ExecuteNonQuery(@"insert into ContractChange_H (Company,UserGroup,Creator,CreateDate,
                        ContractType,ContractNo,Version,OrderDate,TerminationOfService,ModiReason,Confirmed) 
                        values (@Company,@UserGroup,@Creator,@CreateDate,
                        @ContractType,@ContractNo,@Version,@OrderDate,@TerminationOfService,@ModiReason,@Confirmed)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,contractChange_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contractChange_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,contractChange_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtContract_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,contractChange_H.OrderDate),
                            DataAccessMySQL.CreateParameter("TerminationOfService",DbType.String,contractChange_H.TerminationOfService),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,contractChange_H.ModiReason),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,contractChange_H.Confirmed)
                        });

                    DataTable dtContractChange_ProductSerial = null;
                    string OldIsClosed="";
                    if (strItemId != "")
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strProductNoArray = strProductNo.Split(',');
                        string[] strSerialNoArray = strSerialNo.Split(',');
                        string[] strNewIsClosedArray = strNewIsClosed.Split(',');

                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }

                            string[] strSerialArray = strSerialNoArray[i].ToString().Split('#');
                            for (int y = 0; y < strSerialArray.Length; y++)
                            {
                                if (string.IsNullOrEmpty(strSerialArray[y]))
                                {
                                    continue;
                                }

                                dtContractChange_ProductSerial = dbMySQL.ExecuteDataTable(@"select * from ContractChange_ProductSerial 
                                where ContractType=@ContractType and ContractNo=@ContractNo and Version=@Version and ItemId=@ItemId 
                                and SerialNo=@SerialNo", tran,
                                    new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,dtContract_H.Rows[0]["Version"].ToString()),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialArray[y].ToString())
                                });
                                if (dtContractChange_ProductSerial != null && dtContractChange_ProductSerial.Rows.Count > 0)
                                {
                                    OldIsClosed = dtContractChange_ProductSerial.Rows[0]["OldIsClosed"].ToString();
                                }


                                dbMySQL.ExecuteNonQuery(@"insert into ContractChange_ProductSerial (Company,UserGroup,Creator,
                                CreateDate,ContractType,ContractNo,Version,ItemId,ProductNo,SerialNo,NewIsClosed,OldIsClosed) 
                                values (@Company,@UserGroup,@Creator,
                                @CreateDate,@ContractType,@ContractNo,@Version,@ItemId,@ProductNo,@SerialNo,@NewIsClosed,@OldIsClosed)", tran,
                                    new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,contractChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contractChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Creator",DbType.String,contractChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtContract_H.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialArray[y].ToString()),
                                    DataAccessMySQL.CreateParameter("NewIsClosed",DbType.String,strNewIsClosedArray[i].ToString()==""?"N":strNewIsClosedArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("OldIsClosed",DbType.String,OldIsClosed)
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


        public List<ContractChange_H> GetContractChange_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ContractChange_H> objContractChange_H = new List<ContractChange_H>();

            string sql = @"select h.ContractType,o.OrderSName,h.ContractNo,
                ch.Version,ch.CustomerId,c.CustomerName,
                ch.ContractSDate,ch.ContractEDate from ContractChange_H as h
                left join OrderCategory as o on o.OrderType=h.ContractType
                left join contract_h as ch on ch.ContractType=h.ContractType and ch.ContractNo=h.ContractNo
                left join Customer as c on c.CustomerId=ch.CustomerId where 1=1";

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

            DataTable dtContractChange_H = null;
            if (strCondition != "")
            {
                dtContractChange_H = dbMySQL.ExecuteDataTable(sql + strCondition + " group by h.ContractType,o.OrderSName,h.ContractNo", dbParameters);
            }
            else
            {
                dtContractChange_H = dbMySQL.ExecuteDataTable(sql + " group by h.ContractType,o.OrderSName,h.ContractNo");
            }
            if (dtContractChange_H.Rows.Count > 0)
            {

                foreach (DataRow dr in dtContractChange_H.Rows)
                {
                    ContractChange_H obj = new ContractChange_H();
                    obj.ContractType = dr["ContractType"].ToString();       //合約單別
                    obj.OrderSName = dr["OrderSName"].ToString();           //合約單別名稱
                    obj.ContractNo = dr["ContractNo"].ToString();           //合約單號
                    obj.Version = dr["Version"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();           //客戶代號
                    obj.CustomerName = dr["CustomerName"].ToString();       //客戶簡稱
                    obj.ContractSDate = dr["ContractSDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ContractSDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //合約起日
                    obj.ContractEDate = dr["ContractEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ContractEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");      //合約迄日
                    objContractChange_H.Add(obj);
                }
            }

            return objContractChange_H;

        }

        public bool UpdateContractChange_H(ContractChange_H contractChange_H, string strItemId, string strProductNo,
            string strSerialNo, string strNewIsClosed, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update ContractChange_H set Company=@Company,UserGroup=@UserGroup,
                    Modifier=@Modifier,ModiDate=@ModiDate,
                    OrderDate=@OrderDate,TerminationOfService=@TerminationOfService,ModiReason=@ModiReason,
                    Confirmed=@Confirmed where
                    ContractType=@ContractType and ContractNo=@ContractNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,contractChange_H.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contractChange_H.UserGroup),
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,contractChange_H.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,contractChange_H.Version),
                        DataAccessMySQL.CreateParameter("OrderDate",DbType.String,contractChange_H.OrderDate),
                        DataAccessMySQL.CreateParameter("TerminationOfService",DbType.String,contractChange_H.TerminationOfService),
                        DataAccessMySQL.CreateParameter("ModiReason",DbType.String,contractChange_H.ModiReason),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,contractChange_H.Confirmed)
                    });

                    if (strItemId != "")
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strProductNoArray = strProductNo.Split(',');
                        string[] strSerialNoArray = strSerialNo.Split(',');
                        string[] strNewIsClosedArray = strNewIsClosed.Split(',');

                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }

                            string[] strSerialArray = strSerialNoArray[i].ToString().Split('#');
                            for (int y = 0; y < strSerialArray.Length; y++)
                            {
                                if (string.IsNullOrEmpty(strSerialArray[y]))
                                {
                                    continue;
                                }

                                dbMySQL.ExecuteNonQuery(@"update ContractChange_ProductSerial set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,
                                ModiDate=@ModiDate,ProductNo=@ProductNo,NewIsClosed=@NewIsClosed 
                                where ContractType=@ContractType and ContractNo=@ContractNo and Version=@Version and ItemId=@ItemId and SerialNo=@SerialNo", tran,
                                    new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,contractChange_H.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,contractChange_H.UserGroup),
                                    DataAccessMySQL.CreateParameter("Modifier",DbType.String,contractChange_H.Creator),
                                    DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,contractChange_H.Version),
                                    DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,strProductNoArray[i].ToString()),
                                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,strSerialArray[y].ToString()),
                                    DataAccessMySQL.CreateParameter("NewIsClosed",DbType.String,strNewIsClosedArray[i].ToString())
                                });
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

        public bool DelContractChange_H(string ContractType, string ContractNo, string Version)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                dbMySQL.ExecuteNonQuery("delete from ContractChange_H where ContractType=@ContractType and ContractNo=@ContractNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,Version)
                    });

                dbMySQL.ExecuteNonQuery("delete from ContractChange_ProductSerial where ContractType=@ContractType and ContractNo=@ContractNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,Version)
                    });

                dbMySQL.ExecuteNonQuery(@"update Contract_H set Version=@Version where
                        ContractType=@ContractType and ContractNo=@ContractNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                            DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(Version)-1).ToString("0000"))
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

        public ContractChange_H ContractChange_HInfo(string ContractType, string ContractNo, string Version)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            ContractChange_H contractChange_H = new ContractChange_H();

            DataTable dtContractChange_H = dbMySQL.ExecuteDataTable(@"select h.*,e.EmployeeName as ConfirmorName from ContractChange_H as h
                left join Employee as e on h.Confirmor=e.EmployeeId
                where ContractType=@ContractType and ContractNo=@ContractNo and Version=@Version",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,ContractType),
                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,ContractNo),
                    DataAccessMySQL.CreateParameter("Version",DbType.String,Version)
                });

            if (dtContractChange_H != null && dtContractChange_H.Rows.Count > 0)
            {
                contractChange_H.ContractType = dtContractChange_H.Rows[0]["ContractType"].ToString();       //合約單別
                contractChange_H.ContractNo = dtContractChange_H.Rows[0]["ContractNo"].ToString();       //合約單號
                contractChange_H.Version = dtContractChange_H.Rows[0]["Version"].ToString();       //變更版次
                contractChange_H.OrderDate = dtContractChange_H.Rows[0]["OrderDate"].ToString();       //單據日期
                contractChange_H.TerminationOfService = dtContractChange_H.Rows[0]["TerminationOfService"].ToString();       //終止合約
                contractChange_H.ModiReason = dtContractChange_H.Rows[0]["ModiReason"].ToString();       //變更原因
                contractChange_H.Confirmed = dtContractChange_H.Rows[0]["Confirmed"].ToString();       //確認碼
                contractChange_H.Confirmor = dtContractChange_H.Rows[0]["Confirmor"].ToString();       //
                contractChange_H.ConfirmorName = dtContractChange_H.Rows[0]["ConfirmorName"].ToString();       //

                return contractChange_H;
            }
            else
            {
                return null;
            }
        }

        public bool TypeContractChange_H(ContractChange_H contractChange_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            try
            {
                dbMySQL.ExecuteNonQuery(@"update ContractChange_H set
                    Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmedDate=@ConfirmedDate where
                    ContractType=@ContractType and ContractNo=@ContractNo and Version=@Version",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                        DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,contractChange_H.Version),
                        DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,contractChange_H.Confirmed),
                        DataAccessMySQL.CreateParameter("Confirmor",DbType.String,contractChange_H.Confirmor),
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

        public bool IsContractChange_H(ContractChange_H contractChange_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            if(int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from ContractChange_H where ContractType=@ContractType and ContractNo=@ContractNo",
                new DbParameter[] {
                    DataAccessMySQL.CreateParameter("ContractType",DbType.String,contractChange_H.ContractType),
                    DataAccessMySQL.CreateParameter("ContractNo",DbType.String,contractChange_H.ContractNo)
                }).ToString())==0)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}