using System.Web.Mvc;

namespace rmsweb
{
    public class SERI08AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI08";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI08_default",
                "SERI08",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                "SERI08_Add",
                "SERI08/Add",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
               "SERI08_SearchRGA_H",
               "SERI08/SearchRGA_H",
               new { action = "SearchRGA_H", controller = "Main" },
               namespaces: new string[] { "SERI08.Controllers" }
           );

            context.MapRoute(
                "SERI08_SearchOrderType",
                "SERI08/Add/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                "SERI08_GetOrderTypeName",
                "SERI08/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                "SERI08_GetOrderTypeNo",
                "SERI08/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                "SERI08_SearchSupportApl_H",
                "SERI08/SearchSupportApl_H",
                new { action = "SearchSupportApl_H", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                "SERI08_GetSupportApl_ProductD",
                "SERI08/GetSupportApl_ProductD",
                new { action = "GetSupportApl_ProductD", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                 "SERI08_Edit",
                 "SERI08/Edit",
                 new { action = "Edit", controller = "Main" },
                 namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                 "SERI08_Delete",
                 "SERI08/Delete",
                 new { action = "Delete", controller = "Main" },
                 namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                 "SERI08_Details",
                 "SERI08/Details",
                 new { action = "Details", controller = "Main" },
                 namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                "SERI08_SearchPicking_ProductSerial",
                "SERI08/SearchPicking_ProductSerial",
                new { action = "SearchPicking_ProductSerial", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
                "SERI08_AddSerial",
                "SERI08/AddSerial",
                new { action = "AddSerial", controller = "Main" },
                namespaces: new string[] { "SERI08.Controllers" }
            );

            context.MapRoute(
               "SERI08_Print",
               "SERI08/Print",
               new { action = "Print", controller = "Main" },
               namespaces: new string[] { "SERI08.Controllers" }
           );
        }
    }
}