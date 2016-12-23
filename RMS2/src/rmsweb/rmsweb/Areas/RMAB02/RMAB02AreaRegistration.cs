using System.Web.Mvc;

namespace rmsweb
{
    public class RMAB02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RMAB02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RMAB02_default",
                "RMAB02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );

            context.MapRoute(
                name: "RMAB02_CUR",
                url: "RMAB02/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );

            context.MapRoute(
                "RMAB02_delete",
                "RMAB02/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers"}
            );

            context.MapRoute(
                "RMAB02_Details",
                "RMAB02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );

            context.MapRoute(
                "RMAB02_Edit",
                "RMAB02/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );

            context.MapRoute(
                "RMAB02_Copy",
                "RMAB02/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );

            context.MapRoute(
                "RMAB02_CURID",
                "RMAB02/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );

            context.MapRoute(
                "RMAB02_Add",
                "RMAB02/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );

            context.MapRoute(
                "RMAB02_Exit",
                "RMAB02/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "RMAB02.Controllers" }
            );
        }
    }
}