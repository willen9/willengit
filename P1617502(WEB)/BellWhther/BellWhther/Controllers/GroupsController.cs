using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace BellWhther.Controllers
{
    public class GroupsController : Controller
    {
        // GET: Groups
        public ActionResult Index(FormCollection formCollection,string action)
        {
            UsersLogic usersLogic = new UsersLogic();
            ViewBag.Limit = usersLogic.GetLimitByUid(Session["UserId"].ToString());

            GroupsLogic groupsLogic = new GroupsLogic();

            List<Groups> lstGroups = groupsLogic.GetAllGroups();

            SelectList selectList = new SelectList(lstGroups, "GroupId", "GroupId",
                formCollection.Count == 0 ? "==請選擇==" : formCollection["slGroups"]);

            ViewData["slGroupsDisplay"] = selectList;
            ViewData["DisplayData"] = groupsLogic.GetAllFunctions();
            if (action == "delete")
            {
                string msg = "";
                if (groupsLogic.DelGroups(formCollection["slGroups"],out msg))
                {
                    ViewBag.js = "<script>alert('刪除成功');</script>";
                    lstGroups = groupsLogic.GetAllGroups();
                    selectList = new SelectList(lstGroups, "GroupId", "GroupId", "==請選擇==");
                    ViewData["slGroupsDisplay"] = selectList;

                    formCollection["slGroups"] =formCollection["txtGroup"] ="";
                }
                else
                {
                    if (string.IsNullOrEmpty(msg))
                    {
                        ViewBag.js = "<script>alert('刪除失敗');</script>";
                    }
                    else
                    {
                        ViewBag.js = "<script>alert('"+msg+"');</script>";
                    }
                    ViewBag.Fail = "D";
                }
            }
            if (action == "save")
            {
                string groupId = formCollection["mode"] == "M" ? formCollection["slGroups"] : formCollection["txtGroup"];
                if (groupsLogic.SaveGroups(formCollection["chk"], groupId))
                {
                    ViewBag.js = "<script>alert('保存成功');</script>";
                    lstGroups = groupsLogic.GetAllGroups();
                    selectList = new SelectList(lstGroups, "GroupId", "GroupId", "==請選擇==");
                    ViewData["slGroupsDisplay"] = selectList;

                    formCollection["slGroups"] =formCollection["txtGroup"] = "";
                }
            }
            if (formCollection.Count > 0)
            {
                ViewBag.Group = formCollection["txtGroup"];
                ViewBag.Chk = formCollection["chk"];
            }
            return View();
        }

        [HttpPost]
        public JsonResult SlChange(string GroupId)
        {
            GroupsLogic groupsLogic = new GroupsLogic();
            string strReturn=groupsLogic.GetFunctionsByGroupId(GroupId);
            return Json(strReturn, JsonRequestBehavior.AllowGet);
        }
    }
}