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
    public class LabelSettingDAL : ILabelSetting
    {
        public bool DelLabelSettings(List<LabelSetting> lstDel)
        {
            DbTransaction tran = DBHelper.DataAccess.CreateDbTransaction();
            try
            {
                foreach (LabelSetting model in lstDel)
                {
                    DBHelper.DataAccess.ExecuteNonQuery(@"DELETE FROM LabelSetting
                                                            WHERE [Type]=@Type
                                                            AND CustomerCode=@CustomerCode
                                                            AND VKORG=@VKORG
                                                            AND VTWEG=@VTWEG
                                                            AND SPART=@SPART", tran, new DbParameter[]
                    {
                        DataAccess.CreateParameter("Type", DbType.String, model.Type),
                        DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                        DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                        DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                        DataAccess.CreateParameter("SPART", DbType.String, model.SPART)
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

        public DataTable GetAllBtw()
        {
            return DBHelper.DataAccess.ExecuteDataTable("SELECT distinct ls.[Type] FROM LabelSetting AS ls");
        }

        public DataTable GetInfo(LabelSetting model)
        {
            string sql = @"SELECT *
                            FROM LabelSetting AS ls 
                            WHERE 1=1";
            if (!string.IsNullOrEmpty(model.Type))
            {
                sql += "  AND ls.[Type]=@Type ";
            }
            if (!string.IsNullOrEmpty(model.CustomerCode))
            {
                sql += "  AND ls.CustomerCode=@CustomerCode ";
            }
            if (!string.IsNullOrEmpty(model.VKORG))
            {
                sql += " AND ls.VKORG=@VKORG ";
            }
            if (!string.IsNullOrEmpty(model.VTWEG))
            {
                sql += " AND ls.VTWEG=@VTWEG ";
            }
            if (!string.IsNullOrEmpty(model.SPART))
            {
                sql += " AND ls.SPART=@SPART ";
            }
            if (!string.IsNullOrEmpty(model.Ary))
            {
                sql += " AND ls.Ary=@Ary ";
            }
            if (!string.IsNullOrEmpty(model.AryInitialCycle))
            {
                sql += " AND ls.AryInitialCycle=@AryInitialCycle ";
            }
            if (!string.IsNullOrEmpty(model.NextLabel))
            {
                sql += " AND ls.NextLabel=@NextLabel ";
            }

            return DBHelper.DataAccess.ExecuteDataTable(sql, new DbParameter[]
            {
                DataAccess.CreateParameter("Type", DbType.String, model.Type??""),
                DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode??""),
                DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG??""),
                DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG??""),
                DataAccess.CreateParameter("SPART", DbType.String, model.SPART??""),
                DataAccess.CreateParameter("Ary", DbType.String, model.Ary??""),
                DataAccess.CreateParameter("AryInitialCycle", DbType.String, model.AryInitialCycle??""),
                DataAccess.CreateParameter("NextLabel", DbType.String, model.NextLabel??"")
            });
        }

        public bool SaveLabelSetting(LabelSetting modelOld, LabelSetting model, string mode, out string msg)
        {
            msg = "";
            if (mode == "N")
            {
                if (int.Parse(DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) FROM LabelSetting AS ls
                                                                    WHERE ls.[Type]=@Type
                                                                    AND ls.CustomerCode=@CustomerCode
                                                                    AND ls.VKORG=@VKORG
                                                                    AND ls.VTWEG=@VTWEG
                                                                    AND ls.SPART=@SPART", new DbParameter[]
                {
                    DataAccess.CreateParameter("Type", DbType.String, model.Type),
                    DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                    DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                    DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                    DataAccess.CreateParameter("SPART", DbType.String, model.SPART)
                }).ToString()) > 0)
                {
                    msg = "已存在相同主鍵數據";
                    return false;
                }
                DBHelper.DataAccess.ExecuteNonQuery(
                    @"INSERT INTO LabelSetting
                        (
	                        [Type],
	                        CustomerCode,
	                        VKORG,
	                        VTWEG,
	                        SPART,
	                        Ary,
	                        AryInitialCycle,
	                        UpdateTime,
	                        Operator,
	                        NextLabel
                        )
                        VALUES
                        (
                            @Type,
	                        @CustomerCode,
	                        @VKORG,
	                        @VTWEG,
	                        @SPART,
	                        @Ary,
	                        @AryInitialCycle,
	                        GETDATE(),
	                        @Operator,
	                        @NextLabel
                        )",
                    new DbParameter[]
                    {
                        DataAccess.CreateParameter("Type", DbType.String, model.Type),
                        DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                        DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                        DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                        DataAccess.CreateParameter("SPART", DbType.String, model.SPART),
                        DataAccess.CreateParameter("Ary", DbType.String, model.Ary),
                        DataAccess.CreateParameter("AryInitialCycle", DbType.String, model.AryInitialCycle),
                        DataAccess.CreateParameter("Operator", DbType.String, model.Operator),
                        DataAccess.CreateParameter("NextLabel", DbType.String, model.NextLabel??"")
                    });
            }
            if (mode == "M")
            {
                if (int.Parse(DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) FROM LabelSetting AS ls
                                                                    WHERE ls.[Type] = @Type
                                                                    AND ls.CustomerCode = @CustomerCode
                                                                    AND ls.VKORG = @VKORG
                                                                    AND ls.VTWEG = @VTWEG
                                                                    AND ls.SPART = @SPART", new DbParameter[]
               {
                    DataAccess.CreateParameter("Type", DbType.String, modelOld.Type),
                    DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                    DataAccess.CreateParameter("VKORG", DbType.String, modelOld.VKORG),
                    DataAccess.CreateParameter("VTWEG", DbType.String, modelOld.VTWEG),
                    DataAccess.CreateParameter("SPART", DbType.String, modelOld.SPART)
               }).ToString()) <= 0)
                {
                    msg = "數據不存在";
                    return false;
                }
                if (model.Type != modelOld.Type || model.CustomerCode != modelOld.CustomerCode ||
                    model.VKORG != modelOld.VKORG || model.VTWEG != modelOld.VTWEG || model.SPART != modelOld.SPART)
                {
                    if (int.Parse(DBHelper.DataAccess.ExecuteScalar(@"SELECT COUNT(*) FROM LabelSetting AS ls
                                                                    WHERE ls.[Type]=@Type
                                                                    AND ls.CustomerCode=@CustomerCode
                                                                    AND ls.VKORG=@VKORG
                                                                    AND ls.VTWEG=@VTWEG
                                                                    AND ls.SPART=@SPART", new DbParameter[]
                    {
                        DataAccess.CreateParameter("Type", DbType.String, model.Type),
                        DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode),
                        DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                        DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                        DataAccess.CreateParameter("SPART", DbType.String, model.SPART)
                    }).ToString()) > 0)
                    {
                        msg = "已存在相同主鍵數據";
                        return false;
                    }
                }
                DBHelper.DataAccess.ExecuteNonQuery(@"UPDATE LabelSetting
                                                        SET	VKORG =@VKORG,
	                                                        VTWEG = @VTWEG,
	                                                        SPART = @SPART,
	                                                        Ary =@Ary,
	                                                        AryInitialCycle = @AryInitialCycle,
	                                                        UpdateTime = GETDATE(),
	                                                        Operator = @Operator,
	                                                        NextLabel = @NextLabel
                                                        WHERE [Type]=@Type
                                                        AND CustomerCode=@CustomerCode
                                                        AND VKORG=@VKORGOLD
                                                        AND VTWEG=@VTWEGOLD
                                                        AND SPART=@SPARTOLD",
                    new DbParameter[]
                    {
                        DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG),
                        DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG),
                        DataAccess.CreateParameter("SPART", DbType.String, model.SPART),
                        DataAccess.CreateParameter("Ary", DbType.String, model.Ary),
                        DataAccess.CreateParameter("AryInitialCycle", DbType.String, model.AryInitialCycle),
                        DataAccess.CreateParameter("Operator", DbType.String, model.Operator),
                        DataAccess.CreateParameter("NextLabel", DbType.String, model.NextLabel??""),
                        DataAccess.CreateParameter("Type", DbType.String, modelOld.Type),
                        DataAccess.CreateParameter("CustomerCode", DbType.String, modelOld.CustomerCode),
                        DataAccess.CreateParameter("VKORGOLD", DbType.String, modelOld.VKORG),
                        DataAccess.CreateParameter("VTWEGOLD", DbType.String, modelOld.VTWEG),
                        DataAccess.CreateParameter("SPARTOLD", DbType.String, modelOld.SPART)
                    });
            }
            return true;
        }
    }
}