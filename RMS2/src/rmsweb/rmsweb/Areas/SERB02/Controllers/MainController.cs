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

namespace SERB02.Controllers
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
            ViewBag.selCond3 = "<>";
            ViewBag.comfirmed = "V";
            ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
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

            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
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
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;

                string path = Server.MapPath(@"~\ExpotFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                using (FileStream fs = new FileStream(path + @"\" + fileName + ".csv", FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("維修單別,單據名稱,維修單號,客戶簡稱,地址簡稱,品號,品名,序號,狀態,維修人員,資產編號,預計完工日期");
                    List<RGA_H> objRGA_H = ViewData["RGA_H"] as List<RGA_H>;
                    foreach (var obj in objRGA_H)
                    {
                        sw.WriteLine(obj.RGAType + "," + obj.OrderSName + ","
                             + obj.RGANo + "," + obj.CustomerName + "," + obj.AddressSName + ","
                            + obj.ProductNo + "," + obj.ProductName + "," + obj.SerialNo + ","
                            + obj.Confirmed + "," + obj.RepairmanName + "," + obj.AssetNo + "," + obj.PCompletionDate);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                return File(new FileStream(path + @"\" + fileName + ".csv", FileMode.Open), "text/plain", "RGA.csv");
            }
            else
            {
                if (Request.Form["rgano"].Trim() != "")
                {
                    col += ",h.RGANo";
                    condition += "," + Request.Form["selCond1"];
                    value += "," + Request.Form["rgano"];
                }
                if (Request.Form["customerid"].Trim() != "")
                {
                    col += ",h.CustomerId";
                    condition += "," + Request.Form["selCond2"];
                    value += "," + Request.Form["customerid"];
                }
                if (Request.Form["comfirmed"].Trim() != "")
                {
                    col += ",h.Confirmed";
                    condition += "," + Request.Form["selCond3"];
                    value += "," + Request.Form["comfirmed"];
                }
                ViewData["RGA_H"] = logic.SearchRGA_H(col, condition, value);
                ViewBag.strAdvCol = col;
                ViewBag.strAdvCondition = condition;
                ViewBag.strAdvValue = value;
                ViewBag.phoneserno = Request.Form["phoneserno"];
                ViewBag.customerid = Request.Form["customerid"];
                ViewBag.comfirmed = Request.Form["comfirmed"];
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