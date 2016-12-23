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
    public class AssignmentDAL : IAssignmentDAL
    {
        public bool AddAssignment(Assignment assignment,string type, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from Assignment where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    tran = dbMySQL.CreateDbTransaction();

                    DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                       new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,assignment.AssignOrderType)
                       });
                    string AutoConfirm = "N";
                    if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
                    {
                        if (dtOrderCategory.Rows[0]["AutoConfirm"].ToString() == "Y")
                        {
                            if(type=="A2")
                            {
                                dbMySQL.ExecuteNonQuery(@"update supportapl_h set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,
                            OrderStatus=@OrderStatus,AsignCheck=@AsignCheck where SupportAplOrderType=@SupportAplOrderType 
                            and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignment.Modifier),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,assignment.SourceOrderType),
                                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,assignment.SourceOrderNo),
                                //DataAccessMySQL.CreateParameter("AsignDate",DbType.String, assignment.AssignDate),
                                //DataAccessMySQL.CreateParameter("AsignMan",DbType.String,assignment.Assignor),
                                //DataAccessMySQL.CreateParameter("ProcessMan",DbType.String, assignment.Designee),
                                DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,"1"),
                                DataAccessMySQL.CreateParameter("AsignCheck",DbType.String,"Y")
                            });
                            }
                            if (type == "B3")
                            {
                                dbMySQL.ExecuteNonQuery(@"update RGA_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,
                                StatusCode=@StatusCode,Repairman=@Repairman where RGAType=@RGAType 
                                and RGANo=@RGANo", tran,
                                new DbParameter[]{
                                    DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                                    DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                                    DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignment.Modifier),
                                    DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,assignment.SourceOrderType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,assignment.SourceOrderNo),
                                    DataAccessMySQL.CreateParameter("StatusCode",DbType.String, "05"),
                                    DataAccessMySQL.CreateParameter("Repairman",DbType.String, assignment.Designee)
                                });

                                DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable(@"select * from RGA_H where RGAType=@RGAType 
                                and RGANo = @RGANo", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,assignment.SourceOrderType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,assignment.SourceOrderNo),
                                });

                                dbMySQL.ExecuteNonQuery(@"insert into  RepairRecord ( ProductNo,SerielNo, 
                                    RGAType,RGANo,ChangeDate,ChangeOrderType,ChangeOrderNo,ChangeMode) values ( @ProductNo,@SerielNo, 
                                    @RGAType,@RGANo,@ChangeDate,@ChangeOrderType,@ChangeOrderNo,@ChangeMode)", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtSupportApl_H.Rows[0]["ProductNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("SerielNo",DbType.String,dtSupportApl_H.Rows[0]["SerielNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,assignment.SourceOrderType),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,assignment.SourceOrderNo),
                                    DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,assignment.AssignDate),
                                    DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,assignment.AssignOrderType),
                                    DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,assignment.AssignOrderNo),
                                    DataAccessMySQL.CreateParameter("ChangeMode",DbType.String,"05"),
                                });
                            }
                            if (type == "D3")
                            {
                                dbMySQL.ExecuteNonQuery(@"update RoutineService_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,AssignCheck=@AssignCheck,AssignDate=@AssignDate,
                            RoutineSerMan=@RoutineSerMan where RoutineSerOrderType=@RoutineSerOrderType 
                            and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignment.Modifier),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,assignment.SourceOrderType),
                                DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,assignment.SourceOrderNo),
                                DataAccessMySQL.CreateParameter("AssignDate",DbType.String, assignment.AssignDate),
                                DataAccessMySQL.CreateParameter("RoutineSerMan",DbType.String, assignment.Designee),
                                DataAccessMySQL.CreateParameter("AssignCheck",DbType.String,"Y")
                            });
                            }
                            AutoConfirm = "Y";
                        }
                    }

                    if (AutoConfirm == "N")
                    {
                        assignment.Assignor = "";
                        assignment.AssignDate = "";
                    }
                        
                    dbMySQL.ExecuteNonQuery(@"insert into Assignment (Company,UserGroup,Creator,CreateDate,
                        AssignOrderType,AssignOrderNo,OrderDate,CustomerId,Version,SourceOrderType,SourceOrderNo,
                        Designee,Assignor,AssignDate,NoOfPrints,Remark,Confirmed) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @AssignOrderType,@AssignOrderNo,@OrderDate,@CustomerId,@Version,@SourceOrderType,
                        @SourceOrderNo,@Designee,@Assignor,@AssignDate,@NoOfPrints,@Remark,@Confirmed)",tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,assignment.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,assignment.OrderDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,assignment.CustomerId),
                            DataAccessMySQL.CreateParameter("Version",DbType.Double,assignment.Version),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignment.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignment.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,assignment.Designee),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignment.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignment.AssignDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,assignment.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,assignment.Remark),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,AutoConfirm)
                        });

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

        public bool AddAssignmentRGA_H(Assignment assignment, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {

                string count = dbMySQL.ExecuteScalar(@"select count(*) from Assignment where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    tran = dbMySQL.CreateDbTransaction();

                    DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                       new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,assignment.AssignOrderType)
                       });
                    string AutoConfirm = "N";
                    if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
                    {
                        if (dtOrderCategory.Rows[0]["AutoConfirm"].ToString() == "Y")
                        {
                            dbMySQL.ExecuteNonQuery(@"update RGA_H set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,StatusCode=@StatusCode,Repairman=@Repairman
                             where RGAType=@RGAType 
                            and RGANo=@RGANo", tran,
                            new DbParameter[]{
                                DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                                DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignment.Modifier),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,assignment.SourceOrderType),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,assignment.SourceOrderNo),
                                DataAccessMySQL.CreateParameter("StatusCode",DbType.String, "05"),
                                DataAccessMySQL.CreateParameter("Repairman",DbType.String,assignment.Designee)
                            });
                            AutoConfirm = "Y";
                        }
                    }

                    dbMySQL.ExecuteNonQuery(@"insert into Assignment (Company,UserGroup,Creator,CreateDate,AssignOrderType,AssignOrderNo,OrderDate,CustomerId,
                        Version,SourceOrderType,SourceOrderNo,Designee,Assignor,AssignDate,NoOfPrints,Remark,Confirmed) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@AssignOrderType,@AssignOrderNo,@OrderDate,@CustomerId,@Version,@SourceOrderType,@SourceOrderNo,
                        @Designee,@Assignor,@AssignDate,@NoOfPrints,@Remark,@Confirmed)", tran,
                            new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignment.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignment.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,assignment.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,assignment.OrderDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,assignment.CustomerId),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,"0000"),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignment.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignment.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,""),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,""),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,""),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Decimal,0),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,assignment.Remark),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,AutoConfirm)
                            });

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
        
        public bool UpdateAssignment(Assignment assignment, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            //DbTransaction tran = null;
            try
            {
                //tran = dbMySQL.CreateDbTransaction();
                //MajorComponentNo
                dbMySQL.ExecuteNonQuery(@"update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,OrderDate=@OrderDate,CustomerId=@CustomerId,
                         SourceOrderType=@SourceOrderType,SourceOrderNo=@SourceOrderNo,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark
                         where AssignOrderNo=@AssignOrderNo and AssignOrderType=@AssignOrderType",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignment.Modifier),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,assignment.OrderDate),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,assignment.CustomerId),
                            DataAccessMySQL.CreateParameter("Version",DbType.Double,assignment.Version),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignment.SourceOrderType),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignment.SourceOrderNo),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,assignment.Designee),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignment.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignment.AssignDate),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,assignment.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,assignment.Remark),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,assignment.Confirmed)
                        });

                //tran.Commit();
                msg = "";
                return true;

            }
            catch (Exception ex)
            {
                //if (tran != null)
                //{
                //    tran.Rollback();
                //}
                msg = ex.Message;
                return false;
            }
            //finally
            //{
            //    if (tran != null)
            //    {
            //        tran.Dispose();
            //    }
            //}
        }
        public bool DelAssignment(Assignment assignment, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtAssignment = dbMySQL.ExecuteDataTable("select * from Assignment where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                    });
                if (dtAssignment == null || dtAssignment.Rows.Count == 0)
                {
                    tran.Commit();
                    msg = "";
                    return true;
                }

                DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from SupportApl_H where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    });
                
                if(dtSupportApl_H!=null&&dtSupportApl_H.Rows.Count!=0)
                {
                    if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString()=="2")
                    {
                        tran.Commit();
                        msg = "當前支援申請單已完工";
                        return false;
                    }

                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                    }).ToString()) > 0)
                    {

                        tran.Commit();
                        msg = "當前派工單有變更，請先刪除變更單！";
                        return false;
                    }

                    dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='N' , OrderStatus='0' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    });

                }



                dbMySQL.ExecuteNonQuery("delete from Assignment where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo",tran,
                         new DbParameter[]{
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
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

        public bool DelRoutineService(Assignment assignment, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();

                DataTable dtAssignment = dbMySQL.ExecuteDataTable("select * from Assignment where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                    });
                if (dtAssignment == null || dtAssignment.Rows.Count == 0)
                {
                    tran.Commit();
                    msg = "";
                    return true;
                }

                DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from RoutineService_H where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    });

                if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count != 0)
                {
                    if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "2")
                    {
                        tran.Commit();
                        msg = "當前支援申請單已完工";
                        return false;
                    }

                    if (int.Parse(dbMySQL.ExecuteScalar("select count(*) from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                    }).ToString()) > 0)
                    {

                        tran.Commit();
                        msg = "當前派工單有變更，請先刪除變更單！";
                        return false;
                    }

                    dbMySQL.ExecuteNonQuery("update RoutineService_H set AsignCheck='N' , OrderStatus='0' where RoutineSerOrderType=@RoutineSerOrderType and RoutineSerOrderNo=@RoutineSerOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RoutineSerOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("RoutineSerOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    });

                }

                dbMySQL.ExecuteNonQuery("delete from Assignment where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                         new DbParameter[]{
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
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
        public List<Assignment> SearchAssignment(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<Assignment> objAssignment = new List<Assignment>();

            string sql = @"select a.*,o.OrderCategory,c.CustomerName,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName from Assignment as a 
                left join OrderCategory as o on o.OrderType=a.AssignOrderType
                left join Customer as c on c.CustomerId=a.CustomerId 
                left join Employee as e on e.EmployeeId=a.Designee
                left join Employee as ee on ee.EmployeeId=a.Assignor where 1=1";

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


            DataTable dtAssignment = null;
            if (strCondition != "")
            {
                dtAssignment = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtAssignment = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtAssignment.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAssignment.Rows)
                {
                    Assignment obj = new Assignment();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreateDate = dr["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); 
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd"); 
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.Assignor = dr["Assignor"].ToString();
                    obj.Designee = dr["Designee"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.AssignDate = dr["AssignDate"].ToString();
                    obj.NoOfPrints = int.Parse(dr["NoOfPrints"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    obj.Confirmed = dr["Confirmed"].ToString();
                    objAssignment.Add(obj);
                }
            }

            return objAssignment;
        }

        public Assignment GetAssignmentInfo(Assignment assignment)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            Assignment obj = new Assignment();

            string sql = @"select a.*,o.OrderSName,o.AutoConfirm,o.CheckExOrder,
                    c.CustomerName,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName from Assignment as a 
                    left join OrderCategory as o on o.OrderType=a.AssignOrderType
                    left join Customer as c on c.CustomerId=a.CustomerId
                    left join Employee as e on e.EmployeeId=a.Designee
                    left join Employee as ee on ee.EmployeeId=a.Assignor where 
                    1=1";

            if(!String.IsNullOrEmpty(assignment.AssignOrderType))
            {
                sql += " and AssignOrderType=@AssignOrderType";
            }

            if (!String.IsNullOrEmpty(assignment.AssignOrderNo))
            {
                sql += " and AssignOrderNo=@AssignOrderNo";
            }

            if (!String.IsNullOrEmpty(assignment.SourceOrderType))
            {
                sql += " and SourceOrderType=@SourceOrderType";
            }

            if (!String.IsNullOrEmpty(assignment.SourceOrderNo))
            {
                sql += " and SourceOrderNo=@SourceOrderNo";
            }

            if (!String.IsNullOrEmpty(assignment.Confirmed))
            {
                sql += " and Confirmed=@Confirmed";
            }

            DataTable dtAssignment = dbMySQL.ExecuteDataTable(sql,
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo),
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignment.SourceOrderType),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignment.SourceOrderNo),
                        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,assignment.Confirmed)
                    });

            if (dtAssignment.Rows.Count > 0)
            {
                obj.Creator = dtAssignment.Rows[0]["Creator"].ToString();
                obj.CreateDate = dtAssignment.Rows[0]["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dtAssignment.Rows[0]["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.AssignOrderType = dtAssignment.Rows[0]["AssignOrderType"].ToString();
                obj.OrderSName = dtAssignment.Rows[0]["OrderSName"].ToString();
                obj.AutoConfirm = dtAssignment.Rows[0]["AutoConfirm"].ToString();
                obj.CheckExOrder = dtAssignment.Rows[0]["CheckExOrder"].ToString();
                obj.AssignOrderNo = dtAssignment.Rows[0]["AssignOrderNo"].ToString();
                obj.OrderDate = dtAssignment.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtAssignment.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.CustomerId = dtAssignment.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtAssignment.Rows[0]["CustomerName"].ToString();
                obj.Version = dtAssignment.Rows[0]["Version"].ToString();
                obj.SourceOrderType = dtAssignment.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtAssignment.Rows[0]["SourceOrderNo"].ToString();
                obj.Designee = dtAssignment.Rows[0]["Designee"].ToString();
                obj.DesigneeName = dtAssignment.Rows[0]["DesigneeName"].ToString();
                obj.Assignor = dtAssignment.Rows[0]["Assignor"].ToString();
                obj.AssignorName = dtAssignment.Rows[0]["AssignorName"].ToString();
                obj.AssignDate = dtAssignment.Rows[0]["AssignDate"].ToString() == "" ? "" : DateTime.ParseExact(dtAssignment.Rows[0]["AssignDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.NoOfPrints = int.Parse(dtAssignment.Rows[0]["NoOfPrints"].ToString());
                obj.Remark = dtAssignment.Rows[0]["Remark"].ToString();
                obj.Confirmed = dtAssignment.Rows[0]["Confirmed"].ToString();
            }

            return obj;
        }

        public bool Confirmed(Assignment assignment, string type, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtAssignment = dbMySQL.ExecuteDataTable(@"select * from Assignment where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo",tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                    });
                if (dtAssignment == null || dtAssignment.Rows.Count == 0)
                {
                    tran.Commit();
                    msg = "";
                    return true;
                }

                if(type=="A2")
                {
                    DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from SupportApl_H where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                       new DbParameter[] {
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                       });

                    if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count != 0)
                    {
                        if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "2")
                        {
                            tran.Commit();
                            msg = dtAssignment.Rows[0]["SourceOrderType"].ToString()+"-"+ dtAssignment.Rows[0]["SourceOrderNo"].ToString() + "支援單已完工,請確認!";
                            return false;
                        }
                        
                        if (assignment.Confirmed == "V"|| assignment.Confirmed == "N")
                        {
                            dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='N' , OrderStatus='0' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                            });
                        }

                        if (assignment.Confirmed == "Y")
                        {
                            if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "1")
                            {
                                tran.Commit();
                                msg = dtAssignment.Rows[0]["SourceOrderType"].ToString() + "-" + dtAssignment.Rows[0]["SourceOrderNo"].ToString() + "支援單已派工，請確認!";
                                return false;
                            }

                            dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y' , OrderStatus='1' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                            new DbParameter[] {
                            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                            });
                        }
                    }

                }

                if (type == "D3")
                {
                    //DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from SupportApl_H where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    //   new DbParameter[] {
                    //    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //   });

                    //if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count != 0)
                    //{
                    //    if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "2")
                    //    {
                    //        tran.Commit();
                    //        msg = "當前支援申請單已完工";
                    //        return false;
                    //    }
                    //    if (assignment.Confirmed == "V")
                    //    {
                    //        dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='N' , OrderStatus='0' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    //        new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //        });
                    //    }

                    //    if (assignment.Confirmed == "Y")
                    //    {
                    //        dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y' , OrderStatus='1' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    //        new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //        });
                    //    }
                    //}

                }
                if (type == "B3")
                {
                    DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from RGA_H where RGAType=@RGAType and RGANo=@RGANo", tran,
                       new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                       });

                    if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count != 0)
                    {
                        if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "50")
                        {
                            tran.Commit();
                            msg = "當前支援申請單已完工";
                            return false;
                        }

                        if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "05")
                        {
                            tran.Commit();
                            msg = "當前支援申請單已派工";
                            return false;
                        }

                        if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "55")
                        {
                            tran.Commit();
                            msg = "當前支援申請單已結案";
                            return false;
                        }

                        if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "D")
                        {
                            tran.Commit();
                            msg = "當前支援申請單已作廢";
                            return false;
                        }
                        if (assignment.Confirmed == "V" || assignment.Confirmed == "N")
                        {
                            string OrderStatus = dbMySQL.ExecuteScalar(@"select top 1 ChangeMode from RepairRecord where ProductNo=@ProductNo and SerielNo=@SerielNo and 
                                    RGAType=@RGAType and RGANo=@RGANo order by ChangeDate", tran, new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtSupportApl_H.Rows[0]["ProductNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("SerielNo",DbType.String,dtSupportApl_H.Rows[0]["SerielNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                            }).ToString();
                            
                            dbMySQL.ExecuteNonQuery("update RGA_H set OrderStatus=@OrderStatus where RGAType=@RGAType and RGANo=@RGANo", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("OrderStatus",DbType.String,OrderStatus==""?"00":OrderStatus),
                                DataAccessMySQL.CreateParameter("RGAType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                                DataAccessMySQL.CreateParameter("RGANo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                                });

                            dbMySQL.ExecuteNonQuery(@"delete from RepairRecord where ProductNo=@ProductNo and SerielNo=@SerielNo and 
                                    RGAType=@RGAType and RGANo=@RGANo and ChangeDate=@ChangeDate and ChangeOrderType=@ChangeOrderType and ChangeOrderNo=@ChangeOrderNo and ChangeMode=@ChangeMode", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtSupportApl_H.Rows[0]["ProductNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("SerielNo",DbType.String,dtSupportApl_H.Rows[0]["SerielNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,assignment.AssignDate),
                                    DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,assignment.AssignOrderType),
                                    DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,assignment.AssignOrderNo),
                                    DataAccessMySQL.CreateParameter("ChangeMode",DbType.String,"05"),
                                });
                        }

                        if (assignment.Confirmed == "Y")
                        {
                            dbMySQL.ExecuteNonQuery("update RGA_H set  OrderStatus='05',Repairman=@Repairman where RGAType=@RGAType and RGANo=@RGANo", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("Repairman",DbType.String,assignment.Assignor),
                            DataAccessMySQL.CreateParameter("RGAType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("RGANo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                            });

                            dbMySQL.ExecuteNonQuery(@"insert into  RepairRecord ( ProductNo,SerielNo, 
                                    RGAType,RGANo,ChangeDate,ChangeOrderType,ChangeOrderNo,ChangeMode) values ( @ProductNo,@SerielNo, 
                                    @RGAType,@RGANo,@ChangeDate,@ChangeOrderType,@ChangeOrderNo,@ChangeMode)", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("ProductNo",DbType.String,dtSupportApl_H.Rows[0]["ProductNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("SerielNo",DbType.String,dtSupportApl_H.Rows[0]["SerielNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                                    DataAccessMySQL.CreateParameter("ChangeDate",DbType.String,assignment.AssignDate),
                                    DataAccessMySQL.CreateParameter("ChangeOrderType",DbType.String,assignment.AssignOrderType),
                                    DataAccessMySQL.CreateParameter("ChangeOrderNo",DbType.String,assignment.AssignOrderNo),
                                    DataAccessMySQL.CreateParameter("ChangeMode",DbType.String,"05"),
                                });
                        }
                    }
                }


                    if (assignment.Confirmed=="V")
                {
                    dbMySQL.ExecuteNonQuery("update Assignment set Confirmed='V' where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                        });
                }

                if (assignment.Confirmed == "N")
                {
                    dbMySQL.ExecuteNonQuery("update Assignment set Confirmed='N',Assignor='',AssignDate='' where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                        });
                }

                if (assignment.Confirmed == "Y")
                {
                    dbMySQL.ExecuteNonQuery("update Assignment set Confirmed='Y',Assignor=@Assignor,AssignDate=@AssignDate where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignment.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignment.AssignDate),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo)
                        });
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

        public List<Assignment> AssignmentInfo(Assignment assignment)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Assignment> objAssignment = new List<Assignment>();

            DataTable dtAssignment = dbMySQL.ExecuteDataTable(@"select * from Assignment where 
                    SourceOrderType=@SourceOrderType and SourceOrderNo=@SourceOrderNo",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignment.SourceOrderType),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignment.SourceOrderNo)
                    });

            if (dtAssignment.Rows.Count > 0)
            {
                foreach(DataRow dr in dtAssignment.Rows)
                {
                    Assignment obj = new Assignment();
                    obj.Creator = dr["Creator"].ToString();
                    obj.Creator = dr["Creator"].ToString();
                    if(dr["Confirmed"].ToString()=="Y")
                    {
                        obj.Confirmed = "Y:已確認";
                    }
                    if (dr["Confirmed"].ToString() == "N")
                    {
                        obj.Confirmed = "N:未確認";
                    }
                    if (dr["Confirmed"].ToString() == "Y")
                    {
                        obj.Confirmed = "V:作廢";
                    }
                    
                    objAssignment.Add(obj);
                }
            }

            return objAssignment;
        }
    }
}