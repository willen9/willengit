using System.Web.Mvc;

namespace rmsweb
{
    public class SERB01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERB01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERB01_default",
                "SERB01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );

            context.MapRoute(
                name: "SERB01_CUR",
                url: "SERB01/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );

            context.MapRoute(
                "SERB01_delete",
                "SERB01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers"}
            );

            context.MapRoute(
                "SERB01_Details",
                "SERB01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );

            context.MapRoute(
                "SERB01_Edit",
                "SERB01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );

            context.MapRoute(
                "SERB01_Copy",
                "SERB01/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );

            context.MapRoute(
                "SERB01_CURID",
                "SERB01/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );

            context.MapRoute(
                "SERB01_Add",
                "SERB01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );

            context.MapRoute(
                "SERB01_Exit",
                "SERB01/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERB01.Controllers" }
            );
        }
    }
}