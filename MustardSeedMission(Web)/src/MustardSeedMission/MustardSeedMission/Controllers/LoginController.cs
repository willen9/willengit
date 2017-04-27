using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MustardSeedMission.Models;

namespace MustardSeedMission.Controllers
{
    public class LoginController : Controller
    {
        public MustardSeedMissionContext db = new MustardSeedMissionContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string uid = fc["txtUid"];
            string pwd = fc["txtPwd"];
            var employee = db.Users.Where(x => x.UserId == uid && x.Pwd == pwd).SingleOrDefault();
            if (employee == null)
            {
                ViewBag.js = "<script type='text/javascript'>alert('帳號或密碼錯誤');</script>";
                ViewBag.Uid = uid;
                ViewBag.Pwd = pwd;
                return View();
            }
            Session["UID"] = uid;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}