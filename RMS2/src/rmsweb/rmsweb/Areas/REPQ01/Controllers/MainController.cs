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

namespace REPQ01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.Search = false;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ViewBag.Search = false;
            string Col = "";
            string Condition = "";
            string conditionValue = "";
            if (Request.Form["ProductNo"].ToString().Trim()!="")
            {
                Col = ",ProductNo";
                Condition = ","+Request.Form["col01"];
                conditionValue = ","+Request.Form["ProductNo"].ToString();
                ViewBag.Search = true;
            }
            if (Request.Form["CustomerNo"].ToString().Trim() != "")
            {
                Col = ",CustomerNo";
                Condition = "," + Request.Form["col02"];
                conditionValue = "," + Request.Form["CustomerNo"].ToString();
                ViewBag.Search = true;
            }
            if(ViewBag.Search)
            {
                RGA_HLogic logic = new RGA_HLogic();
                ViewBag.Sum = logic.SearchPartSum(Col, Condition, conditionValue);
                ViewBag.W = logic.SearchPartW(Col, Condition, conditionValue);
                ViewBag.Wpar = (double.Parse(ViewBag.W) / double.Parse(ViewBag.Sum)) * 100;
                ViewBag.Wpar = ViewBag.Wpar + "%";
                ViewBag.C = logic.SearchPartC(Col, Condition, conditionValue);
                ViewBag.Cpar = (double.Parse(ViewBag.C) / double.Parse(ViewBag.Sum)) * 100;
                ViewBag.Cpar = ViewBag.Wpar + "%";

                ViewBag.strAdvCol = Col;
                ViewBag.strAdvCondition = Condition;
                ViewBag.strAdvValue = conditionValue;

                ViewBag.ProductNo= Request.Form["ProductNo"].ToString();
                ViewBag.CustomerNo = Request.Form["CustomerNo"].ToString();
                ViewBag.col01= Request.Form["col01"];
                ViewBag.col02 = Request.Form["col02"];
            }
            return View();
        }

        [HttpPost]
        public JsonResult SearchPartLifetime()
        {
            RGA_HLogic logic = new RGA_HLogic();
            List<RGA_H> lst = logic.SearchPartLifetime();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchPartLifetimeCover()
        {
            RGA_HLogic logic = new RGA_HLogic();
            List<RGA_H> lst = logic.SearchPartLifetimeCover();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchWarranty_D(string Col, string Condition, string conditionValue)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            List<Warranty_D> lst = logic.SearchWarranty(Col, Condition, conditionValue);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchRGA_H(string Col, string Condition, string conditionValue)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            List<Warranty_D> lst = logic.SearchWarranty(Col, Condition, conditionValue);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchRGA_HCoCover(string Col, string Condition, string conditionValue)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            List<Warranty_D> lst = logic.SearchWarranty(Col, Condition, conditionValue);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailSum(string col, string condition, string value)
        {
            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i])&&colArray[i]== "ProductNo")
                {
                    ViewBag.ProductNo = conditionArray[i] + valueArray[i];
                }

                if (!String.IsNullOrEmpty(valueArray[i]) && colArray[i] == "CustomerId")
                {
                    ViewBag.CustomerId = conditionArray[i] + valueArray[i];
                }
            }
            RGA_HLogic logic = new RGA_HLogic();
            ViewData["Warranty_D"] = logic.SearchPartSumList(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View("DetailSum");
        }

        [HttpPost]
        public ActionResult DetailSum(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["Warranty_D"] = logic.SearchPartSumList(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

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
                    //主件品號,品名,規格,單位,建立日期,確認日期,備註
                    row.CreateCell(0).SetCellValue("異動日期");
                    row.CreateCell(1).SetCellValue("品號");
                    row.CreateCell(2).SetCellValue("品名");
                    row.CreateCell(3).SetCellValue("序號");
                    row.CreateCell(4).SetCellValue("客戶名稱");
                    row.CreateCell(5).SetCellValue("來源單號");
                    row.CreateCell(6).SetCellValue("備註");
                    List<Warranty_D> objWarranty_D = ViewData["Warranty_D"] as List<Warranty_D>;
                    int i = 0;
                    foreach (var obj in objWarranty_D)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.ChangeDate);
                        row.CreateCell(1).SetCellValue(obj.ProductNo);
                        row.CreateCell(2).SetCellValue(obj.ProductName);
                        row.CreateCell(4).SetCellValue(obj.SerialNo);
                        row.CreateCell(3).SetCellValue(obj.CustomerName);
                        row.CreateCell(5).SetCellValue(obj.ChangeOrderType);
                        row.CreateCell(6).SetCellValue(obj.Remark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "BOM.xls");
            }
            return View();
        }

        public ActionResult Detailw(string col, string condition, string value)
        {
            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]) && colArray[i] == "ProductNo")
                {
                    ViewBag.ProductNo = conditionArray[i] + valueArray[i];
                }

                if (!String.IsNullOrEmpty(valueArray[i]) && colArray[i] == "CustomerId")
                {
                    ViewBag.CustomerId = conditionArray[i] + valueArray[i];
                }
            }
            RGA_HLogic logic = new RGA_HLogic();
            ViewData["RGA_H"] = logic.SearchPartWList(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View("Detailw");
        }

        [HttpPost]
        public ActionResult Detailw(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["RGA_H"] = logic.SearchPartWList(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

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
                    //主件品號,品名,規格,單位,建立日期,確認日期,備註
                    row.CreateCell(0).SetCellValue("單據狀態");
                    row.CreateCell(1).SetCellValue("維修單別");
                    row.CreateCell(2).SetCellValue("單據名稱");
                    row.CreateCell(3).SetCellValue("維修單號");
                    row.CreateCell(4).SetCellValue("客戶代號");
                    row.CreateCell(5).SetCellValue("客戶名稱");
                    row.CreateCell(6).SetCellValue("裝機地點名稱");
                    row.CreateCell(7).SetCellValue("品號");
                    row.CreateCell(8).SetCellValue("品名");
                    row.CreateCell(9).SetCellValue("序號");
                    row.CreateCell(10).SetCellValue("資產編號");
                    row.CreateCell(11).SetCellValue("銷貨日期");
                    row.CreateCell(12).SetCellValue("進貨日期");
                    row.CreateCell(13).SetCellValue("入件說明");
                    row.CreateCell(14).SetCellValue("檢測結果");
                    row.CreateCell(15).SetCellValue("維修人員");
                    row.CreateCell(16).SetCellValue("出件日期");
                    row.CreateCell(17).SetCellValue("出件備註");
                    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;
                    int i = 0;
                    foreach (var obj in objRGA_H)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.StatusCode);
                        row.CreateCell(1).SetCellValue(obj.RGAType);
                        row.CreateCell(2).SetCellValue(obj.OrderSName);
                        row.CreateCell(4).SetCellValue(obj.RGANo);
                        row.CreateCell(3).SetCellValue(obj.CustomerId);
                        row.CreateCell(5).SetCellValue(obj.CustomerName);
                        row.CreateCell(6).SetCellValue(obj.AddressSName);
                        row.CreateCell(7).SetCellValue(obj.ProductNo);
                        row.CreateCell(8).SetCellValue(obj.ProductName);
                        row.CreateCell(9).SetCellValue(obj.SerialNo);
                        row.CreateCell(10).SetCellValue(obj.AssetNo);
                        row.CreateCell(11).SetCellValue(obj.SaleDate);
                        row.CreateCell(12).SetCellValue(obj.PurchaseDate);
                        row.CreateCell(13).SetCellValue(obj.Remark);
                        row.CreateCell(14).SetCellValue(obj.TestResult);
                        row.CreateCell(15).SetCellValue(obj.Confirmor);
                        row.CreateCell(16).SetCellValue(obj.ConfirmedDate);
                        row.CreateCell(17).SetCellValue(obj.InternalRemark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "BOM.xls");
            }
            return View();
        }

        public ActionResult Detailc(string col, string condition, string value)
        {
            string[] colArray = col.Split(',');
            string[] conditionArray = condition.Split(',');
            string[] valueArray = value.Split(',');
            for (int i = 1; i < colArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valueArray[i]) && colArray[i] == "ProductNo")
                {
                    ViewBag.ProductNo = conditionArray[i] + valueArray[i];
                }

                if (!String.IsNullOrEmpty(valueArray[i]) && colArray[i] == "CustomerId")
                {
                    ViewBag.CustomerId = conditionArray[i] + valueArray[i];
                }
            }
            RGA_HLogic logic = new RGA_HLogic();
            ViewData["RGA_H"] = logic.SearchPartCList(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View("Detailc");
        }

        [HttpPost]
        public ActionResult Detailc(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["RGA_H"] = logic.SearchPartCList(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

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
                    //主件品號,品名,規格,單位,建立日期,確認日期,備註
                    row.CreateCell(0).SetCellValue("單據狀態");
                    row.CreateCell(1).SetCellValue("維修單別");
                    row.CreateCell(2).SetCellValue("單據名稱");
                    row.CreateCell(3).SetCellValue("維修單號");
                    row.CreateCell(4).SetCellValue("客戶代號");
                    row.CreateCell(5).SetCellValue("客戶名稱");
                    row.CreateCell(6).SetCellValue("裝機地點名稱");
                    row.CreateCell(7).SetCellValue("品號");
                    row.CreateCell(8).SetCellValue("品名");
                    row.CreateCell(9).SetCellValue("序號");
                    row.CreateCell(10).SetCellValue("資產編號");
                    row.CreateCell(11).SetCellValue("銷貨日期");
                    row.CreateCell(12).SetCellValue("進貨日期");
                    row.CreateCell(13).SetCellValue("入件說明");
                    row.CreateCell(14).SetCellValue("檢測結果");
                    row.CreateCell(15).SetCellValue("維修人員");
                    row.CreateCell(16).SetCellValue("出件日期");
                    row.CreateCell(17).SetCellValue("出件備註");
                    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;
                    int i = 0;
                    foreach (var obj in objRGA_H)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.StatusCode);
                        row.CreateCell(1).SetCellValue(obj.RGAType);
                        row.CreateCell(2).SetCellValue(obj.OrderSName);
                        row.CreateCell(4).SetCellValue(obj.RGANo);
                        row.CreateCell(3).SetCellValue(obj.CustomerId);
                        row.CreateCell(5).SetCellValue(obj.CustomerName);
                        row.CreateCell(6).SetCellValue(obj.AddressSName);
                        row.CreateCell(7).SetCellValue(obj.ProductNo);
                        row.CreateCell(8).SetCellValue(obj.ProductName);
                        row.CreateCell(9).SetCellValue(obj.SerialNo);
                        row.CreateCell(10).SetCellValue(obj.AssetNo);
                        row.CreateCell(11).SetCellValue(obj.SaleDate);
                        row.CreateCell(12).SetCellValue(obj.PurchaseDate);
                        row.CreateCell(13).SetCellValue(obj.Remark);
                        row.CreateCell(14).SetCellValue(obj.TestResult);
                        row.CreateCell(15).SetCellValue(obj.Confirmor);
                        row.CreateCell(16).SetCellValue(obj.ConfirmedDate);
                        row.CreateCell(17).SetCellValue(obj.InternalRemark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "BOM.xls");
            }
            return View();
        }

    }
}