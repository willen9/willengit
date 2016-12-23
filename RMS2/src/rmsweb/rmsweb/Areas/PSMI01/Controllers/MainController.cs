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

namespace PSMI01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            SaleSerialLogic logic = new SaleSerialLogic();
            string col = ",s.SettingCode";
            string condition = ",=";
            string value = ",N";
            //ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value, "");
            ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            ViewBag.SettingCode = "N";
            ViewBag.warrantyType = true;
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            SaleSerial saleSerial = new SaleSerial();
            saleSerial.Company = Session["Company"].ToString();
            saleSerial.UserGroup = Session["UserGroup"].ToString();
            saleSerial.Creator = Session["UserId"].ToString();
            saleSerial.ProductNo = Request.Form["ProductNo"];
            saleSerial.SerialNo = Request.Form["SerialNo"];
            saleSerial.CustomerId = Request.Form["CustomerId"];
            saleSerial.SaleDate = Request.Form["SaleDate"].ToString().Replace("/", "");
            saleSerial.SettingCode = Request.Form["SettingCode"].Substring(0, 1);
            saleSerial.RoutineService = Request.Form["RoutineService"] == "" ? "" : Request.Form["RoutineService"].Substring(0, 1);
            saleSerial.SourceOrderType = Request.Form["SourceOrderType"];
            saleSerial.SourceOrderNo = Request.Form["SourceOrderNo"];
            saleSerial.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            saleSerial.Remark = Request.Form["Remark"];

            SaleSerialLogic logic = new SaleSerialLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddSaleSerial(saleSerial, out infomsg))
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
            SaleSerial saleSerial = new SaleSerial();
            saleSerial.Company = Session["Company"].ToString();
            saleSerial.UserGroup = Session["UserGroup"].ToString();
            saleSerial.Modifier = Session["UserId"].ToString();
            saleSerial.ProductNo = Request.Form["ProductNo"];
            saleSerial.SerialNo = Request.Form["SerialNo"];
            saleSerial.CustomerId = Request.Form["CustomerId"];
            saleSerial.SaleDate = Request.Form["SaleDate"].ToString().Replace("/", "");
            saleSerial.SettingCode = Request.Form["SettingCode"].Substring(0, 1);
            saleSerial.RoutineService = Request.Form["RoutineService"] == "" ? "" : Request.Form["RoutineService"].Substring(0, 1);
            saleSerial.SourceOrderType = Request.Form["SourceOrderType"];
            saleSerial.SourceOrderNo = Request.Form["SourceOrderNo"];
            saleSerial.SourceOrderItemId = Request.Form["SourceOrderItemId"];
            saleSerial.Remark = Request.Form["Remark"];

            SaleSerialLogic logic = new SaleSerialLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateSaleSerial(saleSerial, out infomsg))
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
        public ActionResult Delete(string ProductNo, string SerialNo, string CustomerId, string SaleDate)
        {
            SaleSerialLogic logic = new SaleSerialLogic();

            SaleSerial saleSerial = new SaleSerial();
            saleSerial.ProductNo = ProductNo;
            saleSerial.SerialNo = SerialNo;
            saleSerial.CustomerId = CustomerId;
            saleSerial.SaleDate = SaleDate.Replace("/","");

            string msg = "";
            if (!logic.DelSaleSerial(saleSerial,out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string ProductNo, string SerialNo, string CustomerId, string SaleDate)
        {
            SaleSerialLogic logic = new SaleSerialLogic();

            SaleSerial saleSerial = new SaleSerial();
            saleSerial.ProductNo = ProductNo;
            saleSerial.SerialNo = SerialNo;
            saleSerial.CustomerId = CustomerId;
            saleSerial.SaleDate = SaleDate.Replace("/", "");

            saleSerial = logic.GetSaleSerialInfo(saleSerial);

            ViewBag.ProductNo = saleSerial.ProductNo;
            ViewBag.ProductName = saleSerial.ProductName;
            ViewBag.ProductTypeId1Name = saleSerial.ProductTypeId1Name;
            ViewBag.SerialNo = saleSerial.SerialNo;
            ViewBag.CustomerId = saleSerial.CustomerId;
            ViewBag.CustomerName = saleSerial.CustomerName;
            ViewBag.CustomerType = saleSerial.CustomerType;
            ViewBag.SaleDate = saleSerial.SaleDate;
            ViewBag.SettingCode = saleSerial.SettingCode;
            ViewBag.RoutineService = saleSerial.RoutineService;
            ViewBag.SourceOrderType = saleSerial.SourceOrderType;
            ViewBag.SourceOrderNo = saleSerial.SourceOrderNo;
            ViewBag.SourceOrderItemId = saleSerial.SourceOrderItemId;
            ViewBag.Remark = saleSerial.Remark;

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string ProductNo, string SerialNo, string CustomerId, string SaleDate)
        {
            SaleSerialLogic logic = new SaleSerialLogic();

            SaleSerial saleSerial = new SaleSerial();
            saleSerial.ProductNo = ProductNo;
            saleSerial.SerialNo = SerialNo;
            saleSerial.CustomerId = CustomerId;
            saleSerial.SaleDate = SaleDate.Replace("/", "");

            saleSerial = logic.GetSaleSerialInfo(saleSerial);

            ViewBag.ProductNo = saleSerial.ProductNo;
            ViewBag.ProductName = saleSerial.ProductName;
            ViewBag.ProductTypeId1Name = saleSerial.ProductTypeId1Name;
            ViewBag.SerialNo = saleSerial.SerialNo;
            ViewBag.CustomerId = saleSerial.CustomerId;
            ViewBag.CustomerName = saleSerial.CustomerName;
            ViewBag.CustomerType = saleSerial.CustomerType;
            ViewBag.SaleDate = saleSerial.SaleDate;
            ViewBag.SettingCode = saleSerial.SettingCode;
            ViewBag.RoutineService = saleSerial.RoutineService;
            ViewBag.SourceOrderType = saleSerial.SourceOrderType;
            ViewBag.SourceOrderNo = saleSerial.SourceOrderNo;
            ViewBag.SourceOrderItemId = saleSerial.SourceOrderItemId;
            ViewBag.Remark = saleSerial.Remark;

            ViewBag.Type = "Details";
            ViewBag.warrantyType = true;
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            SaleSerialLogic logic = new SaleSerialLogic();
            string col = "";
            string condition = "";
            string value = "";

            SaleSerial saleSerial = new SaleSerial();
            saleSerial.Company = Session["Company"].ToString();
            saleSerial.UserGroup = Session["UserGroup"].ToString();
            saleSerial.Creator = Session["UserId"].ToString();


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

                if (!logic.ImportFile(path + "\\" + name, saleSerial))
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
                string SerArrangeOrderType= Request.Form["SerArrangeOrderType"];
                string RoutineServiceFreq = Request.Form["WarrantyRoutineServiceFreq"];
                string msg = "";
                if (!logic.AddWarranty(WarrantyId, warranty_D, SerArrangeOrderType, RoutineServiceFreq,out msg))
                {
                    ViewBag.Msg = "設置失敗！"+msg;
                }
                col = ",s.SettingCode";
                condition = ",=";
                value = ",N";
                //ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value, Request.Form["selCond3"]);
                ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value);
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
                ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value);
                //ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value, Request.Form["selCond3"]);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

            }
            else if (Request.Form["action"] == "btnExport")
            {
                if(!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                //ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value, Request.Form["selCond3"]);
                ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value);
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
                //    sw.WriteLine("銷貨日期,品號,品名,序號,客戶,來源單別,來源單號,來源項次,狀態,定保,備註");
                //    List<SaleSerial> objSaleSerial = ViewData["SaleSerial"] as List<SaleSerial>;
                //    foreach (var obj in objSaleSerial)
                //    {
                //        sw.WriteLine(obj.SaleDate + "," + obj.ProductNo + "," + obj.ProductName + ","
                //            + obj.SerialNo + "," + obj.CustomerName + "," + obj.SourceOrderType + ","
                //            + obj.SourceOrderNo + "," + obj.SourceOrderItemId + ","
                //            + obj.SettingCode + "," + obj.RoutineService + "," + obj.Remark);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "SaleSerial.csv");
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
                    //銷貨日期,品號,品名,序號,客戶,來源單別,來源單號,來源項次,狀態,定保,備註
                    row.CreateCell(0).SetCellValue("銷貨日期");
                    row.CreateCell(1).SetCellValue("品號");
                    row.CreateCell(2).SetCellValue("品名");
                    row.CreateCell(3).SetCellValue("序號");
                    row.CreateCell(4).SetCellValue("客戶");
                    row.CreateCell(5).SetCellValue("來源單別");
                    row.CreateCell(6).SetCellValue("來源單號");
                    row.CreateCell(7).SetCellValue("來源項次");
                    row.CreateCell(8).SetCellValue("狀態");
                    row.CreateCell(9).SetCellValue("定保");
                    row.CreateCell(10).SetCellValue("備註");
                    List<SaleSerial> objSaleSerial = ViewData["SaleSerial"] as List<SaleSerial>;
                    int i = 0;
                    foreach (var obj in objSaleSerial)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.SaleDate);
                        row.CreateCell(1).SetCellValue(obj.ProductNo);
                        row.CreateCell(2).SetCellValue(obj.ProductName);
                        row.CreateCell(3).SetCellValue(obj.SerialNo);
                        row.CreateCell(4).SetCellValue(obj.CustomerName);
                        row.CreateCell(5).SetCellValue(obj.SourceOrderType);
                        row.CreateCell(6).SetCellValue(obj.SourceOrderNo);
                        row.CreateCell(7).SetCellValue(obj.SourceOrderItemId);
                        row.CreateCell(8).SetCellValue(obj.SettingCode);
                        row.CreateCell(9).SetCellValue(obj.RoutineService);
                        row.CreateCell(10).SetCellValue(obj.Remark);
                    }
                    book.Write(ms);
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    ms.Close();
                    ms.Dispose();
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "SaleSerial.xls");
            }
            else
            {
                //string settingCode = "";
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
                ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.strProductNo = Request.Form["strProductNo"];
                ViewBag.strSerialNo = Request.Form["strSerialNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.SettingCode = Request.Form["SettingCode"];
                //ViewBag.selCond3 = Request.Form["selCond3"];
            }
            ViewBag.warrantyType = true;
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.SettingCode = "N.未設定";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            SaleSerialLogic logic = new SaleSerialLogic();
            string col = "";
            string condition = "";
            string value = "";
            string WarrantyId = Request.Form["ProductNo"]+"#"+ Request.Form["SerialNo"] +"#"+ Request.Form["CustomerId"] +"#"+ Request.Form["SaleDate"];
            Warranty_D warranty_D = new Warranty_D();
            warranty_D.WarrantyPeriod = Request.Form["WarrantyPeriod"];
            warranty_D.Remark = Request.Form["WarrantyRemark"];
            warranty_D.Creator = "";
            warranty_D.Company = "";
            warranty_D.UserGroup = "";
            string SerArrangeOrderType = Request.Form["SerArrangeOrderType"];
            string RoutineServiceFreq = Request.Form["WarrantyRoutineServiceFreq"];
            string msg = "";
            if (!logic.AddWarranty(WarrantyId, warranty_D, SerArrangeOrderType, RoutineServiceFreq,out msg))
            {
                ViewBag.Msg = "設置失敗！"+msg;

                SaleSerial saleSerial = new SaleSerial();
                saleSerial.ProductNo = Request.Form["ProductNo"];
                saleSerial.SerialNo = Request.Form["SerialNo"];
                saleSerial.CustomerId = Request.Form["CustomerId"];
                saleSerial.SaleDate = Request.Form["SaleDate"].Replace("/", "");

                saleSerial = logic.GetSaleSerialInfo(saleSerial);

                ViewBag.ProductNo = saleSerial.ProductNo;
                ViewBag.ProductName = saleSerial.ProductName;
                ViewBag.ProductTypeId1Name = saleSerial.ProductTypeId1Name;
                ViewBag.SerialNo = saleSerial.SerialNo;
                ViewBag.CustomerId = saleSerial.CustomerId;
                ViewBag.CustomerName = saleSerial.CustomerName;
                ViewBag.CustomerType = saleSerial.CustomerType;
                ViewBag.SaleDate = saleSerial.SaleDate;
                ViewBag.SettingCode = saleSerial.SettingCode;
                ViewBag.RoutineService = saleSerial.RoutineService;
                ViewBag.SourceOrderType = saleSerial.SourceOrderType;
                ViewBag.SourceOrderNo = saleSerial.SourceOrderNo;
                ViewBag.SourceOrderItemId = saleSerial.SourceOrderItemId;
                ViewBag.Remark = saleSerial.Remark;

                ViewBag.Type = "Details";


                return View("CUR");
            }
            col = ",s.SettingCode";
            condition = ",=";
            value = ",N";
            //ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value, Request.Form["selCond3"]);
            ViewData["SaleSerial"] = logic.SearchSaleSerial(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            ViewBag.SettingCode = "N";
            return View("Index");
        }

        //通過品名查找
        public JsonResult SearchProductInfo(string ProductNo)
        {
            ProductLogic logic = new ProductLogic();
            Product product = logic.GetProductInfo(ProductNo);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        //通過客戶代號查找
        public JsonResult SearchCustomerInfo(string CustomerId)
        {
            CustomerLogic logic = new CustomerLogic();
            Customer customer = logic.GetCustomerInfo(CustomerId);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
    }
}