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

namespace RMAB01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RMAAplLogic logic = new RMAAplLogic();
            string col = ",r.Confirmed";
            string condition = ",=";
            string value = ",N";
            ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RMAAplLogic logic = new RMAAplLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);
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
                ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單據名稱,單號,客戶簡稱,品號,品名,序號,建立日期,建立人員");
                //    List<RMAApl> objRMAApl = ViewData["RMAApl"] as List<RMAApl>;
                //    foreach (var obj in objRMAApl)
                //    {
                //        sw.WriteLine(obj.RMAAplType + "," + obj.OrderSName + "," + obj.RMAAplNo + "," + obj.CustomerName + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.CreateDate + "," + obj.CreatorName);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RMAApl");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("單號");
                    row.CreateCell(3).SetCellValue("客戶簡稱");
                    row.CreateCell(4).SetCellValue("品號");
                    row.CreateCell(5).SetCellValue("品名");
                    row.CreateCell(6).SetCellValue("序號");


                    int index = 1;

                    List<RMAApl> objRMAApl = ViewData["RMAApl"] as List<RMAApl>;

                    foreach (var obj in objRMAApl)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.RMAAplType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.RMAAplNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(4).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(5).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(6).SetCellValue(obj.SerialNo.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMAApl.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RMAApl.xls");
            }
            else
            {
                if (Request.Form["CustomerId"].Trim() != "")
                {
                    col += ",r.CustomerId";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["CustomerId"];
                }
                if (Request.Form["CreateDate"].Trim() != "")
                {
                    col += ",r.CreateDate";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["CreateDate"];
                }
                ViewData["RMAApl"] = logic.SearchRMAApl(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.customerid = Request.Form["CustomerId"];
                ViewBag.createdate = Request.Form["CreateDate"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

    }
}