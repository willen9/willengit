using System.Web.Mvc;

namespace rmsweb
{
    public class RMAI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAI03_default",
                "RMAI03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );

            context.MapRoute(
                name: "RMAI03_CUR",
                url: "RMAI03/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );

            context.MapRoute(
                "RMAI03_delete",
                "RMAI03/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers"}
            );

            context.MapRoute(
                "RMAI03_Details",
                "RMAI03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );

            context.MapRoute(
                "RMAI03_Edit",
                "RMAI03/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );

            context.MapRoute(
                "RMAI03_Copy",
                "RMAI03/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );

            context.MapRoute(
                "RMAI03_CURID",
                "RMAI03/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );

            context.MapRoute(
                "RMAI03_Add",
                "RMAI03/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );

            context.MapRoute(
                "RMAI03_Exit",
                "RMAI03/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAI03.Controllers" }
            );
        }
    }
}