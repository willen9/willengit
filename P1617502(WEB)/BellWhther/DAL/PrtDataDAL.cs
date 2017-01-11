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
    public class PrtDataDAL : IPrtData
    {
        public DataTable ExportData(List<PrtData> lstModels)
        {
            throw new NotImplementedException();
        }

        public DataTable GetInfo(PrtData model)
        {
            string sql = @"SELECT *
                            FROM  PrtData AS pd
                            WHERE 1=1 ";
            if (!string.IsNullOrEmpty(model.LabelType))
            {
                sql += "  AND pd.LabelType=@LabelType ";
            }
            if (!string.IsNullOrEmpty(model.CustomerCode))
            {
                sql += "  AND pd.CustomerCode=@CustomerCode ";
            }
            if (!string.IsNullOrEmpty(model.VKORG))
            {
                sql += " AND pd.VKORG=@VKORG ";
            }
            if (!string.IsNullOrEmpty(model.VTWEG))
            {
                sql += " AND pd.VTWEG=@VTWEG ";
            }
            if (!string.IsNullOrEmpty(model.SPART))
            {
                sql += " AND pd.SPART=@SPART ";
            }
            if (!string.IsNullOrEmpty(model.SPEC))
            {
                sql += " AND pd.SPEC=@SPEC ";
            }
            if (!string.IsNullOrEmpty(model.OPDateStart))
            {
                sql += " AND pd.OPDate>=@OPDateStart ";
            }
            if (!string.IsNullOrEmpty(model.OPDateEnd))
            {
                sql += " AND pd.OPDate<=@OPDateEnd ";
            }

            return DBHelper.DataAccess.ExecuteDataTable(sql, new DbParameter[]
            {
                DataAccess.CreateParameter("LabelType", DbType.String, model.LabelType??""),
                DataAccess.CreateParameter("CustomerCode", DbType.String, model.CustomerCode??""),
                DataAccess.CreateParameter("VKORG", DbType.String, model.VKORG??""),
                DataAccess.CreateParameter("VTWEG", DbType.String, model.VTWEG??""),
                DataAccess.CreateParameter("SPART", DbType.String, model.SPART??""),
                DataAccess.CreateParameter("SPEC", DbType.String, model.SPEC??""),
                DataAccess.CreateParameter("OPDateStart", DbType.String, model.OPDateStart??""),
                DataAccess.CreateParameter("OPDateEnd", DbType.String, model.OPDateEnd??"")
            });
        }

        public bool PintRepeat(List<PrtData> lstModels)
        {
            DbTransaction tran = DBHelper.DataAccess.CreateDbTransaction();
            try
            {
                string maxNo =
                  DBHelper.DataAccess.ExecuteScalar(
                      "SELECT MAX(pd.DocEntry) FROM PrtData AS pd WHERE pd.DocEntry LIKE @DocEntry", tran, new DbParameter[]
                      {
                            DataAccess.CreateParameter("DocEntry", DbType.String,
                                DateTime.Now.ToString("yyyyMMdd") + "%")
                      }).ToString();
                if (string.IsNullOrEmpty(maxNo))
                {
                    maxNo = DateTime.Now.ToString("yyyyMMdd") + "000001";
                }
                else
                {
                    maxNo = DateTime.Now.ToString("yyyyMMdd") +
                            (double.Parse(maxNo.Substring(8)) + 1).ToString("000000");
                }
                foreach (PrtData model in lstModels)
                {
                    DBHelper.DataAccess.ExecuteNonQuery(@"INSERT INTO PrtData
                                                            (
	                                                            DocEntry,
	                                                            PrinterName,
	                                                            PrinterIP,
	                                                            CustomerCode,
	                                                            SPEC,
	                                                            VKORG,
	                                                            VTWEG,
	                                                            SPART,
	                                                            LabelType,
	                                                            LabelData,
	                                                            OPDate,
	                                                            PrinterYN,
	                                                            PrintQty
                                                            )
                                                            SELECT @maxNo,
                                                            pd.PrinterName,
                                                            pd.PrinterIP,
                                                            pd.CustomerCode,
                                                            pd.SPEC,
                                                            pd.VKORG,
                                                            pd.VTWEG,
                                                            pd.SPART,
                                                            pd.LabelType,
                                                            pd.LabelData,
                                                            'N',
                                                            '1'
                                                            FROM PrtData AS pd
                                                            WHERE pd.DocEntry=@DocEntry", tran, new DbParameter[]
                    {
                        DataAccess.CreateParameter("maxNo", DbType.String, maxNo),
                        DataAccess.CreateParameter("DocEntry", DbType.String, model.DocEntry)
                    });
                    maxNo = DateTime.Now.ToString("yyyyMMdd") +(double.Parse(maxNo.Substring(8)) + 1).ToString("000000");
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
    }
}