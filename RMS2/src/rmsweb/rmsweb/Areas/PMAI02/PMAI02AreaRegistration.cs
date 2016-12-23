using System.Web.Mvc;

namespace rmsweb
{
    public class PMAI02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAI02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAI02_default",
                "PMAI02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                name: "PMAI02_CUR",
                url: "PMAI02/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                name: "PMAI02_Add",
                url: "PMAI02/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                name: "PMAI02_Copy",
                url: "PMAI02/Copy",
                defaults: new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                name: "PMAI02_Confirmed",
                url: "PMAI02/Confirmed",
                defaults: new { action = "Confirmed", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                name: "PMAI02_Exit",
                url: "PMAI02/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                "PMAI02_delete",
                "PMAI02/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers"}
            );

            context.MapRoute(
                "PMAI02_Details",
                "PMAI02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                "PMAI02_Edit",
                "PMAI02/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                "PMAI02_SearchProductInfo",
                "PMAI02/SearchProductInfo",
                new { action = "SearchProductInfo", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                "PMAI02_SearchCustomerInfo",
                "PMAI02/SearchCustomerInfo",
                new { action = "SearchCustomerInfo", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                "PMAI02_SerialNo",
                "PMAI02/SerialNo",
                new { action = "SerialNo", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                "PMAI02_SearchOrderType",
                "PMAI02/CUR/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

            context.MapRoute(
                "PMAI02_GetOrderTypeName",
                "PMAI02/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "PMAI02.Controllers" }
            );

        }
    }
}