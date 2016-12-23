using System.Web.Mvc;

namespace rmsweb
{
    public class BOMI01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BOMI01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BOMI01_default",
                "BOMI01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                name: "BOMI01_CUR",
                url: "BOMI01/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                name: "BOMI01_Add",
                url: "BOMI01/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                name: "BOMI01_Exit",
                url: "BOMI01/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                "BOMI01_delete",
                "BOMI01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers"}
            );

            context.MapRoute(
                "BOMI01_Details",
                "BOMI01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                "BOMI01_Edit",
                "BOMI01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                "BOMI01_SearchProductInfo",
                "BOMI01/SearchProductInfo",
                new { action = "SearchProductInfo", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                "BOMI01_SearchCustomerInfo",
                "BOMI01/SearchCustomerInfo",
                new { action = "SearchCustomerInfo", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                "BOMI01_MajorComponentNo",
                "BOMI01/MajorComponentNo",
                new { action = "MajorComponentNo", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                "BOMI01_Confirmed",
                "BOMI01/Confirmed",
                new { action = "Confirmed", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

            context.MapRoute(
                "BOMI01_Sure",
                "BOMI01/Sure",
                new { action = "Sure", controller = "Main" },
                namespaces: new string[] { "BOMI01.Controllers" }
            );

        }
    }
}