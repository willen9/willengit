using System.Web.Mvc;

namespace rmsweb
{
    public class SERI13AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI13";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI13_default",
                "SERI13",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );

            context.MapRoute(
                name: "SERI13_CUR",
                url: "SERI13/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );

            context.MapRoute(
                "SERI13_delete",
                "SERI13/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers"}
            );

            context.MapRoute(
                "SERI13_Details",
                "SERI13/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );

            context.MapRoute(
                "SERI13_Edit",
                "SERI13/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );

            context.MapRoute(
                "SERI13_Copy",
                "SERI13/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );

            context.MapRoute(
                "SERI13_CURID",
                "SERI13/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );

            context.MapRoute(
                "SERI13_Add",
                "SERI13/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );

            context.MapRoute(
                "SERI13_Exit",
                "SERI13/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI13.Controllers" }
            );
        }
    }
}