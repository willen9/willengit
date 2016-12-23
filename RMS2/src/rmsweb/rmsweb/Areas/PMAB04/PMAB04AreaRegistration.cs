using System.Web.Mvc;

namespace rmsweb
{
    public class PMAB04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAB04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAB04_default",
                "PMAB04",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );

            context.MapRoute(
                name: "PMAB04_CUR",
                url: "PMAB04/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );

            context.MapRoute(
                "PMAB04_delete",
                "PMAB04/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers"}
            );

            context.MapRoute(
                "PMAB04_Details",
                "PMAB04/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );

            context.MapRoute(
                "PMAB04_Edit",
                "PMAB04/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );

            context.MapRoute(
                "PMAB04_Copy",
                "PMAB04/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );

            context.MapRoute(
                "PMAB04_CURID",
                "PMAB04/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );

            context.MapRoute(
                "PMAB04_Add",
                "PMAB04/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );

            context.MapRoute(
                "PMAB04_Exit",
                "PMAB04/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAB04.Controllers" }
            );
        }
    }
}