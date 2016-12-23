using BusinessLayer.Models;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using rmsweb.Controllers;

namespace PMAB01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ServiceArrange_DLogic logic = new ServiceArrange_DLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["ServiceArrange_D"] = logic.SearchServiceArrange_DInfo(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ServiceArrange_DLogic logic = new ServiceArrange_DLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["ServiceArrange_D"] = logic.SearchServiceArrange_DInfo(col, condition, value);

            }
            if (Request.Form["action"] == "btnExport")
            {
                ViewData["ServiceArrange_D"] = logic.SearchServiceArrange_DInfo(col, condition, value);

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
                //    sw.WriteLine("計劃單別,計劃單號,計劃項次,客戶代號,客戶名稱,客戶形態,品號,品名,序號,保固期限,保固起日,保固迄日");
                //    List<ServiceArrange_D> objServiceArrange_D = ViewData["ServiceArrange_D"] as List<ServiceArrange_D>;
                //    foreach (var obj in objServiceArrange_D)
                //    {
                //        sw.WriteLine(obj.SerArrangeOrderType + "," + obj.SerArrangeOrderNo + "," + obj.ItemId + ","
                //            + obj.CustomerId + "," + obj.CustomerName + "," + obj.CustomerType + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.WarrantyPeriod + "," + obj.WarrantySDate + "," + obj.WarrantyEDate);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "ServiceArrangeModi.csv");
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
                    //計劃單別,計劃單號,計劃項次,客戶代號,客戶名稱,客戶形態,品號,品名,序號,保固期限,保固起日,保固迄日
                    row.CreateCell(0).SetCellValue("計劃單別");
                    row.CreateCell(1).SetCellValue("計劃單號");
                    row.CreateCell(2).SetCellValue("計劃項次");
                    row.CreateCell(3).SetCellValue("客戶代號");
                    row.CreateCell(4).SetCellValue("客戶名稱");
                    row.CreateCell(5).SetCellValue("客戶形態");
                    row.CreateCell(6).SetCellValue("品號");
                    row.CreateCell(7).SetCellValue("品名");
                    row.CreateCell(8).SetCellValue("序號");
                    row.CreateCell(9).SetCellValue("保固期限");
                    row.CreateCell(10).SetCellValue("保固起日");
                    row.CreateCell(11).SetCellValue("保固迄日");
                    List<ServiceArrange_D> objServiceArrange_D = ViewData["ServiceArrange_D"] as List<ServiceArrange_D>;
                    int i = 0;
                    foreach (var obj in objServiceArrange_D)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.SerArrangeOrderType);
                        row.CreateCell(1).SetCellValue(obj.SerArrangeOrderNo);
                        row.CreateCell(2).SetCellValue(obj.ItemId);
                        row.CreateCell(3).SetCellValue(obj.CustomerId);
                        row.CreateCell(4).SetCellValue(obj.CustomerName);
                        row.CreateCell(5).SetCellValue(obj.CustomerType);
                        row.CreateCell(6).SetCellValue(obj.ProductNo);
                        row.CreateCell(7).SetCellValue(obj.ProductName);
                        row.CreateCell(8).SetCellValue(obj.SerialNo);
                        row.CreateCell(9).SetCellValue(obj.WarrantyPeriod);
                        row.CreateCell(10).SetCellValue(obj.WarrantySDate);
                        row.CreateCell(11).SetCellValue(obj.WarrantyEDate);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "ServiceArrangeModi.xls");
            }
            if (Request.Form["action"] == "btnSearch")
            {
                if (Request.Form["strSerArrangeOrderType"].Trim() != "")
                {
                    col += ",d.SerArrangeOrderType";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strSerArrangeOrderType"];
                }

                if (Request.Form["strCustomerId"].Trim() != "")
                {
                    col += ",c.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["strCustomerId"];
                }

                if (Request.Form["strArrangeMonth"].Trim() != "")
                {
                    col += ",d.ArrangeMonth";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["strArrangeMonth"];
                } 
            }
            if (Request.Form["action"] == "btnAllot")
            {
                string chkRemark = Request.Form["chkRemark"] == null ? "N" : "Y";
                RoutineService_H routineService_H = new RoutineService_H();
                routineService_H.RoutineSerOrderType = Request.Form["ordertype"];
                routineService_H.ConfirmedDate = Request.Form["confirmeddate"].Replace("/","");

                if (!logic.ChangeRoutineSdervice(routineService_H, Request.Form["chk"], chkRemark))
                {
                    ViewBag.Msg = "拋轉失敗！";
                }
            }
            ViewData["ServiceArrange_D"] = logic.SearchServiceArrange_DInfo(col, condition, value);
            ViewBag.selCond1 = Request.Form["selCond1"];
            ViewBag.strSerArrangeOrderType = Request.Form["strSerArrangeOrderType"];
            ViewBag.selCond2 = Request.Form["selCond2"];
            ViewBag.strCustomer = Request.Form["strCustomer"];
            ViewBag.selCond3 = Request.Form["selCond3"];
            ViewBag.strArrangeMonth = Request.Form["strArrangeMonth"];
            return View();
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

            return View("CUR");
        }

        [HttpPost]
        public JsonResult GetEmployeeName(string EmployeeId)
        {
            EmployeeLogic logic = new EmployeeLogic();
            return Json(logic.GetEmployeeName(EmployeeId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOrderTypeName(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            return Json(logic.GetOrderTypeName(OrderType, "D2"), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchOrderType(string OrderType, string Col, string Condition, string conditionValue, int Page)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();
        //    OrderCategory orderCategory = new OrderCategory();
        //    orderCategory.OrderType = OrderType;
        //    //orderCategory.OrderSName = OrderSName;
        //    orderCategory.OrderCategoryID = "D2";
        //    List<OrderCategory> lst = logic.GetOrderCategory(orderCategory, Col, Condition, conditionValue, Page);
        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}
    }
}