using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using IDAL;
using Model;
using REGAL.Data.DataAccess;

namespace DAL
{
    public class UsersDAL : IUsers
    {
        public bool CheckLogIn(string uid, string pwd)
        {
            return int.Parse(DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) 
                                                                FROM Users AS u
                                                                WHERE u.UserId = @UserId
                                                                AND u.Pwd = @Pwd", new DbParameter[]
            {
                DataAccess.CreateParameter("UserId", DbType.String, uid),
                DataAccess.CreateParameter("Pwd", DbType.String, pwd),
            }).ToString()) > 0;
        }

        public bool DelUsers(List<Users> lstDel)
        {
            DbTransaction tran = DBHelper.DataAccess.CreateDbTransaction();
            try
            {
                foreach (Users model in lstDel)
                {
                    DBHelper.DataAccess.ExecuteNonQuery(@"DELETE FROM Users WHERE UserId=@UserId", tran, new DbParameter[]
                    {
                        DataAccess.CreateParameter("UserId", DbType.String, model.UserId)
                    });
                    DBHelper.DataAccess.ExecuteNonQuery(@"DELETE FROM SysPermission WHERE UserId=@UserId", tran, new DbParameter[]
                   {
                        DataAccess.CreateParameter("UserId", DbType.String, model.UserId)
                   });
                }
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                tran.Dispose();
            }
        }

        public DataTable GetInfo(Users model)
        {
            string sql = @"SELECT *
                            FROM Users AS u
                            LEFT JOIN SysPermission AS sp ON u.UserId=sp.UserId
                            WHERE 1=1 ";
            if (!string.IsNullOrEmpty(model.UserId))
            {
                sql += "  AND u.UserId=@UserId ";
            }
            if (!string.IsNullOrEmpty(model.Pwd))
            {
                sql += "  AND u.Pwd=@Pwd ";
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                sql += " AND u.UserName like @UserName ";
            }
            if (!string.IsNullOrEmpty(model.GroupID))
            {
                sql += " AND sp.GroupID=@GroupID ";
            }
            return DBHelper.DataAccess.ExecuteDataTable(sql, new DbParameter[]
            {
                DataAccess.CreateParameter("UserId", DbType.String, model.UserId??""),
                DataAccess.CreateParameter("Pwd", DbType.String, model.Pwd??""),
                DataAccess.CreateParameter("UserName", DbType.String,"%"+ (model.UserName??"")+"%"),
                DataAccess.CreateParameter("GroupID", DbType.String, model.GroupID??"")
            });
        }

        public bool SaveUsers(Users modelOld,Users model, string mode, out string msg)
        {
            DbTransaction tran = DBHelper.DataAccess.CreateDbTransaction();
            msg = "";
            try
            {
                if (mode == "N")
                {
                    if (int.Parse(
                        DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) FROM Users AS u WHERE u.UserId=@UserId",tran,
                            new DbParameter[]
                            {
                                DataAccess.CreateParameter("UserId", DbType.String, model.UserId)
                            }).ToString()) > 0)
                    {
                        msg = "已存在相同主鍵數據";
                        return false;
                    }
                    DBHelper.DataAccess.ExecuteNonQuery(
                        @"INSERT INTO Users(UserId,Pwd,UserName,Creator) VALUES (@UserId,@Pwd,@UserName,@Creator)",tran,
                        new DbParameter[]
                        {
                            DataAccess.CreateParameter("UserId", DbType.String, model.UserId),
                            DataAccess.CreateParameter("Pwd", DbType.String, model.Pwd),
                            DataAccess.CreateParameter("UserName", DbType.String, model.UserName),
                            DataAccess.CreateParameter("Creator", DbType.String, model.Creator)
                        });
                    if (int.Parse(
                     DBHelper.DataAccess.ExecuteScalar(
                         "SELECT COUNT(*) FROM SysPermission AS sp WHERE sp.UserId=@UserId AND GroupID=@GroupID ", tran, new DbParameter[]
                         {
                                DataAccess.CreateParameter("UserId", DbType.String, model.UserId),
                                DataAccess.CreateParameter("GroupID", DbType.String, model.GroupID)
                         }).ToString())<= 0)
                    {
                        DBHelper.DataAccess.ExecuteNonQuery(@"INSERT INTO SysPermission(UserId,GroupID,Creator) VALUES(@UserId,@GroupID,@Creator)", tran, new DbParameter[]
                          {
                            DataAccess.CreateParameter("UserId", DbType.String,model.UserId),
                            DataAccess.CreateParameter("GroupID", DbType.String, model.GroupID),
                            DataAccess.CreateParameter("Creator", DbType.String, model.Creator)
                          });
                    }
                }
                if (mode == "M")
                {
                    if (int.Parse(
                        DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) FROM Users AS u WHERE u.UserId=@UserId",tran,
                            new DbParameter[]
                            {
                                DataAccess.CreateParameter("UserId", DbType.String, model.UserId)
                            }).ToString()) <= 0)
                    {
                        msg = "數據不存在";
                        return false;
                    }
                    DBHelper.DataAccess.ExecuteNonQuery(@"UPDATE Users
                                                        SET
	                                                        UserId =@UserId,
	                                                        Pwd =@Pwd,
	                                                        UserName =@UserName,
	                                                        Creator =@Creator
                                                        WHERE UserId=@UserIdOld",tran,
                        new DbParameter[]
                        {
                            DataAccess.CreateParameter("UserId", DbType.String, model.UserId),
                            DataAccess.CreateParameter("Pwd", DbType.String, model.Pwd),
                            DataAccess.CreateParameter("UserName", DbType.String, model.UserName),
                            DataAccess.CreateParameter("Creator", DbType.String, model.Creator),
                            DataAccess.CreateParameter("UserIdOld", DbType.String, modelOld.UserId)
                        });
                    if (int.Parse(
                        DBHelper.DataAccess.ExecuteScalar(
                            "SELECT COUNT(*) FROM SysPermission AS sp WHERE sp.UserId=@UserId AND GroupID=@GroupID ", tran, new DbParameter[]
                            {
                                DataAccess.CreateParameter("UserId", DbType.String, modelOld.UserId),
                                DataAccess.CreateParameter("GroupID", DbType.String, modelOld.GroupID)
                            }).ToString()) > 0)
                    {
                        DBHelper.DataAccess.ExecuteNonQuery(@"UPDATE SysPermission
                                                            SET UserId = @UserId,
                                                                GroupID = @GroupID,
                                                                Creator = @Creator
                                                            WHERE UserId = @UserIdOld
                                                            AND GroupID = @GroupIDOld", tran, new DbParameter[]
                        {
                            DataAccess.CreateParameter("UserId", DbType.String,model.UserId),
                            DataAccess.CreateParameter("GroupID", DbType.String, model.GroupID),
                            DataAccess.CreateParameter("Creator", DbType.String, model.Creator),
                            DataAccess.CreateParameter("UserIdOld", DbType.String, modelOld.UserId),
                            DataAccess.CreateParameter("GroupIDOld", DbType.String, modelOld.GroupID)
                        });
                    }
                    else
                    {
                        DBHelper.DataAccess.ExecuteNonQuery(@"INSERT INTO SysPermission(UserId,GroupID,Creator) VALUES(@UserId,@GroupID,@Creator)", tran, new DbParameter[]
                          {
                            DataAccess.CreateParameter("UserId", DbType.String,model.UserId),
                            DataAccess.CreateParameter("GroupID", DbType.String, model.GroupID),
                            DataAccess.CreateParameter("Creator", DbType.String, model.Creator)
                          });
                    }
                }
                tran.Commit();
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

        public DataTable GetLimitByUid(string uid)
        {
            return DBHelper.DataAccess.ExecuteDataTable(@"SELECT DISTINCT g.SysFunctionsID 
                                                            FROM Groups AS g
                                                            WHERE g.GroupId =
                                                            (
                                                                SELECT sp.GroupID
                                                                FROM SysPermission AS sp

                                                                WHERE sp.UserId = @UserId
                                                            )", new DbParameter[]
            {
                DataAccess.CreateParameter("UserId", DbType.String, uid)
            });
        }
    }
}