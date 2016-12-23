using BusinessLayer.Models;
using BusinessLogic;
using rmsweb.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSI06.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string s)
        {
            ScheduleLogic logic = new ScheduleLogic();
            Schedule schedule = new Schedule();

            schedule.Company = "";
            schedule.UserGroup = "";
            schedule.Creator = "";

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

                if (!logic.ImportFile(path + "\\" + name, schedule))
                {
                    //return Json("NO-匯入失敗", "text/x-json");
                    return Json("NO-匯入失敗", JsonRequestBehavior.AllowGet);
                }
                return Json("YES-匯入成功", JsonRequestBehavior.AllowGet);
            }

            return View();
        }

        [HttpPost]
        public JsonResult GetEventsByMonth(string month)
        {
            ScheduleLogic logic = new ScheduleLogic();
            Schedule schedule = new Schedule();
            List<Schedule> lst = logic.SearchSchedule(month);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelEvents(string id)
        {
            ScheduleLogic logic = new ScheduleLogic();
            logic.DelSchedule(id);
            return View("Index");
        }
    }
}