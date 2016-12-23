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

namespace CMSI04.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = new ProductCategory();
            ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory,0);
            //TempData["ProductCategory"] = ViewData["ProductCategory"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            ProductCategory productCategory = new ProductCategory();

            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            if (hfc.Count > 0)
            {
                string path = Server.MapPath(@"~\ImportFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string name = hfc[0].FileName;
                //判断文件名字是否包含路径名，如果有则提取文件名
                if (name.LastIndexOf("\\") > -1)
                {
                    name = name.Substring(name.LastIndexOf("\\") + 1);
                }
                hfc[0].SaveAs(path + "\\" + name);

                if (!logic.ImportFile(path + "\\" + name))
                {
                    //return Json("NO-匯入失敗", "text/x-json");
                    return Json("NO-匯入失敗", JsonRequestBehavior.AllowGet);
                }
                return Json("YES-匯入成功", JsonRequestBehavior.AllowGet);
            }

            if(Request.Form["action"]=="Advanced")
            {
                string col = Request.Form["slCol"];
                string condition = Request.Form["slCondition"];
                string value = Request.Form["conditionValue"];
                ViewData["ProductCategory"] = logic.SearchProductCategory(col, condition, value);
                //TempData["ProductCategory"] = ViewData["ProductCategory"];
            }
            else if (Request.Form["action"] == "btnExport")
            {
                //List<ProductCategory> objProductCategory = TempData["ProductCategory"] as List<ProductCategory>;
                //ViewData["ProductCategory"] = TempData["ProductCategory"];

                ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory,0);

                string path = Server.MapPath(@"~\exports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
                //using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                //{
                //    StreamWriter sw = new StreamWriter(fs);
                //    sw.WriteLine("分類方式,類別代號,類別名稱");
                //    List<ProductCategory> objProductCategory = ViewData["ProductCategory"] as List<ProductCategory>;
                //    string cateType="";
                //    foreach (var obj in objProductCategory)
                //    {
                //        if (obj.ProductCategoryType == "1")
                //        {
                //            cateType = "分類一";
                //        }
                //        else if (obj.ProductCategoryType == "2")
                //        {
                //            cateType = "分類二";
                //        }
                //        else if (obj.ProductCategoryType == "3")
                //        {
                //            cateType = "分類三";
                //        }
                //        else
                //        {
                //            cateType = "分類四";
                //        }
                //        sw.WriteLine(cateType+","+obj.ProductTypeId+","+obj.ProductType);
                //    }
                //    sw.Close();
                //    sw.Dispose();
                //}

                using (FileStream fs = new FileStream(path + @"\" + fileName + ".xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("ProductCategory");
                    sheet.SetColumnWidth(0, 20 * 256);
                    sheet.SetColumnWidth(1, 20 * 256);
                    sheet.SetColumnWidth(2, 20 * 256);

                    IRow row = sheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("分類方式");
                    row.CreateCell(1).SetCellValue("類別代號");
                    row.CreateCell(2).SetCellValue("類別名稱");


                    int index = 1;

                    List<ProductCategory> objProductCategory = ViewData["ProductCategory"] as List<ProductCategory>;
                    string cateType = "";
                    foreach (var obj in objProductCategory)
                    {
                        row = sheet.CreateRow(index);
                        if (obj.ProductCategoryType == "1")
                        {
                            cateType = "類別一";
                        }
                        else if (obj.ProductCategoryType == "2")
                        {
                            cateType = "類別二";
                        }
                        else if (obj.ProductCategoryType == "3")
                        {
                            cateType = "類別三";
                        }
                        else
                        {
                            cateType = "類別四";
                        }

                        row.CreateCell(0).SetCellValue(cateType.ToString());
                        row.CreateCell(1).SetCellValue(obj.ProductTypeId.ToString());
                        row.CreateCell(2).SetCellValue(obj.ProductType.ToString());

                        index++;
                    }

                    workbook.Write(fs);
                }
                //return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "ProductCategory.csv");
                return File(new FileStream(path + @"\" + fileName + ".xls", FileMode.Open), "text/plain", "ProductCategory.xls");
            }
            else
            {
                string col = "";
                string condition = "";
                string value = "";
                if (Request.Form["parenttype"].Trim() != "")
                {
                    col += ",ProductCategoryType";
                    condition += ",=";
                    value += "," + Request.Form["parenttype"];
                }
                if (Request.Form["companyid"].Trim() != "")
                {
                    col += ",ProductTypeId";
                    condition += "," + Request.Form["selCond"];
                    value += "," + Request.Form["companyid"];
                }
                //productCategory.ProductCategoryType = Request.Form["parenttype"];
                //productCategory.ProductTypeId = Request.Form["companyid"];
                //ViewData["ProductCategory"] = logic.GetProductCategoryALL(productCategory,0);
                ViewData["ProductCategory"] = logic.SearchProductCategory(col, condition, value);
                ViewBag.SelectCategoryType = Request.Form["parenttype"];
                ViewBag.findcompanyid = Request.Form["companyid"];
                ViewBag.selCond = Request.Form["selCond"];
                //TempData["ProductCategory"] = ViewData["ProductCategory"];
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

        [HttpPost]
        public JsonResult CURID(string ProductCategoryType, string ProductTypeId)
        {
            ProductCategoryLogic logic = new ProductCategoryLogic();
            bool type = logic.IsProductTypeId(ProductCategoryType, ProductTypeId);
            return Json(type, JsonRequestBehavior.AllowGet);
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