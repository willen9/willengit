using System.Web.Mvc;

namespace rmsweb
{
    public class PMAI01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAI01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAI01_default",
                "PMAI01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAI01.Controllers" }
            );

            context.MapRoute(
                "PMAI01_CUR",
                "PMAI01/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAI01.Controllers" }
            );

            context.MapRoute(
                "PMAI01_Add",
                "PMAI01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAI01.Controllers" }
            );

            context.MapRoute(
                "PMAI01_Update",
                "PMAI01/Update",
                new { action = "Update", controller = "Main" },
                namespaces: new string[] { "PMAI01.Controllers" }
            );

            context.MapRoute(
                "PMAI01_Delete",
                "PMAI01/Delete/{OrderType}",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAI01.Controllers" }
            );

            context.MapRoute(
                "PMAI01_Details",
                "PMAI01/Details/{OrderType}",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAI01.Controllers" }
            );

            context.MapRoute(
                "PMAI01_Edit",
                "PMAI01/Edit/{OrderType}",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAI01.Controllers" }
            );

        }
    }
}