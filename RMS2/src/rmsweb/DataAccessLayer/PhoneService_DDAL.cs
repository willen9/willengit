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
    public class PhoneService_DDAL : IPhoneService_DDAL
    {
        public List<PhoneService_D> SearchPhoneService_D(PhoneService_D phoneService_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<PhoneService_D> objPhoneService_D = new List<PhoneService_D>();

            string sql = @"select d.*,e.EmployeeName as ProcessManName from PhoneService_D as d 
                    left join Employee as e on e.EmployeeId=d.ProcessMan where PhoneSerType=@PhoneSerType and PhoneSerNo=@PhoneSerNo";


            DataTable dtPhoneService_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("PhoneSerType",DbType.String,phoneService_D.PhoneSerType),
                    DataAccessMySQL.CreateParameter("PhoneSerNo",DbType.String,phoneService_D.PhoneSerNo)
                }); ;

            if (dtPhoneService_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPhoneService_D.Rows)
                {
                    PhoneService_D obj = new PhoneService_D();
                    obj.PhoneSerType = dr["PhoneSerType"].ToString();
                    obj.PhoneSerNo = dr["PhoneSerNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    obj.ProcessDate = dr["ProcessDate"].ToString() == "" ? "" : DateTime.ParseExact(dr["ProcessDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                    obj.Description = dr["Description"].ToString();
                    obj.ProcessMan = dr["ProcessMan"].ToString();
                    obj.ProcessManName = dr["ProcessManName"].ToString();
                    obj.Hours = double.Parse(dr["Hours"].ToString());
                    obj.Unit = dr["Unit"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objPhoneService_D.Add(obj);
                }
            }

            return objPhoneService_D;
        }

    }
}