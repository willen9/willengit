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
    public class GroupsDAL : IGroups
    {
        public bool DelGroups(string groupId, out string msg)
        {
            msg = "";
            if (int.Parse(
                DBHelper.DataAccess.ExecuteScalar("SELECT COUNT(*) FROM SysPermission AS sp WHERE sp.GroupID=@GroupID",
                    new DbParameter[]
                    {
                        DataAccess.CreateParameter("GroupID", DbType.String, groupId)
                    }).ToString()) > 0)
            {
                msg = "有帳號屬於群組";
                return false;
            }
            DBHelper.DataAccess.ExecuteNonQuery("DELETE FROM Groups WHERE GroupId=@GroupId", new DbParameter[]
            {
                DataAccess.CreateParameter("GroupId", DbType.String, groupId)
            });
            return true;
        }

        public DataTable GetAllGroups()
        {
            return DBHelper.DataAccess.ExecuteDataTable("SELECT DISTINCT g.GroupId FROM Groups AS g");
        }

        public DataTable GetAllFunctions()
        {
            return DBHelper.DataAccess.ExecuteDataTable("SELECT * FROM SysFunctions AS sf");
        }

        public bool SaveGroups(List<Groups> lstGroups)
        {
            DbTransaction tran = DBHelper.DataAccess.CreateDbTransaction();
            try
            {
                DBHelper.DataAccess.ExecuteNonQuery("DELETE FROM Groups WHERE GroupId=@GroupId", tran,
                    new DbParameter[]
                    {
                            DataAccess.CreateParameter("GroupId", DbType.String, lstGroups[0].GroupId)
                    });
                if (lstGroups.Count > 0)
                {
                    foreach (Groups model in lstGroups)
                    {
                        DBHelper.DataAccess.ExecuteNonQuery(@"INSERT INTO Groups(GroupId,SysFunctionsID,AD,De,Up,[Query],Ex,Im)
                                                                VALUES(@GroupId, @SysFunctionsID, 1, 1, 1, 1, 1, 1)",
                            tran,
                            new DbParameter[]
                            {
                                DataAccess.CreateParameter("GroupId", DbType.String, model.GroupId),
                                DataAccess.CreateParameter("SysFunctionsID", DbType.String, model.SysFunctionsID??"")
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

        public DataTable GetFunctionsByGroupId(string groupId)
        {
            return DBHelper.DataAccess.ExecuteDataTable("SELECT * FROM Groups AS g WHERE g.GroupId=@GroupId",new DbParameter[ ]
            {
                DataAccess.CreateParameter("GroupId",DbType.String,groupId)
            });
        }
    }
}