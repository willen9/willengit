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
    public class SupportAplAsignDAL : ISupportAplAsignDAL
    {
        public bool AddSupportAplAsign(SupportAplAsign supportAplAsign, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DbTransaction tran = null;

            try
            {
                
                string count = dbMySQL.ExecuteScalar(@"select count(*) from supportaplasign where 
                    AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsign.AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsign.AsignOrderNo)
                    }).ToString();
                tran = dbMySQL.CreateDbTransaction();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into supportaplasign (AsignOrderType,AsignOrderNo,OrderDate,CustomerId,
                        Version,SupportAplOrderType,SupportAplOrderNo,ProcessMan,AsignMan,AsignDate,NoOfPrints,Remark,Confirmed) 
                        VALUES (@AsignOrderType,@AsignOrderNo,@OrderDate,@CustomerId,@Version,@SupportAplOrderType,@SupportAplOrderNo,
                        @ProcessMan,@AsignMan,@AsignDate,@NoOfPrints,@Remark,@Confirmed)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsign.AsignOrderType),
                            DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsign.AsignOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,supportAplAsign.OrderDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,supportAplAsign.CustomerId),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,supportAplAsign.Version),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportAplAsign.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportAplAsign.SupportAplOrderNo),
                            DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsign.ProcessMan),
                            DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsign.AsignMan),
                            DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,supportAplAsign.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,supportAplAsign.Remark),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y")
                        });
                    dbMySQL.ExecuteNonQuery(@"update supportapl_h set AsignDate=@AsignDate,AsignMan=@AsignMan,
                        ProcessMan=@ProcessMan,AsignCheck=@AsignCheck,OrderStatus=@OrderStatus where SupportAplOrderType=@SupportAplOrderType 
                        and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsign.ProcessMan),
                            DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsign.AsignMan),
                            DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"Y"),
                            DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"1"),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportAplAsign.SupportAplOrderType),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportAplAsign.SupportAplOrderNo)
                        });
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "單別和單號已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                msg = ex.Message;
                return false;
            }finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        public List<SupportAplAsign> GetSupportAplAsign(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportAplAsign> objSupportAplAsign = new List<SupportAplAsign>();

            string sql = @"select s.AsignOrderType,s.AsignOrderNo,s.OrderDate,s.CustomerId,c.CustomerName,s.Version,
                    s.SupportAplOrderType,s.SupportAplOrderNo,s.ProcessMan,eee.EmployeeName as ProcessManName,
                    e.EmployeeName as ProcessManName,eee.DepartmentId,d.DepartmentName,
                    s.AsignMan,ee.EmployeeName as AsignManName,s.AsignDate,h.orderstatus,
                    s.NoOfPrints,s.Remark from supportaplasign as s 
                    left join Customer as c on s.CustomerId=c.CustomerId 
                    left join Employee as e on e.EmployeeId=s.ProcessMan
                    left join Employee as ee on ee.EmployeeId=s.AsignMan
                    left join Employee as eee on eee.EmployeeId=s.ProcessMan
                    left join Department as d on d.DepartmentId=e.DepartmentId
					left join SupportApl_H as h on s.SupportAplOrderType=h.SupportAplOrderType and s.SupportAplOrderNo=h.SupportAplOrderNo where 1=1";

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
                strCondition += " AND s." + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i + " ";
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

            DataTable dtSupportAplAsign = null;
            if (strCondition != "")
            {
                dtSupportAplAsign = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSupportAplAsign = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSupportAplAsign.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportAplAsign.Rows)
                {
                    SupportAplAsign obj = new SupportAplAsign();
                    obj.AsignOrderType = dr["AsignOrderType"].ToString();                       //單別
                    obj.AsignOrderNo = dr["AsignOrderNo"].ToString();                           //單號
                    obj.OrderDate = dr["OrderDate"].ToString();                                 //單號日期
                    obj.CustomerId = dr["CustomerId"].ToString();                               //客戶代號
                    obj.CustomerName = dr["CustomerName"].ToString();                           //客戶名稱
                    obj.OrderStatus = dr["orderstatus"].ToString();                             //單據狀態
                    obj.DepartmentName = dr["DepartmentName"].ToString();                       //部門名稱
                    obj.ProcessManName = dr["ProcessManName"].ToString();                       //處理人員
                    obj.AsignManName = dr["AsignManName"].ToString();                           //派工人員
                    obj.AsignDate = dr["AsignDate"].ToString();                                 //派工日期
                    obj.Remark = dr["Remark"].ToString();                                       //備註
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();             //來源單別
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();                 //來源單號
                    objSupportAplAsign.Add(obj);
                }
            }

            return objSupportAplAsign;
        }

        public void DeleteSupportAplAsign(string AsignOrderType,string AsignOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtSupportAplAsign = dbMySQL.ExecuteDataTable(@"select SupportAplOrderType,SupportAplOrderNo from SupportAplAsign 
                    where AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo ", tran,
                    new DbParameter[] { 
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
                    });
                if (dtSupportAplAsign != null && dtSupportAplAsign.Rows.Count>0)
                {
                    dbMySQL.ExecuteNonQuery(@"update SupportApl_H set AsignDate=@AsignDate,AsignMan=@AsignMan,
                        ProcessMan=@ProcessMan,AsignCheck=@AsignCheck,OrderStatus=@OrderStatus where SupportAplOrderType=@SupportAplOrderType 
                        and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,""),
                            DataAccessMySQL.CreateParameter("AsignMan",DbType.String,""),
                            DataAccessMySQL.CreateParameter("AsignDate",DbType.String,""),
                            DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"N"),
                            DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"0"),
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderType"]),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtSupportAplAsign.Rows[0]["SupportAplOrderNo"])
                        });
                }
                dbMySQL.ExecuteNonQuery("delete from SupportAplAsign where AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo ",tran,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                    DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
                });
                dbMySQL.ExecuteNonQuery("delete from SupportAplAsignModi where AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo ", tran,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                    DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
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

        public bool UpdateSupportAplAsign(SupportAplAsign supportAplAsign)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update SupportAplAsign set OrderDate=@OrderDate,CustomerId=@CustomerId,
                    Version=@Version,SupportAplOrderType=@SupportAplOrderType,SupportAplOrderNo=@SupportAplOrderNo,
                    ProcessMan=@ProcessMan,AsignMan=@AsignMan,AsignDate=@AsignDate,
                    Remark=@Remark where AsignOrderType=@AsignOrderType and AsignOrderNo=@AsignOrderNo ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("OrderDate",DbType.String,supportAplAsign.OrderDate),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,supportAplAsign.CustomerId),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,supportAplAsign.Version),
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,supportAplAsign.SupportAplOrderType),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,supportAplAsign.SupportAplOrderNo),
                        DataAccessMySQL.CreateParameter("ProcessMan",DbType.String,supportAplAsign.ProcessMan),
                        DataAccessMySQL.CreateParameter("AsignMan",DbType.String,supportAplAsign.AsignMan),
                        DataAccessMySQL.CreateParameter("AsignDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        //DataAccessMySQL.CreateParameter("PrintingCount",DbType.String,supportAplAsign.PrintingCount),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,supportAplAsign.Remark),
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsign.AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsign.AsignOrderNo)
                    });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public SupportAplAsign GetSupportAplAsignInfo(string AsignOrderType, string AsignOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            SupportAplAsign supportAplAsign = new SupportAplAsign();

            DataTable dtSupportAplAsign = dbMySQL.ExecuteDataTable(@"select s.AsignOrderType,s.AsignOrderNo,s.OrderDate,s.CustomerId,c.CustomerName,s.Version,
                            s.SupportAplOrderType,s.SupportAplOrderNo,s.ProcessMan,e.EmployeeName as ProcessManName,
                            s.AsignMan,ee.EmployeeName as AsignManName,s.AsignDate,s.Confirmed,h.AsignCheck,
                            s.NoOfPrints,s.Remark from SupportAplAsign as s 
                            left join Customer as c on s.CustomerId=c.CustomerId 
                            left join Employee as e on e.EmployeeId=s.ProcessMan
                            left join Employee as ee on ee.EmployeeId=s.AsignMan
							left join SupportApl_H as h on h.SupportAplOrderNo=s.SupportAplOrderNo and h.SupportAplOrderType=s.SupportAplOrderType
                            where s.AsignOrderType=@AsignOrderType and s.AsignOrderNo=@AsignOrderNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                    DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
                });

            if (dtSupportAplAsign != null && dtSupportAplAsign.Rows.Count > 0)
            {
                supportAplAsign.AsignOrderType = dtSupportAplAsign.Rows[0]["AsignOrderType"].ToString();
                supportAplAsign.AsignOrderNo = dtSupportAplAsign.Rows[0]["AsignOrderNo"].ToString();
                supportAplAsign.OrderDate = dtSupportAplAsign.Rows[0]["OrderDate"].ToString();
                supportAplAsign.CustomerId = dtSupportAplAsign.Rows[0]["CustomerId"].ToString();
                supportAplAsign.CustomerName = dtSupportAplAsign.Rows[0]["CustomerName"].ToString();
                supportAplAsign.Version = dtSupportAplAsign.Rows[0]["Version"].ToString();
                supportAplAsign.SupportAplOrderType = dtSupportAplAsign.Rows[0]["SupportAplOrderType"].ToString();
                supportAplAsign.SupportAplOrderNo = dtSupportAplAsign.Rows[0]["SupportAplOrderNo"].ToString();
                supportAplAsign.ProcessMan = dtSupportAplAsign.Rows[0]["ProcessMan"].ToString();
                supportAplAsign.ProcessManName = dtSupportAplAsign.Rows[0]["ProcessManName"].ToString();
                supportAplAsign.AsignMan = dtSupportAplAsign.Rows[0]["AsignMan"].ToString();
                supportAplAsign.AsignManName = dtSupportAplAsign.Rows[0]["AsignManName"].ToString();
                supportAplAsign.AsignDate = dtSupportAplAsign.Rows[0]["AsignDate"].ToString();
                supportAplAsign.NoOfPrints = dtSupportAplAsign.Rows[0]["NoOfPrints"].ToString();
                supportAplAsign.Remark = dtSupportAplAsign.Rows[0]["Remark"].ToString();
                supportAplAsign.Confirmed = dtSupportAplAsign.Rows[0]["Confirmed"].ToString();
                supportAplAsign.AsignCheck = dtSupportAplAsign.Rows[0]["AsignCheck"].ToString();
                return supportAplAsign;
            }
            else
            {
                return null;
            }
        }

        public List<SupportAplAsign> SearchSupportAplAsign(SupportAplAsign supportAplAsign, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportAplAsign> objSupportAplAsign = new List<SupportAplAsign>();

            string sql = @"select s.AsignOrderType,(select OrderSName from 
                            OrderCategory as o where o.OrderType=s.AsignOrderType) 
                            as AsignOrderTypeName,s.AsignOrderNo,s.CustomerId,
                            (select c.CustomerName from Customer as c where 
                            c.CustomerId=s.CustomerId) as CustomerName,
                            s.SupportAplOrderType,s.SupportAplOrderNo,s.ProcessMan,
                            (select EmployeeName from Employee as e where 
                            e.EmployeeId=s.ProcessMan) as ProcessManName,
                            s.AsignDate,s.AsignMan,(select EmployeeName 
                            from Employee as e where e.EmployeeId=s.AsignMan) 
                            as AsignManName,s.Remark,s.Version,s.NoOfPrints 
                            from supportaplasign as s where 1=1";

            if (!String.IsNullOrEmpty(supportAplAsign.AsignOrderType))
            {
                sql += " and s.AsignOrderType=@AsignOrderType";
            }

            if (!String.IsNullOrEmpty(supportAplAsign.AsignOrderNo))
            {
                sql += " and s.AsignOrderNo=@AsignOrderNo";
            }

            sql += " and s.Confirmed!='V'";

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtSupportAplAsign = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,supportAplAsign.AsignOrderType),
                    DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,supportAplAsign.AsignOrderNo),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });

            if (dtSupportAplAsign.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportAplAsign.Rows)
                {
                    SupportAplAsign obj = new SupportAplAsign();
                    obj.AsignOrderType = dr["AsignOrderType"].ToString();
                    obj.AsignOrderNo = dr["AsignOrderNo"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();
                    obj.ProcessMan = dr["ProcessMan"].ToString();
                    obj.ProcessManName = dr["ProcessManName"].ToString();
                    obj.AsignDate = dr["AsignDate"].ToString();
                    obj.AsignMan = dr["AsignMan"].ToString();
                    obj.AsignManName = dr["AsignManName"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.NoOfPrints = dr["NoOfPrints"].ToString();
                    obj.AsignOrderTypeName = dr["AsignOrderTypeName"].ToString();
                    objSupportAplAsign.Add(obj);
                }
            }

            return objSupportAplAsign;
        }

        //列印
        public void PrintSupportAplAsign(string AsignOrderType, string AsignOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            dbMySQL.ExecuteNonQuery(@"update SupportAplAsign set NoOfPrints=NoOfPrints+1 where AsignOrderType=@AsignOrderType 
                    and AsignOrderNo=@AsignOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AsignOrderType",DbType.String,AsignOrderType),
                        DataAccessMySQL.CreateParameter("AsignOrderNo",DbType.String,AsignOrderNo)
                    });
        }

        public SupportAplAsign GetInfo(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            SupportAplAsign supportAplAsign = new SupportAplAsign();

            DataTable dtSupportAplAsign = dbMySQL.ExecuteDataTable(@"select * from SupportAplAsign where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                });

            if (dtSupportAplAsign != null && dtSupportAplAsign.Rows.Count > 0)
            {
                supportAplAsign.AsignOrderType = dtSupportAplAsign.Rows[0]["AsignOrderType"].ToString();
                supportAplAsign.AsignOrderNo = dtSupportAplAsign.Rows[0]["AsignOrderNo"].ToString();
                supportAplAsign.Version = dtSupportAplAsign.Rows[0]["Version"].ToString();
                return supportAplAsign;
            }
            else
            {
                return null;
            }
        }
    }
}