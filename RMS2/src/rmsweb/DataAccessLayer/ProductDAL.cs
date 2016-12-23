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
    public class ProductDAL : IProductDAL
    {
        public List<Product> GetProduct(Product product, string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Product> objProduct = new List<Product>();

            string sql = @"select ProductNo,ProductName,Specification,Unit,ProductTypeId1,productType as ProductTypeId1Name,
                product.VendorId,Vendor.VendorName from product left join Vendor on product.VendorId=Vendor.VendorId
                left join ProductCategory on ProductTypeId=product.ProductTypeId1 and ProductCategoryType='1'  where  1=1";

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



            DataTable dtProduct = null;
            if (strCondition != "")
            {

                dtProduct = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtProduct = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }

            if (dtProduct.Rows.Count > 0)
            {

                foreach (DataRow dr in dtProduct.Rows)
                {
                    Product obj = new Product();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.ProductTypeId1 = dr["ProductTypeId1"].ToString();
                    obj.ProductTypeId1Name = dr["ProductTypeId1Name"].ToString();
                    obj.VendorId = dr["VendorId"].ToString();
                    obj.VendorName = dr["VendorName"].ToString();
                    objProduct.Add(obj);
                }
            }

            return objProduct;
        }

        public List<Product> SearchProductCategory(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Product> objProduct = new List<Product>();

            string sql = @"select ProductNo,ProductName,Specification,Unit,ProductTypeId1,productType as ProductTypeId1Name,
                product.VendorId,Vendor.VendorName from product left join Vendor on product.VendorId=Vendor.VendorId
                left join ProductCategory on ProductTypeId=product.ProductTypeId1  and ProductCategoryType='1'  where  1=1";

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


            DataTable dtProduct = null;
            if (strCondition != "")
            {
                dtProduct = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtProduct = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtProduct.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProduct.Rows)
                {
                    Product obj = new Product();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.ProductTypeId1 = dr["ProductTypeId1"].ToString();
                    obj.ProductTypeId1Name = dr["ProductTypeId1Name"].ToString();
                    obj.VendorId = dr["VendorId"].ToString();
                    obj.VendorName = dr["VendorName"].ToString();
                    objProduct.Add(obj);
                }
            }

            return objProduct;
        }

        public bool IsProductNo(string ProductNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("select count(*) from product where ProductNo=@ProductNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,ProductNo)
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

        public bool AddProduct(Product product, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from product where ProductNo=@ProductNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,product.ProductNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into product (ProductNo,ProductName,Specification,ProductTypeId1,ProductTypeId2,
                        ProductTypeId3,ProductTypeId4,Explanation,VendorId,SerialControl,Unit,BrandId,CreateDate) 
                        VALUES (@ProductNo,@ProductName,@Specification,@ProductTypeId1,@ProductTypeId2,
                        @ProductTypeId3,@ProductTypeId4,@Explanation,@VendorId,@SerialControl,@Unit,@BrandId,@CreateDate)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("ProductNo",DbType.String,product.ProductNo),
                            DataAccessMySQL.CreateParameter("ProductName",DbType.String,product.ProductName),
                            DataAccessMySQL.CreateParameter("Specification",DbType.String,product.Specification),
                            DataAccessMySQL.CreateParameter("ProductTypeId1",DbType.String,product.ProductTypeId1),
                            DataAccessMySQL.CreateParameter("ProductTypeId2",DbType.String,product.ProductTypeId2),
                            DataAccessMySQL.CreateParameter("ProductTypeId3",DbType.String,product.ProductTypeId3),
                            DataAccessMySQL.CreateParameter("ProductTypeId4",DbType.String,product.ProductTypeId4),
                            DataAccessMySQL.CreateParameter("Explanation",DbType.String,product.Explanation),
                            DataAccessMySQL.CreateParameter("VendorId",DbType.String,product.VendorId),
                            DataAccessMySQL.CreateParameter("SerialControl",DbType.String,product.SerialControl),
                            DataAccessMySQL.CreateParameter("Unit",DbType.String,product.Unit),
                            DataAccessMySQL.CreateParameter("BrandId",DbType.String,product.BrandId),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd"))
                        });
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "品號已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool UpdateProduct(Product product, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update product set ProductName=@ProductName,Specification=@Specification,SerialControl=@SerialControl,
                    ProductTypeId1=@ProductTypeId1,ProductTypeId2=@ProductTypeId2,ProductTypeId3=@ProductTypeId3,
                    ProductTypeId4=@ProductTypeId4,Explanation=@Explanation,VendorId=@VendorId,Unit=@Unit,BrandId=@BrandId where 
                    ProductNo=@ProductNo ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("ProductName",DbType.String,product.ProductName),
                        DataAccessMySQL.CreateParameter("Specification",DbType.String,product.Specification),
                        DataAccessMySQL.CreateParameter("ProductTypeId1",DbType.String,product.ProductTypeId1),
                        DataAccessMySQL.CreateParameter("ProductTypeId2",DbType.String,product.ProductTypeId2),
                        DataAccessMySQL.CreateParameter("ProductTypeId3",DbType.String,product.ProductTypeId3),
                        DataAccessMySQL.CreateParameter("ProductTypeId4",DbType.String,product.ProductTypeId4),
                        DataAccessMySQL.CreateParameter("Explanation",DbType.String,product.Explanation),
                        DataAccessMySQL.CreateParameter("VendorId",DbType.String,product.VendorId),
                        DataAccessMySQL.CreateParameter("Unit",DbType.String,product.Unit),
                        DataAccessMySQL.CreateParameter("BrandId",DbType.String,product.BrandId),
                        DataAccessMySQL.CreateParameter("ProductNo",DbType.String,product.ProductNo),
                        DataAccessMySQL.CreateParameter("SerialControl",DbType.String,product.SerialControl),
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

        public void DeleteProduct(string ProductNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            dbMySQL.ExecuteNonQuery("delete from product where ProductNo=@ProductNo",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,ProductNo)
                });

        }

        public Product GetProductInfo(string ProductNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Product product = null;

            DataTable dtProduct = dbMySQL.ExecuteDataTable(@"select ProductNo,ProductName,Specification,Unit,ProductTypeId1,
                p.VendorId,Vendor.VendorName,p.SerialControl,p.Explanation,
                p.BrandId,p1.productType as ProductTypeId1Name,p.ProductTypeId1,
                p2.productType as ProductTypeId2Name,p.ProductTypeId2,
                p3.productType as ProductTypeId3Name,p.ProductTypeId3,
                p4.productType as ProductTypeId4Name,p.ProductTypeId4,
                b.Brand as BrandName
                from product as p 
                left join Vendor on p.VendorId=Vendor.VendorId 
                left join ProductCategory as p1 on p1.ProductTypeId=p.ProductTypeId1 and p1.ProductCategoryType='1'
                left join ProductCategory as p2 on p2.ProductTypeId=p.ProductTypeId2 and p2.ProductCategoryType='2'
                left join ProductCategory as p3 on p3.ProductTypeId=p.ProductTypeId3 and p3.ProductCategoryType='3'
                left join ProductCategory as p4 on p4.ProductTypeId=p.ProductTypeId4 and p4.ProductCategoryType='4'
                left join Brand as b on b.BrandId=p.BrandId where p.ProductNo=@ProductNo;
                    ",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,ProductNo)
                });

            if(dtProduct!=null&&dtProduct.Rows.Count>0)
            {
                product = new Product();
                product.ProductNo = dtProduct.Rows[0]["ProductNo"].ToString();
                product.ProductName = dtProduct.Rows[0]["ProductName"].ToString();
                product.Specification = dtProduct.Rows[0]["Specification"].ToString();
                product.ProductTypeId1 = dtProduct.Rows[0]["ProductTypeId1"].ToString();
                product.ProductTypeId1Name = dtProduct.Rows[0]["ProductTypeId1Name"].ToString();
                product.ProductTypeId2 = dtProduct.Rows[0]["ProductTypeId2"].ToString();
                product.ProductTypeId2Name = dtProduct.Rows[0]["ProductTypeId2Name"].ToString();
                product.ProductTypeId3 = dtProduct.Rows[0]["ProductTypeId3"].ToString();
                product.ProductTypeId3Name = dtProduct.Rows[0]["ProductTypeId3Name"].ToString();
                product.ProductTypeId4 = dtProduct.Rows[0]["ProductTypeId4"].ToString();
                product.ProductTypeId4Name = dtProduct.Rows[0]["ProductTypeId4Name"].ToString();
                product.SerialControl = dtProduct.Rows[0]["SerialControl"].ToString();
                product.Explanation = dtProduct.Rows[0]["Explanation"].ToString();
                product.VendorId = dtProduct.Rows[0]["VendorId"].ToString();
                product.Unit = dtProduct.Rows[0]["Unit"].ToString();
                product.VendorName = dtProduct.Rows[0]["VendorName"].ToString();
                product.BrandId = dtProduct.Rows[0]["BrandId"].ToString();
                product.BrandName = dtProduct.Rows[0]["BrandName"].ToString();
            }

            return product;
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
                            if (valueArray.Length < 10)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from product where ProductNo=@ProductNo",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0].Trim())
                                }).ToString()) > 0)
                        {
                            dataAccessMySql.ExecuteNonQuery(@"update product set ProductName=@ProductName,Specification=@Specification,BrandId=@BrandId,
                            Unit=@Unit,ProductTypeId1=@ProductTypeId1,ProductTypeId2=@ProductTypeId2,ProductTypeId3=@ProductTypeId3,
                            ProductTypeId4=@ProductTypeId4,VendorId=@VendorId,SerialControl=@SerialControl,Explanation=@Explanation  where ProductNo=@ProductNo",
                            tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("Specification", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Unit", DbType.String,valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId1", DbType.String, valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId2", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId3", DbType.String, valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId4", DbType.String, valueArray[7].Trim()),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[8].Trim()),
                                    DataAccessMySQL.CreateParameter("SerialControl", DbType.String,valueArray[9].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("Explanation",DbType.String,valueArray[10].Trim()),
                                    DataAccessMySQL.CreateParameter("BrandId",DbType.String,valueArray[11].Trim())
                                });
                        }
                        else
                        {
                            dataAccessMySql.ExecuteNonQuery(
                                @"insert into product(ProductNo,ProductName,Specification,Unit,ProductTypeId1,
                                ProductTypeId2,ProductTypeId3,ProductTypeId4,VendorId,SerialControl,Explanation,BrandId) values
                                (@ProductNo,@ProductName,@Specification,@Unit,@ProductTypeId1,@ProductTypeId2,
                                @ProductTypeId3,@ProductTypeId4,@VendorId,@SerialControl,@Explanation,@BrandId)",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String,valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("Specification", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Unit", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId1", DbType.String, valueArray[4].Trim()),                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
                                    DataAccessMySQL.CreateParameter("ProductTypeId2", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId3", DbType.String, valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("ProductTypeId4", DbType.String,valueArray[7].Trim()),
                                    DataAccessMySQL.CreateParameter("VendorId", DbType.String, valueArray[8].Trim()),
                                    DataAccessMySQL.CreateParameter("SerialControl", DbType.String, valueArray[9].Trim()),
                                    DataAccessMySQL.CreateParameter("Explanation",DbType.String,valueArray[10].Trim()),
                                    DataAccessMySQL.CreateParameter("BrandId",DbType.String,valueArray[11].Trim())
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

    }
}