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

namespace SERR01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = ",h.Confirmed";
            string condition = ",<>";
            string value = ",V";
            ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
            }
            else if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");


                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RGA_H");
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
                    //sheet.SetColumnWidth(11, 20 * 256);
                    //sheet.SetColumnWidth(12, 20 * 256);
                    //sheet.SetColumnWidth(13, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("單據狀態");
                    row.CreateCell(1).SetCellValue("維修單別");
                    row.CreateCell(2).SetCellValue("單據名稱");
                    row.CreateCell(3).SetCellValue("維修單號");
                    row.CreateCell(4).SetCellValue("客戶編號");
                    row.CreateCell(5).SetCellValue("客戶簡稱");
                    row.CreateCell(6).SetCellValue("裝機地點名稱");
                    row.CreateCell(7).SetCellValue("品號");
                    row.CreateCell(8).SetCellValue("品名");
                    row.CreateCell(9).SetCellValue("序號");
                    row.CreateCell(10).SetCellValue("資產編號");
                    //row.CreateCell(11).SetCellValue("入件說明");
                    //row.CreateCell(12).SetCellValue("檢測結果");
                    //row.CreateCell(13).SetCellValue("配件說明");


                    int index = 1;

                    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;

                    foreach (var obj in objRGA_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.StatusCode.ToString());
                        row.CreateCell(1).SetCellValue(obj.RGAType.ToString());
                        row.CreateCell(2).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(3).SetCellValue(obj.RGANo.ToString());
                        row.CreateCell(4).SetCellValue(obj.CustomerId.ToString());
                        row.CreateCell(5).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(6).SetCellValue(obj.AddressSName.ToString());
                        row.CreateCell(7).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(8).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(9).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(10).SetCellValue(obj.AssetNo.ToString());
                        //row.CreateCell(11).SetCellValue(obj.Remark.ToString());
                        //row.CreateCell(12).SetCellValue(obj.TestResult.ToString());
                        //row.CreateCell(13).SetCellValue(obj.OptionDetail.ToString());


                        index++;
                    }

                    workbook.Write(fs);
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RGA_H.xls");
            }
            else
            {
                
                if (Request.Form["rgatype"].Trim() != "")
                {
                    col += ",h.RGANo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["rgatype"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }
                if (Request.Form["statuscode"].Trim() != "")
                {
                    col += ",h.StatusCode";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["statuscode"];
                }
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
                ViewBag.rgatype = Request.Form["rgatype"];
                ViewBag.customerid = Request.Form["customerid"];
                ViewBag.statuscode = Request.Form["statuscode"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }

    }
}