using System.Web.Mvc;

namespace rmsweb
{
    public class PSMI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PSMI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PSMI03_default",
                "PSMI03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                name: "PSMI03_CUR",
                url: "PSMI03/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                name: "PSMI03_Add",
                url: "PSMI03/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                name: "PSMI03_Exit",
                url: "PSMI03/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                "PSMI03_delete",
                "PSMI03/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers"}
            );

            context.MapRoute(
                "PSMI03_Details",
                "PSMI03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                "PSMI03_Edit",
                "PSMI03/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                "PSMI03_SearchProductInfo",
                "PSMI03/SearchProductInfo",
                new { action = "SearchProductInfo", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                "PSMI03_SearchCustomerInfo",
                "PSMI03/SearchCustomerInfo",
                new { action = "SearchCustomerInfo", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

            context.MapRoute(
                "PSMI03_SerialNo",
                "PSMI03/SerialNo",
                new { action = "SerialNo", controller = "Main" },
                namespaces: new string[] { "PSMI03.Controllers" }
            );

        }
    }
}