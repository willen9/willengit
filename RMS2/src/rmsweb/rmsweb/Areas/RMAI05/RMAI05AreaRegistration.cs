using System.Web.Mvc;

namespace rmsweb
{
    public class RMAI05AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAI05";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAI05_default",
                "RMAI05",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );

            context.MapRoute(
                name: "RMAI05_CUR",
                url: "RMAI05/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );

            context.MapRoute(
                "RMAI05_delete",
                "RMAI05/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers"}
            );

            context.MapRoute(
                "RMAI05_Details",
                "RMAI05/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );

            context.MapRoute(
                "RMAI05_Edit",
                "RMAI05/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );

            context.MapRoute(
                "RMAI05_Copy",
                "RMAI05/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );

            context.MapRoute(
                "RMAI05_CURID",
                "RMAI05/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );

            context.MapRoute(
                "RMAI05_Add",
                "RMAI05/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );

            context.MapRoute(
                "RMAI05_Exit",
                "RMAI05/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAI05.Controllers" }
            );
        }
    }
}