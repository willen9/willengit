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
    public class SupportApl_ProcessDDAL : ISupportApl_ProcessDDAL
    {
        public List<SupportApl_ProcessD> GetSupportApl_ProductD(string SupportAplOrderType, string SupportAplOrderNo)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<SupportApl_ProcessD> objSupportApl_ProcessD = new List<SupportApl_ProcessD>();

            string sql = @"select d.*,e.EmployeeName as ProcessManName from SupportApl_ProcessD as d left join Employee as e on e.EmployeeId=d.ProcessMan where SupportAplOrderType=@SupportAplOrderType and SupportAplOrderNo=@SupportAplOrderNo order by ItemId";


            DataTable dtSupportApl_ProcessD = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SupportAplOrderType",DbType.String,SupportAplOrderType),
                    DataAccessMySQL.CreateParameter("SupportAplOrderNo",DbType.String,SupportAplOrderNo)
                });
            if (dtSupportApl_ProcessD.Rows.Count > 0)
            {

                foreach (DataRow dr in dtSupportApl_ProcessD.Rows)
                {
                    SupportApl_ProcessD obj = new SupportApl_ProcessD();
                    obj.SupportAplOrderType = dr["SupportAplOrderType"].ToString();
                    obj.SupportAplOrderNo = dr["SupportAplOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ProcessDate = dr["ProcessDate"].ToString();
                    obj.ProcessExplanation = dr["ProcessExplanation"].ToString();
                    obj.ProcessMan = dr["ProcessMan"].ToString();
                    obj.ProcessManName = dr["ProcessManName"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objSupportApl_ProcessD.Add(obj);
                }
            }

            return objSupportApl_ProcessD;
        }

    }
}