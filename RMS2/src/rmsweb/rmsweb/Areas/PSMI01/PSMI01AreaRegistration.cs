using System.Web.Mvc;

namespace rmsweb
{
    public class PSMI01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PSMI01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PSMI01_default",
                "PSMI01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

            context.MapRoute(
                name: "PSMI01_CUR",
                url: "PSMI01/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

            context.MapRoute(
                name: "PSMI01_Add",
                url: "PSMI01/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

            context.MapRoute(
                name: "PSMI01_Exit",
                url: "PSMI01/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

            context.MapRoute(
                "PSMI01_delete",
                "PSMI01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers"}
            );

            context.MapRoute(
                "PSMI01_Details",
                "PSMI01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

            context.MapRoute(
                "PSMI01_Edit",
                "PSMI01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

            context.MapRoute(
                "PSMI01_SearchProductInfo",
                "PSMI01/SearchProductInfo",
                new { action = "SearchProductInfo", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

            context.MapRoute(
                "PSMI01_SearchCustomerInfo",
                "PSMI01/SearchCustomerInfo",
                new { action = "SearchCustomerInfo", controller = "Main" },
                namespaces: new string[] { "PSMI01.Controllers" }
            );

        }
    }
}