using System.Web.Mvc;

namespace rmsweb
{
    public class PMAI04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAI04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAI04_default",
                "PMAI04",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                name: "PMAI04_CUR",
                url: "PMAI04/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                name: "PMAI04_Add",
                url: "PMAI04/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                name: "PMAI04_Exit",
                url: "PMAI04/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                "PMAI04_delete",
                "PMAI04/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers"}
            );

            context.MapRoute(
                "PMAI04_Details",
                "PMAI04/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                "PMAI04_Edit",
                "PMAI04/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                "PMAI04_SearchServiceArrangeDInfo",
                "PMAI04/SearchServiceArrangeDInfo",
                new { action = "SearchServiceArrangeDInfo", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                "PMAI04_SearchServiceArrange",
                "PMAI04/CUR/SearchServiceArrange",
                new { action = "SearchServiceArrange", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                "PMAI04_SearchServiceArrangeInfo",
                "PMAI04/SearchServiceArrangeInfo",
                new { action = "SearchServiceArrangeInfo", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                "PMAI04_SearchOrderType",
                "PMAI04/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

            context.MapRoute(
                "PMAI04_SearchServiceArrangeSerial",
                "PMAI04/SearchServiceArrangeSerial",
                new { action = "SearchServiceArrangeSerial", controller = "Main" },
                namespaces: new string[] { "PMAI04.Controllers" }
            );

        }
    }
}