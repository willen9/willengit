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

namespace RMAB04.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            //RMA_DLogic logic = new RMA_DLogic();
            //string col = "";
            //string condition = "";
            //string value = "";
            //ViewData["RMA_D"] = logic.SearchRMA(col, condition, value);
            //ViewBag.strAdvCol = col;
            //ViewBag.strAdvCondition = condition;
            //ViewBag.strAdvValue = value;
            //return View();
            //RMAReturn_HLogic logic = new RMAReturn_HLogic();
            //string col = "";
            //string condition = "";
            //string value = "";
            //ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);
            //ViewBag.strAdvCol = col;
            //ViewBag.strAdvCondition = condition;
            //ViewBag.strAdvValue = value;
            RMA_HLogic logic = new RMA_HLogic();
            string col = ",Confirmed";
            string condition = ",=";
            string value = ",Y";
            ViewData["RMA_H"] = logic.SearchRMA_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RMAReturn_HLogic logic = new RMAReturn_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);
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
                ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單據名稱,單號,項次,廠商,品號,品名,序號,送廠日期,送廠人員");
                //    List<RMA_D> objRMA_D = ViewData["RMA_D"] as List<RMA_D>;
                //    foreach (var obj in objRMA_D)
                //    {
                //        sw.WriteLine(obj.RMAType + "," + obj.OrderSName + "," + obj.RMANo + "," + obj.ItemId + "," + obj.VendorName + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.ConfirmDate + "," + obj.ConfirmorName);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}


                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RMA_D");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);
                    sheet.SetColumnWidth(7, 20 * 256);
                    sheet.SetColumnWidth(8, 20 * 256);
                    sheet.SetColumnWidth(9, 20 * 256);

                    IRow row = sheet.CreateRow(0);

                    row.CreateCell(0).SetCellValue("單別");
                    row.CreateCell(1).SetCellValue("單據名稱");
                    row.CreateCell(2).SetCellValue("單號");
                    row.CreateCell(3).SetCellValue("項次");
                    row.CreateCell(4).SetCellValue("廠商");
                    row.CreateCell(5).SetCellValue("品號");
                    row.CreateCell(6).SetCellValue("品名");
                    row.CreateCell(7).SetCellValue("序號");
                    row.CreateCell(8).SetCellValue("送廠日期");
                    row.CreateCell(9).SetCellValue("送廠人員");


                    int index = 1;

                    List<RMA_D> objRMA_D = ViewData["RMA_D"] as List<RMA_D>;

                    foreach (var obj in objRMA_D)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.RMAType.ToString());
                        row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.RMANo.ToString());
                        row.CreateCell(3).SetCellValue(obj.ItemId.ToString());
                        row.CreateCell(4).SetCellValue(obj.VendorName.ToString());
                        row.CreateCell(5).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(6).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(7).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(8).SetCellValue(obj.ConfirmDate.ToString());
                        row.CreateCell(9).SetCellValue(obj.ConfirmorName.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMA_D.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RMA_D.xls");
            }
            else
            {
                if (Request.Form["VendorId"].Trim() != "")
                {
                    col += ",VendorNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["VendorId"];
                }
                if (Request.Form["ConfirmDate"].Trim() != "")
                {
                    col += ",ConfirmDate";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["ConfirmDate"];
                }
                ViewData["RMAReturn_H"] = logic.SearchRMAReturn_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.VendorId = Request.Form["VendorId"];
                ViewBag.ConfirmDate = Request.Form["ConfirmDate"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

    }
}