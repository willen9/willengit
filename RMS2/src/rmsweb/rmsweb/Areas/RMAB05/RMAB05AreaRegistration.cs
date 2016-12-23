using System.Web.Mvc;

namespace rmsweb
{
    public class RMAB05AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAB05";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAB05_default",
                "RMAB05",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );

            context.MapRoute(
                name: "RMAB05_CUR",
                url: "RMAB05/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );

            context.MapRoute(
                "RMAB05_delete",
                "RMAB05/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers"}
            );

            context.MapRoute(
                "RMAB05_Details",
                "RMAB05/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );

            context.MapRoute(
                "RMAB05_Edit",
                "RMAB05/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );

            context.MapRoute(
                "RMAB05_Copy",
                "RMAB05/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );

            context.MapRoute(
                "RMAB05_CURID",
                "RMAB05/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );

            context.MapRoute(
                "RMAB05_Add",
                "RMAB05/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );

            context.MapRoute(
                "RMAB05_Exit",
                "RMAB05/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAB05.Controllers" }
            );
        }
    }
}