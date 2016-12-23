using System.Web.Mvc;

namespace rmsweb
{
    public class SERI11AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI11";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI11_default",
                "SERI11",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                name: "SERI11_CUR",
                url: "SERI11/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                "SERI11_delete",
                "SERI11/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers"}
            );

            context.MapRoute(
                "SERI11_Details",
                "SERI11/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                "SERI11_Edit",
                "SERI11/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                "SERI11_Copy",
                "SERI11/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                "SERI11_CURID",
                "SERI11/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                "SERI11_Add",
                "SERI11/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                "SERI11_Exit",
                "SERI11/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );

            context.MapRoute(
                "SERI11_Confirmed",
                "SERI11/Confirmed",
                new { action = "Confirmed", controller = "Main" },
                namespaces: new string[] { "SERI11.Controllers" }
            );
        }
    }
}