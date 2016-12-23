using System.Web.Mvc;

namespace rmsweb
{
    public class SERI07AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI07";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI07_default",
                "SERI07",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                name: "SERI07_CUR",
                url: "SERI07/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                name: "SERI07_Add",
                url: "SERI07/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                name: "SERI07_Exit",
                url: "SERI07/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_delete",
                "SERI07/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers"}
            );

            context.MapRoute(
                "SERI07_Details",
                "SERI07/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_Edit",
                "SERI07/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_SearchServiceArrangeDInfo",
                "SERI07/SearchServiceArrangeDInfo",
                new { action = "SearchServiceArrangeDInfo", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_SearchServiceArrange",
                "SERI07/CUR/SearchServiceArrange",
                new { action = "SearchServiceArrange", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_SearchServiceArrangeInfo",
                "SERI07/SearchServiceArrangeInfo",
                new { action = "SearchServiceArrangeInfo", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_SearchRGA",
                "SERI07/SearchRGA",
                new { action = "SearchRGA", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_GetRGAH",
                "SERI07/GetRGAH",
                new { action = "GetRGAH", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_GetRGAD",
                "SERI07/GetRGAD",
                new { action = "GetRGAD", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

            context.MapRoute(
                "SERI07_SearchServiceArrangeSerial",
                "SERI07/SearchServiceArrangeSerial",
                new { action = "SearchServiceArrangeSerial", controller = "Main" },
                namespaces: new string[] { "SERI07.Controllers" }
            );

        }
    }
}