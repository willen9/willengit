using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class ProductCategoryDAL : IProductCategoryDAL
    {
        public List<ProductCategory> GetProductCategoryALL(ProductCategory productCategory, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ProductCategory> objProductCategory = new List<ProductCategory>();

            string sql = @"select ProductCategoryType,ProductTypeId,ProductType from productcategory where 1=1";

            if (productCategory.ProductCategoryType != "" && productCategory.ProductCategoryType != null)
            {
                sql += " and ProductCategoryType like @ProductCategoryType";
            }
            if(productCategory.ProductTypeId!=""&&productCategory.ProductTypeId!=null)
            {
                sql += " and ProductTypeId like @ProductTypeId";
            }
            if(productCategory.ProductType!=""&&productCategory.ProductType!=null)
            {
                sql += " and ProductType like @ProductType";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtProductCategory = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,"%"+productCategory.ProductCategoryType+"%"),
                    DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,"%"+productCategory.ProductTypeId+"%"),
                    DataAccessMySQL.CreateParameter("ProductType",DbType.String,"%"+productCategory.ProductType+"%"),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });
            if (dtProductCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProductCategory.Rows)
                {
                    ProductCategory obj = new ProductCategory();
                    obj.ProductCategoryType = dr["ProductCategoryType"].ToString();
                    obj.ProductTypeId = dr["ProductTypeId"].ToString();
                    obj.ProductType = dr["ProductType"].ToString();
                    objProductCategory.Add(obj);
                }
            }

            return objProductCategory;
        }


        public List<ProductCategory> SearchProductCategory(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<ProductCategory> objProductCategory = new List<ProductCategory>();

            string sql = @"select ProductCategoryType,ProductTypeId,ProductType from productcategory where 1=1";

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


            DataTable dtProductCategory = null;
            if (strCondition != "")
            {
                dtProductCategory = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }else
            {
                dtProductCategory = dbMySQL.ExecuteDataTable(sql);
            }
            
            if (dtProductCategory.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProductCategory.Rows)
                {
                    ProductCategory obj = new ProductCategory();
                    obj.ProductCategoryType = dr["ProductCategoryType"].ToString();
                    obj.ProductTypeId = dr["ProductTypeId"].ToString();
                    obj.ProductType = dr["ProductType"].ToString();
                    objProductCategory.Add(obj);
                }
            }

            return objProductCategory;
        }

        public bool IsProductTypeId(string ProductCategoryType, string ProductTypeId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar(@"select count(*) from productcategory where 
                    ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,ProductCategoryType),
                    DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,ProductTypeId)
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

        public bool AddProductCategory(ProductCategory productCategory,out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from productcategory where 
                    ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,productCategory.ProductCategoryType),
                        DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,productCategory.ProductTypeId)
                    }).ToString();
                if(int.Parse(count)==0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into productcategory (ProductCategoryType,ProductTypeId,ProductType,CreateDate) 
                        VALUES (@ProductCategoryType,@ProductTypeId,@ProductType,@CreateDate)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,productCategory.ProductCategoryType),
                            DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,productCategory.ProductTypeId),
                            DataAccessMySQL.CreateParameter("ProductType",DbType.String,productCategory.ProductType),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd"))
                        });
                    msg = "";
                    return true;
                }else
                {
                    msg = "分類方式和類別代號已存在！";
                    return false;
                }
                
            }catch(Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool UpdateProductCategory(ProductCategory productCategory, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {

                string CategoryType = "";

                if (productCategory.ProductCategoryType == "類別一")
                {
                    CategoryType = "1";
                }
                else if (productCategory.ProductCategoryType == "類別二")
                {
                    CategoryType = "2";
                }
                else if (productCategory.ProductCategoryType == "類別三")
                {
                    CategoryType = "3";
                }
                else if (productCategory.ProductCategoryType == "類別四")
                {
                    CategoryType = "4";
                }

                dbMySQL.ExecuteNonQuery(@"update productcategory set ProductType=@ProductType where 
                    ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProductType",DbType.String,productCategory.ProductType),
                        DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,CategoryType),
                        DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,productCategory.ProductTypeId)
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

        public void DeleteProductCategory(string ProductCategoryType, string ProductTypeId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            dbMySQL.ExecuteNonQuery("delete from productcategory where ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,ProductCategoryType),
                    DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,ProductTypeId)
                });

        }

        public ProductCategory GetProductCategory(string ProductCategoryType, string ProductTypeId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            ProductCategory objProductCategory = new ProductCategory();

            DataTable dtProductCategory = dbMySQL.ExecuteDataTable("select * from productcategory where ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,ProductCategoryType),
                    DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,ProductTypeId)
                });

            if (dtProductCategory != null && dtProductCategory.Rows.Count>0)
            {
                objProductCategory.ProductCategoryType = dtProductCategory.Rows[0]["ProductCategoryType"].ToString();
                objProductCategory.ProductTypeId = dtProductCategory.Rows[0]["ProductTypeId"].ToString();
                objProductCategory.ProductType = dtProductCategory.Rows[0]["ProductType"].ToString();

                return objProductCategory;
            }
            else
            {
                return null;
            }
        }

        public string GetProductType(string ProductCategoryType, string ProductTypeId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            object ProductType = dbMySQL.ExecuteScalar("select ProductType from productcategory where ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductCategoryType",DbType.String,ProductCategoryType),
                    DataAccessMySQL.CreateParameter("ProductTypeId",DbType.String,ProductTypeId)
                });

            if(ProductType!=null)
            {
                return "YES-" + ProductType.ToString();
            }else
            {
                return "NO-類別不存在！";
            }
        }

        public bool ImportFile(string path)
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
                            if (valueArray.Length < 3)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from productcategory where ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductCategoryType", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId", DbType.String, valueArray[1].Trim())
                                }).ToString()) > 0)
                        {
                            dataAccessMySql.ExecuteNonQuery(@"update productcategory set ProductType=@ProductType where ProductCategoryType=@ProductCategoryType and ProductTypeId=@ProductTypeId",
                            tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductCategoryType", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductType", DbType.String,valueArray[2].Trim())
                                });
                        }
                        else
                        {
                            dataAccessMySql.ExecuteNonQuery(
                                @"insert into productcategory(ProductCategoryType,ProductTypeId,ProductType) values
                                (@ProductCategoryType,@ProductTypeId,@ProductType)",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductCategoryType", DbType.String,valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductType", DbType.String, valueArray[2].Trim())
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