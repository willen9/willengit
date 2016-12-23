using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CMSI04_default",
                "CMSI04",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );

            context.MapRoute(
                name: "CMSI04_CUR",
                url: "CMSI04/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );

            context.MapRoute(
                "CMSI04_delete",
                "CMSI04/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers"}
            );

            context.MapRoute(
                "CMSI04_Details",
                "CMSI04/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );

            context.MapRoute(
                "CMSI04_Edit",
                "CMSI04/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );

            context.MapRoute(
                "CMSI04_Copy",
                "CMSI04/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );

            context.MapRoute(
                "CMSI04_CURID",
                "CMSI04/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );

            context.MapRoute(
                "CMSI04_Add",
                "CMSI04/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );

            context.MapRoute(
                "CMSI04_Exit",
                "CMSI04/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "CMSI04.Controllers" }
            );
        }
    }
}