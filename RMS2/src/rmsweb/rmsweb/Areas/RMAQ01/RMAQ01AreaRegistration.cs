using System.Web.Mvc;

namespace rmsweb
{
    public class RMAQ01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAQ01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAQ01_default",
                "RMAQ01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );

            context.MapRoute(
                name: "RMAQ01_CUR",
                url: "RMAQ01/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );

            context.MapRoute(
                "RMAQ01_delete",
                "RMAQ01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers"}
            );

            context.MapRoute(
                "RMAQ01_Details",
                "RMAQ01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );

            context.MapRoute(
                "RMAQ01_Edit",
                "RMAQ01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );

            context.MapRoute(
                "RMAQ01_Copy",
                "RMAQ01/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );

            context.MapRoute(
                "RMAQ01_CURID",
                "RMAQ01/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );

            context.MapRoute(
                "RMAQ01_Add",
                "RMAQ01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );

            context.MapRoute(
                "RMAQ01_Exit",
                "RMAQ01/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAQ01.Controllers" }
            );
        }
    }
}