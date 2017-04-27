using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace REGAL.MVC.UI.Bootstrap
{
    public static class REGALExtensions
    {
        public static ControlHelper REGAL(this HtmlHelper htmlHelper)
        {
            return new ControlHelper(htmlHelper);
        }
    }
}
