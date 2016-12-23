using System.Web.Mvc;

namespace rmsweb
{
    public class PSMI02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PSMI02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PSMI02_default",
                "PSMI02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                name: "PSMI02_CUR",
                url: "PSMI02/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                name: "PSMI02_Add",
                url: "PSMI02/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                name: "PSMI02_Exit",
                url: "PSMI02/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                "PSMI02_delete",
                "PSMI02/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers"}
            );

            context.MapRoute(
                "PSMI02_Details",
                "PSMI02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                "PSMI02_Edit",
                "PSMI02/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                "PSMI02_SearchProductInfo",
                "PSMI02/SearchProductInfo",
                new { action = "SearchProductInfo", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                "PSMI02_SearchCustomerInfo",
                "PSMI02/SearchCustomerInfo",
                new { action = "SearchCustomerInfo", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

            context.MapRoute(
                "PSMI02_checkSure",
                "PSMI02/checkSure",
                new { action = "checkSure", controller = "Main" },
                namespaces: new string[] { "PSMI02.Controllers" }
            );

        }
    }
}