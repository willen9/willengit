using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Helpers;
using System.Web.Mvc;

namespace PMAB04.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            RoutineService_HLogic logic = new RoutineService_HLogic();
            string col = "";
            string condition = "";
            string value = "";
            ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            RoutineService_HLogic logic = new RoutineService_HLogic();
            string col = "";
            string condition = "";
            string value = "";

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);

            }
            else if (Request.Form["action"] == "btnExport")
            {
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("單別,單號,客戶名稱,客戶形態,品號,品名,序號,保養月份,保固起日,保固迄日,完工日期");
                    List<RoutineService_H> objRoutineService_H = ViewData["RoutineService_H"] as List<RoutineService_H>;
                    foreach (var obj in objRoutineService_H)
                    {
                        sw.WriteLine(obj.RoutineSerOrderType + "," + obj.RoutineSerOrderNo + ","
                             + obj.CustomerName + "," + obj.CustomerType + ","
                            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + ","
                            + obj.ArrangeMonth + "," + obj.WarrantySDate + "," + obj.WarrantyEDate + "," + obj.ConfirmedDate);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RoutineService.csv");
            }
            else
            {
                if (Request.Form["supportaplorderno"].Trim() != "")
                {
                    col += ",h.RoutineSerOrderNo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["supportaplorderno"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }

                if (Request.Form["ProcessMan"].Trim() != "")
                {
                    col += ",h.RoutineSerMan";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["ProcessMan"];
                }
                ViewData["RoutineService_H"] = logic.SearchRoutineService_H(col, condition, value);
                ViewBag.strRoutineSerOrderNo = Request.Form["supportaplorderno"];
                ViewBag.strCustomerId = Request.Form["customerid"];
                ViewBag.strArrangeMonth = Request.Form["ProcessMan"];
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