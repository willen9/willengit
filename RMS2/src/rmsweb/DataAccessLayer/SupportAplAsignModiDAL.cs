using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class SupportAplAsignModiDAL : ISupportAplAsignModiDAL
    {
        public void AddSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;

            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtSupportAplAsign = dbMySQL.ExecuteDataTable(@"select * from supportaplasign where 
                    AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo)
                    });
                if(dtSupportAplAsign!=null&&dtSupportAplAsign.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into supportaplasignmodi (AsignOrderType,AsignOrderNo,Version,OrderDate,ModiReason,
                        CustomerId,SupportAplOrderType,SupportAplOrderNo,NoOfPrints,ProcessMan,AsignMan,AsignDate,Remark,
                        OldSupportAplOrderType,OldSupportAplOrderNo,OldCustomerId,OldProcessMan,OldAsignMan,OldAsignDate,OldRemark,Creator,CreateDate,Confirmor,ConfirmedDate) 
                        VALUES (@AsignOrderType,@AsignOrderNo,@Version,@OrderDate,@ModiReason,@CustomerId,@SupportAplOrderType,
                        @SupportAplOrderNo,@NoOfPrints,@ProcessMan,@AsignMan,@AsignDate,@Remark,@OldSupportAplOrderType,@OldSupportAplOrderNo,
                        @OldCustomerId,@OldProcessMan,@OldAsignMan,@OldAsignDate,@OldRemark,@Creator,@CreateDate,@Confirmor,@ConfirmedDate)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                            DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dtSupportAplAsign.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,supportAplAsignModi.OrderDate),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,supportAplAsignModi.ModiReason),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtSupportAplAsign.Rows[0]["CustomerId"].ToString()),                            
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,supportAplAsignModi.NoOfPrints),
                            DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsignModi.ProcessMan),
                            DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsignModi.AsignMan),
                            DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,supportAplAsignModi.Remark),
                            DataAccessMySQL.CreateParameter("OldSupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("OldSupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldCustomerId",DbType.String,dtSupportAplAsign.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("OldProcessMan",DbType.String,dtSupportAplAsign.Rows[0]["ProcessMan"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAsignMan",DbType.String,dtSupportAplAsign.Rows[0]["AsignMan"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAsignDate",DbType.String,dtSupportAplAsign.Rows[0]["AsignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtSupportAplAsign.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportAplAsignModi.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,supportAplAsignModi.Confirmor),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,DateTime.Now.ToString("yyyyMMdd"))
                        });

                    dbMySQL.ExecuteNonQuery(@"update supportaplasign set Version=@Version,AsignMan=@AsignMan,
                            ProcessMan=@ProcessMan,AsignDate=@AsignDate,Remark=@Remark where AsignOrderType=@AsignOrderType 
                            and AsignOrderNo=@AsignOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dtSupportAplAsign.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsignModi.ProcessMan),
                                DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsignModi.AsignMan),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,supportAplAsignModi.Remark),
                                DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                                DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo)
                            });

                    dbMySQL.ExecuteNonQuery(@"update supportapl_h set AsignDate=@AsignDate,AsignMan=@AsignMan,
                            ProcessMan=@ProcessMan,AsignCheck=@AsignCheck,OrderStatus=@OrderStatus where SupportAplOrderType=@SupportAplOrderType 
                            and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsignModi.ProcessMan),
                                DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsignModi.AsignMan),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"Y"),
                                DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"1"),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString()),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString())
                            });
                }
                
                tran.Commit();
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

        public List<SupportAplAsignModi> GetSupportAplAsignModi(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportAplAsignModi> objSupportAplAsignModi = new List<SupportAplAsignModi>();

            string sql = @"select a.AsignOrderType,a.AsignOrderNo,a.Version,a.OrderDate,a.CustomerId,c.CustomerName,
                a.Confirmed,a.ProcessMan,e.EmployeeName as ProcessManName,m.ConfirmedDate,m.Confirmor,
                ee.EmployeeName as ConfirmorName,m.ModiReason,m.Remark from SupportAplAsign as a 
                left join supportaplasignmodi as m on a.AsignOrderType=m.AsignOrderType and a.AsignOrderNo=m.AsignOrderNo and a.Version=m.Version 
                left join Customer as c on c.CustomerId=a.CustomerId
                left join Employee as e on e.EmployeeId=a.ProcessMan
                left join Employee as ee on ee.EmployeeId=m.Confirmor
                where Confirmed<>'V'";

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

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

            DataTable dtSupportAplAsignModi = null;
            if (strCondition != "")
            {
                dtSupportAplAsignModi = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSupportAplAsignModi = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSupportAplAsignModi.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportAplAsignModi.Rows)
                {
                    SupportAplAsignModi obj = new SupportAplAsignModi();
                    obj.AsignOrderType = dr["AsignOrderType"].ToString();
                    obj.AsignOrderNo = dr["AsignOrderNo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.Confirmed = dr["Confirmed"].ToString();
                    obj.ProcessMan = dr["ProcessMan"].ToString();
                    obj.ProcessManName = dr["ProcessManName"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString();
                    obj.Confirmor = dr["Confirmor"].ToString();
                    obj.ConfirmorName = dr["ConfirmorName"].ToString();
                    obj.ModiReason = dr["ModiReason"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objSupportAplAsignModi.Add(obj);
                }
            }

            return objSupportAplAsignModi;
        }

        public void DeleteSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;

            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtSupportAplAsignModi = dbMySQL.ExecuteDataTable(@"select * from supportaplasignmodi where 
                    AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,supportAplAsignModi.Version)
                    });
                if (dtSupportAplAsignModi != null && dtSupportAplAsignModi.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"update supportaplasign set Version=@Version,AsignMan=@AsignMan,
                            ProcessMan=@ProcessMan,AsignDate=@AsignDate,Remark=@Remark where AsignOrderType=@AsignOrderType 
                            and AsignOrderNo=@AsignOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dtSupportAplAsignModi.Rows[0]["Version"].ToString())-1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,dtSupportAplAsignModi.Rows[0]["OldProcessMan"].ToString()),
                                DataAccessMySQL.CreateParameter("AsignMan",DbType.String,dtSupportAplAsignModi.Rows[0]["OldAsignMan"].ToString()),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String,dtSupportAplAsignModi.Rows[0]["OldAsignDate"].ToString()),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,dtSupportAplAsignModi.Rows[0]["OldRemark"].ToString()),
                                DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                                DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo)
                            });

                    dbMySQL.ExecuteNonQuery(@"update supportapl_h set AsignDate=@AsignDate,AsignMan=@AsignMan,
                            ProcessMan=@ProcessMan where SupportAplOrderType=@SupportAplOrderType 
                            and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,dtSupportAplAsignModi.Rows[0]["OldProcessMan"].ToString()),
                                DataAccessMySQL.CreateParameter("AsignMan",DbType.String,dtSupportAplAsignModi.Rows[0]["OldAsignMan"].ToString()),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String,dtSupportAplAsignModi.Rows[0]["OldAsignDate"].ToString()),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo)
                            });
                }

                dbMySQL.ExecuteNonQuery(@"delete from supportaplasignmodi where AsignOrderType=@AsignOrderType and 
                    AsignOrderNo=@AsignOrderNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,supportAplAsignModi.Version)
                    });

                tran.Commit();
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

        public bool UpdateSupportAplAsignModi(SupportAplAsignModi supportAplAsignModi)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtSupportAplAsign = dbMySQL.ExecuteDataTable(@"select * from supportaplasign where 
                    AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo)
                    });
                if (dtSupportAplAsign != null && dtSupportAplAsign.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into supportaplasignmodi (AsignOrderType,AsignOrderNo,Version,OrderDate,ModiReason,
                        CustomerId,SupportAplOrderType,SupportAplOrderNo,NoOfPrints,ProcessMan,AsignMan,AsignDate,Remark,
                        OldSupportAplOrderType,OldSupportAplOrderNo,OldCustomerId,OldProcessMan,OldAsignMan,OldAsignDate,OldRemark,Creator,CreateDate,Confirmor,ConfirmedDate) 
                        VALUES (@AsignOrderType,@AsignOrderNo,@Version,@OrderDate,@ModiReason,@CustomerId,@SupportAplOrderType,
                        @SupportAplOrderNo,@NoOfPrints,@ProcessMan,@AsignMan,@AsignDate,@Remark,@OldSupportAplOrderType,@OldSupportAplOrderNo,
                        @OldCustomerId,@OldProcessMan,@OldAsignMan,@OldAsignDate,@OldRemark,@Creator,@CreateDate,@Confirmor,@ConfirmedDate)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                            DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dtSupportAplAsign.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,supportAplAsignModi.OrderDate),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,supportAplAsignModi.ModiReason),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtSupportAplAsign.Rows[0]["CustomerId"].ToString()),                            
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,supportAplAsignModi.NoOfPrints),
                            DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsignModi.ProcessMan),
                            DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsignModi.AsignMan),
                            DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,supportAplAsignModi.Remark),
                            DataAccessMySQL.CreateParameter("OldSupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("OldSupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldCustomerId",DbType.String,dtSupportAplAsign.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("OldProcessMan",DbType.String,dtSupportAplAsign.Rows[0]["ProcessMan"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAsignMan",DbType.String,dtSupportAplAsign.Rows[0]["AsignMan"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAsignDate",DbType.String,dtSupportAplAsign.Rows[0]["AsignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtSupportAplAsign.Rows[0]["Remark"].ToString()),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportAplAsignModi.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,supportAplAsignModi.Confirmor),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,DateTime.Now.ToString("yyyyMMdd"))
                        });

                    dbMySQL.ExecuteNonQuery(@"update supportaplasign set Version=@Version,AsignMan=@AsignMan,
                            ProcessMan=@ProcessMan,AsignDate=@AsignDate,Remark=@Remark where AsignOrderType=@AsignOrderType 
                            and AsignOrderNo=@AsignOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(Int32.Parse(dtSupportAplAsign.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsignModi.ProcessMan),
                                DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsignModi.AsignMan),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,supportAplAsignModi.Remark),
                                DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsignModi.AsignOrderType),
                                DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsignModi.AsignOrderNo)
                            });

                    dbMySQL.ExecuteNonQuery(@"update supportapl_h set AsignDate=@AsignDate,AsignMan=@AsignMan,
                            ProcessMan=@ProcessMan,AsignCheck=@AsignCheck,OrderStatus=@OrderStatus where SupportAplOrderType=@SupportAplOrderType 
                            and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsignModi.ProcessMan),
                                DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsignModi.AsignMan),
                                DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"Y"),
                                DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"1"),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString()),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString())
                            });
                }

                tran.Commit();
                return true;
            }
            catch
            {
                if (tran!=null)
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

        public SupportAplAsignModi GetSupportAplAsignInfo(string AsignOrderType, string AsignOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            SupportAplAsignModi supportAplAsignModi = new SupportAplAsignModi();

            DataTable dtSupportAplAsignModi = dbMySQL.ExecuteDataTable(@"select a.AsignOrderType,a.AsignOrderNo,a.orderdate,a.customerid,c.CustomerName,
                            a.version,a.processman,e.EmployeeName as processmanName,a.remark,m.ModiReason,
                            a.NoOfPrints,m.ConfirmedDate,m.Confirmor,ee.EmployeeName as ConfirmorName
                            from SupportAplAsign as a
                            left join supportaplasignmodi as m on m.AsignOrderType=a.AsignOrderType and m.AsignOrderNo=a.AsignOrderNo and m.Version=a.Version
                            left join Customer as c on a.CustomerId=c.CustomerId 
                            left join Employee as e on e.EmployeeId=a.ProcessMan
                            left join Employee as ee on ee.EmployeeId=m.Confirmor
                            where a.AsignOrderType=@AsignOrderType and a.AsignOrderNo=@AsignOrderNo ",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                    DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
                });

            if (dtSupportAplAsignModi != null && dtSupportAplAsignModi.Rows.Count > 0)
            {
                supportAplAsignModi.AsignOrderType = dtSupportAplAsignModi.Rows[0]["AsignOrderType"].ToString();
                supportAplAsignModi.AsignOrderNo = dtSupportAplAsignModi.Rows[0]["AsignOrderNo"].ToString();
                supportAplAsignModi.OrderDate = dtSupportAplAsignModi.Rows[0]["OrderDate"].ToString();
                supportAplAsignModi.CustomerId = dtSupportAplAsignModi.Rows[0]["CustomerId"].ToString();
                supportAplAsignModi.CustomerName = dtSupportAplAsignModi.Rows[0]["CustomerName"].ToString();
                supportAplAsignModi.Version = dtSupportAplAsignModi.Rows[0]["Version"].ToString();
                supportAplAsignModi.ConfirmedDate = dtSupportAplAsignModi.Rows[0]["ConfirmedDate"].ToString();
                supportAplAsignModi.ProcessMan = dtSupportAplAsignModi.Rows[0]["ProcessMan"].ToString();
                supportAplAsignModi.NoOfPrints = dtSupportAplAsignModi.Rows[0]["NoOfPrints"].ToString();
                supportAplAsignModi.Confirmor = dtSupportAplAsignModi.Rows[0]["Confirmor"].ToString();
                supportAplAsignModi.ConfirmorName = dtSupportAplAsignModi.Rows[0]["ConfirmorName"].ToString();
                supportAplAsignModi.ModiReason = dtSupportAplAsignModi.Rows[0]["ModiReason"].ToString();
                supportAplAsignModi.Remark = dtSupportAplAsignModi.Rows[0]["Remark"].ToString();
                
            }
            return supportAplAsignModi;
        }
    
        public void voidModi(string AsignOrderType, string AsignOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;

            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtSupportAplAsign = dbMySQL.ExecuteDataTable(@"select * from supportaplasign where 
                    AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo ", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
                    });
                if (dtSupportAplAsign != null && dtSupportAplAsign.Rows.Count > 0)
                {
                    dbMySQL.ExecuteNonQuery(@"update supportapl_h set AsignCheck=@AsignCheck,OrderStatus=@OrderStatus
                            where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"N"),
                                DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"0"),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString()),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString())
                            });

                    dbMySQL.ExecuteNonQuery(@"update supportaplasign set Confirmed=@Confirmed where AsignOrderType=@AsignOrderType 
                            and AsignOrderNo=@AsignOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"V"),
                                DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                                DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
                            });
                }

                tran.Commit();
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
    }
}