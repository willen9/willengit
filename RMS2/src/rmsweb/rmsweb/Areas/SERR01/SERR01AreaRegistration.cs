using System.Web.Mvc;

namespace rmsweb
{
    public class SERR01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERR01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERR01_default",
                "SERR01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );

            context.MapRoute(
                name: "SERR01_CUR",
                url: "SERR01/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );

            context.MapRoute(
                "SERR01_delete",
                "SERR01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers"}
            );

            context.MapRoute(
                "SERR01_Details",
                "SERR01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );

            context.MapRoute(
                "SERR01_Edit",
                "SERR01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );

            context.MapRoute(
                "SERR01_Copy",
                "SERR01/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );

            context.MapRoute(
                "SERR01_CURID",
                "SERR01/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );

            context.MapRoute(
                "SERR01_Add",
                "SERR01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );

            context.MapRoute(
                "SERR01_Exit",
                "SERR01/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERR01.Controllers" }
            );
        }
    }
}