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

namespace RMAB05.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RFQ_HLogic logic = new RFQ_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RFQ_HLogic logic = new RFQ_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                //col += ",r.Closed,r.Confirmed";
                //condition += ",=,=";
                //value += ",N,Y";
                ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);
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
                ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("單別,單號,廠商名稱,品號,品名,序號,接收日期,建立人員");
                //    List<RMAApl> objRMAApl = ViewData["RMAApl"] as List<RMAApl>;
                //    foreach (var obj in objRMAApl)
                //    {
                //        sw.WriteLine(obj.RMAAplType + "," + obj.RMAAplNo + "," + obj.CustomerName + ","
                //            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + "," + obj.ConfirmedDate + "," + obj.CreatorName);
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
                    row.CreateCell(3).SetCellValue("廠商");
                    row.CreateCell(4).SetCellValue("品名");
                    row.CreateCell(5).SetCellValue("序號");
                    row.CreateCell(6).SetCellValue("状态");

                    int index = 1;

                    List<RFQ_H> objRFQ_H = ViewData["RFQ_H"] as List<RFQ_H>;

                    foreach (var obj in objRFQ_H)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.RFQType.ToString());
                        //row.CreateCell(1).SetCellValue(obj.OrderSName.ToString());
                        row.CreateCell(2).SetCellValue(obj.RFQNo.ToString());
                        row.CreateCell(3).SetCellValue(obj.VendorId.ToString());
                        row.CreateCell(4).SetCellValue(obj.ProductName.ToString());
                        row.CreateCell(5).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(6).SetCellValue(obj.StatusCode.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RMAApl.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RMAApl.xls");
            }
            else
            {
                if (Request.Form["RFQType"].Trim() != "")
                {
                    col += ",RFQType";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["RFQType"];
                }
                if (Request.Form["RFQNo"].Trim() != "")
                {
                    col += ",RFQNo";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["RFQNo"];
                }
                ViewData["RFQ_H"] = logic.SearchRFQ_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.RFQType = Request.Form["RFQType"];
                ViewBag.RFQNo = Request.Form["RFQNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
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