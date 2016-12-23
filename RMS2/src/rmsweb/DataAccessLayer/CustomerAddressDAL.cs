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
    public class CustomerAddressDAL : ICustomerAddressDAL
    {
        public List<CustomerAddress> GetCustomerAddress(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

            List<CustomerAddress> objCustomerAddress = new List<CustomerAddress>();

            string sqlStr = @"select * from CustomerAddress where 1=1 ";

            string strCondition = "";
            int len = 1;
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]))
                {
                    len++;
                }
            }

            DbParameter[] dbParameters = new DbParameter[len + 1];
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
                dblen++;
            }

            if(Page!=0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            DataTable dtCustomerAddress = null;
            if (strCondition != "")
            {

                dtCustomerAddress = dbMySQL.ExecuteDataTable(sqlStr + strCondition, dbParameters);
            }
            else
            {
                dtCustomerAddress = dbMySQL.ExecuteDataTable(sqlStr, dbParameters);
            }
            if (dtCustomerAddress.Rows.Count > 0)
            {

                foreach (DataRow dr in dtCustomerAddress.Rows)
                {
                    CustomerAddress obj = new CustomerAddress();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.AddressId = dr["AddressId"].ToString();
                    obj.AddressSName = dr["AddressSName"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Contact = dr["Contact"].ToString();
                    obj.TelNo = dr["TelNo"].ToString();
                    obj.FaxNo = dr["FaxNo"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objCustomerAddress.Add(obj);
                }
            }

            return objCustomerAddress;
        }

        public CustomerAddress GetCustomerAddressInfo(string CustomerId,string AddressSName)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            CustomerAddress obj = null;

            DataTable dtCustomer = dbMySQL.ExecuteDataTable(@"select * from CustomerAddress where CustomerId=@CustomerId and AddressSName=@AddressSName",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,CustomerId),
                    DataAccessMySQL.CreateParameter("AddressSName",DbType.String,AddressSName)
                });
            if (dtCustomer.Rows.Count > 0)
            {
                obj = new CustomerAddress();
                obj.CustomerId = dtCustomer.Rows[0]["CustomerId"].ToString();
                obj.AddressId = dtCustomer.Rows[0]["AddressId"].ToString();
                obj.Contact = dtCustomer.Rows[0]["Contact"].ToString();
                obj.TelNo = dtCustomer.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtCustomer.Rows[0]["FaxNo"].ToString();
                obj.Contact = dtCustomer.Rows[0]["Contact"].ToString();
                obj.AddressSName = dtCustomer.Rows[0]["AddressSName"].ToString();
                obj.Address = dtCustomer.Rows[0]["Address"].ToString();
                obj.Remark = dtCustomer.Rows[0]["Remark"].ToString();
            }

            return obj;
        }
    }
}