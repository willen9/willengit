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
using rmsweb.Controllers;

namespace REPQ05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RGA_HLogic logic = new RGA_HLogic();
            ViewData["RGA_H"] = logic.SearchRGAList_H();
            ViewBag.CustomerId = "";
            ViewBag.CloseCode = "Y";
            return View();
        }


        [HttpPost]
        public ActionResult Index(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            List<RGA_H> list = null;
            if (Request.Form["action"] == "btnExport")
            {
                #region
                if (Request.Form["ProductNo"].Trim() != "")
                {
                    col += ",p.ProductNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["ProductNo"];
                }
                if (Request.Form["CustomerId"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["CustomerId"];
                }
                if (Request.Form["CloseCode"].Trim() != "")
                {
                    col += ",h.Closed";
                    condition += ",=";
                    value += "," + Request.Form["CloseCode"];
                }
                if (Request.Form["SaleSDate"].Trim() != "")
                {
                    col += ",h.SaleDate";
                    condition += ",>=";
                    value += "," + Request.Form["SaleSDate"];
                }
                if (Request.Form["SaleEDate"].Trim() != "")
                {
                    col += ",h.SaleDate";
                    condition += ",<=";
                    value += "," + Request.Form["SaleEDate"];
                }
                if (Request.Form["PurchasesSDate"].Trim() != "")
                {
                    col += ",h.PurchaseDate";
                    condition += ",>=";
                    value += "," + Request.Form["PurchasesSDate"];
                }
                if (Request.Form["PurchasesEDate"].Trim() != "")
                {
                    col += ",h.PurchaseDate";
                    condition += ",<=";
                    value += "," + Request.Form["PurchasesEDate"];
                }
                #endregion

                ViewData["RGA_H"] = list= logic.SearchRGAList_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;
                if (objRGA_H.Count > 0)
                {
                    using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                    {
                        HSSFWorkbook workbook = new HSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("RGA_H");
                        sheet.SetColumnWidth(0, 20 * 256);
                        sheet.SetColumnWidth(1, 20 * 256);
                        sheet.SetColumnWidth(2, 20 * 256);
                        sheet.SetColumnWidth(3, 20 * 256);

                        IRow row = sheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("品號");
                        row.CreateCell(1).SetCellValue("品名");
                        row.CreateCell(2).SetCellValue("數量");
                        row.CreateCell(3).SetCellValue("首次送廠時間-銷(月)");
                        row.CreateCell(4).SetCellValue("首次送廠時間-進(月)");

                        int index = 1;

                        //List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;

                        foreach (var obj in objRGA_H)
                        {
                            row = sheet.CreateRow(index);

                            row.CreateCell(0).SetCellValue(obj.ProductNo.ToString());
                            row.CreateCell(1).SetCellValue(obj.ProductName.ToString());
                            row.CreateCell(2).SetCellValue(obj.PartNume.ToString());
                            row.CreateCell(3).SetCellValue(obj.SaleDate.ToString());
                            row.CreateCell(4).SetCellValue(obj.PurchaseDate.ToString());
                            index++;
                        }

                        workbook.Write(fs);
                    }

                    //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "QuestionList.csv");
                    return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RGA_H.xls");
                }
            }
            if (Request.Form["action"] == "btnSearch")
            //else
            {
                #region
                if (Request.Form["ProductNo"].Trim() != "")
                {
                    col += ",p.ProductNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["ProductNo"];
                }
                if (Request.Form["CustomerId"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["CustomerId"];
                }
                if (Request.Form["CloseCode"].Trim() != "")
                {
                    col += ",h.Closed";
                    condition += ",=";
                    value += "," + Request.Form["CloseCode"];
                }
                if (Request.Form["SaleSDate"].Trim() != "")
                {
                    col += ",h.SaleDate";
                    condition += ",>=";
                    value += "," + Request.Form["SaleSDate"];
                }
                if (Request.Form["SaleEDate"].Trim() != "")
                {
                    col += ",h.SaleDate";
                    condition += ",<=";
                    value += "," + Request.Form["SaleEDate"];
                }
                if (Request.Form["PurchasesSDate"].Trim() != "")
                {
                    col += ",h.PurchaseDate";
                    condition += ",>=";
                    value += "," + Request.Form["PurchasesSDate"];
                }
                if (Request.Form["PurchasesEDate"].Trim() != "")
                {
                    col += ",h.PurchaseDate";
                    condition += ",<=";
                    value += "," + Request.Form["PurchasesEDate"];
                }
                #endregion

                ViewData["RGA_H"] = list = logic.SearchRGAList_H(col, condition, value);

                ViewBag.ProductNo = Request.Form["ProductNo"];
                ViewBag.CustomerId = Request.Form["CustomerId"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.CloseCode = Request.Form["CloseCode"];

                ViewBag.SaleSDate = Request.Form["SaleSDate"];
                ViewBag.SaleEDate = Request.Form["SaleEDate"];
                ViewBag.PurchasesSDate = Request.Form["PurchasesSDate"];
                ViewBag.PurchasesEDate = Request.Form["PurchasesEDate"];

                return View();
            }
            //ViewData["RGA_H"] = list;
            return View();
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            RGA_HLogic logic = new RGA_HLogic();

            if (Request.Form["action"] == "btnExport")
            {
                string str = "";
                if (Request.Form["Closed"] == "已結案")
                {
                    str = "Y";
                }
                if (Request.Form["Closed"] == "未結案")
                {
                    str = "N";
                }

                ViewData["RGA_H"] = logic.SearchRGA(Request.Form["ProductNo"], str, Request.Form["CustomerId"]);
                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

                List<ProductLifetimeRecord> objRGA_H = ViewData["RGA_H"] as List<ProductLifetimeRecord>;
                if (objRGA_H.Count > 0)
                {

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
                        sheet.SetColumnWidth(11, 20 * 256);
                        sheet.SetColumnWidth(12, 20 * 256);

                        IRow row = sheet.CreateRow(0);
                        row.CreateCell(0).SetCellValue("單據狀態");
                        row.CreateCell(1).SetCellValue("維修單別");
                        row.CreateCell(2).SetCellValue("單據名稱");
                        row.CreateCell(3).SetCellValue("維修單號");
                        row.CreateCell(4).SetCellValue("入件日期");
                        row.CreateCell(5).SetCellValue("客戶名稱");
                        row.CreateCell(6).SetCellValue("品號");
                        row.CreateCell(7).SetCellValue("品名");
                        row.CreateCell(8).SetCellValue("序號");
                        row.CreateCell(9).SetCellValue("銷貨日期");
                        row.CreateCell(10).SetCellValue("首次送廠時間-銷(月)");
                        row.CreateCell(11).SetCellValue("進貨日期");
                        row.CreateCell(12).SetCellValue("首次送廠時間-進(月)");

                        int index = 1;

                        foreach (var obj in objRGA_H)
                        {
                            row = sheet.CreateRow(index);

                            row.CreateCell(0).SetCellValue(obj.closed.ToString());
                            row.CreateCell(1).SetCellValue(obj.RGAType.ToString());
                            row.CreateCell(2).SetCellValue(obj.RGAName.ToString());
                            row.CreateCell(3).SetCellValue(obj.RGANo.ToString());
                            row.CreateCell(4).SetCellValue(obj.OrderDate.ToString());
                            row.CreateCell(5).SetCellValue(obj.Confirmor.ToString());
                            row.CreateCell(6).SetCellValue(obj.ProductNo.ToString());
                            row.CreateCell(7).SetCellValue(obj.ProductName.ToString());
                            row.CreateCell(8).SetCellValue(obj.SerialNo.ToString());
                            row.CreateCell(9).SetCellValue(obj.SaleDate.ToString());
                            row.CreateCell(10).SetCellValue(obj.SaleMonth.ToString());
                            row.CreateCell(11).SetCellValue(obj.PurchaseDate.ToString());
                            row.CreateCell(12).SetCellValue(obj.PurchaseMonth.ToString());

                            index++;
                        }

                        workbook.Write(fs);
                    }
                    return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RGA_H.xls");
                }
            }
            return View();
        }

        public ActionResult Details(string ProductNo, string CloseCode, string CustomerId)
        {

            RGA_HLogic logic = new RGA_HLogic();

            ViewData["RGA_H"] = logic.SearchRGA(ProductNo, CloseCode, CustomerId);

            ViewBag.ProductNo = ProductNo;
            ProductLogic logic1 = new ProductLogic();
            Product product = logic1.GetProductInfo(ProductNo);
            ViewBag.ProductName = product.ProductName;

            if (CustomerId != null)
            {
                ViewBag.CustomerId = CustomerId;
                CustomerLogic logic2 = new CustomerLogic();
                Customer customer = logic2.GetCustomerInfo(CustomerId);
                ViewBag.Customer = customer.CustomerName;
            }
            
            if (CloseCode == "")
            {
                ViewBag.Closed = "全部";
            }
            else if (CloseCode == "Y")
            {
                ViewBag.Closed = "已結案";
            }
            else
            {
                ViewBag.Closed = "未結案";
            }
            //List<ProductLifetimeRecord> lst = ViewData["RGA_H"] as List<ProductLifetimeRecord>;
            //return Json(lst, JsonRequestBehavior.AllowGet);
            return View("CUR");
        }
    }
}