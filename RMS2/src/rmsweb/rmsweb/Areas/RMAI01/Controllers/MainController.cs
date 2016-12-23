using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMAI01.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            ViewData["OrderCategory"] = logic.GetOrderCategory(",OrderCategory", ",like%", ",C", 0);
            return View();
        }

        public ActionResult CUR()
        {
            ViewBag.Codestyle = "yyyymmdd";
            ViewBag.DaysAfterCreate = "0";
            return View("CUR");
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            string col = "";
            string condition = "";
            string value = "";
            if (Request.Form["action"] == "Advanced")
            {
                col = Request.Form["slCol"];
                condition = Request.Form["slCondition"];
                value = Request.Form["conditionValue"];
            }
            else
            {
                if (Request.Form["ordertype"].Trim() != "")
                {
                    col = ",OrderType";
                    condition = "," + Request.Form["selCond"];
                    value = "," + Request.Form["ordertype"];
                }
                ViewBag.selCond = Request.Form["selCond"];
                ViewBag.ordertype = Request.Form["ordertype"];

            }
            col += ",OrderCategory";
            condition += ",like%" ;
            value += ",C";

            ViewData["OrderCategory"] = logic.SearchOrderCategory(col, condition, value);
            return View();
        }

        [HttpPost]
        public JsonResult Add()
        {
            OrderCategory orderCategory = new OrderCategory();
            orderCategory.OrderType = Request.Form["ordertype"];
            orderCategory.OrderSName = Request.Form["ordersname"];
            orderCategory.OrderFName = Request.Form["orderfname"];
            orderCategory.OrderCategoryID = Request.Form["ordercategoryId"];
            orderCategory.CodeMode = Request.Form["codemode"];
            orderCategory.SerialNrCodeLength = Request.Form["serialnrcodelength"] == "" ? "0" : Request.Form["serialnrcodelength"];
            orderCategory.AutoConfirm = Request.Form["autoconfirm"] == null ? "N" : "Y";
            orderCategory.CheckExOrder = Request.Form["checkexdoder"] == null ? "N" : "Y" ;
            orderCategory.Remark = Request.Form["remark"];
            orderCategory.DaysAfterCreate = Request.Form["DaysAfterCreate"]==""?"0":Request.Form["DaysAfterCreate"];

            OrderCategoryLogic logic = new OrderCategoryLogic();
            string msgInfo = "";
            string msg = "";
            if (!logic.AddOrderCategory(orderCategory, out msgInfo))
            {
                msg = "NO-更新失敗!" + msgInfo;
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update()
        {
            OrderCategory orderCategory = new OrderCategory();
            orderCategory.OrderType = Request.Form["ordertype"];
            orderCategory.OrderSName = Request.Form["ordersname"];
            orderCategory.OrderFName = Request.Form["orderfname"];
            orderCategory.OrderCategoryID = Request.Form["ordercategoryId"];
            orderCategory.CodeMode = Request.Form["codemode"];
            orderCategory.SerialNrCodeLength = Request.Form["serialnrcodelength"] == "" ? "0" : Request.Form["serialnrcodelength"];
            orderCategory.AutoConfirm = Request.Form["autoconfirm"] == null ? "N" : "Y";
            orderCategory.CheckExOrder = Request.Form["checkexdoder"] == null ? "N" : "Y";
            orderCategory.Remark = Request.Form["remark"];
            orderCategory.DaysAfterCreate = Request.Form["DaysAfterCreate"] == "" ? "0" : Request.Form["DaysAfterCreate"];

            OrderCategoryLogic logic = new OrderCategoryLogic();
            string msg = "";
            if (!logic.UpdataOrderCategory(orderCategory))
            {
                msg = "NO-更新失敗!";
            }
            else
            {
                msg = "OK-保存成功";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Delete(string OrderType)
        //{
        //    OrderCategoryLogic logic = new OrderCategoryLogic();

        //    try
        //    {
        //        logic.DelOrderCategory(OrderType);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Msg = "刪除失敗！" + ex.Message;
        //    }
        //    return RedirectToAction("Index", "Main");
        //}

        public ActionResult Details(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            OrderCategory orderCategory = logic.GetOrderCategoryInfo(OrderType);
            ViewBag.OrderType = orderCategory.OrderType;
            ViewBag.OrderSName = orderCategory.OrderSName;
            ViewBag.OrderFName = orderCategory.OrderFName;
            ViewBag.OrderCategoryID = orderCategory.OrderCategoryID;
            ViewBag.CodeMode = orderCategory.CodeMode;
            ViewBag.SerialNrCodeLength = orderCategory.SerialNrCodeLength;
            ViewBag.AutoConfirm = orderCategory.AutoConfirm;
            ViewBag.CheckExOrder = orderCategory.CheckExOrder;
            ViewBag.Remark = orderCategory.Remark;
            ViewBag.DaysAfterCreate = orderCategory.DaysAfterCreate;
            string codestyle = "";
            if (orderCategory.CodeMode == "1")
            {
                codestyle = "yyyymmdd";
            }
            if (orderCategory.CodeMode == "2")
            {
                codestyle = "yyyymm";
            }
            ViewBag.Codestyle = codestyle + "999999999999".Substring(0, int.Parse(orderCategory.SerialNrCodeLength));
            ViewBag.Type = "Details";
            return View("CUR");
        }

        public ActionResult Edit(string OrderType,string cur)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            OrderCategory orderCategory = logic.GetOrderCategoryInfo(OrderType);
            ViewBag.OrderType = orderCategory.OrderType;
            ViewBag.OrderSName = orderCategory.OrderSName;
            ViewBag.OrderFName = orderCategory.OrderFName;
            ViewBag.OrderCategoryID = orderCategory.OrderCategoryID;
            ViewBag.CodeMode = orderCategory.CodeMode;
            ViewBag.SerialNrCodeLength = orderCategory.SerialNrCodeLength;
            ViewBag.AutoConfirm = orderCategory.AutoConfirm;
            ViewBag.CheckExOrder = orderCategory.CheckExOrder;
            ViewBag.Remark = orderCategory.Remark;
            ViewBag.DaysAfterCreate = orderCategory.DaysAfterCreate;
            string codestyle = "";
            if (orderCategory.CodeMode == "1")
            {
                codestyle = "yyyymmdd";
            }
            if (orderCategory.CodeMode == "2")
            {
                codestyle = "yyyymm";
            }
            ViewBag.Codestyle = codestyle + "999999999999".Substring(0, int.Parse(orderCategory.SerialNrCodeLength));
            ViewBag.Type = "Edit";
            ViewBag.cur = cur;
            return View("CUR");
        }

        public ActionResult Show(string OrderType)
        {
            OrderCategoryLogic logic = new OrderCategoryLogic();
            OrderCategory orderCategory = logic.GetOrderCategoryInfo(OrderType);
            ViewBag.OrderType = orderCategory.OrderType;
            ViewBag.OrderSName = orderCategory.OrderSName;
            ViewBag.OrderFName = orderCategory.OrderFName;
            ViewBag.OrderCategoryID = orderCategory.OrderCategoryID;
            ViewBag.CodeMode = orderCategory.CodeMode;
            ViewBag.SerialNrCodeLength = orderCategory.SerialNrCodeLength;
            ViewBag.AutoConfirm = orderCategory.AutoConfirm;
            ViewBag.CheckExOrder = orderCategory.CheckExOrder;
            ViewBag.Remark = orderCategory.Remark;
            ViewBag.DaysAfterCreate = orderCategory.DaysAfterCreate;
            string codestyle = "";
            if (orderCategory.CodeMode == "1")
            {
                codestyle = "yyyymmdd";
            }
            if (orderCategory.CodeMode == "2")
            {
                codestyle = "yyyymm";
            }
            ViewBag.Codestyle = codestyle + "999999999999".Substring(0, int.Parse(orderCategory.SerialNrCodeLength));
            return View("CUR");
        }
    }
}