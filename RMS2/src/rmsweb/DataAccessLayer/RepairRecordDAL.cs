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
    public class RepairRecordDAL : IRepairRecordDAL
    {
        public List<RepairRecord> SearchRepairRecord(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<RepairRecord> objRepairRecord = new List<RepairRecord>();

            string sql = @"select r.*,p.ProductName from RepairRecord as r left join Product as p on p.ProductNo=r.ProductNo where 1=1";

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


            DataTable dtRepairRecord = null;
            if (strCondition != "")
            {
                dtRepairRecord = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtRepairRecord = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtRepairRecord.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRepairRecord.Rows)
                {
                    RepairRecord obj = new RepairRecord();
                    obj.ProductNo = dr["ProductNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.SerialNo = dr["SerialNo"].ToString();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();
                    obj.ChangeDate = dr["ChangeDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ChangeDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.ChangeOrderType = dr["ChangeOrderType"].ToString();
                    obj.ChangeOrderNo = dr["ChangeOrderNo"].ToString();
                    obj.ChangeMode= dr["ChangeMode"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objRepairRecord.Add(obj);
                }
            }

            return objRepairRecord;
        }
    }
}