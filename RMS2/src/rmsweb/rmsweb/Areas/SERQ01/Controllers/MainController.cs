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

namespace SERQ01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            Quotation_DLogic logic = new Quotation_DLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["Quotation_D"] = logic.SearchQuotation_DInfo(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            Quotation_DLogic logic = new Quotation_DLogic();
            Quotation_D quotation_D = new Quotation_D();

            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"]=="Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["Quotation_D"] = logic.SearchQuotation_DInfo(col, condition, value);
            }
            else if (Request.Form["action"] == "btnExport")
            {
                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["Quotation_D"] = logic.SearchQuotation_DInfo(col, condition, value);
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
                    ISheet sheet = workbook.CreateSheet("Quotation_D");
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
                    sheet.SetColumnWidth(13, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("客戶代號");
                    row.CreateCell(1).SetCellValue("客戶簡稱");
                    row.CreateCell(2).SetCellValue("零件品號");
                    row.CreateCell(3).SetCellValue("零件名稱");
                    row.CreateCell(4).SetCellValue("報價日期");
                    row.CreateCell(5).SetCellValue("數量");
                    row.CreateCell(6).SetCellValue("單價");
                    row.CreateCell(7).SetCellValue("優惠價格");
                    row.CreateCell(8).SetCellValue("報價單別");
                    row.CreateCell(9).SetCellValue("報價單號");
                    row.CreateCell(10).SetCellValue("維修單別");
                    row.CreateCell(11).SetCellValue("維修單號");
                    row.CreateCell(12).SetCellValue("品名");
                    row.CreateCell(13).SetCellValue("備註");


                    int index = 1;

                    List<Quotation_D> objQuotation_D = ViewData["Quotation_D"] as List<Quotation_D>;

                    foreach (var obj in objQuotation_D)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.CustomerId.ToString());
                        row.CreateCell(1).SetCellValue(obj.CustomerName.ToString());
                        row.CreateCell(2).SetCellValue(obj.PartNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.PartName.ToString());
                        row.CreateCell(4).SetCellValue(obj.ConfirmedDate.ToString());
                        row.CreateCell(5).SetCellValue(obj.QTY.ToString());
                        row.CreateCell(6).SetCellValue(obj.UnitPrice.ToString());
                        row.CreateCell(7).SetCellValue(obj.PreferentialPrice.ToString());
                        row.CreateCell(8).SetCellValue(obj.QuotationType.ToString());
                        row.CreateCell(9).SetCellValue(obj.QuotationNo.ToString());
                        row.CreateCell(10).SetCellValue(obj.SourceOrderType.ToString());
                        row.CreateCell(11).SetCellValue(obj.SourceOrderNo.ToString());
                        row.CreateCell(12).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(13).SetCellValue(obj.Remark.ToString());


                        index++;
                    }

                    workbook.Write(fs);
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "Quotation_D.xls");
            }
            else
            {
                if (Request.Form["ljph"].Trim() != "")
                {
                    col += ",d.PartNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["ljph"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }
                if (Request.Form["QuotationDate"].Trim() != "")
                {
                    col += ",h.ConfirmedDate";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["QuotationDate"];
                }
                ViewData["Quotation_D"] = logic.SearchQuotation_DInfo(col, condition, value);
                ViewBag.ljph = Request.Form["ljph"];
                ViewBag.companyid = Request.Form["companyid"];
                ViewBag.QuotationDate = Request.Form["QuotationDate"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }

        public ActionResult CUR()
        {
            return View("CUR");
        }

        [HttpPost]
        public JsonResult Add()
        {
            ProductCategory productCategory = new ProductCategory();
            productCategory.ProductCategoryType = Request.Form["parenttype"];
            productCategory.ProductTypeId = Request.Form["typeid"];
            productCategory.ProductType = Request.Form["typename"];

            ProductCategoryLogic logic = new ProductCategoryLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.AddProductCategory(productCategory, out infomsg))
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
            ProductCategory productCategory = new ProductCategory();
            productCategory.ProductCategoryType = Request.Form["parenttype"];
            productCategory.ProductTypeId = Request.Form["typeid"];
            productCategory.ProductType = Request.Form["typename"];

            ProductCategoryLogic logic = new ProductCategoryLogic();
            string msg = "";

            string infomsg = "";
            if (!logic.UpdateProductCategory(productCategory,out infomsg))
            {
                msg = "NO-更新失敗;" + infomsg;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();

            try
            {
                logic.DeleteProductCategory(ProductCategoryType, ProductTypeId);
            }catch(Exception ex)
            {
                ViewBag.Msg = "刪除失敗！" + ex.Message;
            }
            //ProductCategory productCategory = new ProductCategory();
            //ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory);

            //return View("Index");
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
            ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
            ViewBag.ProductTypeId = productCategory.ProductTypeId;
            ViewBag.ProductType = productCategory.ProductType;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
            ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
            ViewBag.ProductTypeId = productCategory.ProductTypeId;
            ViewBag.ProductType = productCategory.ProductType;
            ViewBag.Type = "Edit";
            return View("CUR");
        }

        public ActionResult Copy(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
            ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
            ViewBag.ProductTypeId = productCategory.ProductTypeId;
            ViewBag.ProductType = productCategory.ProductType;
            return View("CUR");
        }
    }
}