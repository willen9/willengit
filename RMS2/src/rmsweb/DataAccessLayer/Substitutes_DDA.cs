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
    public class Substitutes_DDAL : ISubstitutes_DDAL
    {
        public List<Substitutes_D> SearchSubstitutes_D(Substitutes_D substitutes_D)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Substitutes_D> objSubstitutes_D = new List<Substitutes_D>();

            string sql = @"select * from Substitutes_D as d left join product as p on d.SubstitutesNo=p.ProductNo  where ComponentNo=@ComponentNo and MajorComponentNo=@MajorComponentNo and SubstitutesType=@SubstitutesType";


            DataTable dtSubstitutes_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("ComponentNo",DbType.String,substitutes_D.ComponentNo),
                    DataAccessMySQL.CreateParameter("MajorComponentNo",DbType.String,substitutes_D.MajorComponentNo),
                    DataAccessMySQL.CreateParameter("SubstitutesType",DbType.String,substitutes_D.SubstitutesType)
                }); ;

            if (dtSubstitutes_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSubstitutes_D.Rows)
                {
                    Substitutes_D obj = new Substitutes_D();
                    obj.ComponentNo = dr["ComponentNo"].ToString();
                    obj.MajorComponentNo = dr["MajorComponentNo"].ToString();
                    obj.SubstitutesType = dr["SubstitutesType"].ToString();
                    obj.SubstitutesNo = dr["SubstitutesNo"].ToString();
                    obj.ProductName = dr["ProductName"].ToString();
                    obj.Specification = dr["Specification"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.QTY = double.Parse(dr["QTY"].ToString());
                    obj.Priority = dr["Priority"].ToString();
                    obj.Remark = dr["Remark"].ToString();

                    objSubstitutes_D.Add(obj);
                }
            }

            return objSubstitutes_D;
        }
        
    }
}