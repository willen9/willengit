using System.Web.Mvc;

namespace rmsweb
{
    public class PMAI05AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAI05";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAI05_default",
                "PMAI05",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                name: "PMAI05_CUR",
                url: "PMAI05/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                name: "PMAI05_SearchRGA_H",
                url: "PMAI05/SearchRGA_H",
                defaults: new { action = "SearchRGA_H", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                "PMAI05_delete",
                "PMAI05/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers"}
            );

            context.MapRoute(
                "PMAI05_Details",
                "PMAI05/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                "PMAI05_Edit",
                "PMAI05/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                "PMAI05_Copy",
                "PMAI05/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                "PMAI05_CURID",
                "PMAI05/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                "PMAI05_Add",
                "PMAI05/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );

            context.MapRoute(
                "PMAI05_Exit",
                "PMAI05/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAI05.Controllers" }
            );
        }
    }
}