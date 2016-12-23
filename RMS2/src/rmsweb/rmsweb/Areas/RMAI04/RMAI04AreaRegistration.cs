using System.Web.Mvc;

namespace rmsweb
{
    public class RMAI04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAI04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAI04_default",
                "RMAI04",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                name: "RMAI04_CUR",
                url: "RMAI04/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_delete",
                "RMAI04/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers"}
            );

            context.MapRoute(
                "RMAI04_Details",
                "RMAI04/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_Edit",
                "RMAI04/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_Copy",
                "RMAI04/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_CURID",
                "RMAI04/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_Add",
                "RMAI04/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_Exit",
                "RMAI04/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_SearchRMAApl",
                "RMAI04/SearchRMAApl",
                new { action = "SearchRMAApl", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );

            context.MapRoute(
                "RMAI04_SearchRMAAplInfo",
                "RMAI04/SearchRMAAplInfo",
                new { action = "SearchRMAAplInfo", controller = "Main" },
                namespaces: new string[] { "RMAI04.Controllers" }
            );
        }
    }
}