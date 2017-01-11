using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace BellWhther.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(string uid,string pwd)
        {
            UsersLogic logic = new UsersLogic();
            if (logic.CheckLogIn(uid, pwd))
            {
                Session["UserId"] = uid;
                return RedirectToAction("Index", "Portal");
            }
        
            ViewBag.Uid = uid;
            ViewBag.Pwd = pwd;
            ViewBag.js = "<script>alert('帳號或密碼錯誤，請重新輸入');</script>";
            return View("Index");
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}