using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPI04.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            SupportItemLogic logic = new SupportItemLogic();
            SupportItem supportItem = new SupportItem();
            ViewData["SupportItem"] = logic.GetSupportItem(supportItem,0);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            SupportItemLogic logic = new SupportItemLogic();
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
                if (Request.Form["supportitemid"].Trim() != "")
                {
                    col = ",SupportItemId";
                    condition = "," + Request.Form["selCond"];
                    value = "," + Request.Form["supportitemid"];
                }
            }
            ViewBag.selCond = Request.Form["selCond"];
            ViewBag.supportitemid = Request.Form["supportitemid"];
            ViewData["SupportItem"] = logic.SearchSupportItem(col, condition, value);
            return View();
        }

        public ActionResult CUR()
        {
            return View("CUR");
        }

        [HttpPost]
        public ActionResult CUR(string s)
        {
            SupportItem supportItem = new SupportItem();
            supportItem.SupportItemId = Request.Form["ItemNo"];
            supportItem.SupportItemName = Request.Form["itemName"];
            supportItem.Remark = Request.Form["remark"];
            supportItem.Company = "";
            supportItem.UserGroup = "";
            supportItem.Creator = "";
            supportItem.Modifier = "";

            SupportItemLogic logic = new SupportItemLogic();
            string msg = "";
            if (Request.Form["action"] == "Edit" || Request.Form["action"] == "EditAgain")
            {
                if (!logic.UpdataSupportItem(supportItem))
                {
                    ViewBag.SupportItemId = Request.Form["ItemNo"];
                    ViewBag.SupportItemName = Request.Form["itemName"];
                    ViewBag.Remark = Request.Form["remark"];
                    ViewBag.Msg = "修改失敗！";
                    ViewBag.Type = "Edit";
                    return View("CUR");
                }
                else
                {
                    if (Request.Form["action"] == "Edit")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("CUR");
                    }
                }

            }
            else if (Request.Form["action"] == "Save" || Request.Form["action"] == "SaveAgain")
            {
                if (!logic.AddSupportItem(supportItem, out msg))
                {
                    ViewBag.SupportItemId = Request.Form["ItemNo"];
                    ViewBag.SupportItemName = Request.Form["itemName"];
                    ViewBag.Remark = Request.Form["remark"];
                    ViewBag.Msg = "新增失敗！" + msg;
                    return View("CUR");
                }
                else
                {
                    if (Request.Form["action"] == "Save")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("CUR");
                    }
                }
            }
            else
            {
                ViewBag.SupportItemId = Request.Form["ItemNo"];
                ViewBag.SupportItemName = Request.Form["itemName"];
                ViewBag.Remark = Request.Form["remark"];
                if (Request.Form["action"] == "EditDetails")
                {
                    ViewBag.Type = "Edit";
                }
                return View("CUR");
            }
        }

        public ActionResult Delete(string SupportItemId)
        {
            SupportItemLogic logic = new SupportItemLogic();

            try
            {
                logic.DelSupportItem(SupportItemId);
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "刪除失敗！" + ex.Message;
            }
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(string SupportItemId)
        {
            SupportItemLogic logic = new SupportItemLogic();
            SupportItem supportItem = logic.GetSupportItemInfo(SupportItemId);
            ViewBag.SupportItemId = supportItem.SupportItemId;
            ViewBag.SupportItemName = supportItem.SupportItemName;
            ViewBag.Remark = supportItem.Remark;
            ViewBag.Type = "Details";
            return View("CUR");
        }

        [HttpPost]
        public JsonResult SupportItemId(string SupportItemId)
        {
            SupportItemLogic logic = new SupportItemLogic();
            return Json(logic.IsSupportItemId(SupportItemId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string SupportItemId)
        {
            SupportItemLogic logic = new SupportItemLogic();
            SupportItem supportItem = logic.GetSupportItemInfo(SupportItemId);
            ViewBag.SupportItemId = supportItem.SupportItemId;
            ViewBag.SupportItemName = supportItem.SupportItemName;
            ViewBag.Remark = supportItem.Remark;
            ViewBag.Type = "Edit";
            return View("CUR");
        }
    }
}