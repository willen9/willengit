using System.Web.Mvc;

namespace rmsweb
{
    public class PMAI06AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAI06";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAI06_default",
                "PMAI06",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );

            context.MapRoute(
                name: "PMAI06_CUR",
                url: "PMAI06/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );

            context.MapRoute(
                "PMAI06_delete",
                "PMAI06/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers"}
            );
            context.MapRoute(
                "PMAI06_GetAssignmentServiceApl",
                "PMAI06/GetAssignmentServiceApl",
                new { action = "GetAssignmentServiceApl", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );


            context.MapRoute(
                "PMAI06_Details",
                "PMAI06/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );

            context.MapRoute(
                "PMAI06_Edit",
                "PMAI06/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );

            context.MapRoute(
                "PMAI06_Copy",
                "PMAI06/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );

            context.MapRoute(
                "PMAI06_CURID",
                "PMAI06/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );

            context.MapRoute(
                "PMAI06_Add",
                "PMAI06/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );

            context.MapRoute(
                "PMAI06_Exit",
                "PMAI06/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAI06.Controllers" }
            );
        }
    }
}