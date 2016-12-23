using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using rmsweb.Controllers;

namespace CMSI09.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index(string action)
        {
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            if (hfc.Count > 0)
            {
                NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                string path = Server.MapPath(@"~\ImportFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string name = hfc[0].FileName;
                //判断文件名字是否包含路径名，如果有则提取文件名
                if (name.LastIndexOf("\\") > -1)
                {
                    name = name.Substring(name.LastIndexOf("\\") + 1);
                }
                hfc[0].SaveAs(path + "\\" + name);
                CustomerLogic customerLogic = new CustomerLogic();
                string msg = "";
                if (!customerLogic.ImportFile(path + "\\" + name, nvc.Get("type"), nvc.Get("customerId"), out msg))
                {
                    if (msg != "")
                    {
                        return Json(msg, "text/x-json");
                    }
                    return Json("匯入失敗", "text/x-json");
                }

                return Json("1", "text/x-json");
            }
            if (action == "btnAdd")
            {
                return RedirectToAction("CUR", "Main");
            }
            if (action == "btnExport")
            {
                object obj = new object();
                lock (obj)
                {
                    CustomerLogic customerLogic = new CustomerLogic();
                    Customer customer = new Customer();
                    customer.CustomerId = Request.Form["customerId"];
                    customer.CustomerName = Request.Form["customerName"];
                    DataTable dtExport = customerLogic.SelectCustomer(customer, Request.Form["selectCondition1"], Request.Form["selectCondition2"]);
                    string path = Server.MapPath(@"~\exports");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fs = new FileStream(path + @"\CustomerInfo.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        HSSFWorkbook workbook = new HSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("CustomerInfo");
                        sheet.SetColumnWidth(0, 20 * 256);
                        sheet.SetColumnWidth(1, 20 * 256);
                        sheet.SetColumnWidth(2, 20 * 256);
                        sheet.SetColumnWidth(3, 20 * 256);
                        sheet.SetColumnWidth(4, 20 * 256);

                        IRow row = sheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("客戶代號");
                        row.CreateCell(1).SetCellValue("客戶簡稱");
                        row.CreateCell(2).SetCellValue("聯絡人");
                        row.CreateCell(3).SetCellValue("電話");
                        row.CreateCell(4).SetCellValue("業務員");


                        int index = 1;

                        for (int i = 0; i < dtExport.Rows.Count; i++)
                        {

                            row = sheet.CreateRow(index);
                            row.CreateCell(0).SetCellValue(dtExport.Rows[i]["CustomerId"].ToString());
                            row.CreateCell(1).SetCellValue(dtExport.Rows[i]["CustomerName"].ToString());
                            row.CreateCell(2).SetCellValue(dtExport.Rows[i]["Contact"].ToString());
                            row.CreateCell(3).SetCellValue(dtExport.Rows[i]["TelNo"].ToString());
                            row.CreateCell(4).SetCellValue(dtExport.Rows[i]["Sales"].ToString());
                            index++;

                        }

                        workbook.Write(fs);
                    }
                    //using (FileStream fs = new FileStream(path + @"\CustomerInfo.csv", FileMode.Create, FileAccess.Write))
                    //{
                    //    StreamWriter sw = new StreamWriter(fs);
                    //    sw.WriteLine("客戶代號,客戶簡稱,聯絡人,電話,業務員");
                    //    foreach (DataRow dr in dtExport.Rows)
                    //    {
                    //        sw.WriteLine(dr["CustomerId"].ToString() + "," + dr["CustomerName"].ToString() + "," + dr["Contact"].ToString() + "," + dr["TelNo"].ToString() + "," + dr["Sales"].ToString());
                    //    }
                    //    sw.Close();
                    //    sw.Dispose();
                    //}
                    //return File(new FileStream(path + @"\CustomerInfo.csv", FileMode.Open), "text/plain", "CustomerInfo.csv");
                    return File(new FileStream(path + @"\CustomerInfo.xls", FileMode.Open), "text/plain", "CustomerInfo.xls");
                }
            }
            if (action == "btnSearchDetail")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];

                CustomerLogic customerLogic = new CustomerLogic();
                DataTable dtDisplay = customerLogic.SearchDetailCustomer(col, condition, value);
                List<Customer> lst = new List<Customer>();
                Customer customer = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    customer = new Customer();
                    customer.Company = dr["Company"].ToString();
                    customer.UserGroup = dr["UserGroup"].ToString();
                    customer.Creator = dr["Creator"].ToString();
                    customer.CreateDate = dr["CreateDate"].ToString();
                    customer.Modifier = dr["Modifier"].ToString();
                    customer.ModiDate = dr["ModiDate"].ToString();
                    customer.CustomerId = dr["CustomerId"].ToString();
                    customer.CustomerName = dr["CustomerName"].ToString();
                    customer.CustomerFName = dr["CustomerFName"].ToString();
                    customer.CurrencyId = dr["CurrencyId"].ToString();
                    customer.TaxRate = dr["TaxRate"].ToString();
                    customer.CustomerType = dr["CustomerType"].ToString();
                    customer.Sales = dr["Sales"].ToString();
                    customer.SalesName = dr["EmployeeName"].ToString();
                    customer.Contact = dr["Contact"].ToString();
                    //customer.ContactName = dr["ContactName"].ToString();
                    customer.TelNo = dr["TelNo"].ToString();
                    customer.FaxNo = dr["FaxNo"].ToString();
                    customer.Email = dr["Email"].ToString();
                    customer.AddressSName = dr["AddressSName"].ToString();
                    customer.Address = dr["Address"].ToString();
                    customer.Remark = dr["Remark"].ToString();
                    lst.Add(customer);
                }
                ViewData["DisplayData"] = lst;
                return View();
            }
            if (action == "btnSearch")
            {
                CustomerLogic customerLogic = new CustomerLogic();
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerId"];
                customer.CustomerName = Request.Form["customerName"];
                DataTable dtDisplay = customerLogic.SelectCustomer(customer, Request.Form["selectCondition1"], Request.Form["selectCondition2"]);
                List<Customer> lst = new List<Customer>();
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    customer = new Customer();
                    customer.Company = dr["Company"].ToString();
                    customer.UserGroup = dr["UserGroup"].ToString();
                    customer.Creator = dr["Creator"].ToString();
                    customer.CreateDate = dr["CreateDate"].ToString();
                    customer.Modifier = dr["Modifier"].ToString();
                    customer.ModiDate = dr["ModiDate"].ToString();
                    customer.CustomerId = dr["CustomerId"].ToString();
                    customer.CustomerName = dr["CustomerName"].ToString();
                    customer.CustomerFName = dr["CustomerFName"].ToString();
                    customer.CurrencyId = dr["CurrencyId"].ToString();
                    customer.TaxRate = dr["TaxRate"].ToString();
                    customer.CustomerType = dr["CustomerType"].ToString();
                    customer.Sales = dr["Sales"].ToString();
                    customer.SalesName = dr["EmployeeName"].ToString();
                    customer.Contact = dr["Contact"].ToString();
                    //customer.ContactName = dr["ContactName"].ToString();
                    customer.TelNo = dr["TelNo"].ToString();
                    customer.FaxNo = dr["FaxNo"].ToString();
                    customer.Email = dr["Email"].ToString();
                    customer.AddressSName = dr["AddressSName"].ToString();
                    customer.Address = dr["Address"].ToString();
                    customer.Remark = dr["Remark"].ToString();
                    lst.Add(customer);
                }
                ViewBag.CustomerId = Request.Form["customerId"];
                ViewBag.CustomerName = Request.Form["customerName"];
                ViewBag.SelectCondition1 = Request.Form["selectCondition1"].ToString();
                ViewBag.SelectCondition2 = Request.Form["selectCondition2"].ToString();
                ViewData["DisplayData"] = lst;
            }
            else
            {
                CustomerLogic customerLogic = new CustomerLogic();
                DataTable dtDisplay = customerLogic.SelectCustomer(null,null,null);
                List<Customer> lst = new List<Customer>();
                Customer customer = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    customer = new Customer();
                    customer.Company = dr["Company"].ToString();
                    customer.UserGroup = dr["UserGroup"].ToString();
                    customer.Creator = dr["Creator"].ToString();
                    customer.CreateDate = dr["CreateDate"].ToString();
                    customer.Modifier = dr["Modifier"].ToString();
                    customer.ModiDate = dr["ModiDate"].ToString();
                    customer.CustomerId = dr["CustomerId"].ToString();
                    customer.CustomerName = dr["CustomerName"].ToString();
                    customer.CustomerFName = dr["CustomerFName"].ToString();
                    customer.CurrencyId = dr["CurrencyId"].ToString();
                    customer.TaxRate = dr["TaxRate"].ToString();
                    customer.CustomerType = dr["CustomerType"].ToString();
                    customer.Sales = dr["Sales"].ToString();
                    customer.SalesName = dr["EmployeeName"].ToString();
                    customer.Contact = dr["Contact"].ToString();
                    //customer.ContactName = dr["ContactName"].ToString();
                    customer.TelNo = dr["TelNo"].ToString();
                    customer.FaxNo = dr["FaxNo"].ToString();
                    customer.Email = dr["Email"].ToString();
                    customer.AddressSName = dr["AddressSName"].ToString();
                    customer.Address = dr["Address"].ToString();
                    customer.Remark = dr["Remark"].ToString();
                    lst.Add(customer);
                }
                ViewData["DisplayData"] = lst;
            }
            CustomerLogic customerLogic2 = new CustomerLogic();
            DataTable dtDisplay2 = customerLogic2.SelectCustomer(null,null,null);
            List<Customer> lst2 = new List<Customer>();
            Customer customer2 = null;
            foreach (DataRow dr in dtDisplay2.Rows)
            {
                customer2 = new Customer();
                customer2.CustomerId = dr["CustomerId"].ToString();
                customer2.CustomerName = dr["CustomerName"].ToString();
                lst2.Add(customer2);
            }
            ViewData["SelectData"] = lst2;
            return View();
        }

        public ActionResult CUR(string action, string id, string type)
        {
            CustomerAddress customerAddress = new CustomerAddress();
            List<CustomerAddress> lstAddress = new List<CustomerAddress>();
            if (action == "btnSaveNew")
            {
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerid"];
                customer.CustomerName = Request.Form["CustomerName"];
                customer.CustomerType = Request.Form["CustomerType"];
                customer.CustomerFName = Request.Form["CustomerFName"];
                customer.TelNo = Request.Form["TelNo"];
                customer.Sales = Request.Form["Sales"];
                customer.SalesName = Request.Form["employeeName"];
                customer.Contact = Request.Form["contact"];
                customer.CurrencyId = Request.Form["Currency"];
                //customer.ContactName = Request.Form["contactName"];
                customer.FaxNo = Request.Form["FaxNo"];
                customer.TaxRate = Request.Form["TaxRate"];
                customer.Email = Request.Form["Email"];
                //customer.AddressSName = Request.Form["txtSaveAddress1"];
                //customer.Address = Request.Form["txtSaveAddress2"];
                customer.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];
                string AddressName = Request.Form["txtAddressSName"];
                string Address = Request.Form["txtAddress"];
                string Contact = Request.Form["txtContact"];
                string TelNo = Request.Form["txtTelNo"];
                string FaxNo = Request.Form["txtFaxNo"];
                string Remark = Request.Form["txtRemark"];

                CustomerLogic employeeLogic = new CustomerLogic();
                string returnString = employeeLogic.InsertCustomer(customer, AddressId, AddressName, Address, Contact, TelNo, FaxNo, Remark);
                if (returnString != "0")
                {
                    ViewBag.CustomerId = customer.CustomerId;
                    ViewBag.CustomerName = customer.CustomerName;
                    ViewBag.CustomerFName = customer.CustomerFName;
                    ViewBag.CustomerType = customer.CustomerType;
                    ViewBag.TelNo = customer.TelNo;
                    ViewBag.Sales = customer.Sales;
                    ViewBag.SalesName = customer.SalesName;
                    //ViewBag.ContactName = customer.ContactName;
                    ViewBag.Contact = customer.Contact;
                    ViewBag.FaxNo = customer.FaxNo;
                    ViewBag.TaxRate = customer.TaxRate;
                    ViewBag.Email = customer.Email;
                    ViewBag.AddressSName = customer.AddressSName;
                    ViewBag.Remark = customer.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶代號已存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶簡稱已存在');</script>";
                    }
                    //if (returnString == "3")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶聯絡人不存在');</script>";
                    //}
                    //if (returnString == "4")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址1不存在');</script>";
                    //}
                    //if (returnString == "5")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址2不存在');</script>";
                    //}
                    ViewBag.Type = "New";
                    customerAddress.CustomerId  = customer.CustomerId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextNew")
            {
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerid"];
                customer.CustomerName = Request.Form["CustomerName"];
                customer.CustomerType = Request.Form["CustomerType"];
                customer.CustomerFName = Request.Form["CustomerFName"];
                customer.TelNo = Request.Form["TelNo"];
                customer.Sales = Request.Form["Sales"];
                customer.SalesName = Request.Form["employeeName"];
                customer.Contact = Request.Form["contact"];
                //customer.ContactName = Request.Form["contactName"];
                customer.CurrencyId = Request.Form["Currency"];
                customer.FaxNo = Request.Form["FaxNo"];
                customer.TaxRate = Request.Form["TaxRate"];
                customer.Email = Request.Form["Email"];
                //customer.AddressSName = Request.Form["txtSaveAddress1"];
                //customer.Address = Request.Form["txtSaveAddress2"];
                customer.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];               
                string AddressName = Request.Form["txtAddressSName"];
                string Address = Request.Form["txtAddress"];
                string Contact = Request.Form["txtContact"];
                string TelNo = Request.Form["txtTelNo"];
                string FaxNo = Request.Form["txtFaxNo"];
                string Remark = Request.Form["txtRemark"];

                CustomerLogic employeeLogic = new CustomerLogic();
                string returnString = employeeLogic.InsertCustomer(customer, AddressId, AddressName, Address, Contact, TelNo, FaxNo, Remark);
                if (returnString != "0")
                {
                    ViewBag.CustomerId = customer.CustomerId;
                    ViewBag.CustomerName = customer.CustomerName;
                    ViewBag.CustomerFName = customer.CustomerFName;
                    ViewBag.CustomerType = customer.CustomerType;
                    ViewBag.TelNo = customer.TelNo;
                    ViewBag.Sales = customer.Sales;
                    ViewBag.SalesName = customer.SalesName;
                    ViewBag.Contact = customer.Contact;
                    //ViewBag.ContactName = customer.ContactName;
                    ViewBag.FaxNo = customer.FaxNo;
                    ViewBag.TaxRate = customer.TaxRate;
                    ViewBag.Email = customer.Email;
                    ViewBag.AddressSName = customer.AddressSName;
                    ViewBag.Remark = customer.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶代號已存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶簡稱已存在');</script>";
                    }
                    //if (returnString == "3")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶聯絡人不存在');</script>";
                    //}
                    //if (returnString == "4")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址1不存在');</script>";
                    //}
                    //if (returnString == "5")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址2不存在');</script>";
                    //}
                    customerAddress.CustomerId = customer.CustomerId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
                }
                else
                {
                    ViewData["DisplayDataAddress"] =lstAddress;
                }
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnSaveEdit")
            {
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerid"];
                customer.CustomerName = Request.Form["CustomerName"];
                customer.CustomerType = Request.Form["CustomerType"];
                customer.CustomerFName = Request.Form["CustomerFName"];
                customer.TelNo = Request.Form["TelNo"];
                customer.Sales = Request.Form["Sales"];
                customer.SalesName = Request.Form["employeeName"];
                customer.Contact = Request.Form["contact"];
                //customer.ContactName = Request.Form["contactName"];
                customer.FaxNo = Request.Form["FaxNo"];
                customer.TaxRate = Request.Form["TaxRate"];
                customer.Email = Request.Form["Email"];
                //customer.AddressSName = Request.Form["txtAddressSName"];
                //customer.Address = Request.Form["txtAddress"];
                customer.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];
                string AddressName = Request.Form["txtAddressSName"];
                string Address = Request.Form["txtAddress"];
                string Contact = Request.Form["txtContact"];
                string TelNo = Request.Form["txtTelNo"];
                string FaxNo = Request.Form["txtFaxNo"];
                string Remark = Request.Form["txtRemark"];

                CustomerLogic customerLogic = new CustomerLogic();
                string returnString = customerLogic.UpdateCustomer(customer, AddressId, AddressName, Address, Contact, TelNo, FaxNo, Remark);
                if (returnString != "0")
                {
                    ViewBag.CustomerId = customer.CustomerId;
                    ViewBag.CustomerName = customer.CustomerName;
                    ViewBag.CustomerFName = customer.CustomerFName;
                    ViewBag.CustomerType = customer.CustomerType;
                    ViewBag.TelNo = customer.TelNo;
                    ViewBag.Sales = customer.Sales;
                    ViewBag.SalesName = customer.SalesName;
                    ViewBag.Contact = customer.Contact;
                    //ViewBag.ContactName = customer.ContactName;
                    ViewBag.FaxNo = customer.FaxNo;
                    ViewBag.TaxRate = customer.TaxRate;
                    ViewBag.Email = customer.Email;
                    ViewBag.AddressSName = customer.AddressSName;
                    ViewBag.Remark = customer.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶代號不存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶簡稱已存在');</script>";
                    }
                    //if (returnString == "3")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶聯絡人不存在');</script>";
                    //}
                    //if (returnString == "4")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址1不存在');</script>";
                    //}
                    //if (returnString == "5")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址2不存在');</script>";
                    //}
                    ViewBag.Type = "Edit";
                    customerAddress.CustomerId = customer.CustomerId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextEdit")
            {
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerid"];
                customer.CustomerName = Request.Form["CustomerName"];
                customer.CustomerType = Request.Form["CustomerType"];
                customer.CustomerFName = Request.Form["CustomerFName"];
                customer.TelNo = Request.Form["TelNo"];
                customer.Sales = Request.Form["Sales"];
                customer.SalesName = Request.Form["employeeName"];
                customer.Contact = Request.Form["contact"];
                //customer.ContactName = Request.Form["contactName"];
                customer.FaxNo = Request.Form["FaxNo"];
                customer.TaxRate = Request.Form["TaxRate"];
                customer.Email = Request.Form["Email"];
                //customer.AddressSName = Request.Form["txtAddressSName"];
                //customer.Address = Request.Form["txtAddress"];
                customer.Remark = Request.Form["Remark"];

                string AddressId = Request.Form["txtAddressId"];
                string AddressName = Request.Form["txtAddressSName"];
                string Address = Request.Form["txtAddress"];
                string Contact = Request.Form["txtContact"];
                string TelNo = Request.Form["txtTelNo"];
                string FaxNo = Request.Form["txtFaxNo"];
                string Remark = Request.Form["txtRemark"];

                CustomerLogic customerLogic = new CustomerLogic();
                string returnString = customerLogic.UpdateCustomer(customer, AddressId, AddressName, Address, Contact, TelNo, FaxNo, Remark);
                if (returnString != "0")
                {
                    ViewBag.CustomerId = customer.CustomerId;
                    ViewBag.CustomerName = customer.CustomerName;
                    ViewBag.CustomerFName = customer.CustomerFName;
                    ViewBag.CustomerType = customer.CustomerType;
                    ViewBag.TelNo = customer.TelNo;
                    ViewBag.Sales = customer.Sales;
                    ViewBag.SalesName = customer.SalesName;
                    ViewBag.Contact = customer.Contact;
                    //ViewBag.ContactName = customer.ContactName;
                    ViewBag.FaxNo = customer.FaxNo;
                    ViewBag.TaxRate = customer.TaxRate;
                    ViewBag.Email = customer.Email;
                    ViewBag.AddressSName = customer.AddressSName;
                    ViewBag.Remark = customer.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶代號不存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('客戶簡稱已存在');</script>";
                    }
                    //if (returnString == "3")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶聯絡人不存在');</script>";
                    //}
                    //if (returnString == "4")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址1不存在');</script>";
                    //}
                    //if (returnString == "5")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('客戶地址2不存在');</script>";
                    //}
                    ViewBag.Type = "Edit";
                    customerAddress.CustomerId = customer.CustomerId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
                    return View();
                }
                ViewData["DisplayDataAddress"] = lstAddress;
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnEdit")
            {
                CustomerLogic customerLogic = new CustomerLogic();
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerid"]; ;
                DataTable dtDisplay = customerLogic.SelectCustomer(customer,null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.CustomerId = dtDisplay.Rows[0]["CustomerId"].ToString();
                    ViewBag.CustomerName = dtDisplay.Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerFName = dtDisplay.Rows[0]["CustomerFName"].ToString();
                    ViewBag.CustomerType = dtDisplay.Rows[0]["CustomerType"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                    ViewBag.Sales = dtDisplay.Rows[0]["Sales"].ToString();
                    ViewBag.SalesName = dtDisplay.Rows[0]["EmployeeName"].ToString();
                    ViewBag.Contact = dtDisplay.Rows[0]["Contact"].ToString();
                    //ViewBag.ContactName = dtDisplay.Rows[0]["ContactName"].ToString();
                    ViewBag.FaxNo = dtDisplay.Rows[0]["FaxNo"].ToString();
                    ViewBag.TaxRate = dtDisplay.Rows[0]["TaxRate"].ToString();
                    ViewBag.Email = dtDisplay.Rows[0]["Email"].ToString();
                    ViewBag.AddressSName = dtDisplay.Rows[0]["AddressSName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "Edit";
                customerAddress.CustomerId = customer.CustomerId;
                ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
                return View();
            }
            if (action == "btnAdd")
            {
                CustomerLogic customerLogic = new CustomerLogic();
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerid"]; ;
                DataTable dtDisplay = customerLogic.SelectCustomer(customer,null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    //ViewBag.CustomerId = dtDisplay.Rows[0]["CustomerId"].ToString();
                    ViewBag.CustomerName = dtDisplay.Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerFName = dtDisplay.Rows[0]["CustomerFName"].ToString();
                    ViewBag.CustomerType = dtDisplay.Rows[0]["CustomerType"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                    ViewBag.Sales = dtDisplay.Rows[0]["Sales"].ToString();
                    ViewBag.SalesName = dtDisplay.Rows[0]["EmployeeName"].ToString();
                    ViewBag.Contact = dtDisplay.Rows[0]["Contact"].ToString();
                    //ViewBag.ContactName = dtDisplay.Rows[0]["ContactName"].ToString();
                    ViewBag.FaxNo = dtDisplay.Rows[0]["FaxNo"].ToString();
                    ViewBag.TaxRate = dtDisplay.Rows[0]["TaxRate"].ToString();
                    ViewBag.Email = dtDisplay.Rows[0]["Email"].ToString();
                    ViewBag.AddressSName = dtDisplay.Rows[0]["AddressSName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "New";
                //customerAddress.CustomerId = customer.CustomerId;
                //ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
                ViewData["DisplayDataAddress"] = lstAddress;
                return View();
            }
            if (action == "btnDelete")
            {
                CustomerLogic customerLogic = new CustomerLogic();
                Customer customer = new Customer();
                customer.CustomerId = Request.Form["customerId"];
                if (!customerLogic.DeleteCustomer(customer))
                {
                    ViewBag.CustomerId = Request.Form["customerid"];
                    ViewBag.CustomerName = Request.Form["CustomerName"];
                    ViewBag.CustomerFName = Request.Form["CustomerFName"];
                    ViewBag.CustomerType = Request.Form["CustomerType"];
                    ViewBag.TelNo = Request.Form["TelNo"];
                    ViewBag.Sales = Request.Form["Sales"];
                    ViewBag.SalesName = Request.Form["employeeName"];
                    customer.Contact = Request.Form["contact"];
                    //customer.ContactName = Request.Form["contactName"];
                    ViewBag.FaxNo = Request.Form["FaxNo"];
                    ViewBag.TaxRate = Request.Form["TaxRate"];
                    ViewBag.Email = Request.Form["Email"];
                    ViewBag.AddressSName = Request.Form["AddressSName"];
                    ViewBag.Remark = Request.Form["Remark"];
                    ViewBag.js = "<script type='text/javascript'>alert('客戶代號不存在');</script>";
                    ViewBag.Type = "Detail";
                    customerAddress.CustomerId = customer.CustomerId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (!string.IsNullOrEmpty(id))
            {
                CustomerLogic customerLogic = new CustomerLogic();
                Customer customer = new Customer();
                customer.CustomerId = id;
                DataTable dtDisplay = customerLogic.SelectCustomer(customer,null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.CustomerId = dtDisplay.Rows[0]["CustomerId"].ToString();
                    ViewBag.CustomerName = dtDisplay.Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerFName = dtDisplay.Rows[0]["CustomerFName"].ToString();
                    ViewBag.CustomerType = dtDisplay.Rows[0]["CustomerType"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                    ViewBag.Sales = dtDisplay.Rows[0]["Sales"].ToString();
                    ViewBag.SalesName = dtDisplay.Rows[0]["EmployeeName"].ToString();
                    ViewBag.Contact = dtDisplay.Rows[0]["Contact"].ToString();
                    ViewBag.Currency = dtDisplay.Rows[0]["CurrencyId"].ToString();
                    //ViewBag.ContactName = dtDisplay.Rows[0]["ContactName"].ToString();
                    ViewBag.FaxNo = dtDisplay.Rows[0]["FaxNo"].ToString();
                    ViewBag.TaxRate = dtDisplay.Rows[0]["TaxRate"].ToString();
                    ViewBag.Email = dtDisplay.Rows[0]["Email"].ToString();
                    ViewBag.AddressSName = dtDisplay.Rows[0]["AddressSName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                customerAddress.CustomerId= customer.CustomerId;
                ViewData["DisplayDataAddress"] = GetAddressInfo(customerAddress);
            }
            else
            {
                ViewData["DisplayDataAddress"] = lstAddress;
            }
            ViewBag.Type = type ?? "New";
            return View();
        }

        public ActionResult Delete(string id)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            Customer customer = new Customer();
            customer.CustomerId = id;
            if (!customerLogic.DeleteCustomer(customer))
            {
                ViewBag.js = "<script type='text/javascript'>alert('客戶代號不存在');</script>";
            }
            return RedirectToAction("Index", "Main");
        }

        public JsonResult SearchCustomer(string id, string name)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            Customer customer = new Customer();
            customer.CustomerId = id;
            customer.CustomerName = name;
            DataTable dtDisplay = null;
            if (!string.IsNullOrEmpty(id))
            {
                dtDisplay = customerLogic.SelectCustomer(customer, null, null);
            }
            else
            {
                dtDisplay = customerLogic.SelectCustomer(null, null, null);
            }
            List<Customer> lst = new List<Customer>();
            foreach (DataRow dr in dtDisplay.Rows)
            {
                customer = new Customer();
                customer.CustomerId = dr["CustomerId"].ToString();
                customer.CustomerName = dr["CustomerName"].ToString();
                lst.Add(customer);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchContact(string customerId, string contactId)
        {
            CustomerContact customerContact = new CustomerContact();
            customerContact.CustomerId = customerId;
            customerContact.ContactId = contactId; ;
            return Json(GetContactInfo(customerContact), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchContactInfoAll(string customerId)
        {
            CustomerContact customerContact = new CustomerContact();
            customerContact.CustomerId = customerId;
            return Json(GetContactInfo(customerContact), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteContact(string customerId,string contactId)
        {
            CustomerContact customerContact = new CustomerContact();
            customerContact.CustomerId = customerId;
            customerContact.ContactId = contactId; ;
            CustomerLogic customerLogic = new CustomerLogic();
            if (!customerLogic.DeleteContact(customerContact))
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveContact(CustomerContact customerContact)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            if (customerLogic.SaveContact(customerContact))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("聯絡人代號不存在", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveNewContact(CustomerContact customerContact)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            if (customerLogic.SaveNewContact(customerContact))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("聯絡人代號已存在", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveAddress(CustomerAddress customerAddress)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            if (customerLogic.SaveAddress(customerAddress))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("地址代號不存在", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveNewAddress(CustomerAddress customerAddress)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            if (customerLogic.SaveNewAddress(customerAddress))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("地址代號已存在", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteAddress(string customerId, string addressId)
        {
            CustomerAddress customerAddress = new CustomerAddress();
            customerAddress.CustomerId = customerId;
            customerAddress.AddressId = addressId; ;
            CustomerLogic customerLogic = new CustomerLogic();
            if (!customerLogic.DeleteAddress(customerAddress))
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContactBlur(string customerId, string contactId)
        {
            CustomerContact customerContact = new CustomerContact();
            customerContact.CustomerId = customerId;
            customerContact.ContactId = contactId;
            return Json(GetContactInfo(customerContact), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchEmployeeInfoAll()
        {
            EmployeeLogic employeeLogic = new EmployeeLogic();
            DataTable dtReturn = employeeLogic.SelectEmployee(null,null);
            List<Employee> lst = new List<Employee>();
            Employee employee = null;
            foreach (DataRow dr in dtReturn.Rows)
            {
                employee = new Employee();
                employee.EmployeeId = dr["EmployeeId"].ToString();
                employee.EmployeeName = dr["EmployeeName"].ToString();
                employee.DepartmentId = dr["DepartmentId"].ToString();
                employee.DepartmentName = dr["DepartmentName"].ToString();
                lst.Add(employee);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchEmployeeInfoDetail(string employeeId, string employeeName)
        {
            EmployeeLogic employeeLogic = new EmployeeLogic();
            Employee employee = new Employee();
            employee.EmployeeId = employeeId;
            employee.EmployeeName = employeeName;
            DataTable dtReturn = employeeLogic.SelectEmployee(employee,null);
            List<Employee> lst = new List<Employee>();
            foreach (DataRow dr in dtReturn.Rows)
            {
                employee = new Employee();
                employee.EmployeeId = dr["EmployeeId"].ToString();
                employee.EmployeeName = dr["EmployeeName"].ToString();
                employee.DepartmentId = dr["DepartmentId"].ToString();
                employee.DepartmentName = dr["DepartmentName"].ToString();
                lst.Add(employee);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSalesInfo(string employeeId)
        {
            EmployeeLogic employeeLogic = new EmployeeLogic();
            Employee employee = new Employee();
            employee.EmployeeId = employeeId;
            DataTable dtReturn = employeeLogic.SelectEmployee(employee,null);
            List<Employee> lst = new List<Employee>();
            foreach (DataRow dr in dtReturn.Rows)
            {
                employee = new Employee();
                employee.EmployeeId = dr["EmployeeId"].ToString();
                employee.EmployeeName = dr["EmployeeName"].ToString();
                employee.DepartmentId = dr["DepartmentId"].ToString();
                employee.DepartmentName = dr["DepartmentName"].ToString();
                lst.Add(employee);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        private List<CustomerAddress> GetAddressInfo(CustomerAddress customerAddressInfo)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            CustomerAddress customerAddress = null;
            DataTable dtAddress = customerLogic.GetAddressInfo(customerAddressInfo);
            List<CustomerAddress> lst = new List<CustomerAddress>();
            foreach (DataRow dr in dtAddress.Rows)
            {
                customerAddress = new CustomerAddress();
                customerAddress.Company = dr["Company"].ToString();
                customerAddress.UserGroup = dr["UserGroup"].ToString();
                customerAddress.Creator = dr["Creator"].ToString();
                customerAddress.CreateDate = dr["CreateDate"].ToString();
                customerAddress.Modifier = dr["Modifier"].ToString();
                customerAddress.ModiDate = dr["ModiDate"].ToString();
                customerAddress.CustomerId = dr["CustomerId"].ToString();
                customerAddress.AddressId = dr["AddressId"].ToString();
                customerAddress.AddressSName = dr["AddressSName"].ToString();
                customerAddress.Address = dr["Address"].ToString();
                customerAddress.TelNo = dr["TelNo"].ToString();
                customerAddress.FaxNo = dr["FaxNo"].ToString();
                customerAddress.Contact = dr["Contact"].ToString();
                customerAddress.Remark = dr["Remark"].ToString();
                lst.Add(customerAddress);
            }
            return lst;
        }

        private List<CustomerContact> GetContactInfo(CustomerContact customerContactInfo)
        {
            CustomerLogic customerLogic = new CustomerLogic();
            CustomerContact customerContact = null;
            DataTable dtContact = customerLogic.GetContactInfo(customerContactInfo);
            List<CustomerContact> lst = new List<CustomerContact>();
            foreach (DataRow dr in dtContact.Rows)
            {
                customerContact = new CustomerContact();
                customerContact.Company = dr["Company"].ToString();
                customerContact.UserGroup = dr["UserGroup"].ToString();
                customerContact.Creator = dr["Creator"].ToString();
                customerContact.CreateDate = dr["CreateDate"].ToString();
                customerContact.Modifier = dr["Modifier"].ToString();
                customerContact.ModiDate = dr["ModiDate"].ToString();
                customerContact.CustomerId = dr["CustomerId"].ToString();
                customerContact.ContactId = dr["ContactId"].ToString();
                customerContact.Contact = dr["Contact"].ToString();
                customerContact.Department = dr["Department"].ToString();
                customerContact.Occupation = dr["Occupation"].ToString();
                customerContact.TelNo = dr["TelNo"].ToString();
                customerContact.FaxNo = dr["FaxNo"].ToString();
                customerContact.Email = dr["Email"].ToString();
                customerContact.Remark = dr["Remark"].ToString();
                lst.Add(customerContact);
            }
            return lst;
        }

        public JsonResult SearchCurrencyInfoAll()
        {
            CurrencyLogic currencyLogic = new CurrencyLogic();
            DataTable dtReturn = currencyLogic.SelectCurrency(null, null);
            List<Currency> lst = new List<Currency>();
            Currency currency = null;
            foreach (DataRow dr in dtReturn.Rows)
            {
                currency = new Currency();
                currency.CurrencyId = dr["CurrencyId"].ToString();
                currency.CurrencyName = dr["Currency"].ToString();
                currency.EffectiveDate = dr["EffectiveDate"].ToString();
                currency.ExchangeRate = dr["ExchangeRate"].ToString();
                currency.Remark = dr["Remark"].ToString();
                lst.Add(currency);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCurrencyInfoDetail(string currencyId, string currencyName)
        {
            CurrencyLogic currencyLogic = new CurrencyLogic();
            Currency currency = new Currency();
            currency.CurrencyId = currencyId;
            currency.CurrencyName = currencyName;
            DataTable dtReturn = currencyLogic.SelectCurrency(currency, null);
            List<Currency> lst = new List<Currency>();
            foreach (DataRow dr in dtReturn.Rows)
            {
                currency = new Currency();
                currency.CurrencyId = dr["CurrencyId"].ToString();
                currency.CurrencyName = dr["Currency"].ToString();
                currency.EffectiveDate = dr["EffectiveDate"].ToString();
                currency.ExchangeRate = dr["ExchangeRate"].ToString();
                currency.Remark = dr["Remark"].ToString();
                lst.Add(currency);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrencyInfo(string currencyId)
        {
            CurrencyLogic currencyLogic = new CurrencyLogic();
            Currency currency = new Currency();
            currency.CurrencyId = currencyId;
            DataTable dtReturn = currencyLogic.SelectCurrency(currency, null);
            List<Currency> lst = new List<Currency>();
            foreach (DataRow dr in dtReturn.Rows)
            {
                currency = new Currency();
                currency.CurrencyId = dr["CurrencyId"].ToString();
                currency.CurrencyName = dr["Currency"].ToString();
                currency.EffectiveDate = dr["EffectiveDate"].ToString();
                currency.ExchangeRate = dr["ExchangeRate"].ToString();
                currency.Remark = dr["Remark"].ToString();
                lst.Add(currency);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //通過客戶單號查詢信息
        public JsonResult SearchCustomerInfo(string CustomerId)
        {
            CustomerLogic logic = new CustomerLogic();
            Customer customer = logic.GetCustomerInfo(CustomerId);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CURMID(string customerid)
        {
            CustomerLogic logic = new CustomerLogic();
            bool type = logic.IsModuleId(customerid);
            return Json(type, JsonRequestBehavior.AllowGet);
        }
    }
}