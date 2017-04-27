using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.Models;

namespace MustardSeedMission.Controllers
{
    public class BaseController : Controller
    {
        public MustardSeedMissionContext db = new MustardSeedMissionContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UID"] == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Login"));
            }

            base.OnActionExecuting(filterContext);            
        }

        [HttpPost]
        public JsonResult GetVisiableWork(string workType)
        {
            string workNo = "";
            //string uid = Session["UID"].ToString();
            //SysPermission sysPermission = db.SysPermission.Where(x => x.UserId == uid).FirstOrDefault();

            WorkLimit limit = new WorkLimit();
            limit.msg = "";
            string uid = Session["UID"].ToString();
            SysPermission sysPermission = db.SysPermission.Where(x => x.UserId == uid).FirstOrDefault();
            if (sysPermission != null)
            {
                string groupId = sysPermission.GroupID;
                IEnumerable<Groups> lstGroupses= db.Groups.Where(x => x.GroupId == groupId&&x.AD==false&&x.De==false&&x.Up==false&&x.Query==false&&x.Ex==false).ToList();
                foreach (Groups g in lstGroupses)
                {
                    workNo += g.SysFunctionsID + ",";
                }
                workNo = workNo.TrimEnd(',');
                limit.WorkVisiable = workNo;

                if (!string.IsNullOrEmpty(workType))
                {
                    Groups groups =db.Groups.Where(x => x.GroupId == groupId && x.SysFunctionsID == workType).FirstOrDefault();
                    if (groups == null)
                    {
                        limit.msg = "2"; //查無作業群組
                    }
                    else
                    {
                        limit.msg = "0";
                        limit.AD = groups.AD;
                        limit.De = groups.De;
                        limit.Up = groups.Up;
                        limit.Query = groups.Query;
                        limit.Ex = groups.Ex;
                    }
                }
            }
            else
            {
                //limit.msg = "登錄人無權限群組";
                limit.msg = "1";
            }
            return Json(limit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLimit(string workType)
        {
            WorkLimit limit = new WorkLimit();
            limit.msg = "";
            string uid = Session["UID"].ToString();
            SysPermission sysPermission = db.SysPermission.Where(x => x.UserId == uid).FirstOrDefault();
            if (sysPermission != null)
            {
                string groupId = sysPermission.GroupID;
                Groups groups = db.Groups.Where(x => x.GroupId == groupId && x.SysFunctionsID == workType).FirstOrDefault();
                if (groups == null)
                {
                    limit.msg = "2";//查無作業群組
                }
                else
                {
                    limit.msg = "0";
                    limit.AD = groups.AD;
                    limit.De = groups.De;
                    limit.Up = groups.Up;
                    limit.Query = groups.Query;
                    limit.Ex = groups.Ex;
                }
            }
            else
            {
                //limit.msg = "登錄人無權限群組";
                limit.msg = "1";
            }
            return Json(limit, JsonRequestBehavior.AllowGet);
        }
    }

    public class WorkLimit
    {
        public string msg { get; set; }
        public bool? AD { get; set; }
        public bool? De { get; set; }
        public bool? Up { get; set; }
        public bool? Query { get; set; }
        public bool? Ex { get; set; }
        public string WorkVisiable { get; set; }
    }
}