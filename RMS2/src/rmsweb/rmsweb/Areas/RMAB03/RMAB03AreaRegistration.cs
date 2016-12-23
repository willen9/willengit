using System.Web.Mvc;

namespace rmsweb
{
    public class RMAB03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAB03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAB03_default",
                "RMAB03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );

            context.MapRoute(
                name: "RMAB03_CUR",
                url: "RMAB03/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );

            context.MapRoute(
                "RMAB03_delete",
                "RMAB03/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers"}
            );

            context.MapRoute(
                "RMAB03_Details",
                "RMAB03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );

            context.MapRoute(
                "RMAB03_Edit",
                "RMAB03/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );

            context.MapRoute(
                "RMAB03_Copy",
                "RMAB03/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );

            context.MapRoute(
                "RMAB03_CURID",
                "RMAB03/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );

            context.MapRoute(
                "RMAB03_Add",
                "RMAB03/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );

            context.MapRoute(
                "RMAB03_Exit",
                "RMAB03/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAB03.Controllers" }
            );
        }
    }
}