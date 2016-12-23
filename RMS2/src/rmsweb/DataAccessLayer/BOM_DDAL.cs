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
    public class BOM_DDAL : IBOM_DDAL
    {
        public List<BOM_D> SearchBOM_D(BOM_D bOM_D, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<BOM_D> objBOM_D = new List<BOM_D>();

            string sql = @"select d.*,p.ProductName,p.Specification,p.Unit from BOM_D as d left join Product as p on d.ComponentNo=p.ProductNo where 1=1";

            if(!String.IsNullOrEmpty(bOM_D.MajorComponentNo))
            {
                sql += " and MajorComponentNo=@MajorComponentNo";
            }

            if (!String.IsNullOrEmpty(bOM_D.ComponentNo))
            {
                sql += " and p.ComponentNo=@ComponentNo";
            }

            if (!String.IsNullOrEmpty(bOM_D.ProductName))
            {
                sql += " and p.ProductName=@ProductName";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtBOM_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_D.MajorComponentNo),
                    DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,bOM_D.ComponentNo),
                    DataAccessMySQL.CreateParameter("ProductName",DbType.String,bOM_D.ProductName),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                }); ;

            if (dtBOM_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBOM_D.Rows)
                {
                    BOM_D obj = new BOM_D();
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ComponentNo = dr["ComponentNo"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString()==""?"0": dr["QTY"].ToString());                  
                    obj.Remark = dr["Remark"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();

                    objBOM_D.Add(obj);
                }
            }

            return objBOM_D;
        }

        public BOM_D SearchBOM_DInfo(string ComponentNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            BOM_D obj = null;

            string sql = @"select d.*,p.ProductName,p.Specification,p.Unit from BOM_D as d left join Product as p on d.ComponentNo=p.ProductNo where ComponentNo=@ComponentNo";

            DataTable dtBOM_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,ComponentNo)
                }); 

            if (dtBOM_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBOM_D.Rows)
                {
                    obj = new BOM_D();
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ComponentNo = dr["ComponentNo"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString() == "" ? "0" : dr["QTY"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();

                }
            }

            return obj;
        }

        public bool IsBOM_D(BOM_D bOM_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if(int.Parse(dbMySQL.ExecuteScalar("select count(*) from BOM_D where MajorComponentNo=@MajorComponentNo and ComponentNo=@ComponentNo",
                new DbParameter[] {
                    DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_D.MajorComponentNo),
                    DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,bOM_D.ComponentNo)
                }).ToString())>0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public List<BOM_D> SearchBOMD(string MajorComponentNo, string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<BOM_D> objBOM_D = new List<BOM_D>();

            string sql = @"select d.*,p.ProductName,p.Specification,p.Unit from BOM_D as d left join Product as p on d.ComponentNo=p.ProductNo where MajorComponentNo='"+ MajorComponentNo + "'";

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

            DataTable dtBOM_D = null;
            if (strCondition != "")
            {
                
                dtBOM_D = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtBOM_D = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }

            //DataTable dtBOM_D = dbMySQL.ExecuteDataTable(sql,
            //    new DbParameter[]{
            //        DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
            //        DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
            //    }); ;

            if (dtBOM_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBOM_D.Rows)
                {
                    BOM_D obj = new BOM_D();
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ComponentNo = dr["ComponentNo"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString() == "" ? "0" : dr["QTY"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();

                    objBOM_D.Add(obj);
                }
            }

            return objBOM_D;
        }

        public List<BOM_D> SearchBOMDInfo(string Col, string Condition, string conditionValue)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            List<BOM_D> objBOM_D = new List<BOM_D>();

            string sql = @"select d.*,p.ProductName as MajorComponentName,pr.ProductName as ComponentName,
                (select count(*) from RGA_H where StatusCode not in ('00','05','10') and 
                ProductNo=d.MajorComponentNo) as RGAHsum,
                (select count(*) from RGA_D as rd left join RGA_H as rh on 
                rd.RGAType=rh.RGAType and rd.RGANo=rh.RGANo where StatusCode 
                not in ('00','05','10') and rh.ProductNo=d.MajorComponentNo and rd.PartNo=d.ComponentNo) as RGADsum,
                (select sum(QTY) from RGA_D as rd left join RGA_H as rh on 
                rd.RGAType=rh.RGAType and rd.RGANo=rh.RGANo where StatusCode 
                not in ('00','05','10') and rh.ProductNo=d.MajorComponentNo and rd.PartNo=d.ComponentNo) as RGADQTY
                 from BOM_D as d
                left join product as p on p.ProductNo=d.MajorComponentNo
                left join product as pr on pr.ProductNo=d.ComponentNo where 1=1";

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


            DataTable dtBOM_D = null;
            if (strCondition != "")
            {
                dtBOM_D = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtBOM_D = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtBOM_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBOM_D.Rows)
                {
                    BOM_D obj = new BOM_D();
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.MajorComponentName = dr["MajorComponentName"].ToString();
                    obj.ComponentNo = dr["ComponentNo"].ToString();
                    obj.ComponentName = dr["ComponentName"].ToString();
                    obj.RGAHsum = dr["RGAHsum"].ToString();
                    obj.RGADsum = dr["RGADsum"].ToString();
                    obj.RGADQTY = dr["RGADQTY"].ToString();
                    objBOM_D.Add(obj);
                }
            }

            return objBOM_D;
        }
    }
}