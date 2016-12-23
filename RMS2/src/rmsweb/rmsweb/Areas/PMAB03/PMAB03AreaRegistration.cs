using System.Web.Mvc;

namespace rmsweb
{
    public class PMAB03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAB03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PMAB03_default",
                "PMAB03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );

            context.MapRoute(
                name: "PMAB03_CUR",
                url: "PMAB03/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );

            context.MapRoute(
                "PMAB03_delete",
                "PMAB03/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers"}
            );

            context.MapRoute(
                "PMAB03_Details",
                "PMAB03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );

            context.MapRoute(
                "PMAB03_Edit",
                "PMAB03/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );

            context.MapRoute(
                "PMAB03_Copy",
                "PMAB03/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );

            context.MapRoute(
                "PMAB03_CURID",
                "PMAB03/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );

            context.MapRoute(
                "PMAB03_Add",
                "PMAB03/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );

            context.MapRoute(
                "PMAB03_Exit",
                "PMAB03/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "PMAB03.Controllers" }
            );
        }
    }
}