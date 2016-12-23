using BusinessLayer.Models;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DataAccessLayer
{
    public class SupportItemDAL : ISupportItemDAL
    {
        public bool AddSupportItem(SupportItem supportItem, out string msg)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                string count = dbMySQL.ExecuteScalar(@"select count(*) from supportitem where 
                    SupportItemId=@SupportItemId ",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,supportItem.SupportItemId)
                    }).ToString();
                if (int.Parse(count) == 0)
                {
                    dbMySQL.ExecuteNonQuery(@"insert into supportitem (Company,UserGroup,Creator,CreateDate,SupportItemId,SupportItemName,Remark) 
                        VALUES (@Company,@UserGroup,@Creator,@CreateDate,@SupportItemId,@SupportItemName,@Remark)",
                        new DbParameter[]{
                            DataAccessMySQL.CreateParameter("Company",DbType.String,supportItem.Company),
                            DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportItem.UserGroup),
                            DataAccessMySQL.CreateParameter("Creator",DbType.String,supportItem.Creator),
                            DataAccessMySQL.CreateParameter("CreateDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                            DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,supportItem.SupportItemId),
                            DataAccessMySQL.CreateParameter("SupportItemName",DbType.String,supportItem.SupportItemName),
                            DataAccessMySQL.CreateParameter("Remark",DbType.String,supportItem.Remark)
                        });
                    msg = "";
                    return true;
                }
                else
                {
                    msg = "支援項目代號已存在！";
                    return false;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public List<SupportItem> GetSupportItem(SupportItem supportItem, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportItem> objSupportItem = new List<SupportItem>();

            string sql = @"select * from SupportItem where 1=1";

            if (!String.IsNullOrEmpty(supportItem.SupportItemId))
            {
                sql += " and SupportItemId like @SupportItemId";
            }
            if (!String.IsNullOrEmpty(supportItem.SupportItemName))
            {
                sql += " and SupportItemName like @SupportItemName";
            }

            if (Page != 0)
            {
                sql += " limit @Page1,@Page2";
            }

            DataTable dtSupportItem = dbMySQL.ExecuteDataTable(sql,
                 new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,"%"+supportItem.SupportItemId+"%"),
                    DataAccessMySQL.CreateParameter("SupportItemName",DbType.String,"%"+supportItem.SupportItemName+"%"),
                    DataAccessMySQL.CreateParameter("Page1",DbType.Int32,(Page-1)*20),
                    DataAccessMySQL.CreateParameter("Page2",DbType.Int32,Page*20)
                });
            if (dtSupportItem.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportItem.Rows)
                {
                    SupportItem obj = new SupportItem();
                    obj.SupportItemId = dr["SupportItemId"].ToString();
                    obj.SupportItemName = dr["SupportItemName"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objSupportItem.Add(obj);
                }
            }

            return objSupportItem;
        }

        public bool DelSupportItem(string SupportItemId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {

                dbMySQL.ExecuteNonQuery(@"delete from SupportItem where SupportItemId=@SupportItemId",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,SupportItemId)
                    });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SupportItem GetSupportItemInfo(string SupportItemId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            SupportItem supportItem = new SupportItem();


            DataTable dtSupportItem = dbMySQL.ExecuteDataTable("select * from SupportItem where SupportItemId=@SupportItemId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,SupportItemId)
                });

            if (dtSupportItem != null && dtSupportItem.Rows.Count > 0)
            {
                supportItem.SupportItemId = dtSupportItem.Rows[0]["SupportItemId"].ToString();
                supportItem.SupportItemName = dtSupportItem.Rows[0]["SupportItemName"].ToString();
                supportItem.Remark = dtSupportItem.Rows[0]["Remark"].ToString();
                return supportItem;
            }
            else
            {
                return null;
            }
        }

        public bool IsSupportItemId(string SupportItemId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("select count(*) from SupportItem where SupportItemId=@SupportItemId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,SupportItemId)
                }).ToString();

            if (int.Parse(count) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool UpdataSupportItem(SupportItem supportItem)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            try
            {
                dbMySQL.ExecuteNonQuery(@"update supportitem set Company=@Company,UserGroup=@UserGroup,Modifier=@Modifier,ModiDate=@ModiDate,SupportItemName=@SupportItemName,
                    Remark=@Remark where SupportItemId=@SupportItemId",
                    new DbParameter[]{
                        DataAccessMySQL.CreateParameter("Company",DbType.String,supportItem.Company),
                        DataAccessMySQL.CreateParameter("UserGroup",DbType.String,supportItem.UserGroup),
                        DataAccessMySQL.CreateParameter("Modifier",DbType.String,supportItem.Modifier),
                        DataAccessMySQL.CreateParameter("ModiDate",DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                        DataAccessMySQL.CreateParameter("SupportItemId",DbType.String,supportItem.SupportItemId),
                        DataAccessMySQL.CreateParameter("SupportItemName",DbType.String,supportItem.SupportItemName),
                        DataAccessMySQL.CreateParameter("Remark",DbType.String,supportItem.Remark)
                    });
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<SupportItem> SearchSupportItem(string col, string condition, string value)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            List<SupportItem> objSupportItem = new List<SupportItem>();

            string sql = @"select * from SupportItem where 1=1";

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

            DataTable dtSupportItem = null;
            if (strCondition != "")
            {
                dtSupportItem = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtSupportItem = dbMySQL.ExecuteDataTable(sql);
            }
            if (dtSupportItem.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSupportItem.Rows)
                {
                    SupportItem obj = new SupportItem();
                    obj.SupportItemId = dr["SupportItemId"].ToString();
                    obj.SupportItemName = dr["SupportItemName"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objSupportItem.Add(obj);
                }
            }

            return objSupportItem;
        }
    }
}