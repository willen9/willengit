using System.Web.Mvc;

namespace rmsweb
{
    public class SUPI04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPI04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SUPI04_default",
                "SUPI04",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPI04.Controllers" }
            );

            context.MapRoute(
                "SUPI04_Add",
                "SUPI04/Add",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SUPI04.Controllers" }
            );

            context.MapRoute(
                "SUPI04_Delete",
                "SUPI04/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SUPI04.Controllers" }
            );

            context.MapRoute(
                "SUPI04_Details",
                "SUPI04/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SUPI04.Controllers" }
            );

            context.MapRoute(
                "SUPI04_Edit",
                "SUPI04/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SUPI04.Controllers" }
            );

            context.MapRoute(
                "SUPI04_SupportItemId",
                "SUPI04/SupportItemId",
                new { action = "SupportItemId", controller = "Main" },
                namespaces: new string[] { "SUPI04.Controllers" }
            );
        }
    }
}