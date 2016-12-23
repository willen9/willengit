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
    public class RGA_QDAL : IRGA_QDAL
    {
        public List<RGA_Q> SearchRGA_Q(RGA_Q rGA_Q)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGA_Q> objRGA_Q = new List<RGA_Q>();

            string sql = @"select * from RGA_Q where RGAType=@RGAType and RGANo=@RGANo";

            DataTable dtRGA_Q = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGA_Q.RGAType),
                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGA_Q.RGANo)
                }); ;

            if (dtRGA_Q.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGA_Q.Rows)
                {
                    RGA_Q obj = new RGA_Q();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();              
                    obj.QuestionNo = dr["QuestionNo"].ToString();
                    obj.Question = dr["Question"].ToString();
                    obj.Description = dr["Description"].ToString();

                    objRGA_Q.Add(obj);
                }
            }

            return objRGA_Q;
        }

    }
}