using BusinessLayer.Models;
using BusinessLogic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;

namespace SERI05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();
            string col = ",h.Confirmed";
            string condition = ",<>";
            string value = ",V";
            ViewBag.selCond3 = "<>";
            ViewBag.comfirmed = "V";
            ViewData["PhoneService_H"] = logic.SearchPhoneService_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;

            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.Company = "";
            phoneService_H.UserGroup = "";
            phoneService_H.Creator = "";
            phoneService_H.PhoneSerType = Request.Form["PhoneSerType"];
            phoneService_H.PhoneSerNo = Request.Form["PhoneSerNo"];
            phoneService_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            phoneService_H.ConfirmedDate = Request.Form["ConfirmedDate"];
            phoneService_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"]==null?"0":Request.Form["NoOfPrints"]);
            phoneService_H.CustomerId = Request.Form["CustomerId"];
            phoneService_H.AddressSName = Request.Form["AddressSName"];
            phoneService_H.Address = Request.Form["Address"];
            phoneService_H.Contact = Request.Form["Contact"];
            phoneService_H.TelNo = Request.Form["TelNo"];
            phoneService_H.FaxNo = Request.Form["FaxNo"];
            phoneService_H.ProductNo = Request.Form["ProductNo"];
            phoneService_H.ProductName = Request.Form["ProductName"];
            if (Request.Form["SerialNo"] != "")
            {
                phoneService_H.SerialNo = Request.Form["SerialNo"];
                phoneService_H.ContractType = Request.Form["ContractType"];
                phoneService_H.ContractNo = Request.Form["ContractNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    phoneService_H.UnderWarranty = "Y";
                }
                else
                {
                    phoneService_H.UnderWarranty = "N";
                }
                phoneService_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
                phoneService_H.PurchaseDate = Request.Form["PurchaseDate"] == null ? "" : Request.Form["PurchaseDate"].ToString().Replace("/", "");
                phoneService_H.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                phoneService_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
            }     
            
            phoneService_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            phoneService_H.Confirmor = "";
            phoneService_H.Remark = Request.Form["Remark"];

            string strItemId = Request.Form["strCItemId"];
            string strProcessDate = Request.Form["strCProcessDate"];
            string strDescription = Request.Form["strCDescription"];
            string strProcessMan = Request.Form["strCProcessMan"];
            string strHours = Request.Form["strCHours"];
            string strUnit = Request.Form["strCUnit"];
            string strRemark = Request.Form["strCRemark"];


            PhoneService_HLogic logic = new PhoneService_HLogic();

            string infomsg = "";
            string msg = "";

            if (!logic.AddPhoneService_H(phoneService_H, strItemId,
             strProcessDate, strDescription, strProcessMan, strHours, strUnit, strRemark, out infomsg))
            {
                msg = "NO-新增失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Exit()
        {
            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.Company = "";
            phoneService_H.UserGroup = "";
            phoneService_H.Creator = "";
            phoneService_H.PhoneSerType = Request.Form["PhoneSerType"];
            phoneService_H.PhoneSerNo = Request.Form["PhoneSerNo"];
            phoneService_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            phoneService_H.ConfirmedDate = Request.Form["ConfirmedDate"];
            phoneService_H.NoOfPrints = int.Parse(Request.Form["NoOfPrints"] == null ? "0" : Request.Form["NoOfPrints"]);
            phoneService_H.CustomerId = Request.Form["CustomerId"];
            phoneService_H.AddressSName = Request.Form["AddressSName"];
            phoneService_H.Address = Request.Form["Address"];
            phoneService_H.Contact = Request.Form["Contact"];
            phoneService_H.TelNo = Request.Form["TelNo"];
            phoneService_H.FaxNo = Request.Form["FaxNo"];
            phoneService_H.ProductNo = Request.Form["ProductNo"];
            phoneService_H.ProductName = Request.Form["ProductName"];
            if (Request.Form["SerialNo"] != "")
            {
                phoneService_H.SerialNo = Request.Form["SerialNo"];
                phoneService_H.ContractType = Request.Form["ContractType"];
                phoneService_H.ContractNo = Request.Form["ContractNo"];
                if (DateTime.Parse(Request.Form["WarrantyStartDate"].ToString()) < DateTime.Parse(Request.Form["OrderDate"].ToString()) && DateTime.Parse(Request.Form["OrderDate"].ToString()) < DateTime.Parse(Request.Form["WarrantyExpiryDate"].ToString()))
                {
                    phoneService_H.UnderWarranty = "Y";
                }
                else
                {
                    phoneService_H.UnderWarranty = "N";
                }
                phoneService_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
                phoneService_H.PurchaseDate = Request.Form["PurchaseDate"] == null ? "" : Request.Form["PurchaseDate"].ToString().Replace("/", "");
                phoneService_H.WarrantyStartDate = Request.Form["WarrantyStartDate"] == null ? "" : Request.Form["WarrantyStartDate"].ToString().Replace("/", "");
                phoneService_H.WarrantyExpiryDate = Request.Form["WarrantyExpiryDate"] == null ? "" : Request.Form["WarrantyExpiryDate"].ToString().Replace("/", "");
            }
            phoneService_H.Confirmed = Request.Form["Confirmed"].Substring(0, 1);
            phoneService_H.Confirmor = "";
            phoneService_H.Remark = Request.Form["Remark"];

            string strItemId = Request.Form["strCItemId"];
            string strProcessDate = Request.Form["strCProcessDate"];
            string strDescription = Request.Form["strCDescription"];
            string strProcessMan = Request.Form["strCProcessMan"];
            string strHours = Request.Form["strCHours"];
            string strUnit = Request.Form["strCUnit"];
            string strRemark = Request.Form["strCRemark"];

            PhoneService_HLogic logic = new PhoneService_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdatePhoneService_H(phoneService_H, strItemId,
             strProcessDate, strDescription, strProcessMan, strHours, strUnit, strRemark, out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //刪除
        public ActionResult Delete(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();

            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;

            string msg = "";
            if (!logic.DelPhoneService_H(phoneService_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();

            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;

            phoneService_H = logic.GetPhoneService_HInfo(phoneService_H);

            ViewBag.PhoneSerType = phoneService_H.PhoneSerType;
            ViewBag.PhoneSerNo = phoneService_H.PhoneSerNo;
            ViewBag.OrderSName = phoneService_H.OrderSName;
            ViewBag.OrderDate = phoneService_H.OrderDate;
            ViewBag.NoOfPrints = phoneService_H.NoOfPrints;
            ViewBag.CustomerId = phoneService_H.CustomerId;
            ViewBag.CustomerName = phoneService_H.CustomerName;
            ViewBag.CustomerType = phoneService_H.CustomerType;
            ViewBag.AddressSName = phoneService_H.AddressSName;
            ViewBag.Address = phoneService_H.Address;
            ViewBag.Contact = phoneService_H.Contact;
            ViewBag.TelNo = phoneService_H.TelNo;
            ViewBag.FaxNo = phoneService_H.FaxNo;
            ViewBag.ProductNo = phoneService_H.ProductNo;
            ViewBag.ProductName = phoneService_H.ProductName;
            ViewBag.SerialNo = phoneService_H.SerialNo;
            ViewBag.ContractType = phoneService_H.ContractType;
            ViewBag.ContractNo = phoneService_H.ContractNo;
            ViewBag.UnderWarranty = phoneService_H.UnderWarranty;
            ViewBag.SaleDate = phoneService_H.SaleDate;
            ViewBag.PurchaseDate = phoneService_H.PurchaseDate;
            ViewBag.WarrantyStartDate = phoneService_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = phoneService_H.WarrantyExpiryDate;
            ViewBag.Confirmed = phoneService_H.Confirmed;
            ViewBag.Remark = phoneService_H.Remark;

            PhoneService_DLogic Dlogic = new PhoneService_DLogic();
            PhoneService_D phoneService_D = new PhoneService_D();
            phoneService_D.PhoneSerType = PhoneSerType;
            phoneService_D.PhoneSerNo = PhoneSerNo;
            ViewData["PhoneService_D"] = Dlogic.SearchPhoneService_D(phoneService_D);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();

            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;

            phoneService_H = logic.GetPhoneService_HInfo(phoneService_H);

            //ViewBag.PhoneSerType = phoneService_H.PhoneSerType;
            //ViewBag.PhoneSerNo = phoneService_H.PhoneSerNo;
            //ViewBag.OrderSName = phoneService_H.OrderSName;
            ViewBag.OrderDate = phoneService_H.OrderDate;
            ViewBag.NoOfPrints = phoneService_H.NoOfPrints;
            ViewBag.CustomerId = phoneService_H.CustomerId;
            ViewBag.CustomerName = phoneService_H.CustomerName;
            ViewBag.CustomerType = phoneService_H.CustomerType;
            ViewBag.AddressSName = phoneService_H.AddressSName;
            ViewBag.Address = phoneService_H.Address;
            ViewBag.Contact = phoneService_H.Contact;
            ViewBag.TelNo = phoneService_H.TelNo;
            ViewBag.FaxNo = phoneService_H.FaxNo;
            ViewBag.ProductNo = phoneService_H.ProductNo;
            ViewBag.ProductName = phoneService_H.ProductName;
            ViewBag.SerialNo = phoneService_H.SerialNo;
            ViewBag.ContractType = phoneService_H.ContractType;
            ViewBag.ContractNo = phoneService_H.ContractNo;
            ViewBag.UnderWarranty = phoneService_H.UnderWarranty;
            ViewBag.SaleDate = phoneService_H.SaleDate;
            ViewBag.PurchaseDate = phoneService_H.PurchaseDate;
            ViewBag.WarrantyStartDate = phoneService_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = phoneService_H.WarrantyExpiryDate;
            ViewBag.Confirmed = "N.未處理";
            ViewBag.Remark = phoneService_H.Remark;

            PhoneService_DLogic Dlogic = new PhoneService_DLogic();
            PhoneService_D phoneService_D = new PhoneService_D();
            phoneService_D.PhoneSerType = PhoneSerType;
            phoneService_D.PhoneSerNo = PhoneSerNo;
            ViewData["PhoneService_D"] = Dlogic.SearchPhoneService_D(phoneService_D);

            return View("CUR");
        }

        public ActionResult Details(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();

            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;

            phoneService_H = logic.GetPhoneService_HInfo(phoneService_H);

            ViewBag.PhoneSerType = phoneService_H.PhoneSerType;
            ViewBag.PhoneSerNo = phoneService_H.PhoneSerNo;
            ViewBag.OrderSName = phoneService_H.OrderSName;
            ViewBag.OrderDate = phoneService_H.OrderDate;
            ViewBag.NoOfPrints = phoneService_H.NoOfPrints;
            ViewBag.CustomerId = phoneService_H.CustomerId;
            ViewBag.CustomerName = phoneService_H.CustomerName;
            ViewBag.CustomerType = phoneService_H.CustomerType;
            ViewBag.AddressSName = phoneService_H.AddressSName;
            ViewBag.Address = phoneService_H.Address;
            ViewBag.Contact = phoneService_H.Contact;
            ViewBag.TelNo = phoneService_H.TelNo;
            ViewBag.FaxNo = phoneService_H.FaxNo;
            ViewBag.ProductNo = phoneService_H.ProductNo;
            ViewBag.ProductName = phoneService_H.ProductName;
            ViewBag.SerialNo = phoneService_H.SerialNo;
            ViewBag.ContractType = phoneService_H.ContractType;
            ViewBag.ContractNo = phoneService_H.ContractNo;
            ViewBag.UnderWarranty = phoneService_H.UnderWarranty;
            ViewBag.SaleDate = phoneService_H.SaleDate;
            ViewBag.PurchaseDate = phoneService_H.PurchaseDate;
            ViewBag.WarrantyStartDate = phoneService_H.WarrantyStartDate;
            ViewBag.WarrantyExpiryDate = phoneService_H.WarrantyExpiryDate;
            ViewBag.Confirmed = phoneService_H.Confirmed;
            ViewBag.Remark = phoneService_H.Remark;

            PhoneService_DLogic Dlogic = new PhoneService_DLogic();
            PhoneService_D phoneService_D = new PhoneService_D();
            phoneService_D.PhoneSerType = PhoneSerType;
            phoneService_D.PhoneSerNo = PhoneSerNo;
            ViewData["PhoneService_D"] = Dlogic.SearchPhoneService_D(phoneService_D);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Tree(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();
            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;
            phoneService_H = logic.GetPhoneService_HInfo(phoneService_H);
            ViewBag.PhoneSerType = phoneService_H.PhoneSerType;
            ViewBag.PhoneSerNo = phoneService_H.PhoneSerNo;
            ViewBag.OrderDate = phoneService_H.OrderDate;
            ViewBag.CustomerId = phoneService_H.CustomerId;
            ViewBag.CustomerName = phoneService_H.CustomerName;
            ViewBag.Confirmed = phoneService_H.Confirmed;
            ViewBag.ProductNo = phoneService_H.ProductNo;
            ViewBag.ProductName = phoneService_H.ProductName;
            ViewBag.SerialNo = phoneService_H.SerialNo;

            return View("Tree");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["PhoneService_H"] = logic.SearchPhoneService_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

            }
            else if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["PhoneService_H"] = logic.SearchPhoneService_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單號,客戶名稱,品號,品名,序號,狀態");
                //    List<PhoneService_H> objPhoneService_H = ViewData["PhoneService_H"] as List<PhoneService_H>;
                //    foreach (var obj in objPhoneService_H)
                //    {
                //        sw.WriteLine(obj.PhoneSerType + "," + obj.PhoneSerNo + ","
                //             + obj.CustomerName + "," 
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + ","
                //            + obj.Confirmed);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("PhoneService");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單號");
                    row.CreateCell(2).SetCellValue("客戶名稱");
                    row.CreateCell(3).SetCellValue("品號");
                    row.CreateCell(4).SetCellValue("品名");
                    row.CreateCell(5).SetCellValue("序號");
                    row.CreateCell(6).SetCellValue("狀態");


                    int index = 1;

                    List<PhoneService_H> objPhoneService_H = ViewData["PhoneService_H"] as List<PhoneService_H>;

                    foreach (var obj in objPhoneService_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.PhoneSerType.ToString());
                        row.CreateCell(1).SetCellValue(obj.PhoneSerNo.ToString());
                        row.CreateCell(2).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(3).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(4).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(5).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(6).SetCellValue(obj.Confirmed.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "PhoneService.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "PhoneService.xls");
            }
            else
            {
                if (Request.Form["phoneserno"].Trim() != "")
                {
                    col += ",h.PhoneSerNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["phoneserno"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }
                if (Request.Form["comfirmed"].Trim() != "")
                {
                    col += ",h.Confirmed";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["comfirmed"];
                }
                ViewData["PhoneService_H"] = logic.SearchPhoneService_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.phoneserno = Request.Form["phoneserno"];
                ViewBag.customerid = Request.Form["customerid"];
                ViewBag.comfirmed = Request.Form["comfirmed"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<PhoneService_D> objPhoneService_D = new List<PhoneService_D>();
            ViewData["PhoneService_D"] = objPhoneService_D;
            ViewBag.NoOfPrints = "0";
            ViewBag.Confirmed = "N.未處理";
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            return View("CUR");
        }

        //通過品名查找
        public JsonResult SearchProductInfo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult IsPhoneService(string PhoneSerType, string PhoneSerNo)
        //{
        //    PhoneService_HLogic logic = new PhoneService_HLogic();
        //    PhoneService_H phoneService_H = new PhoneService_H();
        //    phoneService_H.PhoneSerType = PhoneSerType;
        //    phoneService_H.PhoneSerNo = PhoneSerNo;
        //    return Json(logic.IsPhoneService_H(phoneService_H), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchServiceArrangeSerial(string ProductNo, string SerialNo, int Page)
        //{
        //    ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
        //    ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
        //    serviceArrange_H.ProductNo = ProductNo;
        //    serviceArrange_H.SerialNo = SerialNo;
        //    List<ServiceArrange_H> lst = logic.GetServiceArrange_HSerialNo(serviceArrange_H, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult GetOrderTypeName(string OrderType)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    return Json(logic.GetOrderTypeName(OrderType, "B1"), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchOrderType(string OrderType, string OrderSName, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    orderCategory.OrderType = OrderType;
        //    orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = "B1";
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchServiceArrange(string SerArrangeOrderType, string SerArrangeOrderNo, int Page)
        //{
        //    ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
        //    ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
        //    serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
        //    serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
        //    List<ServiceArrange_H> lst = logic.GetServiceArrange_H(serviceArrange_H, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        //通過定保計劃查詢信息
        //public JsonResult SearchServiceArrangeInfo(string SerArrangeOrderType, string SerArrangeOrderNo)
        //{
        //    ServiceArrangeModi_HLogic Hlogic = new ServiceArrangeModi_HLogic();
        //    ServiceArrangeModi_H serviceArrangeModi_H = new ServiceArrangeModi_H();
        //    serviceArrangeModi_H.SerArrangeOrderType = SerArrangeOrderType;
        //    serviceArrangeModi_H.SerArrangeOrderNo = SerArrangeOrderNo;
        //    serviceArrangeModi_H = Hlogic.GetServiceArrangeModi_HInfo(serviceArrangeModi_H);

        //    if(serviceArrangeModi_H==null)
        //    {

        //        ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
        //        ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
        //        serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
        //        serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
        //        serviceArrange_H = logic.GetServiceArrange_HInfo(serviceArrange_H);
        //        return Json(serviceArrange_H, JsonRequestBehavior.AllowGet);
        //    }else
        //    {
        //        return Json(serviceArrangeModi_H, JsonRequestBehavior.AllowGet);
        //    }

        //}

        ////通過定保計劃查詢信息
        //public JsonResult SearchServiceArrangeDInfo(string SerArrangeOrderType, string SerArrangeOrderNo,string Version)
        //{
        //    ServiceArrangeModi_DLogic Dlogic = new ServiceArrangeModi_DLogic();
        //    List<ServiceArrangeModi_D> Dlst = Dlogic.SearchServiceArrangeModi_D(SerArrangeOrderType, SerArrangeOrderNo, Version);
        //    if(Dlst.Count==0)
        //    {
        //        ServiceArrange_DLogic logic = new ServiceArrange_DLogic();
        //        List<ServiceArrange_D> lst = logic.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);
        //        return Json(lst, JsonRequestBehavior.AllowGet);
        //    }else
        //    {
        //        return Json(Dlst, JsonRequestBehavior.AllowGet);
        //    }

            
        //}

        [HttpPost]
        public JsonResult Confirmed(string Type)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();

            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = Request.Form["PhoneSerType"];
            phoneService_H.PhoneSerNo = Request.Form["PhoneSerNo"];
            if(Type == "Y")
            {
                phoneService_H.Confirmed = "Y";
                phoneService_H.Confirmor = "";
                phoneService_H.ConfirmedDate = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            if (Type == "V")
            {
                phoneService_H.Confirmed = "V";
                //phoneService_H.VoidRemark = Request.Form["sureConfirmDate"].Replace("/", "");
            }

            string msg = "";
            string infomsg = "";
            if (!logic.ConfirmedPhoneService_H(phoneService_H, out infomsg))
            {
                msg = "NO-操作失敗！" + infomsg;
            }
            else
            {
                msg = "OK-成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SearchPhoneService(string PhoneSerType,string PhoneSerNo,string CustomerId,string Confirmed,int Page)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();
            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;
            phoneService_H.CustomerId = CustomerId;
            phoneService_H.Confirmed = Confirmed;
            List<PhoneService_H> lst = logic.SearchPhoneService(phoneService_H, Page);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //通過品名查找
        public JsonResult SearchPhoneServiceInfo(string PhoneSerType, string PhoneSerNo)
        {
            PhoneService_HLogic logic = new PhoneService_HLogic();
            PhoneService_H phoneService_H = new PhoneService_H();
            phoneService_H.PhoneSerType = PhoneSerType;
            phoneService_H.PhoneSerNo = PhoneSerNo;
            phoneService_H = logic.GetPhoneService_HInfo(phoneService_H);
            return Json(phoneService_H, JsonRequestBehavior.AllowGet);
        }
    }
}