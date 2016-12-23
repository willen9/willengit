using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace rmsweb.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Signin()
        {
            Session["UserId"] = Request.Form["username"].ToString();
            Session["Company"] = ConfigurationManager.ConnectionStrings["company"].ToString();
            Session["UserGroup"] = "SZ";

            return RedirectToAction("Index", "Portal");
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}