using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace REGAL.MVC.UI.Bootstrap
{
    public class ControlHelper
    {
        private HtmlHelper htmlHelper;

        public ControlHelper(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        public MvcHtmlString FileUpload(string name, string description, string cssClass)
        {
            return new FileUploadControl(name, description, cssClass).RenderHtml();
        }

        public MvcHtmlString MessageBox(string name, string bodyText, MessageBoxButtons buttons, string cssClass)
        {
            return new MessageBoxControl(name, bodyText, buttons, cssClass).RenderHtml();
        }
    }
}
