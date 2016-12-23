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
    public class CustomerContactDAL : ICustomerContactDAL
    {
        public List<CustomerContact> GetCustomerContact(string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<CustomerContact> objCustomerContact = new List<CustomerContact>();

            string sql = @"select * from customercontact where 1=1";

            string[] colArray = Col.Split(',');
            string[] conditionArray = Condition.Split(',');
            string[] valueArray = conditionValue.Split(',');

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
            if (Page != 0)
            {
                strCondition += " limit @Page1,@Page2";
            }
            dbParameters[dblen - 1] = DataAccessMySQL.CreateParameter("Page1", DbType.Int32, (Page - 1) * 20);
            dbParameters[dblen] = DataAccessMySQL.CreateParameter("Page2", DbType.Int32, Page * 20);

            DataTable dtCustomerContact = null;
            if (strCondition != "")
            {

                dtCustomerContact = dbMySQL.ExecuteDataTable(sql + strCondition, dbParameters);
            }
            else
            {
                dtCustomerContact = dbMySQL.ExecuteDataTable(sql, dbParameters);
            }
            if (dtCustomerContact.Rows.Count > 0)
            {

                foreach (DataRow dr in dtCustomerContact.Rows)
                {
                    CustomerContact obj = new CustomerContact();
                    obj.ContactId = dr["ContactId"].ToString();
                    obj.Contact = dr["Contact"].ToString();
                    obj.Department = dr["Department"].ToString();
                    obj.Occupation = dr["Occupation"].ToString();
                    obj.TelNo = dr["TelNo"].ToString();
                    obj.FaxNo = dr["FaxNo"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Remark = dr["Remark"].ToString();
                    objCustomerContact.Add(obj);
                }
            }

            return objCustomerContact;
        }

        public CustomerContact GetCustomerContactInfo(string CustomerId, string Contact)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            CustomerContact obj = null;

            DataTable dtCustomer = dbMySQL.ExecuteDataTable(@"select * from CustomerContact where CustomerId=@CustomerId and Contact=@Contact",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,CustomerId),
                    DataAccessMySQL.CreateParameter("Contact",DbType.String,Contact)
                });
            if (dtCustomer.Rows.Count > 0)
            {
                obj = new CustomerContact();
                obj.CustomerId = dtCustomer.Rows[0]["CustomerId"].ToString();
                obj.ContactId = dtCustomer.Rows[0]["ContactId"].ToString();
                obj.Contact = dtCustomer.Rows[0]["Contact"].ToString();
                obj.Contact = dtCustomer.Rows[0]["Contact"].ToString();
                obj.Department = dtCustomer.Rows[0]["Department"].ToString();
                obj.Email = dtCustomer.Rows[0]["Email"].ToString();
                obj.TelNo = dtCustomer.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtCustomer.Rows[0]["FaxNo"].ToString();
                obj.Remark = dtCustomer.Rows[0]["Remark"].ToString();
            }

            return obj;
        }
    }
}