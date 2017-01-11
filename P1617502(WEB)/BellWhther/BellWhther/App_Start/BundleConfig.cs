using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BellWhther.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/basescripts").Include(
               "~/Scripts/jquery.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrapValidator.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/knockout.js",
                "~/Scripts/baseScript.js",
                "~/Scripts/jquery.timeago.js",
                "~/Scripts/ajaxfileupload.js",
                "~/Scripts/bootstrap-treeview.js",
                "~/Scripts/angular.min.js",
                "~/bower_components/vue/dist/vue.min.js",
                "~/Scripts/rms.js"
               ));

            bundles.Add(new StyleBundle("~/content/basestyles").Include(
                 "~/Content/bootstrap.css",
                "~/Content/bootstrapValidator.css",
                "~/Content/bootstrap-mvc-theme.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/sitestyles.css"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}