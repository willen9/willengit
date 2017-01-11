using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace BellWhther.Controllers
{
    public class PortalController : Controller
    {
        // GET: Portal
        public ActionResult Index()
        {
            UsersLogic usersLogic = new UsersLogic();
            ViewBag.Limit=usersLogic.GetLimitByUid(Session["UserId"].ToString());
            return View();
        }
    }
}