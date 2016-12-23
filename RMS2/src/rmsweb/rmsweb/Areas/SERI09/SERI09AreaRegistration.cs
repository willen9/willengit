using System.Web.Mvc;

namespace rmsweb
{
    public class SERI09AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI09";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI09_default",
                "SERI09",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_Add",
                "SERI09/Add",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_SearchOrderType",
                "SERI09/Add/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_GetOrderTypeName",
                "SERI09/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_SearchSupportAplAsign",
                "SERI09/SearchSupportAplAsign",
                new { action = "SearchSupportAplAsign", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_GetOrderTypeNo",
                "SERI09/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_SearchSupportApl_H",
                "SERI09/SearchSupportApl_H",
                new { action = "SearchSupportApl_H", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_GetSupportApl_ProductD",
                "SERI09/GetSupportApl_ProductD",
                new { action = "GetSupportApl_ProductD", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                 "SERI09_Edit",
                 "SERI09/Edit",
                 new { action = "Edit", controller = "Main" },
                 namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                 "SERI09_Delete",
                 "SERI09/Delete",
                 new { action = "Delete", controller = "Main" },
                 namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                 "SERI09_Details",
                 "SERI09/Details",
                 new { action = "Details", controller = "Main" },
                 namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_SearchPicking_ProductSerial",
                "SERI09/SearchPicking_ProductSerial",
                new { action = "SearchPicking_ProductSerial", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

            context.MapRoute(
                "SERI09_AddSerial",
                "SERI09/AddSerial",
                new { action = "AddSerial", controller = "Main" },
                namespaces: new string[] { "SERI09.Controllers" }
            );

        }
    }
}