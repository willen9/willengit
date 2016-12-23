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

namespace PSMI02.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            PurchasedSerialLogic logic = new PurchasedSerialLogic();
            string col = ",s.SettingCode";
            string condition = ",=";
            string value = ",N";
            ViewData["PurchasedSerial"] = logic.SearchPurchasedSerial(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            ViewBag.SettingCode = "N";
            ViewBag.Type = "Index";
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            PurchasedSerial purchasedSerial = new PurchasedSerial();
            purchasedSerial.Company = Session["Company"].ToString();
            purchasedSerial.UserGroup = Session["UserGroup"].ToString();
            purchasedSerial.Creator = Session["UserId"].ToString();
            purchasedSerial.ProductNo = Request.Form["ProductNo"];
            purchasedSerial.SerialNo = Request.Form["SerialNo"];
            purchasedSerial.VendorId = Request.Form["VendorId"];
            purchasedSerial.PurchaseDate = Request.Form["PurchaseDate"].ToString().Replace("/", "");
            purchasedSerial.SettingCode = Request.Form["SettingCode"].Substring(0, 1);
            purchasedSerial.SourceOrderType = Request.Form["SourceOrderType"];
            purchasedSerial.SourceOrderNo = Request.Form["SourceOrderNo"];
            purchasedSerial.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            purchasedSerial.Remark = Request.Form["Remark"];

            PurchasedSerialLogic logic = new PurchasedSerialLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddPurchasedSerial(purchasedSerial, out infomsg))
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
        public JsonResult checkSure()
        {
            string WarrantyId = Request.Form["ProductNo"]+"#"+ Request.Form["SerialNo"] + "#" + Request.Form["VendorId"] + "#" + Request.Form["PurchaseDate"];
            Warranty_D warranty_D = new Warranty_D();
            warranty_D.WarrantyPeriod = Request.Form["WarrantyPeriod"];
            warranty_D.Remark = Request.Form["WarrantyRemark"];
            warranty_D.Creator = "";
            warranty_D.Company = "";
            warranty_D.UserGroup = "";
            PurchasedSerialLogic logic = new PurchasedSerialLogic();
            string msg = "";
            if (!logic.AddWarranty(WarrantyId, warranty_D,out msg))
            {
                msg = "NO-設置失敗！"+ msg;
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
            PurchasedSerial purchasedSerial = new PurchasedSerial();
            purchasedSerial.Company = Session["Company"].ToString();
            purchasedSerial.UserGroup = Session["UserGroup"].ToString();
            purchasedSerial.Modifier = Session["UserId"].ToString();
            purchasedSerial.ProductNo = Request.Form["ProductNo"];
            purchasedSerial.SerialNo = Request.Form["SerialNo"];
            purchasedSerial.VendorId = Request.Form["VendorId"];
            purchasedSerial.PurchaseDate = Request.Form["PurchaseDate"].ToString().Replace("/", "");
            purchasedSerial.SettingCode = Request.Form["SettingCode"].Substring(0, 1);
            purchasedSerial.SourceOrderType = Request.Form["SourceOrderType"];
            purchasedSerial.SourceOrderNo = Request.Form["SourceOrderNo"];
            purchasedSerial.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            purchasedSerial.Remark = Request.Form["Remark"];

            PurchasedSerialLogic logic = new PurchasedSerialLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdatePurchasedSerial(purchasedSerial, out infomsg))
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
        public ActionResult Delete(string ProductNo, string SerialNo, string VendorId, string PurchaseDate)
        {
            PurchasedSerialLogic logic = new PurchasedSerialLogic();

            PurchasedSerial purchasedSerial = new PurchasedSerial();
            purchasedSerial.ProductNo = ProductNo;
            purchasedSerial.SerialNo = SerialNo;
            purchasedSerial.VendorId = VendorId;
            purchasedSerial.PurchaseDate = PurchaseDate.Replace("/", "");

            string msg = "";
            if (!logic.DelPurchasedSerial(purchasedSerial, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string ProductNo, string SerialNo, string VendorId, string PurchaseDate)
        {
            PurchasedSerialLogic logic = new PurchasedSerialLogic();

            PurchasedSerial purchasedSerial = new PurchasedSerial();
            purchasedSerial.ProductNo = ProductNo;
            purchasedSerial.SerialNo = SerialNo;
            purchasedSerial.VendorId = VendorId;
            purchasedSerial.PurchaseDate = PurchaseDate.Replace("/", "");

            purchasedSerial = logic.GetPurchasedSerialInfo(purchasedSerial);

            ViewBag.ProductNo = purchasedSerial.ProductNo;
            ViewBag.ProductName = purchasedSerial.ProductName;
            ViewBag.ProductTypeId1Name = purchasedSerial.ProductTypeId1Name;
            ViewBag.SerialNo = purchasedSerial.SerialNo;
            ViewBag.VendorId = purchasedSerial.VendorId;
            ViewBag.VendorName = purchasedSerial.VendorName;
            ViewBag.PurchaseDate = purchasedSerial.PurchaseDate;
            ViewBag.SettingCode = purchasedSerial.SettingCode;
            ViewBag.SourceOrderType = purchasedSerial.SourceOrderType;
            ViewBag.SourceOrderNo = purchasedSerial.SourceOrderNo;
            ViewBag.SourceOrderItemId = purchasedSerial.SourceOrderItemId;
            ViewBag.Remark = purchasedSerial.Remark;

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string ProductNo, string SerialNo, string VendorId, string PurchaseDate)
        {
            PurchasedSerialLogic logic = new PurchasedSerialLogic();

            PurchasedSerial purchasedSerial = new PurchasedSerial();
            purchasedSerial.ProductNo = ProductNo;
            purchasedSerial.SerialNo = SerialNo;
            purchasedSerial.VendorId = VendorId;
            purchasedSerial.PurchaseDate = PurchaseDate.Replace("/", "");

            purchasedSerial = logic.GetPurchasedSerialInfo(purchasedSerial);

            ViewBag.ProductNo = purchasedSerial.ProductNo;
            ViewBag.ProductName = purchasedSerial.ProductName;
            ViewBag.ProductTypeId1Name = purchasedSerial.ProductTypeId1Name;
            ViewBag.SerialNo = purchasedSerial.SerialNo;
            ViewBag.VendorId = purchasedSerial.VendorId;
            ViewBag.VendorName = purchasedSerial.VendorName;
            ViewBag.PurchaseDate = purchasedSerial.PurchaseDate;
            ViewBag.SettingCode = purchasedSerial.SettingCode;
            ViewBag.SourceOrderType = purchasedSerial.SourceOrderType;
            ViewBag.SourceOrderNo = purchasedSerial.SourceOrderNo;
            ViewBag.SourceOrderItemId = purchasedSerial.SourceOrderItemId;
            ViewBag.Remark = purchasedSerial.Remark;

            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            PurchasedSerialLogic logic = new PurchasedSerialLogic();
            string col = "";
            string condition = "";
            string value = "";

            PurchasedSerial purchasedSerial = new PurchasedSerial();
            purchasedSerial.Company = Session["Company"].ToString();
            purchasedSerial.UserGroup = Session["UserGroup"].ToString();
            purchasedSerial.Creator = Session["UserId"].ToString();

            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            if (hfc.Count > 0)
            {
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

                if (!logic.ImportFile(path + "\\" + name, purchasedSerial))
                {
                    return Json("NO-匯入失敗", "text/x-json");
                }

                return Json("YES-匯入成功", "text/x-json");
            }
            if (Request.Form["action"] == "btnWarranty")
            {
                string WarrantyId = Request.Form["chk"];
                Warranty_D warranty_D = new Warranty_D();
                warranty_D.WarrantyPeriod = Request.Form["WarrantyPeriod"];
                warranty_D.Remark = Request.Form["WarrantyRemark"];
                warranty_D.Creator = "";
                warranty_D.Company = "";
                warranty_D.UserGroup = "";
                string msg = "";
                if (!logic.AddWarranty(WarrantyId, warranty_D,out msg))
                {
                    ViewBag.Msg = "設置失敗！"+msg;
                }
                col = ",s.SettingCode";
                condition = ",=";
                value = ",N";
                ViewData["PurchasedSerial"] = logic.SearchPurchasedSerial(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.SettingCode = "N";

            }
            else if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["PurchasedSerial"] = logic.SearchPurchasedSerial(col, condition, value);
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
                ViewData["PurchasedSerial"] = logic.SearchPurchasedSerial(col, condition, value);

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
                //    sw.WriteLine("進貨日期,品號,品名,序號,廠商,來源單別,來源單號,來源項次,狀態,備註");
                //    List<PurchasedSerial> objPurchasedSerial = ViewData["PurchasedSerial"] as List<PurchasedSerial>;
                //    foreach (var obj in objPurchasedSerial)
                //    {
                //        sw.WriteLine(obj.PurchaseDate + "," + obj.ProductNo + "," + obj.ProductName + ","
                //            + obj.SerialNo + "," + obj.VendorName + "," + obj.SourceOrderType + "," + obj.SourceOrderNo + "," + obj.SourceOrderItemId + ","
                //            + obj.SettingCode + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "PurchasedSerial.csv");
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
                    //進貨日期,品號,品名,序號,廠商,來源單別,來源單號,來源項次,狀態,備註
                    row.CreateCell(0).SetCellValue("進貨日期");
                    row.CreateCell(1).SetCellValue("品號");
                    row.CreateCell(2).SetCellValue("品名");
                    row.CreateCell(3).SetCellValue("序號");
                    row.CreateCell(4).SetCellValue("廠商");
                    row.CreateCell(5).SetCellValue("來源單別");
                    row.CreateCell(6).SetCellValue("來源單號");
                    row.CreateCell(7).SetCellValue("來源項次");
                    row.CreateCell(8).SetCellValue("狀態");
                    row.CreateCell(9).SetCellValue("備註");
                    List<PurchasedSerial> objPurchasedSerial = ViewData["PurchasedSerial"] as List<PurchasedSerial>;
                    int i = 0;
                    foreach (var obj in objPurchasedSerial)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.PurchaseDate);
                        row.CreateCell(1).SetCellValue(obj.ProductNo);
                        row.CreateCell(2).SetCellValue(obj.ProductName);
                        row.CreateCell(3).SetCellValue(obj.SerialNo);
                        row.CreateCell(4).SetCellValue(obj.VendorName);
                        row.CreateCell(5).SetCellValue(obj.SourceOrderType);
                        row.CreateCell(6).SetCellValue(obj.SourceOrderNo);
                        row.CreateCell(7).SetCellValue(obj.SourceOrderItemId);
                        row.CreateCell(8).SetCellValue(obj.SettingCode);
                        row.CreateCell(9).SetCellValue(obj.Remark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "PurchasedSerial.xls");
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
                if (Request.Form["SettingCode"].Trim() != "")
                {
                    col += ",s.SettingCode";
                    condition += ",=";
                    value += "," + Request.Form["SettingCode"];
                }
                ViewData["PurchasedSerial"] = logic.SearchPurchasedSerial(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.strProductNo = Request.Form["strProductNo"];
                ViewBag.strSerialNo = Request.Form["strSerialNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.SettingCode = Request.Form["SettingCode"];
            }
            ViewBag.Type = "Index";
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.SettingCode = "N.未設定";
            return View("CUR");
        }

        [HttpPost]
        //通過品名查找
        public JsonResult SearchProductInfo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            return Json(logic.GetProductInfo(ProductNo), JsonRequestBehavior.AllowGet);
        }
    }
}