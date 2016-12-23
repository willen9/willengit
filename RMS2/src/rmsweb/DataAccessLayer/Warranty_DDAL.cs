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
    public class Warranty_DDAL : IWarranty_DDAL
    {
        public List<Warranty_D> SearchWarranty_D(string ProductNo, string SerialNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Warranty_D> objWarranty_D = new List<Warranty_D>();

            string sql = @"select * from Warranty_D  where ProductNo=@ProductNo and SerialNo=@SerialNo";


            DataTable dtWarranty_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,ProductNo),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,SerialNo)
                }); ;

            if (dtWarranty_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtWarranty_D.Rows)
                {
                    Warranty_D obj = new Warranty_D();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.ChangeDate = dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.ChangeOrderType = dr["ChangeOrderType"].ToString();
                    obj.ChangeOrderNo = dr["ChangeOrderNo"].ToString();
                    obj.ChangeOrderItemId = dr["ChangeOrderItemId"].ToString();
                    obj.TargetId = dr["TargetId"].ToString();
                    obj.TargetName = dr["TargetName"].ToString();
                    obj.WarrantyPeriod = dr["WarrantyPeriod"].ToString();
                    obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    if (dr["WarrantyType"].ToString() == "0")
                    {
                        obj.WarrantyType = "0.空白";
                    }
                    if (dr["WarrantyType"].ToString() == "1")
                    {
                        obj.WarrantyType = "1.廠商";
                    }
                    if (dr["WarrantyType"].ToString() == "2")
                    {
                        obj.WarrantyType = "2.客戶";
                    }
                    obj.Remark = dr["Remark"].ToString();
                    objWarranty_D.Add(obj);
                }
            }

            return objWarranty_D;
        }

        //TargetId為客戶；WarrantyType=2 or 3
        public List<Warranty_D> SearchSerialNo(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Warranty_D> objWarranty_D = new List<Warranty_D>();

            string sqlStr = @"select d.ProductNo,ProductName,SerialNo,max(ChangeDate) as ChangeDate,
                WarrantySDate,WarrantyEDate,WarrantyType,TargetId,TargetName,WarrantyPeriod from Warranty_D as d 
                left join product as p on p.ProductNo=d.ProductNo where 1=1";

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
            strCondition += " AND WarrantyType in ('2','3') ";

            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            DataTable dtWarranty_D = null;
            if (strCondition != "")
            {

                dtWarranty_D = dbMySQL.ExecuteDataTable(sqlStr + strCondition, dbParameters);
            }
            else
            {
                dtWarranty_D = dbMySQL.ExecuteDataTable(sqlStr, dbParameters);
            }
            if (dtWarranty_D.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dtWarranty_D.Rows[0]["ProductNo"].ToString()))
                {
                    foreach (DataRow dr in dtWarranty_D.Rows)
                    {
                        Warranty_D obj = new Warranty_D();
                        obj.ProductNo = dr["ProductNo"].ToString();
                        obj.ProductName = dr["ProductName"].ToString();
                        obj.SerialNo = dr["SerialNo"].ToString();
                        obj.TargetId = dr["TargetId"].ToString();
                        obj.TargetName = dr["TargetName"].ToString();
                        if (dr["WarrantyType"].ToString() == "0")
                        {
                            obj.WarrantyType = "0.空白";
                        }
                        if (dr["WarrantyType"].ToString() == "1")
                        {
                            obj.WarrantyType = "1.廠商";
                        }
                        if (dr["WarrantyType"].ToString() == "2")
                        {
                            obj.WarrantyType = "2.客戶";
                        }
                        if (dr["WarrantyType"].ToString() == "3")
                        {
                            obj.WarrantyType = "3.合約";
                        }
                        obj.ChangeDate = dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                        obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                        obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                        obj.WarrantyPeriod = dr["WarrantyPeriod"].ToString();
                        objWarranty_D.Add(obj);
                    }
                    return objWarranty_D;
                }
            }

            sqlStr = @"select d.ProductNo,ProductName,SerialNo,max(ChangeDate) as ChangeDate,
                WarrantySDate,WarrantyEDate,WarrantyType,TargetId,TargetName,WarrantyPeriod from Warranty_D as d 
                left join product as p on p.ProductNo=d.ProductNo where 1=1";

            colArray = Col.Split(',');
            conditionArray = Condition.Split(',');
            valueArray = conditionValue.Split(',');

            strCondition = "";

            len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i])&&colArray[i].ToString()!= "WarrantySDate"&& colArray[i].ToString() != "WarrantyEDate")
                {
                    len++;
                }
            }
            dbParameters = new DbParameter[len + 1];
            dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i])|| colArray[i].ToString() == "WarrantySDate"|| colArray[i].ToString() == "WarrantyEDate")
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
            strCondition += " AND WarrantyType in ('2','3') ";

            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            dtWarranty_D = null;
            if (strCondition != "")
            {

                dtWarranty_D = dbMySQL.ExecuteDataTable(sqlStr + strCondition , dbParameters);
            }
            else
            {
                dtWarranty_D = dbMySQL.ExecuteDataTable("select *,max(ChangeDate) from (" + sqlStr + ") as tb", dbParameters);
            }
            if (dtWarranty_D.Rows.Count > 0)
            {
                if(!String.IsNullOrEmpty(dtWarranty_D.Rows[0]["ProductNo"].ToString()))
                {
                    foreach (DataRow dr in dtWarranty_D.Rows)
                    {
                        Warranty_D obj = new Warranty_D();
                        obj.ProductNo = dr["ProductNo"].ToString();
                        obj.ProductName = dr["ProductName"].ToString();
                        obj.SerialNo = dr["SerialNo"].ToString();
                        obj.TargetId = dr["TargetId"].ToString();
                        obj.TargetName = dr["TargetName"].ToString();
                        if (dr["WarrantyType"].ToString() == "0")
                        {
                            obj.WarrantyType = "0.空白";
                        }
                        if (dr["WarrantyType"].ToString() == "1")
                        {
                            obj.WarrantyType = "1.廠商";
                        }
                        if (dr["WarrantyType"].ToString() == "2")
                        {
                            obj.WarrantyType = "2.客戶";
                        }
                        if (dr["WarrantyType"].ToString() == "3")
                        {
                            obj.WarrantyType = "3.合約";
                        }
                        obj.WarrantyPeriod = dr["WarrantyPeriod"].ToString();
                        obj.ChangeDate = dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                        obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                        obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                        objWarranty_D.Add(obj);
                    }
                }
            }

            //if (!String.IsNullOrEmpty(ProductNo))
            //{
            //    sql += " and d.ProductNo like @ProductNo";
            //}
            //if (!String.IsNullOrEmpty(SerialNo))
            //{
            //    sql += " and SerialNo like @SerialNo";
            //}

            //if (!String.IsNullOrEmpty(TargetId))
            //{
            //    sql += " and TargetId like @TargetId";
            //}

            //if (!String.IsNullOrEmpty(ChangeDate))
            //{
            //    sql += " and WarrantySDate<@WarrantySDate and WarrantyEDate>@WarrantyEDate";
            //}
            //if (!String.IsNullOrEmpty(WarrantyType))
            //{
            //    sql += " and  (";

            //    for(int i=0; i< WarrantyType.Length;i++)
            //    {
            //        if(i==0)
            //        {
            //            sql += " WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
            //        }else
            //        {
            //            sql += " or WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
            //        }
            //    }
            //    sql += " )";
            //}

            //sql += " group by p.ProductNo,SerialNo";

            //if (Page != 0)
            //{
            //    sql += " limit @Page1,@Page2";
            //}

            //DataTable dtWarranty_D = dbMySQL.ExecuteDataTable(sql,
            //    new DbParameter[]{
            //        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,"%"+ProductNo+"%"),
            //        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,"%"+SerialNo+"%"),
            //        DataAccessMySQL.CreateParameter("TargetId",DbType.String,"%"+TargetId+"%"),
            //        DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,ChangeDate),
            //        DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,ChangeDate),
            //        DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
            //        DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
            //    });
            //if (dtWarranty_D.Rows.Count > 0)
            //{

            //    foreach (DataRow dr in dtWarranty_D.Rows)
            //    {
            //        Warranty_D obj = new Warranty_D();
            //        obj.ProductNo = dr["ProductNo"].ToString();
            //        obj.ProductName = dr["ProductName"].ToString();
            //        obj.SerialNo = dr["SerialNo"].ToString();
            //        obj.TargetId = dr["TargetId"].ToString();
            //        obj.TargetName = dr["TargetName"].ToString();
            //        if (dr["WarrantyType"].ToString()=="0")
            //        {
            //            obj.WarrantyType = "0.空白";
            //        }
            //        if (dr["WarrantyType"].ToString() == "1")
            //        {
            //            obj.WarrantyType = "1.廠商";
            //        }
            //        if (dr["WarrantyType"].ToString() == "2")
            //        {
            //            obj.WarrantyType = "2.客戶";
            //        }
            //        if (dr["WarrantyType"].ToString() == "3")
            //        {
            //            obj.WarrantyType = "3.合約";
            //        }
            //        obj.ChangeDate = dr["ChangeDate"].ToString();
            //        //if (dr["WarrantyType"].ToString()=="1")
            //        //{
            //        //    obj.SalesDate= dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //銷貨日期
            //        //}
            //        //if (dr["WarrantyType"].ToString() == "2")
            //        //{
            //        //    obj.PurchasesDate = dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");    //進貨日期
            //        //}
            //        obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            //        obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            //        objWarranty_D.Add(obj);
            //    }
            //    return objWarranty_D;
            //}

            //sql = @"select d.ProductNo,ProductName,SerialNo,max(ChangeDate) as ChangeDate,
            //    WarrantySDate,WarrantyEDate,WarrantyType,TargetId,TargetName from Warranty_D as d 
            //    left join product as p on p.ProductNo=d.ProductNo where 1=1";

            //if (!String.IsNullOrEmpty(ProductNo))
            //{
            //    sql += " and d.ProductNo like @ProductNo";
            //}
            //if (!String.IsNullOrEmpty(SerialNo))
            //{
            //    sql += " and SerialNo like @SerialNo";
            //}
            //if (!String.IsNullOrEmpty(TargetId))
            //{
            //    sql += " and TargetId like @TargetId";
            //}
            //if (!String.IsNullOrEmpty(WarrantyType))
            //{
            //    sql += " and  (";

            //    for (int i = 0; i < WarrantyType.Length; i++)
            //    {
            //        if (i == 0)
            //        {
            //            sql += " WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
            //        }
            //        else
            //        {
            //            sql += " or WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
            //        }
            //    }
            //    sql += " )";
            //}

            //sql += " group by p.ProductNo,SerialNo";

            //if (Page != 0)
            //{
            //    sql += " limit @Page1,@Page2";
            //}

            //dtWarranty_D = dbMySQL.ExecuteDataTable(sql,
            //    new DbParameter[]{
            //        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,"%"+ProductNo+"%"),
            //        DataAccessMySQL.CreateParameter("SerialNo",DbType.String,"%"+SerialNo+"%"),
            //        DataAccessMySQL.CreateParameter("TargetId",DbType.String,"%"+TargetId+"%"),
            //        DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
            //        DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
            //    });
            //if (dtWarranty_D.Rows.Count > 0)
            //{

            //    foreach (DataRow dr in dtWarranty_D.Rows)
            //    {
            //        Warranty_D obj = new Warranty_D();
            //        obj.ProductNo = dr["ProductNo"].ToString();
            //        obj.ProductName = dr["ProductName"].ToString();
            //        obj.SerialNo = dr["SerialNo"].ToString();
            //        obj.TargetId = dr["TargetId"].ToString();
            //        obj.TargetName = dr["TargetName"].ToString();
            //        if (dr["WarrantyType"].ToString() == "0")
            //        {
            //            obj.WarrantyType = "0.空白";
            //        }
            //        if (dr["WarrantyType"].ToString() == "1")
            //        {
            //            obj.WarrantyType = "1.廠商";
            //        }
            //        if (dr["WarrantyType"].ToString() == "2")
            //        {
            //            obj.WarrantyType = "2.客戶";
            //        }
            //        if (dr["WarrantyType"].ToString() == "3")
            //        {
            //            obj.WarrantyType = "3.合約";
            //        }
            //        obj.ChangeDate = dr["ChangeDate"].ToString();
            //        //if (dr["WarrantyType"].ToString() == "1")
            //        //{
            //        //    obj.SalesDate = dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //銷貨日期
            //        //}
            //        //if (dr["WarrantyType"].ToString() == "2")
            //        //{
            //        //    obj.PurchasesDate = dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");    //進貨日期
            //        //}
            //        obj.WarrantySDate = dr["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            //        obj.WarrantyEDate = dr["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            //        objWarranty_D.Add(obj);
            //    } 
            //}
            return objWarranty_D;
        }

        //public Warranty_D SearchSerialNoInfo(string SerialNo, string WarrantyType)
        //{
        //    DataAccessMySQL dbMySQL = new DataAccessMySQL();
        //    dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

        //    Warranty_D obj = null;

        //    string sql = @"select d.ProductNo,ProductName,SerialNo,ChangeDate,
        //        WarrantySDate,WarrantyEDate,WarrantyType,TargetId,TargetName from Warranty_D as d
        //        left join product as p on p.ProductNo=d.ProductNo where SerialNo like @SerialNo 
        //        and WarrantySDate<@WarrantySDate and WarrantyEDate>@WarrantyEDate";

        //    DataTable dtSaleSerial = dbMySQL.ExecuteDataTable(,
        //        new DbParameter[]{
        //            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,"%"+SerialNo+"%"),
        //            DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,ChangeDate),
        //            DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,ChangeDate)
        //        });

        //    if (dtSaleSerial.Rows.Count > 0)
        //    {
        //        obj = new Warranty_D();
        //        obj.ProductNo = dtSaleSerial.Rows[0]["ProductNo"].ToString();
        //        obj.ProductName = dtSaleSerial.Rows[0]["ProductName"].ToString();
        //        obj.SerialNo = dtSaleSerial.Rows[0]["SerialNo"].ToString();
        //        obj.TargetId = dtSaleSerial.Rows[0]["TargetId"].ToString();
        //        obj.TargetName = dtSaleSerial.Rows[0]["TargetName"].ToString();
        //        obj.WarrantyType = dtSaleSerial.Rows[0]["WarrantyType"].ToString();
        //        obj.ChangeDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString();
        //        //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "1")
        //        //{
        //        //    obj.SalesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //銷貨日期
        //        //}
        //        //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "2")
        //        //{
        //        //    obj.PurchasesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");    //進貨日期
        //        //}
        //        obj.WarrantySDate = dtSaleSerial.Rows[0]["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
        //        obj.WarrantyEDate = dtSaleSerial.Rows[0]["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
        //    }
        //    else
        //    {
        //        obj = new Warranty_D();
        //        dtSaleSerial = dbMySQL.ExecuteDataTable(@"select d.ProductNo,ProductName,SerialNo,max(ChangeDate) as ChangeDate,
        //        WarrantySDate,WarrantyEDate,WarrantyType,TargetId,TargetName from Warranty_D as d
        //        left join product as p on p.ProductNo=d.ProductNo where SerialNo like @SerialNo",
        //        new DbParameter[]{
        //            DataAccessMySQL.CreateParameter("SerialNo",DbType.String,"%"+SerialNo+"%")
        //        });
        //        if (dtSaleSerial.Rows.Count > 0)
        //        {
        //            obj.ProductNo = dtSaleSerial.Rows[0]["ProductNo"].ToString();
        //            obj.ProductName = dtSaleSerial.Rows[0]["ProductName"].ToString();
        //            obj.SerialNo = dtSaleSerial.Rows[0]["SerialNo"].ToString();
        //            obj.TargetId = dtSaleSerial.Rows[0]["TargetId"].ToString();
        //            obj.TargetName = dtSaleSerial.Rows[0]["TargetName"].ToString();
        //            obj.WarrantyType = dtSaleSerial.Rows[0]["WarrantyType"].ToString();
        //            obj.ChangeDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString();
        //            //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "1")
        //            //{
        //            //    obj.SalesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //銷貨日期
        //            //}
        //            //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "2")
        //            //{
        //            //    obj.PurchasesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");    //進貨日期
        //            //}
        //            obj.WarrantySDate = dtSaleSerial.Rows[0]["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
        //            obj.WarrantyEDate = dtSaleSerial.Rows[0]["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
        //        }
        //    }

        //    return obj;
        //}

        public Warranty_D SearchWarranty_DInfo(string ProductNo, string SerialNo, string ChangeDate, string WarrantyType,string TargetId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Warranty_D obj = null;

            string sql = @"select d.ProductNo,ProductName,SerialNo,ChangeDate,
                TargetId,TargetName,DataDate,DataTime,WarrantyPeriod,WarrantySDate,WarrantyEDate,WarrantyType from Warranty_D as d
                left join product as p on p.ProductNo=d.ProductNo where 1=1";

            if (!String.IsNullOrEmpty(ProductNo))
            {
                sql += " and d.ProductNo = @ProductNo";
            }
            if (!String.IsNullOrEmpty(SerialNo))
            {
                sql += " and SerialNo = @SerialNo";
            }
            if (!String.IsNullOrEmpty(TargetId))
            {
                sql += " and TargetId = @TargetId";
            }

            if (!String.IsNullOrEmpty(ChangeDate))
            {
                sql += " and WarrantySDate<@WarrantySDate and WarrantyEDate>@WarrantyEDate";
            }

            if (!String.IsNullOrEmpty(WarrantyType))
            {
                sql += " and  (";

                for (int i = 0; i < WarrantyType.Length; i++)
                {
                    if (i == 0)
                    {
                        sql += " WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
                    }
                    else
                    {
                        sql += " or WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
                    }
                }
                sql += " )";
            }

            DataTable dtSaleSerial = dbMySQL.ExecuteDataTable(sql+ " order by ChangeDate desc",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,ProductNo),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,SerialNo),
                    DataAccessMySQL.CreateParameter("TargetId",DbType.String,TargetId),
                    DataAccessMySQL.CreateParameter("WarrantySDate",DbType.String,ChangeDate),
                    DataAccessMySQL.CreateParameter("WarrantyEDate",DbType.String,ChangeDate),
                });

            if (dtSaleSerial.Rows.Count > 0)
            {
                obj = new Warranty_D();
                obj.ProductNo = dtSaleSerial.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtSaleSerial.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtSaleSerial.Rows[0]["SerialNo"].ToString();
                obj.TargetId = dtSaleSerial.Rows[0]["TargetId"].ToString();
                obj.TargetName = dtSaleSerial.Rows[0]["TargetName"].ToString();
                obj.WarrantyType = dtSaleSerial.Rows[0]["WarrantyType"].ToString();
                obj.WarrantyPeriod = dtSaleSerial.Rows[0]["WarrantyPeriod"].ToString();
                obj.ChangeDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "1")
                //{
                //    obj.SalesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //銷貨日期
                //}
                //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "2")
                //{
                //    obj.PurchasesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");    //進貨日期
                //}
                obj.WarrantySDate = dtSaleSerial.Rows[0]["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.WarrantyEDate = dtSaleSerial.Rows[0]["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            }
            else
            {
                sql = @"select d.ProductNo,ProductName,SerialNo,ChangeDate,
                TargetId,TargetName,DataDate,DataTime,WarrantyPeriod,WarrantySDate,WarrantyEDate,WarrantyType from Warranty_D as d
                left join product as p on p.ProductNo=d.ProductNo where 1=1";

                if (!String.IsNullOrEmpty(ProductNo))
                {
                    sql += " and d.ProductNo = @ProductNo";
                }
                if (!String.IsNullOrEmpty(SerialNo))
                {
                    sql += " and SerialNo = @SerialNo";
                }
                if (!String.IsNullOrEmpty(TargetId))
                {
                    sql += " and TargetId = @TargetId";
                }

                if (!String.IsNullOrEmpty(WarrantyType))
                {
                    sql += " and  (";

                    for (int i = 0; i < WarrantyType.Length; i++)
                    {
                        if (i == 0)
                        {
                            sql += " WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
                        }
                        else
                        {
                            sql += " or WarrantyType='" + WarrantyType.Substring(i, 1) + "'";
                        }
                    }
                    sql += " )";
                }

                dtSaleSerial = dbMySQL.ExecuteDataTable(sql+ " order by ChangeDate desc",
                    new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,ProductNo),
                    DataAccessMySQL.CreateParameter("SerialNo",DbType.String,SerialNo),
                    DataAccessMySQL.CreateParameter("TargetId",DbType.String,TargetId),
                    });

                if (dtSaleSerial.Rows.Count > 0)
                {
                    obj = new Warranty_D();
                    obj.ProductNo = dtSaleSerial.Rows[0]["ProductNo"].ToString();
                    obj.ProductName = dtSaleSerial.Rows[0]["ProductName"].ToString();
                    obj.SerialNo = dtSaleSerial.Rows[0]["SerialNo"].ToString();
                    obj.TargetId = dtSaleSerial.Rows[0]["TargetId"].ToString();
                    obj.TargetName = dtSaleSerial.Rows[0]["TargetName"].ToString();
                    obj.WarrantyType = dtSaleSerial.Rows[0]["WarrantyType"].ToString();
                    obj.WarrantyPeriod = dtSaleSerial.Rows[0]["WarrantyPeriod"].ToString();
                    obj.ChangeDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "1")
                    //{
                    //    obj.SalesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");     //銷貨日期
                    //}
                    //if (dtSaleSerial.Rows[0]["WarrantyType"].ToString() == "2")
                    //{
                    //    obj.PurchasesDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");    //進貨日期
                    //}
                    obj.WarrantySDate = dtSaleSerial.Rows[0]["WarrantySDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantySDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.WarrantyEDate = dtSaleSerial.Rows[0]["WarrantyEDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["WarrantyEDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                }
            }
            return obj;
        }

        public List<Warranty_D> SearchWarranty(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Warranty_D> objWarranty_D = new List<Warranty_D>();

            string sql  = @"select d.*,p.ProductName,c.TargetName from Warranty_D as d 
                left join Product as p on p.ProductNo=d.ProductNo
                left join Customer as c on c.CustomerId=d.TargetId where WarrantyType in ('2','3')";

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

            DbParameter[] dbParameters = new DbParameter[len - 1];
            int dblen = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                if (colArray[i] == "CustomerNo")
                    colArray[i] = "TargetId";
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

            DataTable dtSaleSerial = null;
            if (strCondition != "")
            {

                dtSaleSerial = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSaleSerial = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }

            if (dtSaleSerial.Rows.Count > 0)
            {
                Warranty_D obj = new Warranty_D();
                obj.ProductNo = dtSaleSerial.Rows[0]["ProductNo"].ToString();
                obj.ProductName = dtSaleSerial.Rows[0]["ProductName"].ToString();
                obj.SerialNo = dtSaleSerial.Rows[0]["SerialNo"].ToString();
                obj.TargetId = dtSaleSerial.Rows[0]["TargetId"].ToString();
                obj.TargetName = dtSaleSerial.Rows[0]["TargetName"].ToString();
                obj.ChangeDate = dtSaleSerial.Rows[0]["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dtSaleSerial.Rows[0]["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Remark = dtSaleSerial.Rows[0]["Remark"].ToString();
                objWarranty_D.Add(obj);
            }
            return objWarranty_D;
        }
    }
}