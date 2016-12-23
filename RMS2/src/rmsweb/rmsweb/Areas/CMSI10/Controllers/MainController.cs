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

namespace CMSI10.Controllers
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
                VendorLogic vendorLogic = new VendorLogic();
                string msg = "";
                if (!vendorLogic.ImportFile(path + "\\" + name, nvc.Get("type"), out msg))
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
                    VendorLogic vendorLogic = new VendorLogic();
                    Vendor vendor = new Vendor();
                    vendor.VendorId = Request.Form["vendorId"];
                    vendor.VendorName = Request.Form["vendorName"];
                    DataTable dtExport = vendorLogic.SelectVendor(vendor, 0, Request.Form["selectCondition1"], Request.Form["selectCondition2"]);
                    string path = Server.MapPath(@"~\exports");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fs = new FileStream(path + @"\VendorInfo.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        HSSFWorkbook workbook = new HSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("VendorInfo");
                        sheet.SetColumnWidth(0, 20 * 256);
                        sheet.SetColumnWidth(1, 20 * 256);
                        sheet.SetColumnWidth(2, 20 * 256);
                        sheet.SetColumnWidth(3, 20 * 256);
                        sheet.SetColumnWidth(4, 20 * 256);
                        sheet.SetColumnWidth(5, 20 * 256);
                        sheet.SetColumnWidth(6, 20 * 256);

                        IRow row = sheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("廠商代號");
                        row.CreateCell(1).SetCellValue("廠商簡稱");
                        row.CreateCell(2).SetCellValue("聯絡人");
                        row.CreateCell(3).SetCellValue("電話");
                        row.CreateCell(4).SetCellValue("傳真");
                        row.CreateCell(5).SetCellValue("地址");
                        row.CreateCell(6).SetCellValue("備註");

                        int index = 1;

                        for (int i = 0; i < dtExport.Rows.Count; i++)
                        {

                            row = sheet.CreateRow(index);
                            row.CreateCell(0).SetCellValue(dtExport.Rows[i]["VendorId"].ToString());
                            row.CreateCell(1).SetCellValue(dtExport.Rows[i]["VendorName"].ToString());
                            row.CreateCell(2).SetCellValue(dtExport.Rows[i]["Contact"].ToString());
                            row.CreateCell(3).SetCellValue(dtExport.Rows[i]["TelNo"].ToString());
                            row.CreateCell(4).SetCellValue(dtExport.Rows[i]["FaxNo"].ToString());
                            row.CreateCell(5).SetCellValue(dtExport.Rows[i]["AddressSName"].ToString());
                            row.CreateCell(6).SetCellValue(dtExport.Rows[i]["Remark"].ToString());

                            index++;

                        }

                        workbook.Write(fs);
                    }

                    //using (FileStream fs = new FileStream(path + @"\VendorInfo.csv", FileMode.Create, FileAccess.Write))
                    //{
                    //    StreamWriter sw = new StreamWriter(fs);
                    //    sw.WriteLine("廠商代號,廠商簡稱,聯絡人,電話,傳真,地址,備註");
                    //    foreach (DataRow dr in dtExport.Rows)
                    //    {
                    //        sw.WriteLine(dr["VendorId"].ToString() + "," + dr["VendorName"].ToString() + "," + dr["Contact"].ToString() + "," + dr["TelNo"].ToString() + "," + dr["FaxNo"].ToString() + "," + dr["AddressSName"].ToString() + "," + dr["Remark"].ToString());
                    //    }
                    //    sw.Close();
                    //    sw.Dispose();
                    //}
                    //return File(new FileStream(path + @"\VendorInfo.csv", FileMode.Open), "text/plain", "VendorInfo.csv");
                    return File(new FileStream(path + @"\VendorInfo.xls", FileMode.Open), "text/plain", "VendorInfo.xls");
                }
            }
            if (action == "btnSearchDetail")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];

                VendorLogic vendorLogic = new VendorLogic();
                DataTable dtDisplay = vendorLogic.SearchDetailVendor(col, condition, value);
                List<Vendor> lst = new List<Vendor>();
                Vendor vendor = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    vendor = new Vendor();
                    vendor.Company = dr["Company"].ToString();
                    vendor.UserGroup = dr["UserGroup"].ToString();
                    vendor.Creator = dr["Creator"].ToString();
                    vendor.CreateDate = dr["CreateDate"].ToString();
                    vendor.Modifier = dr["Modifier"].ToString();
                    vendor.ModiDate = dr["ModiDate"].ToString();
                    vendor.VendorId = dr["VendorId"].ToString();
                    vendor.VendorName = dr["VendorName"].ToString();
                    vendor.VendorFName = dr["VendorFName"].ToString();
                    vendor.CurrencyId = dr["CurrencyId"].ToString();
                    vendor.TaxRate = dr["TaxRate"].ToString();
                    vendor.Contact = dr["Contact"].ToString();
                    vendor.TelNo = dr["TelNo"].ToString();
                    vendor.FaxNo = dr["FaxNo"].ToString();
                    vendor.Email = dr["Email"].ToString();
                    vendor.AddressSName = dr["AddressSName"].ToString();
                    vendor.Remark = dr["Remark"].ToString();
                    lst.Add(vendor);
                }
                ViewData["DisplayData"] = lst;
                return View();
            }
            if (action == "btnSearch")
            {
                VendorLogic vendorLogic = new VendorLogic();
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                vendor.VendorName = Request.Form["vendorName"];
                DataTable dtDisplay = vendorLogic.SelectVendor(vendor, 0, Request.Form["selectCondition1"], Request.Form["selectCondition2"]);
                List<Vendor> lst = new List<Vendor>();
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    vendor = new Vendor();
                    vendor.Company = dr["Company"].ToString();
                    vendor.UserGroup = dr["UserGroup"].ToString();
                    vendor.Creator = dr["Creator"].ToString();
                    vendor.CreateDate = dr["CreateDate"].ToString();
                    vendor.Modifier = dr["Modifier"].ToString();
                    vendor.ModiDate = dr["ModiDate"].ToString();
                    vendor.VendorId = dr["VendorId"].ToString();
                    vendor.VendorName = dr["VendorName"].ToString();
                    vendor.VendorFName = dr["VendorFName"].ToString();
                    vendor.CurrencyId = dr["CurrencyId"].ToString();
                    vendor.TaxRate = dr["TaxRate"].ToString();
                    vendor.Contact = dr["Contact"].ToString();
                    //vendor.ContactName = dr["ContactName"].ToString();
                    vendor.TelNo = dr["TelNo"].ToString();
                    vendor.FaxNo = dr["FaxNo"].ToString();
                    vendor.Email = dr["Email"].ToString();
                    vendor.AddressSName = dr["AddressSName"].ToString();
                    vendor.Remark = dr["Remark"].ToString();
                    lst.Add(vendor);
                }
                ViewBag.VendorId = Request.Form["vendorId"];
                ViewBag.VendorName = Request.Form["vendorName"];
                ViewBag.SelectCondition1 = Request.Form["selectCondition1"].ToString();
                ViewBag.SelectCondition2 = Request.Form["selectCondition2"].ToString();
                ViewData["DisplayData"] = lst;
            }
            else
            {
                VendorLogic vendorLogic = new VendorLogic();
                DataTable dtDisplay = vendorLogic.SelectVendor(null,0,null,null);
                List<Vendor> lst = new List<Vendor>();
                Vendor vendor = null;
                foreach (DataRow dr in dtDisplay.Rows)
                {
                    vendor = new Vendor();
                    vendor.Company = dr["Company"].ToString();
                    vendor.UserGroup = dr["UserGroup"].ToString();
                    vendor.Creator = dr["Creator"].ToString();
                    vendor.CreateDate = dr["CreateDate"].ToString();
                    vendor.Modifier = dr["Modifier"].ToString();
                    vendor.ModiDate = dr["ModiDate"].ToString();
                    vendor.VendorId = dr["VendorId"].ToString();
                    vendor.VendorName = dr["VendorName"].ToString();
                    vendor.VendorFName = dr["VendorFName"].ToString();
                    vendor.CurrencyId = dr["CurrencyId"].ToString();
                    vendor.TaxRate = dr["TaxRate"].ToString();
                    vendor.Contact = dr["Contact"].ToString();
                    //vendor.ContactName = dr["ContactName"].ToString();
                    vendor.TelNo = dr["TelNo"].ToString();
                    vendor.FaxNo = dr["FaxNo"].ToString();
                    vendor.Email = dr["Email"].ToString();
                    vendor.AddressSName = dr["AddressSName"].ToString();
                    vendor.Remark = dr["Remark"].ToString();
                    lst.Add(vendor);
                }
                ViewData["DisplayData"] = lst;
            }
            return View();
        }

        public ActionResult CUR(string action, string id, string type)
        {
            VendorAddress vendorAddress = new VendorAddress();
            List<VendorAddress> lstAddress = new List<VendorAddress>();
            if (action == "btnSaveNew")
            {
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                vendor.VendorName = Request.Form["vendorName"];
                vendor.VendorFName = Request.Form["vendorfullname"];
                vendor.Contact = Request.Form["contact"];
                //vendor.ContactName = Request.Form["contactName"];
                vendor.TaxRate = Request.Form["rate"];
                vendor.TelNo = Request.Form["TelNo"];
                vendor.FaxNo = Request.Form["faxno"];
                vendor.Email = Request.Form["email"];
                vendor.AddressSName = Request.Form["txtSaveAddress1"];
                vendor.Address = Request.Form["txtSaveAddress2"];
                vendor.Remark = Request.Form["remark"];

                string AddressId = Request.Form["txtAddressId"];
                string txtAddress1 = Request.Form["txtAddress1"];
                string txtAddress2 = Request.Form["txtAddress2"];
                string txtAddressContact = Request.Form["txtAddressContact"];
                string txtAddressTelNo = Request.Form["txtAddressTelNo"];
                string txtAddressFaxNo = Request.Form["txtAddressFaxNo"];
                string txtAddressRemark = Request.Form["txtAddressRemark"];

                VendorLogic vendorLogic = new VendorLogic();

                string returnString = vendorLogic.InsertVendor(vendor,AddressId,txtAddress1,txtAddress2,txtAddressContact,txtAddressTelNo,txtAddressFaxNo,txtAddressRemark);

                if (returnString != "0")
                {
                    ViewBag.VendorId = vendor.VendorId;
                    ViewBag.VendorName = vendor.VendorName;
                    ViewBag.VendorFullName = vendor.VendorFName;
                    ViewBag.Contact = vendor.Contact;
                    //ViewBag.ContactName = vendor.ContactName;
                    ViewBag.TelNo = vendor.TelNo;
                    ViewBag.FaxNo = vendor.FaxNo;
                    ViewBag.Email = vendor.Email;
                    ViewBag.Address = vendor.AddressSName;
                    ViewBag.TaxRate = vendor.TaxRate;
                    ViewBag.Remark = vendor.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商代號已存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商簡稱已存在');</script>";
                    }
                    if (returnString == "3")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商联络人不存在');</script>";
                    }
                    if (returnString == "4")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商地址1不存在');</script>";
                    }
                    if (returnString == "5")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商地址2不存在');</script>";
                    }
                    ViewBag.Type = "New";
                    vendorAddress.VendorId = vendor.VendorId;
                    ViewData["DisplayDataAddress"] =GetAddressInfo(vendorAddress);
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextNew")
            {
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                vendor.VendorName = Request.Form["vendorName"];
                vendor.TaxRate = Request.Form["rate"];
                vendor.VendorFName = Request.Form["vendorfullname"];
                vendor.Contact = Request.Form["contact"];
                //vendor.ContactName = Request.Form["contactName"];
                vendor.TelNo = Request.Form["TelNo"];
                vendor.FaxNo = Request.Form["faxno"];
                vendor.Email = Request.Form["email"];
                vendor.AddressSName = Request.Form["txtSaveAddress1"];
                vendor.Address = Request.Form["txtSaveAddress2"];
                vendor.Remark = Request.Form["remark"];

                string AddressId = Request.Form["txtAddressId"];
                string txtAddress1 = Request.Form["txtAddress1"];
                string txtAddress2 = Request.Form["txtAddress2"];
                string txtAddressContact = Request.Form["txtAddressContact"];
                string txtAddressTelNo = Request.Form["txtAddressTelNo"];
                string txtAddressFaxNo = Request.Form["txtAddressFaxNo"];
                string txtAddressRemark = Request.Form["txtAddressRemark"];

                VendorLogic vendorLogic = new VendorLogic();
                string returnString = vendorLogic.InsertVendor(vendor, AddressId, txtAddress1, txtAddress2, txtAddressContact, txtAddressTelNo, txtAddressFaxNo, txtAddressRemark);
                if (returnString != "0")
                {
                    ViewBag.VendorId = vendor.VendorId;
                    ViewBag.VendorName = vendor.VendorName;
                    ViewBag.VendorFullName = vendor.VendorFName;
                    ViewBag.Contact = vendor.Contact;
                    //ViewBag.ContactName = vendor.ContactName;
                    ViewBag.TelNo = vendor.TelNo;
                    ViewBag.FaxNo = vendor.FaxNo;
                    ViewBag.TaxRate = vendor.TaxRate;
                    ViewBag.Email = vendor.Email;
                    ViewBag.Address = vendor.AddressSName;
                    ViewBag.Remark = vendor.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商代號已存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商簡稱已存在');</script>";
                    }
                    //if (returnString == "3")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商联络人不存在');</script>";
                    //}
                    //if (returnString == "4")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商地址1不存在');</script>";
                    //}
                    //if (returnString == "5")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商地址2不存在');</script>";
                    //}
                    vendorAddress.VendorId = vendor.VendorId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(vendorAddress);
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
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                vendor.VendorName = Request.Form["vendorName"];
                vendor.VendorFName = Request.Form["vendorfullname"];
                vendor.TaxRate = Request.Form["rate"];
                vendor.Contact = Request.Form["contact"];
                //vendor.ContactName = Request.Form["contactName"];
                vendor.TelNo = Request.Form["telno"];
                vendor.FaxNo = Request.Form["faxno"];
                vendor.Email = Request.Form["email"];
                vendor.AddressSName = Request.Form["txtAddress1"];
                vendor.Address = Request.Form["txtAddress2"];
                vendor.Remark = Request.Form["remark"];

                string AddressId = Request.Form["txtAddressId"];
                string txtAddress1 = Request.Form["txtAddress1"];
                string txtAddress2 = Request.Form["txtAddress2"];
                string txtAddressContact = Request.Form["txtAddressContact"];
                string txtAddressTelNo = Request.Form["txtAddressTelNo"];
                string txtAddressFaxNo = Request.Form["txtAddressFaxNo"];
                string txtAddressRemark = Request.Form["txtAddressRemark"];

                VendorLogic vendorLogic = new VendorLogic();

                string returnString = vendorLogic.UpdateVendor(vendor,AddressId, txtAddress1, txtAddress2, txtAddressContact, txtAddressTelNo, txtAddressFaxNo, txtAddressRemark);
                if (returnString != "0")
                {
                    ViewBag.VendorId = vendor.VendorId;
                    ViewBag.VendorName = vendor.VendorName;
                    ViewBag.VendorFullName = vendor.VendorFName;
                    ViewBag.TaxRate = vendor.TaxRate;
                    ViewBag.Contact = vendor.Contact;
                    //ViewBag.ContactName = vendor.ContactName;
                    ViewBag.TelNo = vendor.TelNo;
                    ViewBag.FaxNo = vendor.FaxNo;
                    ViewBag.Email = vendor.Email;
                    ViewBag.Address = vendor.AddressSName;
                    ViewBag.Remark = vendor.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商代號不存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商簡稱已存在');</script>";
                    }
                    //if (returnString == "3")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商联络人不存在');</script>";
                    //}
                    //if (returnString == "4")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商地址1不存在');</script>";
                    //}
                    //if (returnString == "5")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商地址2不存在');</script>";
                    //}
                    ViewBag.Type = "Edit";
                    vendorAddress.VendorId = vendor.VendorId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(vendorAddress);
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (action == "btnNextEdit")
            {
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                vendor.VendorName = Request.Form["vendorName"];
                vendor.VendorFName = Request.Form["vendorfullname"];
                vendor.TaxRate = Request.Form["rate"];
                vendor.Contact = Request.Form["contact"];
                //vendor.ContactName = Request.Form["contactName"];
                vendor.TelNo = Request.Form["TelNo"];
                vendor.FaxNo = Request.Form["faxno"];
                vendor.Email = Request.Form["email"];
                vendor.AddressSName = Request.Form["txtAddress1"];
                vendor.Address = Request.Form["txtAddress1"];
                vendor.Remark = Request.Form["remark"];

                string AddressId = Request.Form["txtAddressId"];
                string txtAddress1 = Request.Form["txtAddress1"];
                string txtAddress2 = Request.Form["txtAddress2"];
                string txtAddressContact = Request.Form["txtAddressContact"];
                string txtAddressTelNo = Request.Form["txtAddressTelNo"];
                string txtAddressFaxNo = Request.Form["txtAddressFaxNo"];
                string txtAddressRemark = Request.Form["txtAddressRemark"];

                VendorLogic vendorLogic = new VendorLogic();
                string returnString = vendorLogic.UpdateVendor(vendor, AddressId, txtAddress1, txtAddress2, txtAddressContact, txtAddressTelNo, txtAddressFaxNo, txtAddressRemark);
                if (returnString != "0")
                {
                    ViewBag.VendorId = vendor.VendorId;
                    ViewBag.VendorName = vendor.VendorName;
                    ViewBag.VendorFullName = vendor.VendorFName;
                    ViewBag.TaxRate = vendor.TaxRate;
                    ViewBag.Contact = vendor.Contact;
                    //ViewBag.ContactName = vendor.ContactName;
                    ViewBag.TelNo = vendor.TelNo;
                    ViewBag.FaxNo = vendor.FaxNo;
                    ViewBag.Email = vendor.Email;
                    ViewBag.Address = vendor.AddressSName;
                    ViewBag.Remark = vendor.Remark;
                    if (returnString == "1")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商代號不存在');</script>";
                    }
                    if (returnString == "2")
                    {
                        ViewBag.js = "<script type='text/javascript'>alert('廠商簡稱已存在');</script>";
                    }
                    //if (returnString == "3")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商联络人不存在');</script>";
                    //}
                    //if (returnString == "4")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商地址1不存在');</script>";
                    //}
                    //if (returnString == "5")
                    //{
                    //    ViewBag.js = "<script type='text/javascript'>alert('廠商地址2不存在');</script>";
                    //}
                    ViewBag.Type = "Edit";
                    vendorAddress.VendorId = vendor.VendorId;
                    ViewData["DisplayDataAddress"] = GetAddressInfo(vendorAddress);
                    return View();
                }
               
                ViewData["DisplayDataAddress"] = lstAddress;
                ViewBag.Type = "New";
                return View();
            }
            if (action == "btnEdit")
            {
                VendorLogic vendorLogic = new VendorLogic();
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                DataTable dtDisplay = vendorLogic.SelectVendor(vendor,0,null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.VendorId = dtDisplay.Rows[0]["VendorId"].ToString();
                    ViewBag.VendorName = dtDisplay.Rows[0]["VendorName"].ToString();
                    ViewBag.VendorFullName = dtDisplay.Rows[0]["VendorFName"].ToString();
                    ViewBag.TaxRate = dtDisplay.Rows[0]["TaxRate"].ToString();
                    ViewBag.Contact = dtDisplay.Rows[0]["Contact"].ToString();
                    //ViewBag.ContactName = dtDisplay.Rows[0]["ContactName"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                    ViewBag.FaxNo = dtDisplay.Rows[0]["FaxNo"].ToString();
                    ViewBag.Email = dtDisplay.Rows[0]["Email"].ToString();
                    ViewBag.Address = dtDisplay.Rows[0]["AddressSName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "Edit";
                vendorAddress.VendorId = vendor.VendorId;
                ViewData["DisplayDataAddress"] = GetAddressInfo(vendorAddress);
                return View();
            }
            if (action == "btnAdd")
            {
                VendorLogic vendorLogic = new VendorLogic();
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                DataTable dtDisplay = vendorLogic.SelectVendor(vendor,0,null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.VendorId = dtDisplay.Rows[0]["VendorId"].ToString();
                    ViewBag.VendorName = dtDisplay.Rows[0]["VendorName"].ToString();
                    ViewBag.TaxRate = dtDisplay.Rows[0]["TaxRate"].ToString();
                    ViewBag.VendorFullName = dtDisplay.Rows[0]["VendorFName"].ToString();
                    ViewBag.Contact = dtDisplay.Rows[0]["Contact"].ToString();
                    //ViewBag.ContactName = dtDisplay.Rows[0]["ContactName"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                    ViewBag.FaxNo = dtDisplay.Rows[0]["FaxNo"].ToString();
                    ViewBag.Email = dtDisplay.Rows[0]["Email"].ToString();
                    ViewBag.Address = dtDisplay.Rows[0]["AddressSName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                ViewBag.Type = "New";
                //vendorAddress.VendorId = vendor.VendorId;
                //ViewData["DisplayDataAddress"] = GetAddressInfo(vendorAddress);
                ViewData["DisplayDataAddress"] = lstAddress;
                return View();
            }
            if (action == "btnDelete")
            {
                VendorLogic vendorLogic = new VendorLogic();
                Vendor vendor = new Vendor();
                vendor.VendorId = Request.Form["vendorId"];
                if (!vendorLogic.DeleteVendor(vendor))
                {
                    ViewBag.VendorId = Request.Form["vendorId"];
                    ViewBag.VendorName = Request.Form["vendorName"];
                    ViewBag.TaxRate = Request.Form["rate"];
                    ViewBag.VendorFullName = Request.Form["vendorfullname"];
                    ViewBag.Contact = Request.Form["contact"];
                    //ViewBag.ContactName = Request.Form["contactName"];
                    ViewBag.TelNo = Request.Form["TelNo"];
                    ViewBag.FaxNo = Request.Form["faxno"];
                    ViewBag.Email = Request.Form["email"];
                    ViewBag.Address = Request.Form["address"];
                    ViewBag.Remark = Request.Form["remark"];
                    ViewBag.js = "<script type='text/javascript'>alert('廠商代號不存在');</script>";
                    ViewBag.Type = "Detail";
                    vendorAddress.VendorId = Request.Form["vendorId"];
                    ViewData["DisplayDataAddress"] = GetAddressInfo(vendorAddress);
                    return View();
                }
                return RedirectToAction("Index", "Main");
            }
            if (!string.IsNullOrEmpty(id))
            {
                VendorLogic vendorLogic = new VendorLogic();
                Vendor vendor = new Vendor();
                vendor.VendorId = id;
                DataTable dtDisplay = vendorLogic.SelectVendor(vendor,0,null,null);
                if (dtDisplay.Rows.Count > 0)
                {
                    ViewBag.VendorId = dtDisplay.Rows[0]["VendorId"].ToString();
                    ViewBag.VendorName = dtDisplay.Rows[0]["VendorName"].ToString();
                    ViewBag.TaxRate = dtDisplay.Rows[0]["TaxRate"].ToString();
                    ViewBag.VendorFullName = dtDisplay.Rows[0]["VendorFName"].ToString();
                    ViewBag.Contact = dtDisplay.Rows[0]["Contact"].ToString();
                    //ViewBag.ContactName = dtDisplay.Rows[0]["ContactName"].ToString();
                    ViewBag.TelNo = dtDisplay.Rows[0]["TelNo"].ToString();
                    ViewBag.FaxNo = dtDisplay.Rows[0]["FaxNo"].ToString();
                    ViewBag.Email = dtDisplay.Rows[0]["Email"].ToString();
                    ViewBag.Address = dtDisplay.Rows[0]["AddressSName"].ToString();
                    ViewBag.Remark = dtDisplay.Rows[0]["Remark"].ToString();
                }
                vendorAddress.VendorId = id;
                ViewData["DisplayDataAddress"] = GetAddressInfo(vendorAddress);
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
            VendorLogic vendorLogic = new VendorLogic();
            Vendor vendor = new Vendor();
            vendor.VendorId = id;
            if (!vendorLogic.DeleteVendor(vendor))
            {
                ViewBag.js = "<script type='text/javascript'>alert('廠商代號不存在');</script>";
            }
            return RedirectToAction("Index", "Main");
        }

        public JsonResult SearchVendor(string id, string name)
        {
            VendorLogic vendorLogic = new VendorLogic();
            Vendor vendor = new Vendor();
            vendor.VendorId = id;
            vendor.VendorName = name;
            DataTable dtDisplay = null;
            if (!string.IsNullOrEmpty(id))
            {
                dtDisplay = vendorLogic.SelectVendor(vendor,0,null,null);
            }
            else
            {
                dtDisplay = vendorLogic.SelectVendor(null,0,null,null);
            }
            List<Vendor> lst = new List<Vendor>();
            foreach (DataRow dr in dtDisplay.Rows)
            {
                vendor = new Vendor();
                vendor.VendorId = dr["VendorId"].ToString();
                vendor.VendorName = dr["VendorName"].ToString();
                lst.Add(vendor);
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAddress(string vendorId, string addressId)
        {
            VendorAddress vendorAddress = new VendorAddress();
            vendorAddress.VendorId = vendorId;
            vendorAddress.AddressId = addressId;;
            return Json(GetAddressInfo(vendorAddress), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchContact(string vendorId, string contactId)
        {
            VendorContact vendorContact = new VendorContact();
            vendorContact.VendorId = vendorId;
            vendorContact.ContactId = contactId; ;
            return Json(GetContactInfo(vendorContact), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNewContact(VendorContact vendorContact)
        {
            VendorLogic vendorLogic = new VendorLogic();
            if (vendorLogic.SaveNewContact(vendorContact))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("聯絡人代號已存在", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteContact(string vendorId,string contactId)
        {
            VendorContact vendorContact = new VendorContact();
            vendorContact.VendorId = vendorId;
            vendorContact.ContactId = contactId; ;
            VendorLogic vendorLogic = new VendorLogic();
            if (!vendorLogic.DeleteContact(vendorContact))
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveContact(VendorContact vendorContact)
        {
            VendorLogic vendorLogic = new VendorLogic();
            if (vendorLogic.SaveContact(vendorContact))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("聯絡人代號不存在", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SearchContactInfoAll(string vendorId)
        {
            VendorContact vendorContact = new VendorContact();
            vendorContact.VendorId = vendorId;
            return Json(GetContactInfo(vendorContact), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContactBlur(string vendorId,string contactId)
        {
            VendorContact vendorContact = new VendorContact();
            vendorContact.VendorId = vendorId;
            vendorContact.ContactId = contactId;
            return Json(GetContactInfo(vendorContact), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAddress(string vendorId, string addressId)
        {
            VendorAddress vendorAddress = new VendorAddress();
            vendorAddress.VendorId = vendorId;
            vendorAddress.AddressId = addressId; ;
            VendorLogic vendorLogic = new VendorLogic();
            if (!vendorLogic.DeleteAddress(vendorAddress))
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAddress(VendorAddress vendorAddress)
        {
            VendorLogic vendorLogic = new VendorLogic();
            if (vendorLogic.SaveAddress(vendorAddress))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("地址代號不存在", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveNewAddress(VendorAddress vendorAddress)
        {
            VendorLogic vendorLogic = new VendorLogic();
            if (vendorLogic.SaveNewAddress(vendorAddress))
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("地址代號已存在", JsonRequestBehavior.AllowGet);
            }
        }

        private List<VendorAddress> GetAddressInfo(VendorAddress vendorAddressInfo)
        {
            VendorLogic vendorLogic = new VendorLogic();
            VendorAddress vendorAddress = null;
            DataTable dtAddress = vendorLogic.GetAddressInfo(vendorAddressInfo);
            List<VendorAddress> lst = new List<VendorAddress>();
            foreach (DataRow dr in dtAddress.Rows)
            {
                vendorAddress = new VendorAddress();
                vendorAddress.Company = dr["Company"].ToString();
                vendorAddress.UserGroup = dr["UserGroup"].ToString();
                vendorAddress.Creator = dr["Creator"].ToString();
                vendorAddress.CreateDate = dr["CreateDate"].ToString();
                vendorAddress.Modifier = dr["Modifier"].ToString();
                vendorAddress.ModiDate = dr["ModiDate"].ToString();
                vendorAddress.VendorId = dr["VendorId"].ToString();
                vendorAddress.AddressId = dr["AddressId"].ToString();
                vendorAddress.AddressSName = dr["AddressSName"].ToString();
                vendorAddress.Address = dr["Address"].ToString();
                vendorAddress.Contact = dr["Contact"].ToString();
                vendorAddress.TelNo = dr["TelNo"].ToString();
                vendorAddress.FaxNo = dr["FaxNo"].ToString();
                vendorAddress.Remark = dr["Remark"].ToString();
                lst.Add(vendorAddress);
            }
            return lst;
        }

        private List<VendorContact> GetContactInfo(VendorContact vendorContactInfo)
        {
            VendorLogic vendorLogic = new VendorLogic();
            VendorContact vendorContact = null;
            DataTable dtContact = vendorLogic.GetContactInfo(vendorContactInfo);
            List<VendorContact> lst = new List<VendorContact>();
            foreach (DataRow dr in dtContact.Rows)
            {
                vendorContact = new VendorContact();
                vendorContact.Company = dr["Company"].ToString();
                vendorContact.UserGroup = dr["UserGroup"].ToString();
                vendorContact.Creator = dr["Creator"].ToString();
                vendorContact.CreateDate = dr["CreateDate"].ToString();
                vendorContact.Modifier = dr["Modifier"].ToString();
                vendorContact.ModiDate = dr["ModiDate"].ToString();
                vendorContact.VendorId = dr["VendorId"].ToString();
                vendorContact.ContactId = dr["ContactId"].ToString();
                vendorContact.Contact = dr["Contact"].ToString();
                vendorContact.Department = dr["Department"].ToString();
                vendorContact.Occupation = dr["Occupation"].ToString();
                vendorContact.TelNo = dr["TelNo"].ToString();
                vendorContact.FaxNo = dr["FaxNo"].ToString();
                vendorContact.Email = dr["Email"].ToString();
                vendorContact.Remark = dr["Remark"].ToString();
                lst.Add(vendorContact);
            }
            return lst;
        }

        [HttpPost]
        public JsonResult CURMID(string vendorid)
        {
            VendorLogic logic = new VendorLogic();
            bool type = logic.IsModuleId(vendorid);
            return Json(type, JsonRequestBehavior.AllowGet);
        }
    }
}