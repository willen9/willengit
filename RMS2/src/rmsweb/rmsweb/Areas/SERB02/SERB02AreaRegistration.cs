using System.Web.Mvc;

namespace rmsweb
{
    public class SERB02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERB02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERB02_default",
                "SERB02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );

            context.MapRoute(
                name: "SERB02_CUR",
                url: "SERB02/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );

            context.MapRoute(
                "SERB02_delete",
                "SERB02/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers"}
            );

            context.MapRoute(
                "SERB02_Details",
                "SERB02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );

            context.MapRoute(
                "SERB02_Edit",
                "SERB02/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );

            context.MapRoute(
                "SERB02_Copy",
                "SERB02/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );

            context.MapRoute(
                "SERB02_CURID",
                "SERB02/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );

            context.MapRoute(
                "SERB02_Add",
                "SERB02/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );

            context.MapRoute(
                "SERB02_Exit",
                "SERB02/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERB02.Controllers" }
            );
        }
    }
}