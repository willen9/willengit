using System.Web.Mvc;

namespace rmsweb
{
    public class PMAB02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAB02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAB02_default",
                "PMAB02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                name: "PMAB02_CUR",
                url: "PMAB02/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );
            context.MapRoute(
               name: "PMAB02_Print",
               url: "PMAB02/Print",
               defaults: new { action = "Print", controller = "Main" },
               namespaces: new string[] { "PMAB02.Controllers" }
           );
            context.MapRoute(
                name: "PMAB02_Add",
                url: "PMAB02/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                name: "PMAB02_Exit",
                url: "PMAB02/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                "PMAB02_delete",
                "PMAB02/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers"}
            );

            context.MapRoute(
                "PMAB02_Details",
                "PMAB02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                "PMAB02_Edit",
                "PMAB02/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                "PMAB02_SearchServiceArrangeDInfo",
                "PMAB02/SearchServiceArrangeDInfo",
                new { action = "SearchServiceArrangeDInfo", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                "PMAB02_SearchServiceArrange",
                "PMAB02/CUR/SearchServiceArrange",
                new { action = "SearchServiceArrange", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                "PMAB02_SearchServiceArrangeInfo",
                "PMAB02/SearchServiceArrangeInfo",
                new { action = "SearchServiceArrangeInfo", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                "PMAB02_SearchOrderType",
                "PMAB02/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

            context.MapRoute(
                "PMAB02_SearchServiceArrangeSerial",
                "PMAB02/SearchServiceArrangeSerial",
                new { action = "SearchServiceArrangeSerial", controller = "Main" },
                namespaces: new string[] { "PMAB02.Controllers" }
            );

        }
    }
}