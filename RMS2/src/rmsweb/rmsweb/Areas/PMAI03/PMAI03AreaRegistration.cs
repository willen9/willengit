using System.Web.Mvc;

namespace rmsweb
{
    public class PMAI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAI03_default",
                "PMAI03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                name: "PMAI03_CUR",
                url: "PMAI03/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                name: "PMAI03_Add",
                url: "PMAI03/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                name: "PMAI03_Exit",
                url: "PMAI03/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                "PMAI03_delete",
                "PMAI03/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers"}
            );

            context.MapRoute(
                "PMAI03_Details",
                "PMAI03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                "PMAI03_Edit",
                "PMAI03/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                "PMAI03_SearchServiceArrangeDInfo",
                "PMAI03/SearchServiceArrangeDInfo",
                new { action = "SearchServiceArrangeDInfo", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                "PMAI03_SearchServiceArrange",
                "PMAI03/CUR/SearchServiceArrange",
                new { action = "SearchServiceArrange", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

            context.MapRoute(
                "PMAI03_SearchServiceArrangeInfo",
                "PMAI03/SearchServiceArrangeInfo",
                new { action = "SearchServiceArrangeInfo", controller = "Main" },
                namespaces: new string[] { "PMAI03.Controllers" }
            );

        }
    }
}