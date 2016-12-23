using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using DataAccess.MySQL;
using DataAccessLayer.Interfaces;
using BusinessLayer.Models;

namespace DataAccessLayer
{
    public class CustomerDAL : ICustomer
    {
        DataTable ICustomer.SelectCustomer(BusinessLayer.Models.Customer customer,string condition1,string condition2)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select customer.*,Employee.EmployeeName,CurrencyId from customer left join Employee on customer.Sales=Employee.EmployeeId where 1=1 ";
            if (customer != null)
            {
                if (!string.IsNullOrEmpty(customer.Company))
                {
                    sqlStr += " and customer.Company=@Company ";
                }
                if (!string.IsNullOrEmpty(customer.UserGroup))
                {
                    sqlStr += " and customer.UserGroup=@UserGroup ";
                }
                if (!string.IsNullOrEmpty(customer.Creator))
                {
                    sqlStr += " and customer.Creator=@Creator ";
                }
                string departmentIdParameterValue1 = "";
                string departmentIdParameterValue2 = "";
                if (!string.IsNullOrEmpty(customer.CustomerId))
                {
                    if (!string.IsNullOrEmpty(condition1))
                    {
                        if (condition1 == "like1")
                        {
                            sqlStr += " and customer.CustomerId like @CustomerId ";
                            departmentIdParameterValue1 = "%" + customer.CustomerId;
                        }
                        else if (condition1 == "like2")
                        {
                            sqlStr += " and customer.CustomerId like @CustomerId ";
                            departmentIdParameterValue1 = customer.CustomerId + "%";
                        }
                        else if (condition1 == "like3")
                        {
                            sqlStr += " and customer.CustomerId like @CustomerId ";
                            departmentIdParameterValue1 = "%" + customer.CustomerId + "%";
                        }
                        else
                        {
                            sqlStr += " and customer.CustomerId " + condition1 + " @CustomerId ";
                            departmentIdParameterValue1 = customer.CustomerId;
                        }
                    }
                    else
                    {
                        sqlStr += " and customer.CustomerId=@CustomerId ";
                        departmentIdParameterValue1 = customer.CustomerId;
                    }
                }
                if (!string.IsNullOrEmpty(customer.CustomerName))
                {
                    if (!string.IsNullOrEmpty(condition2))
                    {
                        if (condition2 == "like1")
                        {
                            sqlStr += " and customer.CustomerName like @CustomerName ";
                            departmentIdParameterValue2 = "%" + customer.CustomerName;
                        }
                        else if (condition2 == "like2")
                        {
                            sqlStr += " and customer.CustomerName like @CustomerName ";
                            departmentIdParameterValue2 = customer.CustomerName + "%";
                        }
                        else if (condition2 == "like3")
                        {
                            sqlStr += " and customer.CustomerName like @CustomerName ";
                            departmentIdParameterValue2 = "%" + customer.CustomerName + "%";
                        }
                        else
                        {
                            sqlStr += " and customer.CustomerName " + condition2 + " @CustomerName ";
                            departmentIdParameterValue2 = customer.CustomerName;
                        }
                    }
                    else
                    {
                        sqlStr += " and customer.CustomerName like @CustomerName ";
                        departmentIdParameterValue2 = "%" + customer.CustomerName + "%";
                    }
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("Company", DbType.String, customer.Company),
                    DataAccessMySQL.CreateParameter("UserGroup", DbType.String, customer.UserGroup),
                    DataAccessMySQL.CreateParameter("Creator", DbType.String, customer.Creator),
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, departmentIdParameterValue1),
                    DataAccessMySQL.CreateParameter("CustomerName", DbType.String, departmentIdParameterValue2)
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }
        public bool IsModuleId(string customerid)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string count = dbMySQL.ExecuteScalar("Select Count(*) from customer where CustomerId=@CustomerId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,customerid)
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
        public List<Customer> GetCustomer( string Col, string Condition, string conditionValue, int Page)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            List<Customer> objCustomer = new List<Customer>();

            string sqlStr = @"select c.*,e.EmployeeName as SalesName from customer as c 
                    left join Employee as e on c.Sales=e.EmployeeId where 1=1 ";

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



            DataTable dtCustomer = null;
            if (strCondition != "")
            {

                dtCustomer = dbMySQL.ExecuteDataTable(sqlStr + strCondition, dbParameters);
            }
            else
            {
                dtCustomer = dbMySQL.ExecuteDataTable(sqlStr, dbParameters);
            }
            if (dtCustomer.Rows.Count > 0)
            {

                foreach (DataRow dr in dtCustomer.Rows)
                {
                    Customer obj = new Customer();
                    obj.CustomerId = dr["CustomerId"].ToString();
                    obj.CustomerName = dr["CustomerName"].ToString();
                    if(dr["CustomerType"].ToString()!="")
                    {
                        obj.CustomerType = dr["CustomerType"].ToString() == "A" ? "A:一般客戶" : "B:經銷商";
                    }else
                    {
                        obj.CustomerType = "";
                    }
                    
                    obj.Contact = dr["Contact"].ToString();
                    obj.TaxRate = dr["TaxRate"].ToString();
                    obj.Contact = dr["Contact"].ToString();
                    //obj.CustomerType = dr["CustomerType"].ToString();
                    //obj.Sales = dr["Sales"].ToString();
                    obj.TelNo = dr["TelNo"].ToString();
                    obj.FaxNo = dr["FaxNo"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Sales = dr["Sales"].ToString();
                    obj.SalesName = dr["SalesName"].ToString();
                    obj.AddressSName = dr["AddressSName"].ToString();
                    obj.Address = dr["Address"].ToString();
                    objCustomer.Add(obj);
                }
            }

            return objCustomer;
        }

        public Customer GetCustomerInfo(string CustomerId)
        {
            DataAccessMySQL dbMySQL = new DataAccessMySQL();
            dbMySQL.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            Customer obj = null;

            DataTable dtCustomer = dbMySQL.ExecuteDataTable(@"select c.*,e.EmployeeName as SalesName from customer as c 
                    left join Employee as e on c.Sales=e.EmployeeId where CustomerId=@CustomerId",
                new DbParameter[]{
                    DataAccessMySQL.CreateParameter("CustomerId",DbType.String,CustomerId)
                });
            if (dtCustomer.Rows.Count > 0)
            {
                obj = new Customer();
                obj.CustomerId = dtCustomer.Rows[0]["CustomerId"].ToString();
                obj.CustomerName = dtCustomer.Rows[0]["CustomerName"].ToString();
                obj.CustomerFName = dtCustomer.Rows[0]["CustomerFName"].ToString();
                obj.CurrencyId = dtCustomer.Rows[0]["CurrencyId"].ToString();
                if(dtCustomer.Rows[0]["CustomerType"].ToString()!="")
                {
                    obj.CustomerType = dtCustomer.Rows[0]["CustomerType"].ToString() == "A" ? "A:一般客戶" : "A:一般客戶";
                }else
                {
                    obj.CustomerType = "";
                }
                
                obj.TaxRate = dtCustomer.Rows[0]["TaxRate"].ToString();
                obj.Contact = dtCustomer.Rows[0]["Contact"].ToString();
                obj.TelNo = dtCustomer.Rows[0]["TelNo"].ToString();
                obj.FaxNo = dtCustomer.Rows[0]["FaxNo"].ToString();
                obj.Email = dtCustomer.Rows[0]["Email"].ToString();
                obj.Sales = dtCustomer.Rows[0]["Sales"].ToString();
                obj.SalesName = dtCustomer.Rows[0]["SalesName"].ToString();
                obj.Contact = dtCustomer.Rows[0]["Contact"].ToString();
                obj.AddressSName = dtCustomer.Rows[0]["AddressSName"].ToString();
                obj.Address = dtCustomer.Rows[0]["Address"].ToString();
                obj.Remark = dtCustomer.Rows[0]["Remark"].ToString();
            }

            return obj;
        }

        string ICustomer.UpdateCustomer(BusinessLayer.Models.Customer customer,string AddressId,string AddressName,string Address,string Contact,string TelNo,string FaxNo,string Remark)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = dataAccessMySql.CreateDbTransaction();
            if (int.Parse(
              dataAccessMySql.ExecuteScalar("select count(*) from customer where CustomerId=@CustomerId", tran,
                  new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
                    }).ToString()) <= 0)
            {
                return "1";
            }
            if (int.Parse(
             dataAccessMySql.ExecuteScalar("select count(*) from customer where CustomerName=@CustomerName and CustomerId!=@CustomerId", tran,
                 new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerName", DbType.String, customer.CustomerName),
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
                    }).ToString()) > 0)
            {
                return "2";
            }
            //if (!string.IsNullOrEmpty(customer.Contact))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from CustomerContact where CustomerId=@CustomerId and ContactId=@ContactId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
            //                DataAccessMySQL.CreateParameter("ContactId", DbType.String, customer.Contact)
            //            }).ToString()) <= 0)
            //    {
            //        return "3";
            //    }
            //}
            //if (!string.IsNullOrEmpty(customer.AddressSName))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
            //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, customer.AddressSName)
            //            }).ToString()) <= 0)
            //    {
            //        return "4";
            //    }
            //}
            //if (!string.IsNullOrEmpty(customer.Address))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
            //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, customer.Address)
            //            }).ToString()) <= 0)
            //    {
            //        return "5";
            //    }
            //}
            dataAccessMySql.ExecuteNonQuery(
                "update customer set CustomerName=@CustomerName,CustomerType=@CustomerType,ModiDate=@ModiDate,CustomerFName=@CustomerFName,TelNo=@TelNo,Sales=@Sales,Contact=@Contact,FaxNo=@FaxNo,TaxRate=@TaxRate,Email=@Email,AddressSName=@AddressSName,Address=@Address,Remark=@Remark where CustomerId=@CustomerId",
               tran, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CustomerName", DbType.String,customer.CustomerName),
                    DataAccessMySQL.CreateParameter("CustomerType", DbType.String, customer.CustomerType),
                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("CustomerFName", DbType.String, customer.CustomerFName),
                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, customer.TelNo),
                    DataAccessMySQL.CreateParameter("Sales", DbType.String, customer.Sales),
                    DataAccessMySQL.CreateParameter("Contact", DbType.String, customer.Contact),
                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customer.FaxNo),
                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String, customer.TaxRate),
                    DataAccessMySQL.CreateParameter("Email", DbType.String, customer.Email),
                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, customer.AddressSName),
                    DataAccessMySQL.CreateParameter("Address", DbType.String, customer.Address),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, customer.Remark),
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
                });

            dataAccessMySql.ExecuteNonQuery("Delete From CustomerAddress where CustomerId=@CustomerId",tran,new DbParameter[] {
            DataAccessMySQL.CreateParameter("CustomerId",DbType.String, customer.CustomerId)});

            if (AddressName != "" && AddressId != null)
            {
                string[] txtAddressId = AddressId.Split(',');
                string[] txtAddressName = AddressName.Split(',');
                string[] txtAddress = Address.Split(',');
                string[] txtContact = Contact.Split(',');
                string[] txtTelNo = TelNo.Split(',');
                string[] txtFaxNo = FaxNo.Split(',');
                string[] txtRemark = Remark.Split(',');

                for (int i = 0; i < txtAddressId.Length; i++)
                {
                    if (string.IsNullOrEmpty(txtAddressId[i]))
                    {
                        continue;
                    }

                    dataAccessMySql.ExecuteNonQuery("insert into CustomerAddress(CustomerId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo,Remark) values(@CustomerId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Remark)", tran,
new DbParameter[]
{
                      DataAccessMySQL.CreateParameter("AddressSName", DbType.String, txtAddressName[i].ToString()),
                      DataAccessMySQL.CreateParameter("Address", DbType.String, txtAddress[i].ToString()),
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, txtContact[i].ToString()),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, txtTelNo[i].ToString()),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, txtFaxNo[i].ToString()),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, txtRemark[i].ToString()),
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
                      DataAccessMySQL.CreateParameter("AddressId", DbType.String, txtAddressId[i].ToString())
});

                }
            }
            tran.Commit();
            return "0";
        }

        bool ICustomer.DeleteCustomer(BusinessLayer.Models.Customer customer)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = dataAccessMySql.CreateDbTransaction();
            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from customer where CustomerId=@CustomerId", tran,
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from customer where  CustomerId=@CustomerId", tran,
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
                });

            dataAccessMySql.ExecuteNonQuery("Delete From CustomerAddress where CustomerId=@CustomerId", tran, new DbParameter[] {
            DataAccessMySQL.CreateParameter("CustomerId",DbType.String, customer.CustomerId)});

            dataAccessMySql.ExecuteNonQuery("delete from CustomerContact where  CustomerId=@CustomerId",tran,
               new DbParameter[]
               {
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
               });

            tran.Commit();

            return true;
        }

        string ICustomer.InsertCustomer(BusinessLayer.Models.Customer customer, string AddressId, string AddressName,
            string Address, string Contact, string TelNo, string FaxNo, string Remark)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = dataAccessMySql.CreateDbTransaction();
            if (int.Parse(
                dataAccessMySql.ExecuteScalar("select count(*) from customer where CustomerId=@CustomerId", tran,
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
                    }).ToString()) > 0)
            {
                return "1";
            }
            if (int.Parse(
                dataAccessMySql.ExecuteScalar("select count(*) from customer where CustomerId=@CustomerId", tran,
                    new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId)
                    }).ToString()) > 0)
            {
                return "2";
            }
            //if (!string.IsNullOrEmpty(customer.Contact))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from CustomerContact where CustomerId=@CustomerId and ContactId=@ContactId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
            //                DataAccessMySQL.CreateParameter("ContactId", DbType.String, customer.Contact)
            //            }).ToString()) <= 0)
            //    {
            //        return "3";
            //    }
            //}
            //if (!string.IsNullOrEmpty(customer.AddressSName))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
            //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, customer.AddressSName)
            //            }).ToString()) <= 0)
            //    {
            //        return "4";
            //    }
            //}
            //if (!string.IsNullOrEmpty(customer.Address))
            //{
            //    if (int.Parse(
            //        dataAccessMySql.ExecuteScalar(
            //            "select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
            //            new DbParameter[]
            //            {
            //                DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
            //                DataAccessMySQL.CreateParameter("AddressId", DbType.String, customer.Address)
            //            }).ToString()) <= 0)
            //    {
            //        return "5";
            //    }
            //}
            dataAccessMySql.ExecuteNonQuery(
                "insert into customer(CurrencyId,CreateDate,CustomerId,CustomerName,CustomerFName,TaxRate,CustomerType,Sales,Contact,TelNo,FaxNo,Email,AddressSName,Address,Remark) values(@CurrencyId,@CreateDate,@CustomerId,@CustomerName,@CustomerFName,@TaxRate,@CustomerType,@Sales,@Contact,@TelNo,@FaxNo,@Email,@AddressSName,@Address,@Remark)",
                tran, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String, customer.CurrencyId),
                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
                    DataAccessMySQL.CreateParameter("CustomerName", DbType.String, customer.CustomerName),
                    DataAccessMySQL.CreateParameter("CustomerFName", DbType.String, customer.CustomerFName),
                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String, customer.TaxRate),
                    DataAccessMySQL.CreateParameter("CustomerType", DbType.String, customer.CustomerType),
                    DataAccessMySQL.CreateParameter("Sales", DbType.String, customer.Sales),
                    DataAccessMySQL.CreateParameter("Contact", DbType.String, customer.Contact),
                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, customer.TelNo),
                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customer.FaxNo),
                    DataAccessMySQL.CreateParameter("Email", DbType.String, customer.Email),
                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, customer.AddressSName),
                    DataAccessMySQL.CreateParameter("Address", DbType.String, customer.Address),
                    DataAccessMySQL.CreateParameter("Remark", DbType.String, customer.Remark)
                });

            if (AddressName != "" && AddressId != null)
            {
                string[] txtAddressId = AddressId.Split(',');
                string[] txtAddressName = AddressName.Split(',');
                string[] txtAddress = Address.Split(',');
                string[] txtContact = Contact.Split(',');
                string[] txtTelNo = TelNo.Split(',');
                string[] txtFaxNo = FaxNo.Split(',');
                string[] txtRemark = Remark.Split(',');

                for (int i = 0; i < txtAddressId.Length; i++)
                {
                    if (string.IsNullOrEmpty(txtAddressId[i]))
                    {
                        continue;
                    }

                    dataAccessMySql.ExecuteNonQuery(
                        "insert into CustomerAddress(CustomerId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo,Remark) values(@CustomerId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Remark)",
                        tran,
                        new DbParameter[]
                        {
                            DataAccessMySQL.CreateParameter("AddressSName", DbType.String, txtAddressName[i].ToString()),
                            DataAccessMySQL.CreateParameter("Address", DbType.String, txtAddress[i].ToString()),
                            DataAccessMySQL.CreateParameter("Contact", DbType.String, txtContact[i].ToString()),
                            DataAccessMySQL.CreateParameter("TelNo", DbType.String, txtTelNo[i].ToString()),
                            DataAccessMySQL.CreateParameter("FaxNo", DbType.String, txtFaxNo[i].ToString()),
                            DataAccessMySQL.CreateParameter("Remark", DbType.String, txtRemark[i].ToString()),
                            DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customer.CustomerId),
                            DataAccessMySQL.CreateParameter("AddressId", DbType.String, txtAddressId[i].ToString())
                        });

                }
            }
            tran.Commit();
            return "0";
        }

        DataTable ICustomer.SearchDetailCustomer(string col, string condition, string value)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');

            string sqlStr = @"select customer.*,employee.EmployeeName from customer left join employee on customer.Sales=employee.EmployeeId  where 1=1 ";
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
            if (strCondition != "")
            {
                return dataAccessMySql.ExecuteDataTable(sqlStr + strCondition, dbParameters);
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        bool ICustomer.ImportFile(string path,string type,string customerId,out string msg)
        {
            msg = "";
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            DbTransaction tran = null;
            try
            {
                tran = dataAccessMySql.CreateDbTransaction();
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    bool isFirstRow = true;
                    while (sr.Peek() > -1)
                    {
                        string[] valueArray = sr.ReadLine().Split(',');
                        if (isFirstRow)
                        {
                            if (type == "B")
                            {
                                if (valueArray.Length < 13)
                                {
                                    msg = "匯入檔欄位數量不正確";
                                    return false;
                                }
                            }
                            if (type == "A")
                            {
                                if (valueArray.Length <6)
                                {
                                    msg = "匯入檔欄位數量不正確";
                                    return false;
                                }
                            }
                            if (type == "C")
                            {
                                if (valueArray.Length < 7)
                                {
                                    msg = "匯入檔欄位數量不正確";
                                    return false;
                                }
                            }
                            isFirstRow = false;
                            continue;
                        }
                        if (type == "A")
                        {
                            if (int.Parse(
                                dataAccessMySql.ExecuteScalar(
                                    "select count(*) from customer where CustomerId=@CustomerId",
                                    tran,
                                    new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId)
                                    }).ToString()) <= 0)
                            {
                                msg = "客戶代號：" + customerId + "不存在";
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                                return false;
                            }
                            if (int.Parse(
                                dataAccessMySql.ExecuteScalar(
                                    "select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
                                    tran,
                                    new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String,customerId),
                                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, valueArray[0].Trim())
                                    }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery("update CustomerAddress set AddressSName=@AddressSName,Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,ModiDate=@ModiDate where CustomerId=@CustomerId and AddressId=@AddressId",
                                tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String,valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId),
                                    DataAccessMySQL.CreateParameter("AddressId", DbType.String, valueArray[0].Trim())
                                });
                            }
                            else
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                    "insert into CustomerAddress(CreateDate,CustomerId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo) values(@CreateDate,@CustomerId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo)",
                                   tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId),
                                    DataAccessMySQL.CreateParameter("AddressId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[5].Trim())
                                });
                            }
                        }
                        if (type == "B")
                        {
                            if (int.Parse(
                               dataAccessMySql.ExecuteScalar(
                                   "select count(*) from customer where CustomerId=@CustomerId",
                                   tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, valueArray[0].Trim())
                                    }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery("update Customer set CustomerName=@CustomerName,CustomerFName=@CustomerFName,CurrencyId=@CurrencyId,TaxRate=@TaxRate,CustomerType=@CustomerType,Sales=@Sales,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,AddressSName=@AddressSName,Address=@Address,ModiDate=@ModiDate where CustomerId=@CustomerId",
                                tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CustomerName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("CustomerFName", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String,valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("CustomerType", DbType.String,valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("Sales", DbType.String,valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String,valueArray[7].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[8].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[9].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[10].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[11].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[12].Trim()),
                                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, valueArray[0].Trim())
                                });
                            }
                            else
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                    "insert into customer(CreateDate,CustomerId,CustomerName,CustomerFName,CurrencyId,TaxRate,CustomerType,Sales,Contact,TelNo,FaxNo,Email,AddressSName,Address) values(@CreateDate,@CustomerId,@CustomerName,@CustomerFName,@CurrencyId,@TaxRate,@CustomerType,@Sales,@Contact,@TelNo,@FaxNo,@Email,@AddressSName,@Address)",
                                   tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("CustomerName", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("CustomerFName", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("CurrencyId", DbType.String,valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TaxRate", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("CustomerType", DbType.String,valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("Sales", DbType.String,valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String,valueArray[7].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[8].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[9].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[10].Trim()),
                                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, valueArray[11].Trim()),
                                    DataAccessMySQL.CreateParameter("Address", DbType.String, valueArray[12].Trim())
                                });
                            }
                        }
                        if (type == "C")
                        {
                            if (int.Parse(
                               dataAccessMySql.ExecuteScalar(
                                   "select count(*) from customer where CustomerId=@CustomerId",
                                   tran,
                                   new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId)
                                    }).ToString()) <= 0)
                            {
                                msg = "客戶代號：" + customerId + "不存在";
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                                return false;
                            }
                            if (int.Parse(
                                dataAccessMySql.ExecuteScalar(
                                    "select count(*) from CustomerContact where CustomerId=@CustomerId and ContactId=@ContactId",
                                    tran,
                                    new DbParameter[]
                                    {
                                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId),
                                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, valueArray[1].Trim())
                                    }).ToString()) > 0)
                            {
                                dataAccessMySql.ExecuteNonQuery("update CustomerContact set Contact=@Contact,Department=@Department,Occupation=@Occupation,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,ModiDate=@ModiDate where CustomerId=@CustomerId and ContactId=@ContactId",
                                tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("Department", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Occupation", DbType.String,valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String,valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[6].Trim()),
                                    DataAccessMySQL.CreateParameter("ModiDate", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId),
                                    DataAccessMySQL.CreateParameter("ContactId", DbType.String, valueArray[0].Trim())
                                });
                            }
                            else
                            {
                                dataAccessMySql.ExecuteNonQuery(
                                    "insert into CustomerContact(CreateDate,CustomerId,ContactId,Contact,Department,Occupation,TelNo,FaxNo,Email) values(@CreateDate,@CustomerId,@ContactId,@Contact,@Department,@Occupation,@TelNo,@FaxNo,@Email)",
                                   tran,
                                    new DbParameter[]
                                {
                                    DataAccessMySQL.CreateParameter("CreateDate", DbType.String,DateTime.Now.ToString("yyyyMMdd")),
                                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId),
                                    DataAccessMySQL.CreateParameter("ContactId", DbType.String, valueArray[0].Trim()),
                                    DataAccessMySQL.CreateParameter("Contact", DbType.String, valueArray[1].Trim()),
                                    DataAccessMySQL.CreateParameter("Department", DbType.String, valueArray[2].Trim()),
                                    DataAccessMySQL.CreateParameter("Occupation", DbType.String, valueArray[3].Trim()),
                                    DataAccessMySQL.CreateParameter("TelNo", DbType.String, valueArray[4].Trim()),
                                    DataAccessMySQL.CreateParameter("FaxNo", DbType.String, valueArray[5].Trim()),
                                    DataAccessMySQL.CreateParameter("Email", DbType.String, valueArray[6].Trim())
                                });
                            }
                        }
                    }
                    tran.Commit();
                    sr.Close();
                    sr.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        DataTable ICustomer.GetAddressInfo(BusinessLayer.Models.CustomerAddress customerAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from CustomerAddress where 1=1 ";
            if (customerAddress != null)
            {
                if (!string.IsNullOrEmpty(customerAddress.CustomerId))
                {
                    sqlStr += " and CustomerId=@CustomerId ";
                }
                if (!string.IsNullOrEmpty(customerAddress.AddressId))
                {
                    sqlStr += " and AddressId=@AddressId ";
                }
                if (!string.IsNullOrEmpty(customerAddress.AddressSName))
                {
                    sqlStr += " and AddressSName like @AddressSName ";
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerAddress.CustomerId),
                    DataAccessMySQL.CreateParameter("AddressId", DbType.String, customerAddress.AddressId),
                    DataAccessMySQL.CreateParameter("AddressSName", DbType.String, "%" + customerAddress.AddressSName + "%")
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        DataTable ICustomer.GetContactInfo(BusinessLayer.Models.CustomerContact customerContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            string sqlStr = @"select * from CustomerContact where 1=1 ";
            if (customerContact != null)
            {
                if (!string.IsNullOrEmpty(customerContact.CustomerId))
                {
                    sqlStr += " and CustomerId=@CustomerId ";
                }
                if (!string.IsNullOrEmpty(customerContact.ContactId))
                {
                    sqlStr += " and ContactId=@ContactId ";
                }
                if (!string.IsNullOrEmpty(customerContact.Contact))
                {
                    sqlStr += " and Contact like @Contact ";
                }
                return dataAccessMySql.ExecuteDataTable(sqlStr, new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                    DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId),
                    DataAccessMySQL.CreateParameter("Contact", DbType.String, "%" + customerContact.Contact + "%")
                });
            }
            return dataAccessMySql.ExecuteDataTable(sqlStr);
        }

        bool ICustomer.DeleteContact(BusinessLayer.Models.CustomerContact customerContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from CustomerContact where CustomerId=@CustomerId and ContactId=@ContactId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from CustomerContact where  CustomerId=@CustomerId and ContactId=@ContactId",
                new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                      DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId)
                });
            return true;
        }

        bool ICustomer.SaveContact(BusinessLayer.Models.CustomerContact customerContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from CustomerContact where CustomerId=@CustomerId and ContactId=@ContactId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId)
                    }).ToString()) <= 0)
            {
                return false;
                //dataAccessMySql.ExecuteNonQuery("insert into CustomerContact(CustomerId,ContactId,Contact,Department,Occupation,TelNo,FaxNo,Email,Remark) values(@CustomerId,@ContactId,@Contact,@Department,@Occupation,@TelNo,@FaxNo,@Email,@Remark)",
                //new DbParameter[]
                //{
                //      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                //      DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId),
                //      DataAccessMySQL.CreateParameter("Contact", DbType.String, customerContact.Contact),
                //      DataAccessMySQL.CreateParameter("Department", DbType.String, customerContact.Department),
                //      DataAccessMySQL.CreateParameter("Occupation", DbType.String, customerContact.Occupation),
                //      DataAccessMySQL.CreateParameter("TelNo", DbType.String, customerContact.TelNo),
                //      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customerContact.FaxNo),
                //      DataAccessMySQL.CreateParameter("Email", DbType.String, customerContact.Email),
                //      DataAccessMySQL.CreateParameter("Remark", DbType.String, customerContact.Remark)
                //});
            }
            else
            {
                dataAccessMySql.ExecuteNonQuery("update CustomerContact set Contact=@Contact,Department=@Department,Occupation=@Occupation,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,Remark=@Remark where  CustomerId=@CustomerId and ContactId=@ContactId",
               new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, customerContact.Contact),
                      DataAccessMySQL.CreateParameter("Department", DbType.String, customerContact.Department),
                      DataAccessMySQL.CreateParameter("Occupation", DbType.String, customerContact.Occupation),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, customerContact.TelNo),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customerContact.FaxNo),
                      DataAccessMySQL.CreateParameter("Email", DbType.String, customerContact.Email),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, customerContact.Remark),
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                      DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId)
                });
            }
           
            return true;
        }

        bool ICustomer.SaveNewContact(BusinessLayer.Models.CustomerContact customerContact)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from CustomerContact where CustomerId=@CustomerId and ContactId=@ContactId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                        DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId)
                    }).ToString()) <= 0)
            {
                dataAccessMySql.ExecuteNonQuery("insert into CustomerContact(CustomerId,ContactId,Contact,Department,Occupation,TelNo,FaxNo,Email,Remark) values(@CustomerId,@ContactId,@Contact,@Department,@Occupation,@TelNo,@FaxNo,@Email,@Remark)",
                new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
                      DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId),
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, customerContact.Contact),
                      DataAccessMySQL.CreateParameter("Department", DbType.String, customerContact.Department),
                      DataAccessMySQL.CreateParameter("Occupation", DbType.String, customerContact.Occupation),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, customerContact.TelNo),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customerContact.FaxNo),
                      DataAccessMySQL.CreateParameter("Email", DbType.String, customerContact.Email),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, customerContact.Remark)
                });
            }
            else
            {
                return false;
               // dataAccessMySql.ExecuteNonQuery("update CustomerContact set Contact=@Contact,Department=@Department,Occupation=@Occupation,TelNo=@TelNo,FaxNo=@FaxNo,Email=@Email,Remark=@Remark where  CustomerId=@CustomerId and ContactId=@ContactId",
               //new DbParameter[]
               // {
               //       DataAccessMySQL.CreateParameter("Contact", DbType.String, customerContact.Contact),
               //       DataAccessMySQL.CreateParameter("Department", DbType.String, customerContact.Department),
               //       DataAccessMySQL.CreateParameter("Occupation", DbType.String, customerContact.Occupation),
               //       DataAccessMySQL.CreateParameter("TelNo", DbType.String, customerContact.TelNo),
               //       DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customerContact.FaxNo),
               //       DataAccessMySQL.CreateParameter("Email", DbType.String, customerContact.Email),
               //       DataAccessMySQL.CreateParameter("Remark", DbType.String, customerContact.Remark),
               //       DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerContact.CustomerId),
               //       DataAccessMySQL.CreateParameter("ContactId", DbType.String, customerContact.ContactId)
               // });
            }

            return true;
        }

        string ICustomer.GetContactInfo(string customerId, string contact)
        {
             DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            DataTable dtReturn = dataAccessMySql.ExecuteDataTable(
                "select * from CustomerContact where CustomerId=@CustomerId and Contact=@Contact",
                new DbParameter[]
                {
                    DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerId),
                    DataAccessMySQL.CreateParameter("Contact", DbType.String, contact)
                });
            return dtReturn.Rows.Count <= 0 ? "" : dtReturn.Rows[0]["ContactId"].ToString();
        }

        bool ICustomer.SaveNewAddress(BusinessLayer.Models.CustomerAddress customerAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerAddress.CustomerId),
                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, customerAddress.AddressId)
                    }).ToString()) <= 0)
            {
                dataAccessMySql.ExecuteNonQuery("insert into CustomerAddress(CustomerId,AddressId,AddressSName,Address,Contact,TelNo,FaxNo,Remark) values(@CustomerId,@AddressId,@AddressSName,@Address,@Contact,@TelNo,@FaxNo,@Remark)",
                new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerAddress.CustomerId),
                      DataAccessMySQL.CreateParameter("AddressId", DbType.String, customerAddress.AddressId),
                      DataAccessMySQL.CreateParameter("AddressSName", DbType.String, customerAddress.AddressSName),
                      DataAccessMySQL.CreateParameter("Address", DbType.String, customerAddress.Address),
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, customerAddress.Contact),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, customerAddress.TelNo),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customerAddress.FaxNo),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, customerAddress.Remark)
                });
            }
            else
            {
                return false;
            }

            return true;
        }

        bool ICustomer.DeleteAddress(BusinessLayer.Models.CustomerAddress customerAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerAddress.CustomerId),
                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, customerAddress.AddressId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            dataAccessMySql.ExecuteNonQuery("delete from CustomerAddress where  CustomerId=@CustomerId and AddressId=@AddressId",
                new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerAddress.CustomerId),
                      DataAccessMySQL.CreateParameter("AddressId", DbType.String, customerAddress.AddressId)
                });
            return true;
        }

        bool ICustomer.SaveAddress(BusinessLayer.Models.CustomerAddress customerAddress)
        {
            DataAccessMySQL dataAccessMySql = new DataAccessMySQL();
            dataAccessMySql.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ToString();

            if (int.Parse(
               dataAccessMySql.ExecuteScalar("select count(*) from CustomerAddress where CustomerId=@CustomerId and AddressId=@AddressId",
                   new DbParameter[]
                    {
                        DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerAddress.CustomerId),
                        DataAccessMySQL.CreateParameter("AddressId", DbType.String, customerAddress.AddressId)
                    }).ToString()) <= 0)
            {
                return false;
            }
            else
            {
                dataAccessMySql.ExecuteNonQuery("update CustomerAddress set AddressSName=@AddressSName,Address=@Address,Contact=@Contact,TelNo=@TelNo,FaxNo=@FaxNo,Remark=@Remark where  CustomerId=@CustomerId and AddressId=@AddressId",
               new DbParameter[]
                {
                      DataAccessMySQL.CreateParameter("AddressSName", DbType.String, customerAddress.AddressSName),
                      DataAccessMySQL.CreateParameter("Address", DbType.String, customerAddress.Address),
                      DataAccessMySQL.CreateParameter("Contact", DbType.String, customerAddress.Contact),
                      DataAccessMySQL.CreateParameter("TelNo", DbType.String, customerAddress.TelNo),
                      DataAccessMySQL.CreateParameter("FaxNo", DbType.String, customerAddress.FaxNo),
                      DataAccessMySQL.CreateParameter("Remark", DbType.String, customerAddress.Remark),
                      DataAccessMySQL.CreateParameter("CustomerId", DbType.String, customerAddress.CustomerId),
                      DataAccessMySQL.CreateParameter("AddressId", DbType.String, customerAddress.AddressId)
                });
            }

            return true;
        }
    }
}