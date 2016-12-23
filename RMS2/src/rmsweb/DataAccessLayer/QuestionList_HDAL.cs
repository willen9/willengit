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
    public class QuestionList_HDAL : IQuestionList_HDAL
    {
        public bool AddQuestionList_H(QuestionList_H questionList_H, string strItemId,
            string strSolution, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                string count = dbMySQL.ExecuteScalar(@"select count(*) from QuestionList_H where 
                    QuestionNo=@QuestionNo", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into QuestionList_H (Company,UserGroup,Creator,CreateDate,
                        QuestionNo,QuestionTopic,QuestionDetail,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @QuestionNo,@QuestionTopic,@QuestionDetail,@Remark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,questionList_H.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,questionList_H.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,questionList_H.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo),
                            DataAccessMySQL.CreateParameter("QuestionTopic",DbType.String,questionList_H.QuestionTopic),
                            DataAccessMySQL.CreateParameter("QuestionDetail",DbType.String,questionList_H.QuestionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,questionList_H.Remark)
                        });

                    string sql = "";

                    if (strItemId != "" && strItemId != null)
                    {
                        string[] strItemIdArray = strItemId.Split(',');
                        string[] strSolutionArray = strSolution.Split(',');
                        string[] strRemarkArray = strRemark.Split(',');

                        sql = @"insert into QuestionList_D (Company,UserGroup,Creator,
                        CreateDate,QuestionNo,ItemId,Solution,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@QuestionNo,@ItemId,@Solution,@Remark)";


                        for (int i = 0; i < strItemIdArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(strItemIdArray[i]))
                            {
                                continue;
                            }
                            dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,questionList_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,questionList_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,questionList_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Solution",DbType.String,strSolutionArray[i].ToString()),
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
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
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

        public bool UpdateQuestionList_H(QuestionList_H questionList_H, string strItemId,
            string strSolution, string strRemark, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery(@"update QuestionList_H set Modifier=@Modifier,
                        ModiDate=@ModiDate,QuestionTopic=@QuestionTopic,QuestionDetail=@QuestionDetail,
                        Remark=@Remark
                        where QuestionNo=@QuestionNo", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,questionList_H.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo),
                            DataAccessMySQL.CreateParameter("QuestionTopic",DbType.String,questionList_H.QuestionTopic),
                            DataAccessMySQL.CreateParameter("QuestionDetail",DbType.String,questionList_H.QuestionDetail),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,questionList_H.Remark)
                        });

                dbMySQL.ExecuteNonQuery("delete from QuestionList_D where QuestionNo=@QuestionNo",
                    tran, new DbParameter[]{
                        DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo)
                    });
                string sql = "";

                if (strItemId != "" && strItemId != null)
                {
                    string[] strItemIdArray = strItemId.Split(',');
                    string[] strSolutionArray = strSolution.Split(',');
                    string[] strRemarkArray = strRemark.Split(',');

                    sql = @"insert into QuestionList_D (Company,UserGroup,Creator,
                        CreateDate,QuestionNo,ItemId,Solution,Remark) values (@Company,@UserGroup,@Creator,
                        @CreateDate,@QuestionNo,@ItemId,@Solution,@Remark)";


                    for (int i = 0; i < strItemIdArray.Length; i++)
                    {
                        if (string.IsNullOrEmpty(strItemIdArray[i]))
                        {
                            continue;
                        }
                        dbMySQL.ExecuteNonQuery(sql, tran, new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,questionList_H.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,questionList_H.UserGroup),
                                DataAccessMySQL.CreateParameter("Creator",DbType.String,questionList_H.Creator),
                                DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo),
                                DataAccessMySQL.CreateParameter("ItemId",DbType.String,strItemIdArray[i].ToString()),
                                DataAccessMySQL.CreateParameter("Solution",DbType.String,strSolutionArray[i].ToString()),
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

        public bool DelQuestionList_H(QuestionList_H questionList_H, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                dbMySQL.ExecuteNonQuery("delete from QuestionList_H where QuestionNo=@QuestionNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo)
                        });
                dbMySQL.ExecuteNonQuery("delete from QuestionList_D where QuestionNo=@QuestionNo",
                        tran, new DbParameter[]{
                            DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo)
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

        public List<QuestionList_H> SearchQuestionList_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<QuestionList_H> objQuestionList_H = new List<QuestionList_H>();

            string sql = @"select * from QuestionList_H  where 1=1";

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


            DataTable dtQuestionList_H = null;
            if (strCondition != "")
            {
                dtQuestionList_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtQuestionList_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtQuestionList_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQuestionList_H.Rows)
                {
                    QuestionList_H obj = new QuestionList_H();
                    obj.QuestionNo = dr["QuestionNo"].ToString();
                    obj.QuestionTopic = dr["QuestionTopic"].ToString();
                    obj.QuestionDetail = dr["QuestionDetail"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objQuestionList_H.Add(obj);
                }
            }

            return objQuestionList_H;
        }

        public QuestionList_H GetQuestionList_HInfo(QuestionList_H questionList_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            QuestionList_H obj = null;

            DataTable dtQuestionList_H = dbMySQL.ExecuteDataTable(@"select * from QuestionList_H where 
                    QuestionNo=@QuestionNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo)
                    });

            if (dtQuestionList_H.Rows.Count > 0)
            {
                obj = new QuestionList_H();
                obj.QuestionNo = dtQuestionList_H.Rows[0]["QuestionNo"].ToString();
                obj.QuestionTopic = dtQuestionList_H.Rows[0]["QuestionTopic"].ToString();
                obj.QuestionDetail = dtQuestionList_H.Rows[0]["QuestionDetail"].ToString();
                obj.Remark = dtQuestionList_H.Rows[0]["Remark"].ToString();
            }

            return obj;
        }

        public bool IsQuestionList_H(QuestionList_H questionList_H)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from QuestionList_H where 
                    QuestionNo=@QuestionNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,questionList_H.QuestionNo)
                    }).ToString()) > 0)
            {
                return false;
            }
            else
            {
                return true;
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
                            if (valueArray.Length < 4)
                            {
                                return false;
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (int.Parse(
                            dataAccessMySql.ExecuteScalar("select count(*) from QuestionList_H where QuestionNo=@QuestionNo",
                               tran,
                                new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("QuestionNo", DbType.String, valueArray[0]),
                                }).ToString()) == 0)
                        {
                            dataAccessMySql.ExecuteNonQuery(
                                   @"insert into QuestionList_H(QuestionNo,QuestionTopic,QuestionDetail,Remark) values
                                    (@QuestionNo,@QuestionTopic,@QuestionDetail,@Remark)",
                                  tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("QuestionNo", DbType.String,valueArray[0]),
                                        DataAccessMySQL.CreateParameter("QuestionTopic", DbType.String,valueArray[1]),
                                        DataAccessMySQL.CreateParameter("QuestionDetail", DbType.String,valueArray[2]),
                                        DataAccessMySQL.CreateParameter("Remark", DbType.String,valueArray[3])
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

        public List<QuestionList_H> SearchQuestionList(QuestionList_H questionList_H, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<QuestionList_H> objQuestionList_H = new List<QuestionList_H>();

            string sql = @"select * from QuestionList_H  where 1=1";

            if (!String.IsNullOrEmpty(questionList_H.QuestionNo))
            {
                sql += " and QuestionNo like @QuestionNo";
            }

            if (!String.IsNullOrEmpty(questionList_H.QuestionTopic))
            {
                sql += " and QuestionTopic like @QuestionTopic";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

           
            DataTable dtQuestionList_H = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("QuestionNo",DbType.String,"%"+questionList_H.QuestionNo+"%"),
                    DataAccessMySQL.CreateParameter("QuestionTopic",DbType.String,"%"+questionList_H.QuestionTopic+"%"),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                }); ;
            if (dtQuestionList_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQuestionList_H.Rows)
                {
                    QuestionList_H obj = new QuestionList_H();
                    obj.QuestionNo = dr["QuestionNo"].ToString();
                    obj.QuestionTopic = dr["QuestionTopic"].ToString();
                    obj.QuestionDetail = dr["QuestionDetail"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objQuestionList_H.Add(obj);
                }
            }

            return objQuestionList_H;
        }

        public DataTable Search_H(QuestionList_H customer, string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string RecSDate="";

            string RecEDate="";

            string sql = @"Select h.ProductNo,h.ProductName,d.ComponentNo,
                            (Select MajorComponentName from BOM_H as b 
                            where b.MajorComponentNo=h.ProductNo)as MajorComponentName,
                            s.QTY,(Select AVG(Lifetime) from 
                            PartLifetimeRecord as p where p.ProductNo=h.ProductNo and 
                            p.PartNo=d.ComponentNo) as Lifetime from RGA_H as h 
                            left join BOM_D as d on h.ProductNo=d.MajorComponentNo
                            left join Substitutes_D as s on h.ProductNo=s.MajorComponentNo 
                            and d.ComponentNo=s.ComponentNo where 1=1 ";
            if (customer != null)
            {
                if (!string.IsNullOrEmpty(customer.Company))
                {
                    sql += " and Company=@Company ";
                }
                if (!string.IsNullOrEmpty(customer.UserGroup))
                {
                    sql += " and UserGroup=@UserGroup ";
                }
                if (!string.IsNullOrEmpty(customer.Creator))
                {
                    sql += " and Creator=@Creator ";
                }
                if (customer.StatusCode != "55")
                {
                    sql += " and StatusCode=''";
                }
                if (!string.IsNullOrEmpty(customer.SubstitutesType))
                {
                    sql += " and SubstitutesType='1'";
                }

                if (!string.IsNullOrEmpty(customer.RecSDate))
                {

                    RecSDate = DateTime.Parse(customer.RecSDate).ToString("yyyyMMdd");
                    if (!string.IsNullOrEmpty(customer.RecEDate))
                    {
                        RecEDate = DateTime.Parse(customer.RecEDate).ToString("yyyyMMdd");
                        sql += " and OrderDate between @RecSDate and @RecEDate";
                    }
                    else
                    {
                        RecSDate = DateTime.Parse(customer.RecSDate).ToString("yyyyMMdd");
                        sql += " and OrderDate>=@RecSDate";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(customer.RecEDate))
                    {
                        RecEDate = DateTime.Parse(customer.RecEDate).ToString("yyyyMMdd");
                        sql += " and OrderDate<=@RecEDate";
                    }
                }

                string departmentIdParameterValue1 = "";
                string departmentIdParameterValue2 = "";
                string departmentIdParameterValue3 = "";
                if (!string.IsNullOrEmpty(customer.CustomerId))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (value == "like1")
                        {
                            sql += " and CustomerId like @CustomerId ";
                            departmentIdParameterValue1 = "%" + customer.CustomerId;
                        }
                        else if (value == "like2")
                        {
                            sql += " and CustomerId like @CustomerId ";
                            departmentIdParameterValue1 = customer.CustomerId + "%";
                        }
                        else if (value == "like3")
                        {
                            sql += " and CustomerId like @CustomerId ";
                            departmentIdParameterValue1 = "%" + customer.CustomerId + "%";
                        }
                        else
                        {
                            sql += " and CustomerId " + value + " @CustomerId ";
                            departmentIdParameterValue1 = customer.CustomerId;
                        }
                    }
                    else
                    {
                        sql += " and CustomerId=@CustomerId ";
                        departmentIdParameterValue1 = customer.CustomerId;
                    }
                }
                if (!string.IsNullOrEmpty(customer.ProductNo))
                {
                    if (!string.IsNullOrEmpty(col))
                    {
                        if (col == "like1")
                        {
                            sql += " and ProductNo like @ProductNo ";
                            departmentIdParameterValue2 = "%" + customer.ProductNo;
                        }
                        else if (col == "like2")
                        {
                            sql += " and ProductNo like @ProductNo ";
                            departmentIdParameterValue2 = customer.ProductNo + "%";
                        }
                        else if (col == "like3")
                        {
                            sql += " and ProductNo like @ProductNo ";
                            departmentIdParameterValue2 = "%" + customer.ProductNo + "%";
                        }
                        else
                        {
                            sql += " and ProductNo " + col + " @ProductNo ";
                            departmentIdParameterValue2 = customer.ProductNo;
                        }
                    }
                    else
                    {
                        sql += " and ProductNo like @ProductNo ";
                        departmentIdParameterValue2 = "%" + customer.ProductNo + "%";
                    }
                }
                if (!string.IsNullOrEmpty(customer.ComponentNo))
                {
                    if (!string.IsNullOrEmpty(col))
                    {
                        if (col == "like1")
                        {
                            sql += " and d.ComponentNo like @ComponentNo ";
                            departmentIdParameterValue3 = "%" + customer.ComponentNo;
                        }
                        else if (col == "like2")
                        {
                            sql += " and d.ComponentNo like @ComponentNo ";
                            departmentIdParameterValue3 = customer.ComponentNo + "%";
                        }
                        else if (col == "like3")
                        {
                            sql += " and d.ComponentNo like @ComponentNo ";
                            departmentIdParameterValue3 = "%" + customer.ComponentNo + "%";
                        }
                        else
                        {
                            sql += " and d.ComponentNo " + col + " @ComponentNo ";
                            departmentIdParameterValue3 = customer.ComponentNo;
                        }
                    }
                    else
                    {
                        sql += " and d.ComponentNo like @ComponentNo ";
                        departmentIdParameterValue3 = "%" + customer.ComponentNo + "%";
                    }
                }
                sql += " group by d.ComponentNo";
                return dbMySQL.ExecuteDataTable(sql, new DbParameter[]
              {
                    DataAccessMySQL.CreateParameter("Company", DbType.String, customer.Company),
                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, customer.UserGroup),
                    DataAccessMySQL.CreateParameter("Creator", DbType.String, customer.Creator),
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, departmentIdParameterValue1),
                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, departmentIdParameterValue2),
                    DataAccessMySQL.CreateParameter("ComponentNo", DbType.String, departmentIdParameterValue3),
                    DataAccessMySQL.CreateParameter("RecSDate",DbType.String,RecSDate),
                    DataAccessMySQL.CreateParameter("RecEDate",DbType.String,RecEDate)
              });
            }
            sql += " group by d.ComponentNo";
            return dbMySQL.ExecuteDataTable(sql);
        }

        public DataTable Search_HCUR(QuestionList_H customer)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string RecSDate = "";

            string RecEDate = "";

            string sql = @"Select StatusCode,RGAType,(Select OrderSName from OrderCategory 
                            where OrderType=h.RGAType)as OrderSName,RGANo, 
                            (Select CustomerName from Customer where CustomerId=h.CustomerId)
                            as CustomerName,ProductNo,ProductName,SerialNo,d.ComponentNo,
                            (Select MajorComponentName from BOM_H as b where 
                            b.MajorComponentNo=h.ProductNo)as MajorComponentName,
                            (Select QTY from Substitutes_D as s where ProductNo=s.MajorComponentNo
                            and d.ComponentNo=s.ComponentNo Group by s.ComponentNo)as QTY,
                            (Select AVG(Lifetime) from PartLifetimeRecord as p where p.ProductNo=h.ProductNo and 
                            p.PartNo=d.ComponentNo) as Lifetime from RGA_H as h left join BOM_D as d on 
                            d.MajorComponentNo=h.ProductNo where 1=1";
            if (customer != null)
            {
                if (!string.IsNullOrEmpty(customer.ProductNo))
                {
                    sql += " and ProductNo=@ProductNo ";
                }
                return dbMySQL.ExecuteDataTable(sql, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("ProductNo", DbType.String, customer.ProductNo)
                });
            }
            return dbMySQL.ExecuteDataTable(sql);
        }

        public DataTable Search_CCUR(QuestionList_H customer)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string RecSDate = "";

            string RecEDate = "";

            string sql = @"Select StatusCode,RGAType,(Select OrderSName from OrderCategory 
                            where OrderType=h.RGAType)as OrderSName,RGANo, 
                            (Select CustomerName from Customer where CustomerId=h.CustomerId)
                            as CustomerName,ProductNo,ProductName,SerialNo,d.ComponentNo,
                            (Select MajorComponentName from BOM_H as b where 
                            b.MajorComponentNo=h.ProductNo)as MajorComponentName,
                            (Select QTY from Substitutes_D as s where ProductNo=s.MajorComponentNo
                            and d.ComponentNo=s.ComponentNo Group by s.ComponentNo)as QTY,
                            (Select AVG(Lifetime) from PartLifetimeRecord as p where p.ProductNo=h.ProductNo and 
                            p.PartNo=d.ComponentNo) as Lifetime from RGA_H as h left join BOM_D as d on 
                            d.MajorComponentNo=h.ProductNo where 1=1";
            if (customer != null)
            {
                if (!string.IsNullOrEmpty(customer.ComponentNo))
                {
                    sql += " and ComponentNo=@ComponentNo ";
                }
                return dbMySQL.ExecuteDataTable(sql, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("ComponentNo", DbType.String, customer.ComponentNo)
                });
            }
            return dbMySQL.ExecuteDataTable(sql);
        }

        public List<QuestionList_H> SearchRGA_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<QuestionList_H> objQuestionList_H = new List<QuestionList_H>();

            string sql = @"Select h.ProductNo,h.ProductName,d.ComponentNo,
                            (Select MajorComponentName from BOM_H as b 
                            where b.MajorComponentNo=h.ProductNo)as MajorComponentName,
                            (Select s.QTY from Substitutes_D as s where 
                            h.ProductNo=s.MajorComponentNo and d.ComponentNo=s.ComponentNo group by d.ComponentNo)as QTY,
                            (Select AVG(Lifetime) from PartLifetimeRecord as p where p.ProductNo=h.ProductNo and p.PartNo=d.ComponentNo) as Lifetime
                            from RGA_H as h left join BOM_D as d on h.ProductNo=d.MajorComponentNo
                             where 1=1 ";

            string strCondition = "";
            DbParameter[] dbParameters = new DbParameter[colArray.Length - 1];
            for (int i = 1; i < colArray.Length; i++)
            {
                if (string.IsNullOrEmpty(valueArray[i]))
                {
                    continue;
                }
                strCondition += " AND " + colArray[i] + " " + conditionArray[i].Replace("%", "") + " @Parameter" + i +
                                " ";
                if (conditionArray[i] == "like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String,
                        valueArray[i] + "%");
                }
                else if (conditionArray[i] == "%like")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String,
                        "%" + valueArray[i]);
                }
                else if (conditionArray[i] == "%like%")
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String,
                        "%" + valueArray[i] + "%");
                }
                else
                {
                    dbParameters[i - 1] = DataAccessMySQL.CreateParameter("Parameter" + i, DbType.String, valueArray[i]);
                }
            }


            DataTable dtQuestionList_H = null;
            if (strCondition != "")
            {
                dtQuestionList_H = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtQuestionList_H = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtQuestionList_H.Rows.Count > 0)
            {
                foreach (DataRow dr in dtQuestionList_H.Rows)
                {
                    QuestionList_H obj = new QuestionList_H();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.ComponentNo = dr["ComponentNo"].ToString();
                    obj.MajorComponentName = dr["MajorComponentName"].ToString();
                    obj.QTY = dr["QTY"].ToString();
                    objQuestionList_H.Add(obj);
                }
            }

            return objQuestionList_H;
        }
    }
}