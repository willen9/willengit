using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;

namespace BellWhther.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index(FormCollection formCollection,string action)
        {
           
            GroupsLogic groupsLogic = new GroupsLogic();
            UsersLogic usersLogic = new UsersLogic();
            ViewBag.Limit = usersLogic.GetLimitByUid(Session["UserId"].ToString());

            List<Groups> lstGroups = groupsLogic.GetAllGroups();

            SelectList selectList = new SelectList(lstGroups, "GroupId", "GroupId",
                formCollection.Count == 0 ? "==請選擇==" : formCollection["slGroups"]);

            ViewData["slGroupsDisplay"] = selectList;
            ViewData["DisplayData"] = new List<Users>();
            if (action == "search")
            {
                ViewData["DisplayData"] = Search(usersLogic, formCollection);
            }
            if (action == "delete")
            {
                if (usersLogic.DelUsers(formCollection["chk"]))
                {
                    ViewBag.js = "<script>alert('刪除成功');</script>";
                    lstGroups = groupsLogic.GetAllGroups();
                    selectList = new SelectList(lstGroups, "GroupId", "GroupId", "==請選擇==");
                    ViewData["slGroupsDisplay"] = selectList;

                    formCollection["slGroups"] =
                        formCollection["txtUid"] =
                        formCollection["txtName"] =
                            formCollection["txtPwd"] = formCollection["userIdOld"] = formCollection["groupIdOld"] = ""; 
                }
                else
                {
                    ViewBag.js = "<script>alert('刪除失敗');</script>";
                    ViewBag.Fail = "D";
                }
                ViewData["DisplayData"] = Search(usersLogic, formCollection);
            }
            if (action == "save")
            {
                string msg = "";
                Users userOld = new Users();
                if (formCollection["mode"] == "M")
                {
                    userOld.UserId = formCollection["userIdOld"];
                    userOld.GroupID = formCollection["groupIdOld"];
                }
                Users user = new Users();
                user.UserId = formCollection["txtUid"];
                user.Pwd = formCollection["txtPwd"];
                user.UserName = formCollection["txtName"];
                user.GroupID = formCollection["slGroups"];
                user.Creator = Session["UserId"].ToString();
                if (usersLogic.SaveUsers(userOld,user, formCollection["mode"], out msg))
                {
                    ViewBag.js = "<script>alert('保存成功');</script>";
                    lstGroups = groupsLogic.GetAllGroups();
                    selectList = new SelectList(lstGroups, "GroupId", "GroupId", "==請選擇==");
                    ViewData["slGroupsDisplay"] = selectList;

                    formCollection["slGroups"] =
                       formCollection["txtUid"] =
                       formCollection["txtName"] =
                           formCollection["txtPwd"] = formCollection["userIdOld"] = formCollection["groupIdOld"]= "";
                }
                else
                {
                    if (msg.Length > 0)
                    {
                        ViewBag.js = "<script>alert('" + msg + "');</script>";
                    }
                    else
                    {
                        ViewBag.js = "<script>alert('保存失敗');</script>";
                    }
                    ViewBag.Fail = formCollection["mode"];
                }
                ViewData["DisplayData"] = Search(usersLogic, formCollection);
            }
            if (formCollection.Count > 0)
            {
                ViewBag.Uid = formCollection["txtUid"];
                ViewBag.Name = formCollection["txtName"];
                ViewBag.Pwd = formCollection["txtPwd"];
                ViewBag.Chk = formCollection["chk"];
                ViewBag.UserIdOld = formCollection["userIdOld"];
                ViewBag.GroupIdOld = formCollection["groupIdOld"];
            }
            return View();
        }

        private List<Users> Search(UsersLogic usersLogic, FormCollection formCollection)
        {
            Users users = new Users();
            if (formCollection.Count > 0 && formCollection["slGroups"] != "==請選擇==")
            {
                users.GroupID = formCollection["slGroups"];
            }
            users.UserId = formCollection["txtUid"];
            users.UserName = formCollection["txtName"];
            users.Pwd = formCollection["txtPwd"];
            return usersLogic.GetInfo(users);
        }
    }
}