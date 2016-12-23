using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rmsweb.Controllers
{
    public class ModuleController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Launch(string moduleId)
        {
            return View(moduleId);
        }

        public ActionResult CRUOperation(string moduleId)
        {
            return View(moduleId + "-CRU");
        }
    }
}