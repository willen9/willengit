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

namespace RMAQ01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RFQ_DLogic logic = new RFQ_DLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RFQ_D"] = logic.SearchRFQ_DInfo(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RFQ_DLogic logic = new RFQ_DLogic();
            RFQ_D rFQ_D = new RFQ_D();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RFQ_D"] = logic.SearchRFQ_DInfo(col, condition, value);
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

                ViewData["RFQ_D"] = logic.SearchRFQ_DInfo(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單據名稱,單號,廠商,品名,序號,狀態,回覆狀態,建立人員");
                //    List<RFQ_H> objRFQ_H = ViewData["RFQ_H"] as List<RFQ_H>;

                //    foreach (var obj in objRFQ_H)
                //    {
                //        sw.WriteLine(obj.RFQType + "," + obj.OrderSName + "," + obj.RFQNo + "," + obj.VendorName + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.Confirmed + "," + obj.CreatorName);
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
                    sheet.SetColumnWidth(7, 20 * 256);
                    sheet.SetColumnWidth(8, 20 * 256);
                    sheet.SetColumnWidth(9, 20 * 256);
                    sheet.SetColumnWidth(10, 20 * 256);
                    sheet.SetColumnWidth(11, 20 * 256);
                    sheet.SetColumnWidth(12, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("客戶代號");
                    row.CreateCell(1).SetCellValue("客戶簡稱");
                    row.CreateCell(2).SetCellValue("零件品號");
                    row.CreateCell(3).SetCellValue("零件名稱");
                    row.CreateCell(4).SetCellValue("報價日期");
                    row.CreateCell(5).SetCellValue("數量");
                    row.CreateCell(6).SetCellValue("單價");
                    row.CreateCell(7).SetCellValue("報價單別");
                    row.CreateCell(8).SetCellValue("報價單號");
                    row.CreateCell(9).SetCellValue("維修單別");
                    row.CreateCell(10).SetCellValue("維修單號");
                    row.CreateCell(11).SetCellValue("品號");
                    row.CreateCell(12).SetCellValue("備註");


                    int index = 1;
              
                    List<RFQ_D> objRFQ_D = ViewData["RFQ_D"] as List<RFQ_D>;

                    foreach (var obj in objRFQ_D)
                    {
                        row = sheet.CreateRow(index);

                        //row.CreateCell(0).SetCellValue(obj.RMAAplType.ToString());
                        //row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.PartNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.PartName.ToString());
                        //row.CreateCell(4).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(5).SetCellValue(obj.QTY.ToString());
                        row.CreateCell(6).SetCellValue(obj.UnitPrice.ToString());
                        row.CreateCell(7).SetCellValue(obj.RFQType.ToString());
                        row.CreateCell(8).SetCellValue(obj.RFQNo.ToString());
                        //row.CreateCell(9).SetCellValue(obj.RMAAplNo.ToString());
                        //row.CreateCell(10).SetCellValue(obj.CustomerName.ToString());
                        //row.CreateCell(11).SetCellValue(obj.ProductNo.ToString());
                        //row.CreateCell(12).SetCellValue(obj.ProductName.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "ProductCategory.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RFQ_D.xls");
            }
            else
            {
                if (Request.Form["rfqtype"].Trim() != "")
                {
                    col += ",RFQType";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["rfqtype"];
                }
                if (Request.Form["rfqno"].Trim() != "")
                {
                    col += ",RFQNo";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["rfqno"];
                }
                ViewData["RFQ_D"] = logic.SearchRFQ_DInfo(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.rfqtype = Request.Form["rfqtype"];
                ViewBag.rfqno = Request.Form["rfqno"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
            }
            return View();
        }

    }
}