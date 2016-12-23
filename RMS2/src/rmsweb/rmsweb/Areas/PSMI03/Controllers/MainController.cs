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

namespace PSMI03.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            Warranty_HLogic logic = new Warranty_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["Warranty_H"] = logic.SearchWarranty_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            Warranty_H warranty_H = new Warranty_H();
            warranty_H.Company = Session["Company"].ToString();
            warranty_H.UserGroup = Session["UserGroup"].ToString();
            warranty_H.Creator = Session["UserId"].ToString();
            warranty_H.ProductNo = Request.Form["ProductNo"];
            warranty_H.SerialNo = Request.Form["SerialNo"];
            warranty_H.Remark = Request.Form["Remark"];

            string strChangeDate = Request.Form["strCChangeDate"];
            string strChangeOrderType = Request.Form["strCChangeOrderType"];
            string strChangeOrderNo = Request.Form["strCChangeOrderNo"];
            string strChangeOrderItemId = Request.Form["strCChangeOrderItemId"];
            string strTargetId = Request.Form["strCTargetId"];
            string strTargetName = Request.Form["strCTargetName"];
            string strWarrantyPeriod = Request.Form["strCWarrantyPeriod"];
            string strWarrantySDate = Request.Form["strCWarrantySDate"];
            string strWarrantyEDate = Request.Form["strCWarrantyEDate"];
            string strWarrantyType = Request.Form["strCWarrantyType"];
            string strRemark = Request.Form["strCRemark"];

            Warranty_HLogic logic = new Warranty_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.AddWarranty_H(warranty_H,strChangeDate,
             strChangeOrderType,  strChangeOrderNo,  strChangeOrderItemId,
             strTargetId,  strTargetName,
             strWarrantyPeriod,  strWarrantySDate, strWarrantyEDate,
             strWarrantyType,  strRemark, out infomsg))
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
            Warranty_H warranty_H = new Warranty_H();
            warranty_H.Company = Session["Company"].ToString();
            warranty_H.UserGroup = Session["UserGroup"].ToString();
            warranty_H.Modifier = Session["UserId"].ToString();
            warranty_H.ProductNo = Request.Form["ProductNo"];
            warranty_H.SerialNo = Request.Form["SerialNo"];
            warranty_H.Remark = Request.Form["Remark"];

            string strChangeDate = Request.Form["strCChangeDate"];
            string strChangeOrderType = Request.Form["strCChangeOrderType"];
            string strChangeOrderNo = Request.Form["strCChangeOrderNo"];
            string strChangeOrderItemId = Request.Form["strCChangeOrderItemId"];
            string strTargetId = Request.Form["strCTargetId"];
            string strTargetName = Request.Form["strCTargetName"];
            string strWarrantyPeriod = Request.Form["strCWarrantyPeriod"];
            string strWarrantySDate = Request.Form["strCWarrantySDate"];
            string strWarrantyEDate = Request.Form["strCWarrantyEDate"];
            string strWarrantyType = Request.Form["strCWarrantyType"];
            string strRemark = Request.Form["strCRemark"];

            Warranty_HLogic logic = new Warranty_HLogic();

            string infomsg = "";
            string msg = "";
            if (!logic.UpdateWarranty_H(warranty_H, strChangeDate,
             strChangeOrderType, strChangeOrderNo, strChangeOrderItemId,
             strTargetId, strTargetName,
             strWarrantyPeriod, strWarrantySDate, strWarrantyEDate,
             strWarrantyType, strRemark, out infomsg))
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
        public ActionResult Delete(string ProductNo, string SerialNo)
        {
            Warranty_HLogic logic = new Warranty_HLogic();

            Warranty_H warranty_H = new Warranty_H();
            warranty_H.ProductNo = ProductNo;
            warranty_H.SerialNo = SerialNo;

            string msg = "";
            if (!logic.DelWarranty_H(warranty_H, out msg))
            {
                ViewBag.Msg = "刪除失敗！" + msg;
            }

            return RedirectToAction("Index", "Main");
        }

        public ActionResult Edit(string ProductNo, string SerialNo)
        {
            Warranty_HLogic logic = new Warranty_HLogic();

            Warranty_H warranty_H = new Warranty_H();
            warranty_H.ProductNo = ProductNo;
            warranty_H.SerialNo = SerialNo;

            warranty_H = logic.GetWarranty_HInfo(warranty_H);

            ViewBag.ProductNo = warranty_H.ProductNo;
            ViewBag.ProductName = warranty_H.ProductName;
            ViewBag.SerialNo = warranty_H.SerialNo;
            ViewBag.LastWarrantyType = warranty_H.LastWarrantyType;
            ViewBag.LastWarrantyRemark = warranty_H.LastWarrantyRemark;
            ViewBag.LastWarrantySDate = warranty_H.LastWarrantySDate;
            ViewBag.LastWarrantyEDate = warranty_H.LastWarrantyEDate;
            ViewBag.Remark = warranty_H.Remark;

            Warranty_DLogic Dlogic = new Warranty_DLogic();
            ViewData["Warranty_D"] = Dlogic.SearchWarranty_D(ProductNo, SerialNo);

            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Details(string ProductNo, string SerialNo)
        {
            Warranty_HLogic logic = new Warranty_HLogic();

            Warranty_H warranty_H = new Warranty_H();
            warranty_H.ProductNo = ProductNo;
            warranty_H.SerialNo = SerialNo;

            warranty_H = logic.GetWarranty_HInfo(warranty_H);

            ViewBag.ProductNo = warranty_H.ProductNo;
            ViewBag.ProductName = warranty_H.ProductName;
            ViewBag.SerialNo = warranty_H.SerialNo;
            ViewBag.LastWarrantyType = warranty_H.LastWarrantyType;
            ViewBag.LastWarrantyRemark = warranty_H.LastWarrantyRemark;
            ViewBag.LastWarrantySDate = warranty_H.LastWarrantySDate;
            ViewBag.LastWarrantyEDate = warranty_H.LastWarrantyEDate;
            ViewBag.Remark = warranty_H.Remark;

            Warranty_DLogic Dlogic = new Warranty_DLogic();
            ViewData["Warranty_D"] = Dlogic.SearchWarranty_D(ProductNo, SerialNo);

            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            Warranty_HLogic logic = new Warranty_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["Warranty_H"] = logic.SearchWarranty_H(col, condition, value);
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

                ViewData["Warranty_H"] = logic.SearchWarranty_H(col, condition, value);

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
                    //品號,品名,序號,最新保固類型,最新保固起日,最新保固迄日,備註
                    row.CreateCell(0).SetCellValue("品號");
                    row.CreateCell(1).SetCellValue("品名");
                    row.CreateCell(2).SetCellValue("序號");
                    row.CreateCell(3).SetCellValue("最新保固類型");
                    row.CreateCell(4).SetCellValue("最新保固起日");
                    row.CreateCell(5).SetCellValue("最新保固迄日");
                    row.CreateCell(6).SetCellValue("備註");
                    List<Warranty_H> objWarranty_H = ViewData["Warranty_H"] as List<Warranty_H>;
                    int i = 0;
                    foreach (var obj in objWarranty_H)
                    {
                        i++;
                        row = sheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(obj.ProductNo);
                        row.CreateCell(1).SetCellValue(obj.ProductName);
                        row.CreateCell(2).SetCellValue(obj.SerialNo);
                        row.CreateCell(3).SetCellValue(obj.LastWarrantyType);
                        row.CreateCell(4).SetCellValue(obj.LastWarrantySDate);
                        row.CreateCell(5).SetCellValue(obj.LastWarrantyEDate);
                        row.CreateCell(6).SetCellValue(obj.Remark);
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
                    col += ",w.ProductNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["strProductNo"];
                }
                if (Request.Form["strSerialNo"].Trim() != "")
                {
                    col += ",w.SerialNo";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["strSerialNo"];
                }
                ViewData["Warranty_H"] = logic.SearchWarranty_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.strProductNo = Request.Form["strProductNo"];
                ViewBag.strSerialNo = Request.Form["strSerialNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            List<Warranty_D> objWarranty_D = new List<Warranty_D>();
            ViewData["Warranty_D"] = objWarranty_D;
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
    }
}