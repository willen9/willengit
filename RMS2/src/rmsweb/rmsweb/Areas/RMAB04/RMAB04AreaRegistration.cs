using System.Web.Mvc;

namespace rmsweb
{
    public class RMAB04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAB04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAB04_default",
                "RMAB04",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );

            context.MapRoute(
                name: "RMAB04_CUR",
                url: "RMAB04/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );

            context.MapRoute(
                "RMAB04_delete",
                "RMAB04/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers"}
            );

            context.MapRoute(
                "RMAB04_Details",
                "RMAB04/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );

            context.MapRoute(
                "RMAB04_Edit",
                "RMAB04/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );

            context.MapRoute(
                "RMAB04_Copy",
                "RMAB04/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );

            context.MapRoute(
                "RMAB04_CURID",
                "RMAB04/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );

            context.MapRoute(
                "RMAB04_Add",
                "RMAB04/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );

            context.MapRoute(
                "RMAB04_Exit",
                "RMAB04/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAB04.Controllers" }
            );
        }
    }
}