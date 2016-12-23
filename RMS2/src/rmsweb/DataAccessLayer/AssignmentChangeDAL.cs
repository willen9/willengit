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
    public class AssignmentChangeDAL : IAssignmentChangeDAL
    {
        public bool AddAssignmentChange(AssignmentChange assignmentChange, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                
                if(int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and ConfirmedDate=''",
                    new DbParameter[] {
                         DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                         DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    }).ToString())>0)
                {
                    msg = "當前變更單有未確認，請先確認！";
                    return false;
                }

                DataTable dtAssignment= dbMySQL.ExecuteDataTable(@"select * from Assignment where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    });
                if (dtAssignment!=null&& dtAssignment.Rows.Count>0)
                {
                    tran = dbMySQL.CreateDbTransaction();
                    dbMySQL.ExecuteNonQuery(@"insert into AssignmentChange (Company,UserGroup,Creator,CreateDate,Confirmor,ConfirmedDate,
                        AssignOrderType,AssignOrderNo,Version,OrderDate,ModiReason,CustomerId,SourceOrderType,SourceOrderNo,
                        NoOfPrints,Designee,Assignor,AssignDate,Remark,OldOrderType,OldOrderNo,OldCustomerId,OldDesignee,
                        OldAssignor,OldAssignDate,OldRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@Confirmor,@ConfirmedDate,
                        @AssignOrderType,@AssignOrderNo,@Version,@OrderDate,@ModiReason,@CustomerId,@SourceOrderType,@SourceOrderNo,
                        @NoOfPrints,@Designee,@Assignor,@AssignDate,@Remark,@OldOrderType,@OldOrderNo,@OldCustomerId,@OldDesignee,
                        @OldAssignor,@OldAssignDate,@OldRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,assignmentChange.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,""),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,""),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,assignmentChange.OrderDate),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,assignmentChange.ModiReason),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtAssignment.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,assignmentChange.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignmentChange.AssignDate),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,assignmentChange.Remark),
                            DataAccessMySQL.CreateParameter("OldOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("OldOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldCustomerId",DbType.String,dtAssignment.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("OldDesignee",DbType.String,dtAssignment.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignor",DbType.String,dtAssignment.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignDate",DbType.String,dtAssignment.Rows[0]["AssignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtAssignment.Rows[0]["Remark"].ToString())
                        });

                    //dbMySQL.ExecuteNonQuery(@"update Assignment set CustomerId=@CustomerId,Version=@Version,SourceOrderType=@SourceOrderType,
                    //    SourceOrderNo=@SourceOrderNo,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark where 
                    //    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                    //        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,assignmentChange.CustomerId),
                    //        DataAccessMySQL.CreateParameter("Version",DbType.Double,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString()),
                    //        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignmentChange.SourceOrderType),
                    //        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignmentChange.SourceOrderNo),
                    //        DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                    //        DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                    //        DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignmentChange.AssignDate),
                    //        DataAccessMySQL.CreateParameter("Remark",DbType.String,assignmentChange.Remark)
                    //    });
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "資料不存在！";
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

        public bool AddAssignmentServiceArrangeChange(AssignmentChange assignmentChange, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {

                if (int.Parse(dbMySQL.ExecuteScalar(@"select count(*) from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and ConfirmedDate=''",
                    new DbParameter[] {
                         DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                         DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    }).ToString()) > 0)
                {
                    msg = "當前變更單有未確認，請先確認！";
                    return false;
                }

                DataTable dtAssignment = dbMySQL.ExecuteDataTable(@"select * from Assignment where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    });
                if (dtAssignment != null && dtAssignment.Rows.Count > 0)
                {
                    tran = dbMySQL.CreateDbTransaction();
                    dbMySQL.ExecuteNonQuery(@"insert into AssignmentChange (Company,UserGroup,Creator,CreateDate,Confirmor,ConfirmedDate,
                        AssignOrderType,AssignOrderNo,Version,OrderDate,ModiReason,CustomerId,SourceOrderType,SourceOrderNo,
                        NoOfPrints,Designee,Assignor,AssignDate,Remark,OldOrderType,OldOrderNo,OldCustomerId,OldDesignee,
                        OldAssignor,OldAssignDate,OldRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@Confirmor,@ConfirmedDate,
                        @AssignOrderType,@AssignOrderNo,@Version,@OrderDate,@ModiReason,@CustomerId,@SourceOrderType,@SourceOrderNo,
                        @NoOfPrints,@Designee,@Assignor,@AssignDate,@Remark,@OldOrderType,@OldOrderNo,@OldCustomerId,@OldDesignee,
                        @OldAssignor,@OldAssignDate,@OldRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,assignmentChange.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,""),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,""),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,assignmentChange.OrderDate),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,assignmentChange.ModiReason),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtAssignment.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,assignmentChange.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignmentChange.AssignDate),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,assignmentChange.Remark),
                            DataAccessMySQL.CreateParameter("OldOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("OldOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("OldCustomerId",DbType.String,dtAssignment.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("OldDesignee",DbType.String,dtAssignment.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignor",DbType.String,dtAssignment.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignDate",DbType.String,dtAssignment.Rows[0]["AssignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtAssignment.Rows[0]["Remark"].ToString())
                        });

                    //dbMySQL.ExecuteNonQuery(@"update Assignment set CustomerId=@CustomerId,Version=@Version,SourceOrderType=@SourceOrderType,
                    //    SourceOrderNo=@SourceOrderNo,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark where 
                    //    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                    //        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,assignmentChange.CustomerId),
                    //        DataAccessMySQL.CreateParameter("Version",DbType.Double,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString()),
                    //        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignmentChange.SourceOrderType),
                    //        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignmentChange.SourceOrderNo),
                    //        DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                    //        DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                    //        DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignmentChange.AssignDate),
                    //        DataAccessMySQL.CreateParameter("Remark",DbType.String,assignmentChange.Remark)
                    //    });
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "資料不存在！";
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

        public bool UpdateAssignmentChange(AssignmentChange assignmentChange, string type, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtAssignment = dbMySQL.ExecuteDataTable(@"select * from AssignmentChange where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and Version=@Version",tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,assignmentChange.Version),
                    });
                if (dtAssignment != null && dtAssignment.Rows.Count > 0)
                {
                    if (dtAssignment.Rows[0]["ConfirmedDate"].ToString() == "")
                    {
                        msg = "請先確認！";
                        return false;
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
                                    msg = "當前支援申請單已完工";
                                    return false;
                                }
                            }
                    }
                    
                    dbMySQL.ExecuteNonQuery(@"insert into AssignmentChange (Company,UserGroup,Creator,CreateDate,
                        AssignOrderType,AssignOrderNo,Version,OrderDate,ModiReason,CustomerId,SourceOrderType,SourceOrderNo,
                        NoOfPrints,Designee,Assignor,AssignDate,Remark,OldDesignee,
                        OldAssignor,OldAssignDate,OldRemark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,
                        @AssignOrderType,@AssignOrderNo,@Version,@OrderDate,@ModiReason,@CustomerId,@SourceOrderType,@SourceOrderNo,
                        @NoOfPrints,@Designee,@Assignor,@AssignDate,@Remark,@OldDesignee,
                        @OldAssignor,@OldAssignDate,@OldRemark)", tran,
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,assignmentChange.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,assignmentChange.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,assignmentChange.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString("0000")),
                            DataAccessMySQL.CreateParameter("OrderDate",DbType.String,assignmentChange.OrderDate),
                            DataAccessMySQL.CreateParameter("ModiReason",DbType.String,assignmentChange.ModiReason),
                            DataAccessMySQL.CreateParameter("CustomerId",DbType.String,assignmentChange.CustomerId),
                            DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                            DataAccessMySQL.CreateParameter("NoOfPrints",DbType.Int32,assignmentChange.NoOfPrints),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignmentChange.AssignDate),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,assignmentChange.Remark),
                            //DataAccessMySQL.CreateParameter("OldOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                            //DataAccessMySQL.CreateParameter("OldOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString()),
                            //DataAccessMySQL.CreateParameter("OldCustomerId",DbType.String,dtAssignment.Rows[0]["CustomerId"].ToString()),
                            DataAccessMySQL.CreateParameter("OldDesignee",DbType.String,dtAssignment.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignor",DbType.String,dtAssignment.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("OldAssignDate",DbType.String,dtAssignment.Rows[0]["AssignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("OldRemark",DbType.String,dtAssignment.Rows[0]["Remark"].ToString())
                        });


                    DataTable dtOrderCategory = dbMySQL.ExecuteDataTable("select AutoConfirm from OrderCategory where OrderType=@OrderType", tran,
                       new DbParameter[] {
                            DataAccessMySQL.CreateParameter("OrderType",DbType.String,assignmentChange.AssignOrderType)
                       });
                    if (dtOrderCategory != null && dtOrderCategory.Rows.Count > 0)
                    {
                        if (dtOrderCategory.Rows[0]["AutoConfirm"].ToString() == "Y")
                        {
                            dbMySQL.ExecuteNonQuery("update AssignmentChange set Confirmor=@Confirmor, ConfirmedDate=@ConfirmedDate where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and Version=@Version", tran,
                            new DbParameter[] {
                                DataAccessMySQL.CreateParameter("Confirmor",DbType.String,assignmentChange.Confirmor),
                                DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                                DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString("0000"))
                            });

                            dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Confirmed=@Confirmed,Version=@Version where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                                new DbParameter[] {
                                    DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                                    DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignment.Rows[0]["Designee"].ToString()),
                                    DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignment.Rows[0]["Assignor"].ToString()),
                                    DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignment.Rows[0]["AssignDate"].ToString()),
                                    DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                                    DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString("0000")),
                                    DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                                    DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                                });

                        }
                        //else
                        //{
                        //    if (type == "A2")
                        //    {
                        //        dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='N' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                        //            new DbParameter[] {
                        //                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,assignmentChange.AssignOrderType),
                        //                DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                        //            });
                        //    }
                        //}
                    }


                    //dbMySQL.ExecuteNonQuery(@"update Assignment set CustomerId=@CustomerId,Version=@Version,SourceOrderType=@SourceOrderType,
                    //    SourceOrderNo=@SourceOrderNo,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark where 
                    //    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                    //        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,assignmentChange.CustomerId),
                    //        DataAccessMySQL.CreateParameter("Version",DbType.Double,(int.Parse(dtAssignment.Rows[0]["Version"].ToString())+1).ToString()),
                    //        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,assignmentChange.SourceOrderType),
                    //        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,assignmentChange.SourceOrderNo),
                    //        DataAccessMySQL.CreateParameter("Designee",DbType.String,assignmentChange.Designee),
                    //        DataAccessMySQL.CreateParameter("Assignor",DbType.String,assignmentChange.Assignor),
                    //        DataAccessMySQL.CreateParameter("AssignDate",DbType.String,assignmentChange.AssignDate),
                    //        DataAccessMySQL.CreateParameter("Remark",DbType.String,assignmentChange.Remark)
                    //    });
                    tran.Commit();
                    msg = "";
                    return true;
                }
                else
                {
                    tran.Commit();
                    msg = "資料不存在！";
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

        public bool DelAssignmentChange(AssignmentChange assignmentChange, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtAssignmentChange = dbMySQL.ExecuteDataTable("select * from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo",tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    });
                if (dtAssignmentChange == null || dtAssignmentChange.Rows.Count == 0)
                {
                    tran.Commit();
                    msg = "";
                    return true;
                }
                DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from SupportApl_H where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignmentChange.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignmentChange.Rows[0]["SourceOrderNo"].ToString())
                    });
                if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count != 0)
                {
                    if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "2")
                    {
                        tran.Commit();
                        msg = "當前支援申請單已完工";
                        return false;
                    }
                }
                dbMySQL.ExecuteNonQuery(@"update Assignment set CustomerId=@CustomerId,Version=@Version,SourceOrderType=@SourceOrderType,
                    SourceOrderNo=@SourceOrderNo,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtAssignmentChange.Rows[0]["OldCustomerId"].ToString()),
                        DataAccessMySQL.CreateParameter("Version",DbType.Double,(int.Parse(dtAssignmentChange.Rows[0]["Version"].ToString())-1).ToString()),
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dtAssignmentChange.Rows[0]["OldOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dtAssignmentChange.Rows[0]["OldOrderNo"].ToString()),
                        DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignmentChange.Rows[0]["OldDesignee"].ToString()),
                        DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignmentChange.Rows[0]["OldAssignor"].ToString()),
                        DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignmentChange.Rows[0]["OldAssignDate"].ToString()),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,dtAssignmentChange.Rows[0]["OldRemark"].ToString())
                    });
                dbMySQL.ExecuteNonQuery("delete from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                        new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
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

        public bool DelAssignmentChangeRGA_H(AssignmentChange assignmentChange, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtAssignmentChange = dbMySQL.ExecuteDataTable("select * from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    });
                if (dtAssignmentChange == null || dtAssignmentChange.Rows.Count == 0)
                {
                    tran.Commit();
                    msg = "";
                    return true;
                }
                DataTable dtSupportApl_H = dbMySQL.ExecuteDataTable("select * from RGA_H where RGAType=@RGAType and RGANo=@RGANo", tran,
                       new DbParameter[] {
                        DataAccessMySQL.CreateParameter("RGAType",DbType.String,dtAssignmentChange.Rows[0]["SourceOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("RGANo",DbType.String,dtAssignmentChange.Rows[0]["SourceOrderNo"].ToString())
                       });

                if (dtSupportApl_H != null && dtSupportApl_H.Rows.Count != 0)
                {
                    if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "50")
                    {
                        tran.Commit();
                        msg = "當前維修單已完工";
                        return false;
                    }

                    if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "55")
                    {
                        tran.Commit();
                        msg = "當前維修單已結案";
                        return false;
                    }

                    if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "D")
                    {
                        tran.Commit();
                        msg = "當前維修單已作廢";
                        return false;
                    }
                }
                dbMySQL.ExecuteNonQuery(@"update Assignment set CustomerId=@CustomerId,Version=@Version,SourceOrderType=@SourceOrderType,
                    SourceOrderNo=@SourceOrderNo,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Remark=@Remark where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    new DbParameter[] {
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                        DataAccessMySQL.CreateParameter("CustomerId",DbType.String,dtAssignmentChange.Rows[0]["OldCustomerId"].ToString()),
                        DataAccessMySQL.CreateParameter("Version",DbType.Double,(int.Parse(dtAssignmentChange.Rows[0]["Version"].ToString())-1).ToString()),
                        DataAccessMySQL.CreateParameter("SourceOrderType",DbType.String,dtAssignmentChange.Rows[0]["OldOrderType"].ToString()),
                        DataAccessMySQL.CreateParameter("SourceOrderNo",DbType.String,dtAssignmentChange.Rows[0]["OldOrderNo"].ToString()),
                        DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignmentChange.Rows[0]["OldDesignee"].ToString()),
                        DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignmentChange.Rows[0]["OldAssignor"].ToString()),
                        DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignmentChange.Rows[0]["OldAssignDate"].ToString()),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,dtAssignmentChange.Rows[0]["OldRemark"].ToString())
                    });
                dbMySQL.ExecuteNonQuery("delete from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                        new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
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


        public List<AssignmentChange> SearchAssignmentChangeSUPI(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<AssignmentChange> objAssignmentChange = new List<AssignmentChange>();

            string sql = @"SELECT a.AssignOrderType,a.AssignOrderNo,Version,a.OrderDate,a.Remark,c.CustomerName,
                a.ModiReason,a.CustomerId,a.SourceOrderType,a.SourceOrderNo,a.Confirmor,a.ConfirmedDate,ea.EmployeeName as ConfirmorName,
                o.OrderCategory,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName  from AssignmentChange as a 
                left join OrderCategory as o on o.OrderType=a.AssignOrderType
                left join SupportApl_H as h on h.SupportAplOrderType=a.SourceOrderType and h.SupportAplOrderNo=a.SourceOrderNo
                left join Employee as e on e.EmployeeId=a.Designee
                left join Employee as ee on ee.EmployeeId=a.Assignor 
                left join Employee as ea on ea.EmployeeId=a.Confirmor
                left join Customer as c on c.CustomerId=a.CustomerId 
                where o.OrderCategory='A2'";

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


            DataTable dtAssignmentChange = null;
            if (strCondition != "")
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable("select * from ("+sql + strCondition + " order by a.AssignOrderType,a.AssignOrderNo,Version desc) as tb group by AssignOrderType,AssignOrderNo", dbParameters);
            }
            else
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable("select * from (" + sql + " order by a.AssignOrderType,a.AssignOrderNo,Version desc) as tb group by AssignOrderType,AssignOrderNo");
            }
            if (dtAssignmentChange.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    AssignmentChange obj = new AssignmentChange();
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.ModiReason = dr["ModiReason"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    obj.ConfirmorName = dr["ConfirmorName"].ToString();
                    obj.Confirmor = dr["Confirmor"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    objAssignmentChange.Add(obj);
                }
            }

            return objAssignmentChange;
        }

        public List<AssignmentChange> SearchAssignmentChange(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<AssignmentChange> objAssignmentChange = new List<AssignmentChange>();

            string sql = @"SELECT a.AssignOrderType,a.AssignOrderNo,max(a.Version) as Version,a.OrderDate,a.OldRemark,
                a.ModiReason,a.CustomerId,a.SourceOrderType,a.SourceOrderNo,
                o.OrderCategory,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName  from AssignmentChange as a 
                left join OrderCategory as o on o.OrderType=a.AssignOrderType
                left join ServiceArrangeModi_H as h on h.SourceOrderType=a.SourceOrderType and h.SourceOrderNo=a.SourceOrderNo
                left join Employee as e on e.EmployeeId=a.Designee
                left join Employee as ee on ee.EmployeeId=a.Assignor 
                left join Customer as c on c.CustomerId=a.CustomerId 
                where o.OrderCategory='D3'";

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


            DataTable dtAssignmentChange = null;
            if (strCondition != "")
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + strCondition+ " group by a.AssignOrderType,a.AssignOrderNo", dbParameters);
            }
            else
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + " group by a.AssignOrderType,a.AssignOrderNo");
            }
            if (dtAssignmentChange.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    AssignmentChange obj = new AssignmentChange();
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.ModiReason = dr["ModiReason"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    //obj.OrderStatus = dr["OrderStatus"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.Remark = dr["OldRemark"].ToString();
                    objAssignmentChange.Add(obj);
                }
            }

            return objAssignmentChange;
        }

        public List<AssignmentChange> SearchAssignmentChangeRGA_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<AssignmentChange> objAssignmentChange = new List<AssignmentChange>();

            string sql = @"SELECT a.AssignOrderType,a.AssignOrderNo,max(a.Version) as Version,a.OrderDate,
                a.ModiReason,a.CustomerId,a.SourceOrderType,a.SourceOrderNo,
                o.OrderCategory,h.StatusCode,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName,a.Confirmor,a.ConfirmedDate  from AssignmentChange as a 
                left join OrderCategory as o on o.OrderType=a.AssignOrderType
                left join RGA_H as h on h.RGAType=a.SourceOrderType and h.RGANo=a.SourceOrderNo
                left join Employee as e on e.EmployeeId=a.Designee
                left join Employee as ee on ee.EmployeeId=a.Assignor 
                left join Customer as c on c.CustomerId=a.CustomerId 
                where o.OrderCategory='B3' and h.StatusCode='05'";

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


            DataTable dtAssignmentChange = null;
            if (strCondition != "")
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + strCondition + " group by a.AssignOrderType,a.AssignOrderNo", dbParameters);
            }
            else
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + " group by a.AssignOrderType,a.AssignOrderNo");
            }
            if (dtAssignmentChange.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    AssignmentChange obj = new AssignmentChange();
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.ModiReason = dr["ModiReason"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    obj.StatusCode = dr["StatusCode"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.Confirmor = dr["Confirmor"].ToString();
                    obj.ConfirmedDate = dr["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    objAssignmentChange.Add(obj);
                }
            }

            return objAssignmentChange;
        }
        public List<AssignmentChange> SearchAssignmentChangeRoutineService_H(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<AssignmentChange> objAssignmentChange = new List<AssignmentChange>();

            string sql = @"SELECT a.*,o.OrderCategory,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName,c.CustomerName  from AssignmentChange as a 
                left join OrderCategory as o on o.OrderType=a.AssignOrderType
                    left join RoutineService_H as h on h.RoutineSerOrderType=a.SourceOrderType and h.RoutineSerOrderNo=a.SourceOrderNo
                    left join Employee as e on e.EmployeeId=a.Designee
                    left join Employee as ee on ee.EmployeeId=a.Assignor 
                    left join Customer as c on c.CustomerId=a.CustomerId 
                    where o.OrderCategory='D3' and h.Confirmed='N' and h.AssignCheck='Y'";

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


            DataTable dtAssignmentChange = null;
            if (strCondition != "")
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + strCondition + " group by a.AssignOrderType,a.AssignOrderNo", dbParameters);
            }
            else
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + " group by a.AssignOrderType,a.AssignOrderNo");
            }
            if (dtAssignmentChange.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    AssignmentChange obj = new AssignmentChange();
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.ModiReason = dr["ModiReason"].ToString();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    obj.OrderStatus = dr["OrderStatus"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    objAssignmentChange.Add(obj);
                }
            }

            return objAssignmentChange;
        }

        public AssignmentChange GetAssignmentChangeInfo(AssignmentChange assignmentChange)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            AssignmentChange obj = null;

            DataTable dtAssignmentChange = dbMySQL.ExecuteDataTable(@"select a.*,c.CustomerName,e.EmployeeName as ConfirmorName,ee.EmployeeName as DesigneeName from AssignmentChange as a 
                    left join Employee as e on e.EmployeeId=a.Confirmor
                    left join Employee as ee on ee.EmployeeId=a.Designee
                    left join Customer as c on c.CustomerId=a.CustomerId
                    where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and Version=@Version",
                   new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,assignmentChange.Version)
                    });

            if (dtAssignmentChange.Rows.Count > 0)
            {
                obj = new AssignmentChange();
                obj.Creator = dtAssignmentChange.Rows[0]["Creator"].ToString();
                obj.CreateDate = dtAssignmentChange.Rows[0]["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dtAssignmentChange.Rows[0]["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.AssignOrderType = dtAssignmentChange.Rows[0]["AssignOrderType"].ToString();
                obj.AssignOrderNo = dtAssignmentChange.Rows[0]["AssignOrderNo"].ToString();
                obj.OrderDate = dtAssignmentChange.Rows[0]["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dtAssignmentChange.Rows[0]["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                obj.CustomerId = dtAssignmentChange.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtAssignmentChange.Rows[0]["CustomerName"].ToString();
                obj.Version = dtAssignmentChange.Rows[0]["Version"].ToString();
                obj.SourceOrderType = dtAssignmentChange.Rows[0]["SourceOrderType"].ToString();
                obj.SourceOrderNo = dtAssignmentChange.Rows[0]["SourceOrderNo"].ToString();
                obj.Assignor = dtAssignmentChange.Rows[0]["Assignor"].ToString();
                obj.AssignDate = dtAssignmentChange.Rows[0]["AssignDate"].ToString();
                obj.NoOfPrints = int.Parse(dtAssignmentChange.Rows[0]["NoOfPrints"].ToString());
                obj.Remark = dtAssignmentChange.Rows[0]["Remark"].ToString();
                obj.ModiReason = dtAssignmentChange.Rows[0]["ModiReason"].ToString();
                obj.Designee = dtAssignmentChange.Rows[0]["Designee"].ToString();
                obj.DesigneeName = dtAssignmentChange.Rows[0]["DesigneeName"].ToString();
                obj.Confirmor = dtAssignmentChange.Rows[0]["Confirmor"].ToString();
                obj.ConfirmorName = dtAssignmentChange.Rows[0]["ConfirmorName"].ToString();
                obj.ConfirmedDate = dtAssignmentChange.Rows[0]["ConfirmedDate"].ToString() == "" ? "" : DateTime.ParseExact(dtAssignmentChange.Rows[0]["ConfirmedDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            }

            return obj;
        }

        public List<Assignment> GetAssignmentSupportApl(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Assignment> objAssignment = new List<Assignment>();

            string sql = @"SELECT a.*,o.OrderCategory,o.OrderSName,h.OrderStatus,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName,c.CustomerName  from Assignment as a 
                    left join OrderCategory as o on o.OrderType=a.AssignOrderType
                    left join supportapl_h as h on h.SupportAplOrderType=a.SourceOrderType and h.SupportAplOrderNo=a.SourceOrderNo
                    left join Employee as e on e.EmployeeId=a.Designee
                    left join Employee as ee on ee.EmployeeId=a.Assignor 
                    left join Customer as c on c.CustomerId=a.CustomerId 
                    where o.OrderCategory='A2' and h.OrderStatus='1'";

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

            DataTable dtAssignmentChange = null;
            if (strCondition != "")
            {

                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }


            if (dtAssignmentChange.Rows.Count > 0)
            {

                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    Assignment obj = new Assignment();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreateDate = dr["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AsignOrderTypeName = dr["OrderSName"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.Designee = dr["Designee"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.Assignor = dr["Assignor"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.AssignDate = dr["AssignDate"].ToString();
                    obj.NoOfPrints = int.Parse(dr["NoOfPrints"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    objAssignment.Add(obj);
                }
            }

            return objAssignment;
        }

        public List<Assignment> GetAssignmentServiceApl(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Assignment> objAssignment = new List<Assignment>();

            string sql = @"SELECT a.*,o.OrderCategory,o.OrderSName,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName,c.CustomerName  from Assignment as a 
                    left join OrderCategory as o on o.OrderType=a.AssignOrderType
                    left join ServiceArrangeModi_H as h on h.SerArrangeOrderType=a.SourceOrderType and h.SerArrangeOrderNo=a.SourceOrderNo
                    left join Employee as e on e.EmployeeId=a.Designee
                    left join Employee as ee on ee.EmployeeId=a.Assignor 
                    left join Customer as c on c.CustomerId=a.CustomerId 
                    where o.OrderCategory='D3'";

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

            DataTable dtAssignmentChange = null;
            if (strCondition != "")
            {

                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }


            if (dtAssignmentChange.Rows.Count > 0)
            {

                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    Assignment obj = new Assignment();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreateDate = dr["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AsignOrderTypeName = dr["OrderSName"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.Designee = dr["Designee"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.Assignor = dr["Assignor"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.AssignDate = dr["AssignDate"].ToString();
                    obj.NoOfPrints = int.Parse(dr["NoOfPrints"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    objAssignment.Add(obj);
                }
            }

            return objAssignment;
        }

        public List<Assignment> GetAssignmentRGA_H(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Assignment> objAssignment = new List<Assignment>();

            string sql = @"SELECT a.*,o.OrderCategory,o.OrderSName,h.StatusCode,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName,c.CustomerName  from Assignment as a 
                    left join OrderCategory as o on o.OrderType=a.AssignOrderType
                    left join RGA_H as h on h.RGAType=a.SourceOrderType and h.RGANo=a.SourceOrderNo
                    left join Employee as e on e.EmployeeId=a.Designee
                    left join Employee as ee on ee.EmployeeId=a.Assignor 
                    left join Customer as c on c.CustomerId=a.CustomerId 
                    where o.OrderCategory='B3' and h.StatusCode='05'";

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

            DataTable dtAssignmentChange = null;
            if (strCondition != "")
            {

                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtAssignmentChange = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }


            if (dtAssignmentChange.Rows.Count > 0)
            {

                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    Assignment obj = new Assignment();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreateDate = dr["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.AsignOrderTypeName = dr["OrderSName"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.Designee = dr["Designee"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.Assignor = dr["Assignor"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.AssignDate = dr["AssignDate"].ToString();
                    obj.NoOfPrints = int.Parse(dr["NoOfPrints"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    objAssignment.Add(obj);
                }
            }

            return objAssignment;
        }

        public List<Assignment> GetAssignmentRoutineService_H(Assignment assignment, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Assignment> objAssignment = new List<Assignment>();

            string sql = @"SELECT a.AssignOrderType,a.AssignOrderNo,max(a.Version) as Version,a.OrderDate,
                a.ModiReason,a.CustomerId,a.SourceOrderType,a.SourceOrderNo,
                o.OrderCategory,h.OrderStatus,e.EmployeeName as DesigneeName,ee.EmployeeName as AssignorName  from AssignmentChange as a 
                    left join OrderCategory as o on o.OrderType=a.AssignOrderType
                    left join RoutineService_H as h on h.RoutineSerOrderType=a.SourceOrderType and h.RoutineSerOrderNo=a.SourceOrderNo
                    left join Employee as e on e.EmployeeId=a.Designee
                    left join Employee as ee on ee.EmployeeId=a.Assignor 
                    left join Customer as c on c.CustomerId=a.CustomerId 
                    where o.OrderCategory='D3' and h.Confirmed='N' and h.AssignCheck='Y'";

            if (!String.IsNullOrEmpty(assignment.AssignOrderType))
            {
                sql += " and a.AssignOrderType=@AssignOrderType";
            }

            if (!String.IsNullOrEmpty(assignment.AssignOrderNo))
            {
                sql += " and a.AssignOrderNo=@AssignOrderNo";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtAssignmentChange = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignment.AssignOrderType),
                    DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignment.AssignOrderNo),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });

            if (dtAssignmentChange.Rows.Count > 0)
            {

                foreach (DataRow dr in dtAssignmentChange.Rows)
                {
                    Assignment obj = new Assignment();
                    obj.Creator = dr["Creator"].ToString();
                    obj.CreateDate = dr["CreateDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["CreateDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.AssignOrderType = dr["AssignOrderType"].ToString();
                    obj.OrderCategory = dr["OrderCategory"].ToString();
                    obj.AssignOrderNo = dr["AssignOrderNo"].ToString();
                    obj.OrderDate = dr["OrderDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["OrderDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.SourceOrderType = dr["SourceOrderType"].ToString();
                    obj.SourceOrderNo = dr["SourceOrderNo"].ToString();
                    obj.Designee = dr["Designee"].ToString();
                    obj.DesigneeName = dr["DesigneeName"].ToString();
                    obj.Assignor = dr["Assignor"].ToString();
                    obj.AssignorName = dr["AssignorName"].ToString();
                    obj.AssignDate = dr["AssignDate"].ToString();
                    obj.NoOfPrints = int.Parse(dr["NoOfPrints"].ToString());
                    obj.Remark = dr["Remark"].ToString();
                    objAssignment.Add(obj);
                }
            }

            return objAssignment;
        }

        public bool Confirmed(AssignmentChange assignmentChange, string type, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dbMySQL.CreateDbTransaction();
                DataTable dtAssignment = dbMySQL.ExecuteDataTable(@"select * from AssignmentChange where 
                    AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and Version=@Version", tran,
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                        DataAccessMySQL.CreateParameter("Version",DbType.String,assignmentChange.Version)
                    });
                if (dtAssignment == null || dtAssignment.Rows.Count == 0)
                {
                    tran.Commit();
                    msg = "";
                    return true;
                }

                if (type == "A2")
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
                            msg = "當前支援申請單已完工";
                            return false;
                        }

                        if (dtSupportApl_H.Rows[0]["OrderStatus"].ToString() == "3")
                        {
                            tran.Commit();
                            msg = "當前支援申請單已作廢";
                            return false;
                        }
                    }
                    //if (assignmentChange.Confirmed == "V" || assignmentChange.Confirmed == "N")
                    //{
                    //    dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='N' , OrderStatus='0' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //    });
                    //}

                    //if (assignmentChange.Confirmed == "Y")
                    //{
                    //    dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y' , OrderStatus='1' where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //    });
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
                            msg = "當前維修單已完工";
                            return false;
                        }

                        if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "55")
                        {
                            tran.Commit();
                            msg = "當前維修單已結案";
                            return false;
                        }

                        if (dtSupportApl_H.Rows[0]["StatusCode"].ToString() == "D")
                        {
                            tran.Commit();
                            msg = "當前維修單已作廢";
                            return false;
                        }
                    }
                }

                if (assignmentChange.Confirmed == "N")
                {
                    dbMySQL.ExecuteNonQuery("update AssignmentChange set Confirmor=@Confirmor, ConfirmedDate=@ConfirmedDate where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and Version=@Version", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,""),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,""),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,assignmentChange.Version)
                        });

                    dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Version=@Version where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                        new DbParameter[] {
                                DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                                DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignment.Rows[0]["OldDesignee"].ToString()),
                                DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignment.Rows[0]["OldAssignor"].ToString()),
                                DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignment.Rows[0]["OldAssignDate"].ToString()),
                                //DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                                DataAccessMySQL.CreateParameter("Version",DbType.String,(int.Parse(assignmentChange.Version)-1).ToString("0000")),
                                DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                                DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                        });

                    //DataTable dtAssignmentChange = dbMySQL.ExecuteDataTable(@"select * from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and Version=@Version", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                    //        DataAccessMySQL.CreateParameter("Version",DbType.String,assignmentChange.Version)
                    //    });
                    //if (dtAssignmentChange != null && dtAssignmentChange.Rows.Count > 0)
                    //{
                    //    dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    new DbParameter[] {
                    //            DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                    //            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString()),
                    //            DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignment.Rows[0]["Designee"].ToString()),
                    //            DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignment.Rows[0]["Assignor"].ToString()),
                    //            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignment.Rows[0]["AssignDate"].ToString()),
                    //            //DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                    //            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //    });
                    //    //dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y', OrderStatus='1' where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    //    new DbParameter[] {
                    //    //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //    //    });
                    //    //if (type == "A2")
                    //    //{

                    //    //}
                    //}
                    //if (type == "A2")
                    //{
                    //    //DataTable dtAssignmentChange = dbMySQL.ExecuteDataTable(@"select * from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and ConfirmedDate<>'' and ConfirmedDate<>null order by ConfirmedDate desc", tran,
                    //    //new DbParameter[] {
                    //    //    DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //    //    DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //    //});
                    //    //if (dtAssignmentChange != null && dtAssignmentChange.Rows.Count > 0)
                    //    //{
                    //    //    dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Confirmed=@Confirmed where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    //    new DbParameter[] {
                    //    //        DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                    //    //        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString()),
                    //    //        DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignmentChange.Rows[0]["Designee"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignmentChange.Rows[0]["Assignor"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignmentChange.Rows[0]["AssignDate"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                    //    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //    //    });
                    //    //    dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y', OrderStatus='1' where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    //        new DbParameter[] {
                    //    //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //    //        });
                    //    //}

                    //    dtAssignmentChange = dbMySQL.ExecuteDataTable(@"select * from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo order by Version", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //    });
                    //    if (dtAssignmentChange != null && dtAssignmentChange.Rows.Count > 0)
                    //    {
                    //        dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Confirmed=@Confirmed where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //        new DbParameter[] {
                    //            DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                    //            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString()),
                    //            DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignmentChange.Rows[0]["Designee"].ToString()),
                    //            DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignmentChange.Rows[0]["Assignor"].ToString()),
                    //            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignmentChange.Rows[0]["AssignDate"].ToString()),
                    //            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                    //            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //        });
                    //        dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y', OrderStatus='1' where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //            new DbParameter[] {
                    //            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //            });
                    //    }

                    //}

                    //if (type == "B3")
                    //{
                    //    //DataTable dtAssignmentChange = dbMySQL.ExecuteDataTable(@"select * from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and ConfirmedDate<>'' and ConfirmedDate<>null order by ConfirmedDate desc", tran,
                    //    //new DbParameter[] {
                    //    //    DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //    //    DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //    //});
                    //    //if (dtAssignmentChange != null && dtAssignmentChange.Rows.Count > 0)
                    //    //{
                    //    //    dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Confirmed=@Confirmed where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    //    new DbParameter[] {
                    //    //        DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                    //    //        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString()),
                    //    //        DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignmentChange.Rows[0]["Designee"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignmentChange.Rows[0]["Assignor"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignmentChange.Rows[0]["AssignDate"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                    //    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //    //    });
                    //    //    dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y', OrderStatus='1' where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //    //        new DbParameter[] {
                    //    //        DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //    //        DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //    //        });
                    //    //}

                    //    dtAssignmentChange = dbMySQL.ExecuteDataTable(@"select * from AssignmentChange where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo order by Version", tran,
                    //    new DbParameter[] {
                    //        DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //        DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //    });
                    //    if (dtAssignmentChange != null && dtAssignmentChange.Rows.Count > 0)
                    //    {
                    //        dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Confirmed=@Confirmed where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //        new DbParameter[] {
                    //            DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                    //            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString()),
                    //            DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignmentChange.Rows[0]["Designee"].ToString()),
                    //            DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignmentChange.Rows[0]["Assignor"].ToString()),
                    //            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignmentChange.Rows[0]["AssignDate"].ToString()),
                    //            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                    //            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                    //            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
                    //        });
                    //        dbMySQL.ExecuteNonQuery("update SupportApl_H set AsignCheck='Y', OrderStatus='1' where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                    //            new DbParameter[] {
                    //            DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,dtAssignment.Rows[0]["SourceOrderType"].ToString()),
                    //            DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,dtAssignment.Rows[0]["SourceOrderNo"].ToString())
                    //            });
                    //    }

                    //}
                }

                if (assignmentChange.Confirmed == "Y")
                {
                    dbMySQL.ExecuteNonQuery("update AssignmentChange set Confirmor=@Confirmor, ConfirmedDate=@ConfirmedDate where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo and Version=@Version", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("Confirmor",DbType.String,assignmentChange.Confirmor),
                            DataAccessMySQL.CreateParameter("ConfirmedDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,assignmentChange.Version)
                        });

                    dbMySQL.ExecuteNonQuery("update Assignment set Modifier=@Modifier,ModiDate=@ModiDate,Designee=@Designee,Assignor=@Assignor,AssignDate=@AssignDate,Confirmed=@Confirmed,Version=@Version where AssignOrderType=@AssignOrderType and AssignOrderNo=@AssignOrderNo", tran,
                        new DbParameter[] {
                            DataAccessMySQL.CreateParameter("Modifier",DbType.String,assignmentChange.Confirmor),
                            DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("Designee",DbType.String,dtAssignment.Rows[0]["Designee"].ToString()),
                            DataAccessMySQL.CreateParameter("Assignor",DbType.String,dtAssignment.Rows[0]["Assignor"].ToString()),
                            DataAccessMySQL.CreateParameter("AssignDate",DbType.String,dtAssignment.Rows[0]["AssignDate"].ToString()),
                            DataAccessMySQL.CreateParameter("Confirmed",DbType.String,"Y"),
                            DataAccessMySQL.CreateParameter("Version",DbType.String,assignmentChange.Version),
                            DataAccessMySQL.CreateParameter("AssignOrderType",DbType.String,assignmentChange.AssignOrderType),
                            DataAccessMySQL.CreateParameter("AssignOrderNo",DbType.String,assignmentChange.AssignOrderNo)
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
    }
}