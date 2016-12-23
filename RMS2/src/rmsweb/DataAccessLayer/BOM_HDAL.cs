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
    public class BOM_HDAL : IBOM_HDAL
    {
        public bool AddBOM_H(BOM_H bOM_H, string strItemId,
            string strComponentNo, string strQTY, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                
                string count = dbMySQL.ExecuteScalar(@"select count(*) from BOM_H where 
                    MajorComponentNo=@MajorComponentNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    tran = dbMySQL.CreateDbTransaction();
                    dbMySQL.ExecuteNonQuery(@"insert into BOM_H (Company,UserGroup,Creator,CreateDate,
                        MajorComponentNo,MajorComponentName,Specification,Unit,StandardQTY,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @MajorComponentNo,@MajorComponentName,@Specification,@Unit,@StandardQTY,@Remark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,bOM_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,bOM_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,bOM_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentName",DbType.String,bOM_H.MajorComponentName),
                            DataAccessMySQL.CreateParameter("Specification",DbType.String,bOM_H.Specification),
                            DataAccessMySQL.CreateParameter("Unit",DbType.String,bOM_H.Unit),
                            DataAccessMySQL.CreateParameter("StandardQTY",DbType.Double,bOM_H.StandardQTY),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,bOM_H.Remark)
                        });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strComponentNoArray = strComponentNo.Split(',');
                        string[] strQTYArray = strQTY.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into BOM_D (Company,UserGroup,Creator,
                        CreateDate,MajorComponentNo,ItemId,ComponentNo,QTY,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@MajorComponentNo,@ItemId,@ComponentNo,@QTY,@Remark)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,bOM_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,bOM_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,bOM_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,strComponentNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                            });
                        }
                    }                        
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "資料已存在！";
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
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        public bool UpdateBOM_H(BOM_H bOM_H, string strItemId,
            string strComponentNo, string strQTY, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update BOM_H set Modifier=@Modifier,ModiDate=@ModiDate,MajorComponentName=@MajorComponentName,Specification=@Specification,
                         Unit=@Unit,StandardQTY=@StandardQTY,Remark=@Remark,Confirmed=@Confirmed
                         where MajorComponentNo=@MajorComponentNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,bOM_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                            DataAccessMySQL.CreateParameter("MajorComponentName",DbType.String,bOM_H.MajorComponentName),
                            DataAccessMySQL.CreateParameter("Specification",DbType.String,bOM_H.Specification),
                            DataAccessMySQL.CreateParameter("Unit",DbType.String,bOM_H.Unit),
                            DataAccessMySQL.CreateParameter("StandardQTY",DbType.Double,bOM_H.StandardQTY),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,bOM_H.Remark),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,bOM_H.Confirmed)
                        });

                dbMySQL.ExecuteNonQuery("delete from BOM_D where MajorComponentNo=@MajorComponentNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo)
                    });
                string sql = "";

                if (strItemId != "" && strItemId != null)
                {
                    string[] strItemIdArray = strItemId.Split(',');
                    string[] strComponentNoArray = strComponentNo.Split(',');
                    string[] strQTYArray = strQTY.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into BOM_D (Company,UserGroup,Creator,
                        CreateDate,MajorComponentNo,ItemId,ComponentNo,QTY,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@MajorComponentNo,@ItemId,@ComponentNo,@QTY,@Remark)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,bOM_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,bOM_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,bOM_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,strComponentNoArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("QTY",DbType.Double,double.Parse(strQTYArray[i].ToString()==""?"0":strQTYArray[i].ToString())),
                                DataAccessMySQL.CreateParameter("Remark",DbType.String,strRemarkArray[i].ToString())
                            });
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

        public bool DelBOM_H(BOM_H bOM_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from BOM_H where MajorComponentNo=@MajorComponentNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from BOM_D where MajorComponentNo=@MajorComponentNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo)
                        });
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

        public List<BOM_H> SearchBOM_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<BOM_H> objBOM_H = new List<BOM_H>();

            string sql = @"select * from BOM_H  where 1=1";

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


            DataTable dtBOM_H = null;
            if (strCondition != "")
            {
                dtBOM_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtBOM_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtBOM_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBOM_H.Rows)
                {
                    BOM_H obj = new BOM_H();
                    obj.CreateDate = dr["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); 
                    obj.Confirmor = dr["Confirmor"].ToString();
                    obj.ConfirmDate = dr["ConfirmDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); 
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.MajorComponentName = dr["MajorComponentName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.StandardQTY = double.Parse(dr["StandardQTY"].ToString()==""?"0": dr["StandardQTY"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.Confirmed = dr["Confirmed"].ToString();
                    objBOM_H.Add(obj);
                }
            }

            return objBOM_H;
        }

        public BOM_H GetBOM_HInfo(BOM_H bOM_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            BOM_H obj = null;

            DataTable dtBOM_H = dbMySQL.ExecuteDataTable(@"select * from BOM_H where 
                    MajorComponentNo=@MajorComponentNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo)
                    });

            if (dtBOM_H.Rows.Count > 0)
            {
                obj = new BOM_H();
                obj.CreateDate = dtBOM_H.Rows[0]["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dtBOM_H.Rows[0]["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.ModiDate = dtBOM_H.Rows[0]["ModiDate"].ToString() == "" ? "" : DateTime.ParseExact(dtBOM_H.Rows[0]["ModiDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.Confirmor = dtBOM_H.Rows[0]["Confirmor"].ToString();
                obj.ConfirmDate = dtBOM_H.Rows[0]["ConfirmDate"].ToString() == "" ? "" : DateTime.ParseExact(dtBOM_H.Rows[0]["ConfirmDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.MajorComponentNo = dtBOM_H.Rows[0]["MajorComponentNo"].ToString();
                obj.MajorComponentName = dtBOM_H.Rows[0]["MajorComponentName"].ToString();
                obj.Specification = dtBOM_H.Rows[0]["Specification"].ToString();
                obj.Unit = dtBOM_H.Rows[0]["Unit"].ToString();
                obj.StandardQTY = double.Parse(dtBOM_H.Rows[0]["StandardQTY"].ToString()==""?"0":dtBOM_H.Rows[0]["StandardQTY"].ToString());
                obj.Remark = dtBOM_H.Rows[0]["Remark"].ToString();
                obj.Confirmed = dtBOM_H.Rows[0]["Confirmed"].ToString();
            }

            return obj;
        }

        public bool IsBOM_H(BOM_H bOM_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from BOM_H where 
                    MajorComponentNo=@MajorComponentNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo)
                    }).ToString()) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ConfirmedBOM_H(BOM_H bOM_H,out string msg)
        {
            try
            {
                DataAccessMySQL dbMySQL = new DataAccessMySQL();
                dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

                //if(bOM_H.Confirmed=="Y")
                //{
                //    dbMySQL.ExecuteNonQuery("update BOM_H set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate WHERE MajorComponentNo=@MajorComponentNo",
                //        new DbParameter[] {
                //            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                //            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,bOM_H.Confirmed),
                //            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,bOM_H.Confirmor),
                //            DataAccessMySQL.CreateParameter("ConfirmDate",DbType.String,DateTime.Now.ToString("yyyyMMdd"))
                //        });
                //}else
                //{
                //    dbMySQL.ExecuteNonQuery("update BOM_H set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate WHERE MajorComponentNo=@MajorComponentNo",
                //        new DbParameter[] {
                //            DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                //            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,bOM_H.Confirmed),
                //            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,""),
                //            DataAccessMySQL.CreateParameter("ConfirmDate",DbType.String,"")
                //        });
                //}

                dbMySQL.ExecuteNonQuery("update BOM_H set Confirmed=@Confirmed,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate WHERE MajorComponentNo=@MajorComponentNo",
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,bOM_H.Confirmed),
                        DataAccessMySQL.CreateParameter("Confirmor",DbType.String,bOM_H.Confirmor),
                        DataAccessMySQL.CreateParameter("ConfirmDate",DbType.String,bOM_H.ConfirmDate)
                    });

                msg = "";
                return true;
            }catch(Exception ex)
            {
                msg = ex.Message;
                return false;
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
                            if (valueArray.Length < 8)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from BOM_H where MajorComponentNo=@MajorComponentNo",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String, valueArray[0].Trim()),
                                }).ToString()) == 0)
                        {
                            DataTable dtProduct = dataAccessMySql.ExecuteDataTable("select * from Product where ProductNo=@ProductNo", tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[0].Trim()),
                                });
                            if(dtProduct!=null&&dtProduct.Rows.Count>0)
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                   @"insert into BOM_H(MajorComponentNo,MajorComponentName,Specification,Unit,StandardQTY,Remark) values
                                    (@MajorComponentNo,@MajorComponentName,@Specification,@Unit,@StandardQTY,@Remark)",
                                  tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String,valueArray[0].Trim()),
                                        DataAccessMySQL.CreateParameter("StandardQTY", DbType.Double, double.Parse(valueArray[1].Trim()==""?"0":valueArray[1].Trim())),
                                        DataAccessMySQL.CreateParameter("Remark", DbType.String, valueArray[2].Trim()),
                                        DataAccessMySQL.CreateParameter("MajorComponentName", DbType.String,dtProduct.Rows[0]["ProductName"].ToString()),
                                        DataAccessMySQL.CreateParameter("Specification", DbType.String, dtProduct.Rows[0]["Specification"].ToString()),
                                        DataAccessMySQL.CreateParameter("Unit", DbType.String, dtProduct.Rows[0]["Unit"].ToString())
                                    });
                            }
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from BOM_D where MajorComponentNo=@MajorComponentNo and ItemId=@ItemId",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("ItemId", DbType.String, valueArray[3].Trim()),
                                }).ToString()) == 0)
                        {
                            DataTable dtProduct = dataAccessMySql.ExecuteDataTable("select * from Product where ProductNo=@ProductNo", tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, valueArray[4].Trim()),
                                });
                            if (dtProduct != null && dtProduct.Rows.Count > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                   @"insert into BOM_D(MajorComponentNo,ItemId,ComponentNo,QTY,Remark) values
                                    (@MajorComponentNo,@ItemId,@ComponentNo,@QTY,@Remark)",
                                  tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("MajorComponentNo", DbType.String,valueArray[0].Trim()),
                                        DataAccessMySQL.CreateParameter("ItemId", DbType.String, valueArray[3].Trim()),
                                        DataAccessMySQL.CreateParameter("ComponentNo", DbType.String, valueArray[4].Trim()),
                                        DataAccessMySQL.CreateParameter("QTY", DbType.Double, double.Parse(valueArray[6].Trim()==""?"0":valueArray[6].Trim())),
                                        DataAccessMySQL.CreateParameter("Remark", DbType.String, valueArray[7].Trim())
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

        public List<BOM_H> SearchBOMH(BOM_H bOM_H, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<BOM_H> objBOM_H = new List<BOM_H>();

            string sql = @"select * from BOM_H where 1=1";

            if (!String.IsNullOrEmpty(bOM_H.MajorComponentNo))
            {
                sql += " and MajorComponentNo=@MajorComponentNo";
            }
            
            if (!String.IsNullOrEmpty(bOM_H.MajorComponentName))
            {
                sql += " and p.MajorComponentName=@MajorComponentName";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtBOM_H = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,bOM_H.MajorComponentNo),
                    DataAccessMySQL.CreateParameter("MajorComponentName",DbType.String,bOM_H.MajorComponentName),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                }); ;

            if (dtBOM_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBOM_H.Rows)
                {
                    BOM_H obj = new BOM_H();
                    obj.CreateDate = dr["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); ;
                    obj.Confirmor = dr["Confirmor"].ToString();
                    obj.ConfirmDate = dr["ConfirmDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); ;
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.MajorComponentName = dr["MajorComponentName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.StandardQTY = double.Parse(dr["StandardQTY"].ToString() == "" ? "0" : dr["StandardQTY"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.Confirmed = dr["Confirmed"].ToString();
                    objBOM_H.Add(obj);
                }
            }

            return objBOM_H;
        }

    }
}