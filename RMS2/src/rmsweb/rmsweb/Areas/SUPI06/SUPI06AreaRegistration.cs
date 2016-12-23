using System.Web.Mvc;

namespace rmsweb
{
    public class SUPI06AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPI06";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SUPI06_default",
                "SUPI06",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_Add",
                "SUPI06/Add",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_SearchOrderType",
                "SUPI06/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_GetOrderTypeNo",
                "SUPI06/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_SearchCustomer",
                "SUPI06/SearchCustomer",
                new { action = "SearchCustomer", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_SearchSupportApl_H",
                "SUPI06/SearchSupportApl_H",
                new { action = "SearchSupportApl_H", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_SearchEmployee",
                "SUPI06/SearchEmployee",
                new { action = "SearchEmployee", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_Delete",
                "SUPI06/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_Edit",
                "SUPI06/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
                "SUPI06_Details",
                "SUPI06/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SUPI06.Controllers" }
            );

            context.MapRoute(
               "SUPI06_Print",
               "SUPI06/Print",
               new { action = "Print", controller = "Main" },
               namespaces: new string[] { "SUPI06.Controllers" }
           );
        }
    }
}