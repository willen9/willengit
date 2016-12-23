using System.Web.Mvc;

namespace rmsweb
{
    public class SERQ01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERQ01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERQ01_default",
                "SERQ01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );

            context.MapRoute(
                name: "SERQ01_CUR",
                url: "SERQ01/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );

            context.MapRoute(
                "SERQ01_delete",
                "SERQ01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers"}
            );

            context.MapRoute(
                "SERQ01_Details",
                "SERQ01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );

            context.MapRoute(
                "SERQ01_Edit",
                "SERQ01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );

            context.MapRoute(
                "SERQ01_Copy",
                "SERQ01/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );

            context.MapRoute(
                "SERQ01_CURID",
                "SERQ01/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );

            context.MapRoute(
                "SERQ01_Add",
                "SERQ01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );

            context.MapRoute(
                "SERQ01_Exit",
                "SERQ01/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERQ01.Controllers" }
            );
        }
    }
}