using System.Web.Mvc;

namespace rmsweb
{
    public class SERI05AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI05";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI05_default",
                "SERI05",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                name: "SERI05_CUR",
                url: "SERI05/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                name: "SERI05_Add",
                url: "SERI05/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                name: "SERI05_Exit",
                url: "SERI05/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_delete",
                "SERI05/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers"}
            );

            context.MapRoute(
                "SERI05_Details",
                "SERI05/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_Edit",
                "SERI05/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_Copy",
                "SERI05/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchServiceArrangeDInfo",
                "SERI05/SearchServiceArrangeDInfo",
                new { action = "SearchServiceArrangeDInfo", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchServiceArrange",
                "SERI05/CUR/SearchServiceArrange",
                new { action = "SearchServiceArrange", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchServiceArrangeInfo",
                "SERI05/SearchServiceArrangeInfo",
                new { action = "SearchServiceArrangeInfo", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchOrderType",
                "SERI05/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchServiceArrangeSerial",
                "SERI05/SearchServiceArrangeSerial",
                new { action = "SearchServiceArrangeSerial", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchProductInfo",
                "SERI05/SearchProductInfo",
                new { action = "SearchProductInfo", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_Confirmed",
                "SERI05/Confirmed",
                new { action = "Confirmed", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_Tree",
                "SERI05/Tree",
                new { action = "Tree", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchPhoneService",
                "SERI05/SearchPhoneService",
                new { action = "SearchPhoneService", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

            context.MapRoute(
                "SERI05_SearchPhoneServiceInfo",
                "SERI05/SearchPhoneServiceInfo",
                new { action = "SearchPhoneServiceInfo", controller = "Main" },
                namespaces: new string[] { "SERI05.Controllers" }
            );

        }
    }
}