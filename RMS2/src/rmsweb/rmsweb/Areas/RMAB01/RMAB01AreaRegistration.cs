using System.Web.Mvc;

namespace rmsweb
{
    public class RMAB01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAB01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAB01_default",
                "RMAB01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );

            context.MapRoute(
                name: "RMAB01_CUR",
                url: "RMAB01/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );

            context.MapRoute(
                "RMAB01_delete",
                "RMAB01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers"}
            );

            context.MapRoute(
                "RMAB01_Details",
                "RMAB01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );

            context.MapRoute(
                "RMAB01_Edit",
                "RMAB01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );

            context.MapRoute(
                "RMAB01_Copy",
                "RMAB01/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );

            context.MapRoute(
                "RMAB01_CURID",
                "RMAB01/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );

            context.MapRoute(
                "RMAB01_Add",
                "RMAB01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );

            context.MapRoute(
                "RMAB01_Exit",
                "RMAB01/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAB01.Controllers" }
            );
        }
    }
}