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
    public class BrandDAL : IBrandDAL
    {
        public string GetBrandName(string BrandId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            object BrandName = dbMySQL.ExecuteScalar("select brand from Brand where BrandId=@BrandId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("BrandId",DbType.String,BrandId)
                });
            if (BrandName!=null)
            {
                return "YES-"+BrandName.ToString();
            }else
            {
                return "NO-品牌不存在!";
            }
            
        }

        public bool IsModuleId(string brandId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from Brand where BrandId=@BrandId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("BrandId",DbType.String,brandId)
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

        public List<Brand> GetBrand(Brand brand, string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Brand> objBrand = new List<Brand>();

            string sql = @"select BrandId,Brand,Remark from brand where 1=1";

            //if (brand.BrandId != "" && brand.BrandId != null)
            //{
            //    sql += " and BrandId like @BrandId";
            //}
            //if (brand.BrandName != "" && brand.BrandName != null)
            //{
            //    sql += " and Brand like @Brand";
            //}

            //if(Page!=0)
            //{
            //    sql += " limit @Page1,@Page2";
            //}

            //DataTable dtBrand = dbMySQL.ExecuteDataTable(sql,
            //    new DbParameter[]{
            //        DataAccessMySQL.CreateParameter("BrandId",DbType.String,"%"+brand.BrandId+"%"),
            //        DataAccessMySQL.CreateParameter("Brand",DbType.String,"%"+brand.BrandName+"%"),
            //        DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
            //        DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
            //    });
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

            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            DataTable dtBrand = null;
            if (strCondition != "")
            {

                dtBrand = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtBrand = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            if (dtBrand.Rows.Count > 0)
            {

                foreach (DataRow dr in dtBrand.Rows)
                {
                    Brand obj = new Brand();
                    obj.BrandId = dr["BrandId"].ToString();
                    obj.BrandName = dr["Brand"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objBrand.Add(obj);
                }
            }

            return objBrand;
        }

        public bool AddBrand(Brand brand, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from brand where 
                    BrandId=@BrandId ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("BrandId",DbType.String,brand.BrandId)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into Brand (BrandId,Brand,Remark) 
                        VALUES (@BrandId,@Brand,@Remark)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("BrandId",DbType.String,brand.BrandId.Trim()),
                            DataAccessMySQL.CreateParameter("Brand",DbType.String,brand.BrandName.Trim()),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,brand.Remark.Trim())
                        });
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "品牌代號已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool UpdateBrand(Brand brand)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update brand set Brand=@Brand,Remark=@Remark where 
                    BrandId=@BrandId ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("BrandId",DbType.String,brand.BrandId.Trim()),
                        DataAccessMySQL.CreateParameter("Brand",DbType.String,brand.BrandName.Trim()),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,brand.Remark.Trim())
                    });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteBrand(string BrandId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            dbMySQL.ExecuteNonQuery("delete from brand where BrandId=@BrandId ",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("BrandId",DbType.String,BrandId)
                });

        }

        public Brand GetBrand(string BrandId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Brand brand = new Brand();

            DataTable dtBrand = dbMySQL.ExecuteDataTable(@"select BrandId,Brand,Remark from brand where BrandId=@BrandId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("BrandId",DbType.String,BrandId)
                });

            if (dtBrand != null && dtBrand.Rows.Count > 0)
            {
                brand.BrandId = dtBrand.Rows[0]["BrandId"].ToString();
                brand.BrandName = dtBrand.Rows[0]["Brand"].ToString();
                brand.Remark = dtBrand.Rows[0]["Remark"].ToString();
                return brand;
            }
            else
            {
                return null;
            }
        }

        public List<Brand> SearchBrand(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Brand> objBrand = new List<Brand>();

            string sql = @"select BrandId,Brand,Remark from brand where 1=1";

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


            DataTable dtBrand = null;
            if (strCondition != "")
            {
                dtBrand = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtBrand = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtBrand.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBrand.Rows)
                {
                    Brand obj = new Brand();
                    obj.BrandId = dr["BrandId"].ToString();
                    obj.BrandName = dr["Brand"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objBrand.Add(obj);
                }
            }

            return objBrand;
        }
    }
}