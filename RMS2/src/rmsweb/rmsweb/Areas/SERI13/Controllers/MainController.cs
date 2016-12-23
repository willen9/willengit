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

namespace SERI13.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RepairRecordLogic logic = new RepairRecordLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RepairRecord"] = logic.SearchRepairRecord(col, condition, value);
            ViewBag.strAdvCol = col;
            ViewBag.strAdvCondition = condition;
            ViewBag.strAdvValue = value;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RepairRecordLogic logic = new RepairRecordLogic();
            RepairRecord repairRecord = new RepairRecord();

            string col = "";
            string condition = "";
            string value = "";

            //HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            //if (hfc.Count > 0)
            //{
            //    string path = Server.MapPath(@"~\ImportFile");
            //    if (!Directory.Exists(path))
            //    {
            //        Directory.CreateDirectory(path);
            //    }
            //    string name = hfc[0].FileName;
            //    //判断文件名字是否包含路径名，如果有则提取文件名
            //    if (name.LastIndexOf("\\") > -1)
            //    {
            //        name = name.Substring(name.LastIndexOf("\\") + 1);
            //    }
            //    hfc[0].SaveAs(path + "\\" + name);

            //    if (!logic.ImportFile(path + "\\" + name))
            //    {
            //        //return Json("NO-匯入失敗", "text/x-json");
            //        return Json("NO-匯入失敗", JsonRequestBehavior.AllowGet);
            //    }
            //    return Json("YES-匯入成功", JsonRequestBehavior.AllowGet);
            //}

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RepairRecord"] = logic.SearchRepairRecord(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
            }
            else if (Request.Form["action"] == "btnExport")
            {
                //List<ProductCategory> objProductCategory = TempData["ProductCategory"] as List<ProductCategory>;
                //ViewData["ProductCategory"] = TempData["ProductCategory"];

                //ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory, 0);

                if (!String.IsNullOrEmpty(Request.Form["strAdvCol"]))
                {
                    col = Request.Form["strAdvCol"];
                    condition = Request.Form["strAdvCondition"];
                    value = Request.Form["strAdvValue"];
                }
                ViewData["RepairRecord"] = logic.SearchRepairRecord(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("RepairRecord");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);
                    sheet.SetColumnWidth(3, 20 * 256);
                    sheet.SetColumnWidth(4, 20 * 256);
                    sheet.SetColumnWidth(5, 20 * 256);
                    sheet.SetColumnWidth(6, 20 * 256);
                    sheet.SetColumnWidth(7, 20 * 256);
                    sheet.SetColumnWidth(8, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("品號");
                    row.CreateCell(1).SetCellValue("序號");
                    row.CreateCell(2).SetCellValue("維修單別");
                    row.CreateCell(3).SetCellValue("維修單號");
                    row.CreateCell(4).SetCellValue("異動日期");
                    row.CreateCell(5).SetCellValue("異動單別");
                    row.CreateCell(6).SetCellValue("異動單號");
                    row.CreateCell(7).SetCellValue("異動別");
                    row.CreateCell(8).SetCellValue("備註");


                    int index = 1;

                    List<RepairRecord> objRepairRecord = ViewData["RepairRecord"] as List<RepairRecord>;

                    foreach (var obj in objRepairRecord)
                    {
                        row = sheet.CreateRow(index);

                        row.CreateCell(0).SetCellValue(obj.ProductNo.ToString());
                        row.CreateCell(1).SetCellValue(obj.SerialNo.ToString());
                        row.CreateCell(2).SetCellValue(obj.RGAType.ToString());
                        row.CreateCell(3).SetCellValue(obj.RGANo.ToString());
                        row.CreateCell(4).SetCellValue(obj.ChangeDate.ToString());
                        row.CreateCell(5).SetCellValue(obj.ChangeOrderType.ToString());
                        row.CreateCell(6).SetCellValue(obj.ChangeOrderNo.ToString());
                        row.CreateCell(7).SetCellValue(obj.ChangeMode.ToString());
                        row.CreateCell(8).SetCellValue(obj.Remark.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }

                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "RepairRecord.xls");
            }
            else
            {
                if (Request.Form["RGANo"].Trim() != "")
                {
                    col += ",RGANo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["RGANo"];
                }
                if (Request.Form["ProductNo"].Trim() != "")
                {
                    col += ",ProductNo";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["ProductNo"];
                }
                if (Request.Form["SerielNo"].Trim() != "")
                {
                    col += ",SerialNo";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["SerielNo"];
                }
                ViewData["RepairRecord"] = logic.SearchRepairRecord(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.RGANo = Request.Form["RGANo"];
                ViewBag.ProductNo = Request.Form["ProductNo"];
                ViewBag.SerielNo = Request.Form["SerielNo"];
                ViewBag.selCond1 = Request.Form["selCond1"];
                ViewBag.selCond2 = Request.Form["selCond2"];
                ViewBag.selCond3 = Request.Form["selCond3"];
            }
            return View();
        }

        //public ActionResult CUR()
        //{
        //    return View("CUR");
        //}

        //[HttpPost]
        //public JsonResult Add()
        //{
        //    ProductCategory productCategory = new ProductCategory();
        //    productCategory.ProductCategoryType = Request.Form["parenttype"];
        //    productCategory.ProductTypeId = Request.Form["typeid"];
        //    productCategory.ProductType = Request.Form["typename"];

        //    ProductCategoryLogic logic = new ProductCategoryLogic();
        //    string msg = "";

        //    string infomsg = "";
        //    if (!logic.AddProductCategory(productCategory, out infomsg))
        //    {
        //        msg = "NO-新增失敗;" + infomsg;
        //    }
        //    else
        //    {
        //        msg = "OK-保存成功";
        //    }
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult Exit()
        //{
        //    ProductCategory productCategory = new ProductCategory();
        //    productCategory.ProductCategoryType = Request.Form["parenttype"];
        //    productCategory.ProductTypeId = Request.Form["typeid"];
        //    productCategory.ProductType = Request.Form["typename"];

        //    ProductCategoryLogic logic = new ProductCategoryLogic();
        //    string msg = "";

        //    string infomsg = "";
        //    if (!logic.UpdateProductCategory(productCategory,out infomsg))
        //    {
        //        msg = "NO-更新失敗;" + infomsg;
        //    }
        //    else
        //    {
        //        msg = "OK-保存成功";
        //    }
        //    return Json(msg, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Delete(string ProductCategoryType, string ProductTypeId)
        //{
        //    ProductCategoryLogic logic = new ProductCategoryLogic();

        //    try
        //    {
        //        logic.DeleteProductCategory(ProductCategoryType, ProductTypeId);
        //    }catch(Exception ex)
        //    {
        //        ViewBag.Msg = "刪除失敗！" + ex.Message;
        //    }
        //    //ProductCategory productCategory = new ProductCategory();
        //    //ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory);

        //    //return View("Index");
        //    return RedirectToAction("Index", "Main");
        //}

        //public ActionResult Details(string ProductCategoryType, string ProductTypeId)
        //{
        //    ProductCategoryLogic logic = new ProductCategoryLogic();
        //    ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
        //    ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
        //    ViewBag.ProductTypeId = productCategory.ProductTypeId;
        //    ViewBag.ProductType = productCategory.ProductType;
        //    ViewBag.Type = "Details";
        //    return View("CUR");
        //}

        //public ActionResult Edit(string ProductCategoryType, string ProductTypeId)
        //{
        //    ProductCategoryLogic logic = new ProductCategoryLogic();
        //    ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
        //    ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
        //    ViewBag.ProductTypeId = productCategory.ProductTypeId;
        //    ViewBag.ProductType = productCategory.ProductType;
        //    ViewBag.Type = "Edit";
        //    return View("CUR");
        //}

        //public ActionResult Copy(string ProductCategoryType, string ProductTypeId)
        //{
        //    ProductCategoryLogic logic = new ProductCategoryLogic();
        //    ProductCategory productCategory = logic.GetProductCategory(ProductCategoryType, ProductTypeId);
        //    ViewBag.ProductCategoryType = productCategory.ProductCategoryType;
        //    ViewBag.ProductTypeId = productCategory.ProductTypeId;
        //    ViewBag.ProductType = productCategory.ProductType;
        //    return View("CUR");
        //}
    }
}