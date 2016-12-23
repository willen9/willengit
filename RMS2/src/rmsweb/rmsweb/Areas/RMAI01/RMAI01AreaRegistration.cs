using System.Web.Mvc;

namespace rmsweb
{
    public class RMAI01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAI01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAI01_default",
                "RMAI01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );

            context.MapRoute(
                "RMAI01_CUR",
                "RMAI01/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );

            context.MapRoute(
                "RMAI01_Add",
                "RMAI01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );

            context.MapRoute(
                "RMAI01_Update",
                "RMAI01/Update",
                new { action = "Update", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );

            context.MapRoute(
                "RMAI01_Delete",
                "RMAI01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );

            context.MapRoute(
                "RMAI01_Details",
                "RMAI01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );

            context.MapRoute(
                "RMAI01_Edit",
                "RMAI01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );

            context.MapRoute(
                "RMAI01_Show",
                "RMAI01/Show",
                new { action = "Show", controller = "Main" },
                namespaces: new string[] { "RMAI01.Controllers" }
            );
        }
    }
}