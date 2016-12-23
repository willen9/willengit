using System.Web.Mvc;

namespace rmsweb
{
    public class SERI02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI02_default",
                "SERI02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );

            context.MapRoute(
                "SERI02_CUR",
                "SERI02/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );

            context.MapRoute(
                "SERI02_Add",
                "SERI02/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );

            context.MapRoute(
                "SERI02_Update",
                "SERI02/Update",
                new { action = "Update", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );

            context.MapRoute(
                "SERI02_Delete",
                "SERI02/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );

            context.MapRoute(
                "SERI02_Details",
                "SERI02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );

            context.MapRoute(
                "SERI02_Edit",
                "SERI02/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );

            context.MapRoute(
                "SERI02_Show",
                "SERI02/Show",
                new { action = "Show", controller = "Main" },
                namespaces: new string[] { "SERI02.Controllers" }
            );
        }
    }
}