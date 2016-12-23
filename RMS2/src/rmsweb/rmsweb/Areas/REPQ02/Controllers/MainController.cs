using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REPQ02.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.Search = false;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ViewBag.Search = false;
            string Col = "";
            string Condition = "";
            string conditionValue = "";
            if (Request.Form["ProductNo"].ToString().Trim() != "")
            {
                Col = ",ProductNo";
                Condition = "," + Request.Form["col01"];
                conditionValue = "," + Request.Form["ProductNo"].ToString();
                ViewBag.Search = true;
            }
            if (Request.Form["CustomerNo"].ToString().Trim() != "")
            {
                Col = ",CustomerNo";
                Condition = "," + Request.Form["col02"];
                conditionValue = "," + Request.Form["CustomerNo"].ToString();
                ViewBag.Search = true;
            }
            if (ViewBag.Search)
            {
                RGA_HLogic logic = new RGA_HLogic();
                ViewBag.Sum = logic.Search_ContractSum(Col, Condition, conditionValue);
                ViewBag.W = logic.SearchPartW(Col, Condition, conditionValue);
                ViewBag.Wpar = (double.Parse(ViewBag.W) / double.Parse(ViewBag.Sum)) * 100;
                ViewBag.Wpar = ViewBag.Wpar + "%";
                ViewBag.C = logic.SearchPartC(Col, Condition, conditionValue);
                ViewBag.Cpar = (double.Parse(ViewBag.C) / double.Parse(ViewBag.Sum)) * 100;
                ViewBag.Cpar = ViewBag.Wpar + "%";

                ViewBag.strAdvCol = Col;
                ViewBag.strAdvCondition = Condition;
                ViewBag.strAdvValue = conditionValue;

                ViewBag.ProductNo = Request.Form["ProductNo"].ToString();
                ViewBag.CustomerNo = Request.Form["CustomerNo"].ToString();
                ViewBag.col01 = Request.Form["col01"];
                ViewBag.col02 = Request.Form["col02"];
            }
            return View();
        }

        [HttpPost]
        public JsonResult SearchPartLifetime()
        {
            RGA_HLogic logic = new RGA_HLogic();
            List<RGA_H> lst = logic.Search_Contract();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchPartLifetimeCover()
        {
            RGA_HLogic logic = new RGA_HLogic();
            List<RGA_H> lst = logic.Search_ContractCover();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchWarranty_D(string Col, string Condition, string conditionValue)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            List<Warranty_D> lst = logic.SearchWarranty(Col, Condition, conditionValue);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchRGA_H(string Col, string Condition, string conditionValue)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            List<Warranty_D> lst = logic.SearchWarranty(Col, Condition, conditionValue);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchRGA_HCoCover(string Col, string Condition, string conditionValue)
        {
            Warranty_DLogic logic = new Warranty_DLogic();
            List<Warranty_D> lst = logic.SearchWarranty(Col, Condition, conditionValue);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}