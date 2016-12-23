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
    public class ServiceArrangeModi_DDAL : IServiceArrangeModi_DDAL
    {
        public List<ServiceArrangeModi_D> SearchServiceArrangeModi_D(string SerArrangeOrderType, string SerArrangeOrderNo, string Version)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<ServiceArrangeModi_D> objServiceArrangeChange_D = new List<ServiceArrangeModi_D>();

            string sql = @"select * from ServiceArrangeModi_D  where SerArrangeOrderType=@SerArrangeOrderType 
            and SerArrangeOrderNo=@SerArrangeOrderNo and Version=@Version";


            DataTable dtServiceArrangeModi_D = dbMySQL.ExecuteDataTable(sql,
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("SerArrangeOrderType",DbType.String,SerArrangeOrderType),
                    DataAccessMySQL.CreateParameter("SerArrangeOrderNo",DbType.String,SerArrangeOrderNo),
                    DataAccessMySQL.CreateParameter("Version",DbType.String,Version)
                }); ;

            if (dtServiceArrangeModi_D.Rows.Count > 0)
            {
                foreach (DataRow dr in dtServiceArrangeModi_D.Rows)
                {
                    ServiceArrangeModi_D obj = new ServiceArrangeModi_D();
                    obj.SerArrangeOrderType = dr["SerArrangeOrderType"].ToString();
                    obj.SerArrangeOrderNo = dr["SerArrangeOrderNo"].ToString();
                    obj.ItemId = dr["ItemId"].ToString();
                    obj.ArrangeMonth = obj.NewArrangeMonth = dr["NewArrangeMonth"].ToString();
                    obj.AddressSName = obj.NewAddressSName = dr["NewAddressSName"].ToString();
                    obj.Address = obj.NewAddress = dr["NewAddress"].ToString();
                    if (dr["NewIsClosed"].ToString() == "Y")
                    {
                        obj.IsClosed = obj.NewIsClosed = "Y.指定結案";
                    }
                    if (dr["NewIsClosed"].ToString() == "N")
                    {
                        obj.IsClosed = obj.NewIsClosed = "N.不結案";
                    }

                    obj.Remark = obj.NewRemark = dr["NewRemark"].ToString();

                    objServiceArrangeChange_D.Add(obj);
                }
            }

            return objServiceArrangeChange_D;
        }

        
    }
}