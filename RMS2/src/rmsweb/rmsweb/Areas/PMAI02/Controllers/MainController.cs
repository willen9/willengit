using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace PMAI02.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["ServiceArrange_H"] = logic.SearchServiceArrange_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.Company = "";
            serviceArrange_H.UserGroup = "";
            serviceArrange_H.Creator = "";
            serviceArrange_H.SerArrangeOrderType = Request.Form["SerArrangeOrderType"];
            serviceArrange_H.SerArrangeOrderNo = Request.Form["SerArrangeOrderNo"];
            serviceArrange_H.OrderDate = Request.Form["OrderDate"] == null?"":Request.Form["OrderDate"].ToString().Replace("/", "");
            serviceArrange_H.ConfirmedDate = Request.Form["ConfirmedDate"].Replace("/", "");
            serviceArrange_H.CustomerId = Request.Form["CustomerId"];
            serviceArrange_H.Version = Request.Form["Version"];
            serviceArrange_H.SourceOrderType = Request.Form["SourceOrderType"];
            serviceArrange_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            serviceArrange_H.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            serviceArrange_H.ProductNo = Request.Form["ProductNo"];
            serviceArrange_H.ProductName = Request.Form["ProductName"];
            serviceArrange_H.SerialNo = Request.Form["SerialNo"];
            serviceArrange_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
            serviceArrange_H.RoutineServiceFreq = Request.Form["RoutineServiceFreq"] == "" ? "0" : Request.Form["RoutineServiceFreq"];
            serviceArrange_H.WarrantyPeriod = Request.Form["WarrantyPeriod"] == "" ? "0" : Request.Form["WarrantyPeriod"];
            serviceArrange_H.WarrantySDate = Request.Form["WarrantySDate"] == null ? "" : Request.Form["WarrantySDate"].ToString().Replace("/", "");
            serviceArrange_H.WarrantyEDate = Request.Form["WarrantyEDate"] == null ? "" : Request.Form["WarrantyEDate"].ToString().Replace("/", "");
            serviceArrange_H.Confirmed = "N";
            serviceArrange_H.Confirmor = "";
            serviceArrange_H.Remark = Request.Form["Remark"];
            serviceArrange_H.InternalRemark = Request.Form["InternalRemark"];


            string strItemId = Request.Form["strCItemId"];
            string strArrangeMonth = Request.Form["strCArrangeMonth"];
            string strAddressSName = Request.Form["strCAddressSName"];
            string strAddress = Request.Form["strCAddress"];
            string strIsClosed = Request.Form["strCIsClosed"];
            string strRemark = Request.Form["strCRemark"];

            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddServiceArrange_H(serviceArrange_H, strItemId,
             strArrangeMonth, strAddressSName, strAddress,
             strIsClosed, strRemark, out infomsg))
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
            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.Company = "";
            serviceArrange_H.UserGroup = "";
            serviceArrange_H.Modifier = "";
            serviceArrange_H.SerArrangeOrderType = Request.Form["SerArrangeOrderType"];
            serviceArrange_H.SerArrangeOrderNo = Request.Form["SerArrangeOrderNo"];
            serviceArrange_H.OrderDate = Request.Form["OrderDate"] == null ? "" : Request.Form["OrderDate"].ToString().Replace("/", "");
            serviceArrange_H.ConfirmedDate = Request.Form["ConfirmedDate"].Replace("/", "");
            serviceArrange_H.CustomerId = Request.Form["CustomerId"];
            serviceArrange_H.Version = Request.Form["Version"];
            serviceArrange_H.SourceOrderType = Request.Form["SourceOrderType"];
            serviceArrange_H.SourceOrderNo = Request.Form["SourceOrderNo"];
            serviceArrange_H.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            serviceArrange_H.ProductNo = Request.Form["ProductNo"];
            serviceArrange_H.ProductName = Request.Form["ProductName"];
            serviceArrange_H.SerialNo = Request.Form["SerialNo"];
            serviceArrange_H.SaleDate = Request.Form["SaleDate"] == null ? "" : Request.Form["SaleDate"].ToString().Replace("/", "");
            serviceArrange_H.RoutineServiceFreq = Request.Form["RoutineServiceFreq"] == "" ? "0" : Request.Form["RoutineServiceFreq"];
            serviceArrange_H.WarrantyPeriod = Request.Form["WarrantyPeriod"] == "" ? "0" : Request.Form["WarrantyPeriod"];
            serviceArrange_H.WarrantySDate = Request.Form["WarrantySDate"] == null ? "" : Request.Form["WarrantySDate"].ToString().Replace("/", "");
            serviceArrange_H.WarrantyEDate = Request.Form["WarrantyEDate"] == null ? "" : Request.Form["WarrantyEDate"].ToString().Replace("/", "");
            serviceArrange_H.Confirmed = "N";
            serviceArrange_H.Confirmor = "";
            serviceArrange_H.Remark = Request.Form["Remark"];
            serviceArrange_H.InternalRemark = Request.Form["InternalRemark"];


            string strItemId = Request.Form["strCItemId"];
            string strArrangeMonth = Request.Form["strCArrangeMonth"];
            string strAddressSName = Request.Form["strCAddressSName"];
            string strAddress = Request.Form["strCAddress"];
            string strIsClosed = Request.Form["strCIsClosed"];
            string strRemark = Request.Form["strCRemark"];

            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateServiceArrange_H(serviceArrange_H, strItemId,
             strArrangeMonth, strAddressSName, strAddress,
             strIsClosed, strRemark, out infomsg))
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
        public ActionResult Delete(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();

            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;

            string msg = "";
            if (!logic.DelServiceArrange_H(serviceArrange_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();

            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;

            serviceArrange_H = logic.GetServiceArrange_HInfo(serviceArrange_H);

            ViewBag.SerArrangeOrderType = serviceArrange_H.SerArrangeOrderType;
            ViewBag.SerArrangeOrderNo = serviceArrange_H.SerArrangeOrderNo;
            ViewBag.OrderDate = serviceArrange_H.OrderDate;
            ViewBag.ConfirmedDate = serviceArrange_H.ConfirmedDate;
            ViewBag.CustomerId = serviceArrange_H.CustomerId;
            ViewBag.CustomerName = serviceArrange_H.CustomerName;
            ViewBag.CustomerType = serviceArrange_H.CustomerType;
            ViewBag.Version = serviceArrange_H.Version;
            ViewBag.SourceOrderType = serviceArrange_H.SourceOrderType;
            ViewBag.SourceOrderNo = serviceArrange_H.SourceOrderNo;
            ViewBag.SourceOrderItemId = serviceArrange_H.SourceOrderItemId;
            ViewBag.ProductNo = serviceArrange_H.ProductNo;
            ViewBag.ProductName = serviceArrange_H.ProductName;
            ViewBag.SerialNo = serviceArrange_H.SerialNo;
            ViewBag.SaleDate = serviceArrange_H.SaleDate;
            ViewBag.RoutineServiceFreq = serviceArrange_H.RoutineServiceFreq;
            ViewBag.WarrantyPeriod = serviceArrange_H.WarrantyPeriod;
            ViewBag.WarrantySDate = serviceArrange_H.WarrantySDate;
            ViewBag.WarrantyEDate = serviceArrange_H.WarrantyEDate;
            ViewBag.Confirmed = serviceArrange_H.Confirmed;
            ViewBag.Confirmor = serviceArrange_H.Confirmor;
            ViewBag.Remark = serviceArrange_H.Remark;
            ViewBag.InternalRemark = serviceArrange_H.InternalRemark;

            ServiceArrange_DLogic Dlogic = new ServiceArrange_DLogic();
            ViewData["ServiceArrange_D"] = Dlogic.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();

            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;

            serviceArrange_H = logic.GetServiceArrange_HInfo(serviceArrange_H);

            ViewBag.SerArrangeOrderType = serviceArrange_H.SerArrangeOrderType;
            ViewBag.SerArrangeOrderNo = serviceArrange_H.SerArrangeOrderNo;
            ViewBag.OrderDate = serviceArrange_H.OrderDate;
            ViewBag.ConfirmedDate = serviceArrange_H.ConfirmedDate;
            ViewBag.CustomerId = serviceArrange_H.CustomerId;
            ViewBag.CustomerName = serviceArrange_H.CustomerName;
            ViewBag.CustomerType = serviceArrange_H.CustomerType;
            ViewBag.Version = serviceArrange_H.Version;
            ViewBag.SourceOrderType = serviceArrange_H.SourceOrderType;
            ViewBag.SourceOrderNo = serviceArrange_H.SourceOrderNo;
            ViewBag.SourceOrderItemId = serviceArrange_H.SourceOrderItemId;
            ViewBag.ProductNo = serviceArrange_H.ProductNo;
            ViewBag.ProductName = serviceArrange_H.ProductName;
            ViewBag.SerialNo = serviceArrange_H.SerialNo;
            ViewBag.SaleDate = serviceArrange_H.SaleDate;
            ViewBag.RoutineServiceFreq = serviceArrange_H.RoutineServiceFreq;
            ViewBag.WarrantyPeriod = serviceArrange_H.WarrantyPeriod;
            ViewBag.WarrantySDate = serviceArrange_H.WarrantySDate;
            ViewBag.WarrantyEDate = serviceArrange_H.WarrantyEDate;
            ViewBag.Confirmed = serviceArrange_H.Confirmed;
            ViewBag.Confirmor = serviceArrange_H.Confirmor;
            ViewBag.Remark = serviceArrange_H.Remark;
            ViewBag.InternalRemark = serviceArrange_H.InternalRemark;

            ServiceArrange_DLogic Dlogic = new ServiceArrange_DLogic();
            ViewData["ServiceArrange_D"] = Dlogic.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Copy(string SerArrangeOrderType, string SerArrangeOrderNo)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();

            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;

            serviceArrange_H = logic.GetServiceArrange_HInfo(serviceArrange_H);

            //ViewBag.SerArrangeOrderType = serviceArrange_H.SerArrangeOrderType;
            //ViewBag.SerArrangeOrderNo = serviceArrange_H.SerArrangeOrderNo;
            ViewBag.OrderDate = serviceArrange_H.OrderDate;
            ViewBag.ConfirmedDate = serviceArrange_H.ConfirmedDate;
            ViewBag.CustomerId = serviceArrange_H.CustomerId;
            ViewBag.CustomerName = serviceArrange_H.CustomerName;
            ViewBag.CustomerType = serviceArrange_H.CustomerType;
            ViewBag.Version = serviceArrange_H.Version;
            ViewBag.SourceOrderType = serviceArrange_H.SourceOrderType;
            ViewBag.SourceOrderNo = serviceArrange_H.SourceOrderNo;
            ViewBag.SourceOrderItemId = serviceArrange_H.SourceOrderItemId;
            ViewBag.ProductNo = serviceArrange_H.ProductNo;
            ViewBag.ProductName = serviceArrange_H.ProductName;
            ViewBag.SerialNo = serviceArrange_H.SerialNo;
            ViewBag.SaleDate = serviceArrange_H.SaleDate;
            ViewBag.RoutineServiceFreq = serviceArrange_H.RoutineServiceFreq;
            ViewBag.WarrantyPeriod = serviceArrange_H.WarrantyPeriod;
            ViewBag.WarrantySDate = serviceArrange_H.WarrantySDate;
            ViewBag.WarrantyEDate = serviceArrange_H.WarrantyEDate;
            ViewBag.Confirmed = "N:未確認";
            ViewBag.Remark = serviceArrange_H.Remark;
            ViewBag.InternalRemark = serviceArrange_H.InternalRemark;

            ServiceArrange_DLogic Dlogic = new ServiceArrange_DLogic();
            ViewData["ServiceArrange_D"] = Dlogic.SearchServiceArrange_D(SerArrangeOrderType, SerArrangeOrderNo);
            
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["ServiceArrange_H"] = logic.SearchServiceArrange_H(col, condition, value);

            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["ServiceArrange_H"] = logic.SearchServiceArrange_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                #region
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("品號,品名,序號,最新保固類型,最新保固起日,最新保固迄日,備註");
                //    List<Warranty_H> objWarranty_H = ViewData["Warranty_H"] as List<Warranty_H>;
                //    foreach (var obj in objWarranty_H)
                //    {
                //        sw.WriteLine(obj.ProductNo + "," + obj.ProductName + ","
                //            + obj.SerialNo + "," + obj.LastWarrantyType + "," + obj.LastWarrantySDate + ","
                //            + obj.LastWarrantyEDate + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "Warranty_H.csv");
                #endregion

                MemoryStream ms = null;
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    //XSSFWorkbook workbook = new XSSFWorkbook();
                    HSSFWorkbook book = new HSSFWorkbook();
                    ISheet sheet = book.CreateSheet();
                    HSSFCellStyle lo_Style = (HSSFCellStyle)book.CreateCellStyle();
                    lo_Style.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                    ms = new MemoryStream();
                    IRow row = null;
                    row = sheet.CreateRow(0);

                    row.CreateCell(0).SetCellValue("定保計畫單別");
                    row.CreateCell(1).SetCellValue("單別名稱");
                    row.CreateCell(2).SetCellValue("計劃單號");
                    row.CreateCell(3).SetCellValue("客戶名稱");
                    row.CreateCell(4).SetCellValue("品號");
                    row.CreateCell(5).SetCellValue("品名");
                    row.CreateCell(6).SetCellValue("序號");
                    row.CreateCell(7).SetCellValue("保固起日");
                    row.CreateCell(8).SetCellValue("保固迄日");
                    List<ServiceArrange_H> objServiceArrange_H = ViewData["ServiceArrange_H"] as List<ServiceArrange_H>;
                    int i = 0;
                    foreach (var obj in objServiceArrange_H)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.SerArrangeOrderType);
                        row.CreateCell(1).SetCellValue("");//單別名稱
                        row.CreateCell(2).SetCellValue(obj.SerArrangeOrderNo);
                        row.CreateCell(3).SetCellValue(obj.CustomerName);
                        row.CreateCell(4).SetCellValue(obj.ProductNo);
                        row.CreateCell(5).SetCellValue(obj.ProductName);
                        row.CreateCell(6).SetCellValue(obj.SerialNo);
                        row.CreateCell(7).SetCellValue(obj.WarrantySDate);
                        row.CreateCell(8).SetCellValue(obj.WarrantyEDate);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Warranty_H.xls");
            }
            else
            {
                if (Request.Form["strProductNo"].Trim() != "")
                {
                    col += ",s.ProductNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strProductNo"];
                }
                if (Request.Form["strSerialNo"].Trim() != "")
                {
                    col += ",s.SerialNo";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["strSerialNo"];
                }
                ViewData["ServiceArrange_H"] = logic.SearchServiceArrange_H(col, condition, value);
                ViewBag.strProductNo = Request.Form["strProductNo"];
                ViewBag.strSerialNo = Request.Form["strSerialNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<ServiceArrange_D> objServiceArrange_D = new List<ServiceArrange_D>();
            ViewData["ServiceArrange_D"] = objServiceArrange_D;
            ViewBag.OrderDate = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.Version = "0000";
            ViewBag.Confirmed = "N:未確認";
            return View("CUR");
        }

        //通過品名查找
        public JsonResult SearchProductInfo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SerialNo(string SerialNo)
        {
            Warranty_HLogic logic = new Warranty_HLogic();
            return Json(logic.IsSerialNo(SerialNo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "D1"), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchOrderType(string OrderType, string Col, string Condition, string conditionValue, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    orderCategory.OrderType = OrderType;
        //    //orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = "D1";
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Confirmed(string SerArrangeOrderType, string SerArrangeOrderNo, string type)
        {
            ServiceArrange_H serviceArrange_H = new ServiceArrange_H();
            serviceArrange_H.SerArrangeOrderType = SerArrangeOrderType;
            serviceArrange_H.SerArrangeOrderNo = SerArrangeOrderNo;
            serviceArrange_H.Company = Session["Company"].ToString();
            serviceArrange_H.UserGroup = Session["UserGroup"].ToString();
            serviceArrange_H.Modifier = Session["UserId"].ToString();
            serviceArrange_H.Confirmed = type;
            serviceArrange_H.Confirmor = Session["UserId"].ToString();
            ServiceArrange_HLogic logic = new ServiceArrange_HLogic();
            logic.UpdateConfirmed(serviceArrange_H);
            return RedirectToAction("Index");
        }
    }
}