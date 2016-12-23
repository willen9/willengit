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
using System.Web.Mvc;

namespace REPQ06.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = ",Closed";
            string condition = ",=";
            string value = ",Y";
            ViewData["RGA_H"] = logic.AvgRGA_H(col, condition, value);
            ViewBag.Closed = "Y";
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "btnExport")
            {
                ViewData["RGA_H"] = logic.AvgRGA_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RGA_H");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("品號");
                    row.CreateCell(1).SetCellValue("品名");
                    row.CreateCell(2).SetCellValue("數量（臺）");


                    int index = 1;

                    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;

                    foreach (var obj in objRGA_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(1).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(2).SetCellValue(obj.sumQty.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }
                
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RGA_H.xls");
            }
            else
            {
                if (Request.Form["ProductNo"].Trim() != "")
                {
                    col += ",ProductNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["ProductNo"];
                }
                if (Request.Form["RecSDate"].Trim() != "")
                {
                    col += ",OrderDate";
                    condition += ",<";
                    value += "," + Request.Form["RecSDate"];
                }
                if (Request.Form["Closed"].Trim() != "")
                {
                    col += ",Closed";
                    condition += ",=";
                    value += "," + Request.Form["Closed"];
                }
                if (Request.Form["CustomerId"].Trim() != "")
                {
                    col += ",CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["CustomerId"];
                }
                if (Request.Form["RecEDate"].Trim() != "")
                {
                    col += ",OrderDate";
                    condition += ",>";
                    value += "," + Request.Form["RecEDate"];
                }
                ViewData["RGA_H"] = logic.AvgRGA_H(col, condition, value);
                ViewBag.ProductNo = Request.Form["ProductNo"];
                ViewBag.RecSDate = Request.Form["RecSDate"];
                ViewBag.Closed = Request.Form["Closed"];
                ViewBag.CustomerId = Request.Form["CustomerId"];
                ViewBag.RecEDate = Request.Form["RecEDate"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            return View();
        }

        public ActionResult CUR(string ProductNo,string col, string condition,string value)
        {
            col += ",ProductNo";
            condition += ",=";
            value += ","+ ProductNo;
            ViewBag.ProductNo = ProductNo;
            RGA_HLogic logic = new RGA_HLogic();
            ViewData["RGA_H"] = logic.RGA_H(col, condition, value);
            return View("CUR");
        }

        public ActionResult Detail(string RGAType, string RGANo)
        {
            string col = ",RGAType";
            string condition = ",=";
            string value = "," + RGAType;

            col += ",RGANo";
            condition += ",=";
            value += "," + RGANo;

            ViewBag.RGAType = RGAType;
            ViewBag.RGANo = RGANo;

            RepairRecordLogic logic = new RepairRecordLogic();
            ViewData["RepairRecord"] = logic.SearchRepairRecord(col, condition, value);
            return View("Detail");
        }
    }
}