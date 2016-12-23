using System.Web.Mvc;

namespace rmsweb
{
    public class RMAI07AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAI07";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAI07_default",
                "RMAI07",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );

            context.MapRoute(
                name: "RMAI07_CUR",
                url: "RMAI07/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );

            context.MapRoute(
                "RMAI07_delete",
                "RMAI07/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers"}
            );

            context.MapRoute(
                "RMAI07_Details",
                "RMAI07/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );

            context.MapRoute(
                "RMAI07_Edit",
                "RMAI07/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );

            context.MapRoute(
                "RMAI07_Copy",
                "RMAI07/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );

            context.MapRoute(
                "RMAI07_CURID",
                "RMAI07/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );

            context.MapRoute(
                "RMAI07_Add",
                "RMAI07/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );

            context.MapRoute(
                "RMAI07_Exit",
                "RMAI07/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAI07.Controllers" }
            );
        }
    }
}