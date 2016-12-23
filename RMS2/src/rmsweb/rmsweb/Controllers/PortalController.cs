using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rmsweb.Controllers
{
    public class PortalController : BaseController
    {
        [Route("dashboard")]
        public ActionResult Index()
        {
            return View("Default");
        }
    }
}