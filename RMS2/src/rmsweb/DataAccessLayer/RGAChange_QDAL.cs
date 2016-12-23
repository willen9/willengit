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
    public class RGAChange_QDAL : IRGAChange_QDAL
    {
        public List<RGAChange_Q> SearchRGAChange_Q(RGAChange_Q rGAChange_Q)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<RGAChange_Q> objRGAChange_Q = new List<RGAChange_Q>();

            string sql = @"select * from RGAChange_Q where RGAType=@RGAType and RGANo=@RGANo";

            DataTable dtRGAChange_Q = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("RGAType",DbType.String,rGAChange_Q.RGAType),
                    DataAccessMySQL.CreateParameter("RGANo",DbType.String,rGAChange_Q.RGANo)
                }); ;

            if (dtRGAChange_Q.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRGAChange_Q.Rows)
                {
                    RGAChange_Q obj = new RGAChange_Q();
                    obj.RGAType = dr["RGAType"].ToString();
                    obj.RGANo = dr["RGANo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.Version = dr["Version"].ToString();
                    obj.NewQuestionNo = dr["NewQuestionNo"].ToString();
                    obj.NewQuestion = dr["NewQuestion"].ToString();
                    obj.NewDescription = dr["NewDescription"].ToString();
                    obj.OldQuestionNo = dr["OldQuestionNo"].ToString();
                    obj.OldQuestion = dr["OldQuestion"].ToString();
                    obj.OldDescription = dr["OldDescription"].ToString();

                    objRGAChange_Q.Add(obj);
                }
            }

            return objRGAChange_Q;
        }

    }
}