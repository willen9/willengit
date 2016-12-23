using System.Web.Mvc;

namespace rmsweb
{
    public class RMAI06AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAI06";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAI06_default",
                "RMAI06",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );

            context.MapRoute(
                name: "RMAI06_CUR",
                url: "RMAI06/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );

            context.MapRoute(
                "RMAI06_delete",
                "RMAI06/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers"}
            );

            context.MapRoute(
                "RMAI06_Details",
                "RMAI06/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );

            context.MapRoute(
                "RMAI06_Edit",
                "RMAI06/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );

            context.MapRoute(
                "RMAI06_Copy",
                "RMAI06/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );

            context.MapRoute(
                "RMAI06_CURID",
                "RMAI06/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );

            context.MapRoute(
                "RMAI06_Add",
                "RMAI06/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );

            context.MapRoute(
                "RMAI06_Exit",
                "RMAI06/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAI06.Controllers" }
            );
        }
    }
}