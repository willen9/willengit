using System.Web.Mvc;

namespace rmsweb
{
    public class SERI12AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI12";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI12_default",
                "SERI12",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );

            context.MapRoute(
                name: "SERI12_CUR",
                url: "SERI12/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );

            context.MapRoute(
                "SERI12_delete",
                "SERI12/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers"}
            );

            context.MapRoute(
                "SERI12_Details",
                "SERI12/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );

            context.MapRoute(
                "SERI12_Edit",
                "SERI12/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );

            context.MapRoute(
                "SERI12_Copy",
                "SERI12/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );

            context.MapRoute(
                "SERI12_CURID",
                "SERI12/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );

            context.MapRoute(
                "SERI12_Add",
                "SERI12/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );

            context.MapRoute(
                "SERI12_Exit",
                "SERI12/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI12.Controllers" }
            );
        }
    }
}