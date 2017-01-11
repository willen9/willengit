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
    public class LabelMaintainDAL:ILabelMaintain
    {
        public DataTable GetInfo(LabelMaintain model)
        {
            string sql = @"SELECT * 
                            FROM LabelMaintain AS lm
                            WHERE 1 = 1 ";
            if (!string.IsNullOrEmpty(model.Type))
            {
                sql += "  AND lm.[Type]=@Type ";
            }
            if (!string.IsNullOrEmpty(model.CustomerCode))
            {
                sql += "  AND lm.CustomerCode=@CustomerCode ";
            }
            if (!string.IsNullOrEmpty(model.VKORG))
            {
                sql += " AND lm.VKORG=@VKORG ";
            }
            if (!string.IsNullOrEmpty(model.VTWEG))
            {
                sql += " AND lm.VTWEG=@VTWEG ";
            }
            if (!string.IsNullOrEmpty(model.SPART))
            {
                sql += " AND lm.SPART=@SPART ";
            }
            if (!string.IsNullOrEmpty(model.SPEC))
            {
                sql += " AND lm.SPEC=@SPEC ";
            }
            if (!string.IsNullOrEmpty(model.LCP))
            {
                sql += " AND lm.LCP = @LCP ";
            }
            if (!string.IsNullOrEmpty(model.MSLLevel))
            {
                sql += " AND lm.MSLLevel = @MSLLevel ";
            }
            if (!string.IsNullOrEmpty(model.CustomerVer))
            {
                sql += " AND lm.CustomerVer = @CustomerVer ";
            }
            if (!string.IsNullOrEmpty(model.Rev))
            {
                sql += " AND lm.Rev = @Rev ";
            }
            if (!string.IsNullOrEmpty(model.Other1))
            {
                sql += " AND lm.Other1 = @Other1 ";
            }
            if (!string.IsNullOrEmpty(model.Other2))
            {
                sql += " AND lm.Other2 = @Other2 ";
            }
            if (!string.IsNullOrEmpty(model.Other3))
            {
                sql += " AND lm.Other3 = @Other3 ";
            }
            if (!string.IsNullOrEmpty(model.Other4))
            {
                sql += " AND lm.Other4 = @Other4 ";
            }
            if (!string.IsNullOrEmpty(model.Other5))
            {
                sql += " AND lm.Other5 = @Other5 ";
            }
           
            return DBHelper.DataAccess.ExecuteDataTable(sql, new DbParameter[]
            {
                DataAccess.CreateParameter("Type", DbType.String, model.Type??""),
                DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode??""),
                DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG??""),
                DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG??""),
                DataAccess.CreateParameter("SPART", DbType.String, model.SPART??""),
                DataAccess.CreateParameter("SPEC", DbType.String, model.SPEC??""),
                DataAccess.CreateParameter("LCP", DbType.String, model.LCP??""),
                DataAccess.CreateParameter("MSLLevel", DbType.String, model.MSLLevel??""),
                DataAccess.CreateParameter("CustomerVer", DbType.String, model.CustomerVer??""),
                DataAccess.CreateParameter("Rev", DbType.String, model.Rev??""),
                DataAccess.CreateParameter("Other1", DbType.String, model.Other1??""),
                DataAccess.CreateParameter("Other2", DbType.String, model.Other2??""),
                DataAccess.CreateParameter("Other3", DbType.String, model.Other3??""),
                DataAccess.CreateParameter("Other4", DbType.String, model.Other4??""),
                DataAccess.CreateParameter("Other5", DbType.String, model.Other5??"")
            });
        }

        public bool DelLabelMaintains(List<LabelMaintain> lstDel)
        {
            DbTransaction tran = DBHelper.DataAccess.CreateDbTransaction();
            try
            {
                foreach (LabelMaintain model in lstDel)
                {
                    DBHelper.DataAccess.ExecuteNonQuery(@"DELETE FROM LabelMaintain 
                                                        WHERE[Type] = @Type
                                                        AND CustomerCode = @CustomerCode
                                                        AND VKORG = @VKORG
                                                        AND VTWEG = @VTWEG
                                                        AND SPART = @SPART
                                                        AND SPEC = @SPEC", tran, new DbParameter[]
                    {
                        DataAccess.CreateParameter("Type", DbType.String, model.Type),
                        DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                        DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                        DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                        DataAccess.CreateParameter("SPART", DbType.String, model.SPART),
                        DataAccess.CreateParameter("SPEC", DbType.String, model.SPEC)
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

        public bool SaveLabelMaintain(LabelMaintain model, string mode,out string msg)
        {
            msg = "";
            if (mode == "N")
            {
                if (int.Parse(DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) FROM LabelMaintain 
                                                        WHERE [Type]=@Type 
                                                        AND CustomerCode=@CustomerCode 
                                                        AND VKORG=@VKORG 
                                                        AND VTWEG=@VTWEG 
                                                        AND SPART=@SPART 
                                                        AND SPEC=@SPEC", new DbParameter[]
                {
                    DataAccess.CreateParameter("Type", DbType.String, model.Type),
                    DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                    DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                    DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                    DataAccess.CreateParameter("SPART", DbType.String, model.SPART),
                    DataAccess.CreateParameter("SPEC", DbType.String, model.SPEC)
                }).ToString()) > 0)
                {
                    msg = "已存在相同主鍵數據";
                    return false;
                }
                DBHelper.DataAccess.ExecuteNonQuery(
                    @"INSERT INTO LabelMaintain([Type],CustomerCode,VKORG,VTWEG,SPART,SPEC,LCP,MSLLevel,CustomerVer,Rev,Other1,Other2,Other3,Other4,Other5)
                                                        VALUES(@Type, @CustomerCode, @VKORG, @VTWEG, @SPART, @SPEC, @LCP, @MSLLevel, @CustomerVer, @Rev, @Other1, @Other2, @Other3, @Other4, @Other5)",
                    new DbParameter[]
                    {
                        DataAccess.CreateParameter("Type", DbType.String, model.Type),
                        DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                        DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                        DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                        DataAccess.CreateParameter("SPART", DbType.String, model.SPART),
                        DataAccess.CreateParameter("SPEC", DbType.String, model.SPEC),
                        DataAccess.CreateParameter("LCP", DbType.String, model.LCP),
                        DataAccess.CreateParameter("MSLLevel", DbType.String, model.MSLLevel),
                        DataAccess.CreateParameter("CustomerVer", DbType.String, model.CustomerVer),
                        DataAccess.CreateParameter("Rev", DbType.String, model.Rev),
                        DataAccess.CreateParameter("Other1", DbType.String, model.Other1),
                        DataAccess.CreateParameter("Other2", DbType.String, model.Other2),
                        DataAccess.CreateParameter("Other3", DbType.String, model.Other3),
                        DataAccess.CreateParameter("Other4", DbType.String, model.Other4),
                        DataAccess.CreateParameter("Other5", DbType.String, model.Other5)
                    });
            }
            if (mode == "M")
            {
                if (int.Parse(DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) FROM LabelMaintain 
                                                        WHERE [Type]=@Type 
                                                        AND CustomerCode=@CustomerCode 
                                                        AND VKORG=@VKORG 
                                                        AND VTWEG=@VTWEG 
                                                        AND SPART=@SPART 
                                                        AND SPEC=@SPEC", new DbParameter[]
               {
                    DataAccess.CreateParameter("Type", DbType.String, model.Type),
                    DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                    DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                    DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                    DataAccess.CreateParameter("SPART", DbType.String, model.SPART),
                    DataAccess.CreateParameter("SPEC", DbType.String, model.SPEC)
               }).ToString()) <= 0)
                {
                    msg = "數據不存在";
                    return false;
                }
                DBHelper.DataAccess.ExecuteNonQuery(@"UPDATE LabelMaintain
                                                        SET	LCP = @LCP,
	                                                        MSLLevel = @MSLLevel,
	                                                        CustomerVer = @CustomerVer,
	                                                        Rev = @Rev,
	                                                        Other1 = @Other1,
	                                                        Other2 = @Other2,
	                                                        Other3 = @Other3,
	                                                        Other4 = @Other4,
	                                                        Other5 =@Other5
                                                        WHERE  [Type]=@Type 
                                                        AND CustomerCode=@CustomerCode 
                                                        AND VKORG=@VKORG 
                                                        AND VTWEG=@VTWEG 
                                                        AND SPART=@SPART 
                                                        AND SPEC=@SPEC",
                    new DbParameter[]
                    {
                        DataAccess.CreateParameter("LCP", DbType.String, model.LCP),
                        DataAccess.CreateParameter("MSLLevel", DbType.String, model.MSLLevel),
                        DataAccess.CreateParameter("CustomerVer", DbType.String, model.CustomerVer),
                        DataAccess.CreateParameter("Rev", DbType.String, model.Rev),
                        DataAccess.CreateParameter("Other1", DbType.String, model.Other1),
                        DataAccess.CreateParameter("Other2", DbType.String, model.Other2),
                        DataAccess.CreateParameter("Other3", DbType.String, model.Other3),
                        DataAccess.CreateParameter("Other4", DbType.String, model.Other4),
                        DataAccess.CreateParameter("Other5", DbType.String, model.Other5),
                        DataAccess.CreateParameter("Type", DbType.String, model.Type),
                        DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                        DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                        DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                        DataAccess.CreateParameter("SPART", DbType.String, model.SPART),
                        DataAccess.CreateParameter("SPEC", DbType.String, model.SPEC)
                    });
            }
            return true;
        }
    }
}