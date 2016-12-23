using System.Web.Mvc;

namespace rmsweb
{
    public class SUPI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SUPI03_default",
                "SUPI03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_CUR",
                "SUPI03/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_Delete",
                "SUPI03/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_Details",
                "SUPI03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_Edit",
                "SUPI03/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_Show",
                "SUPI03/Show",
                new { action = "Show", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_OrderType",
                "SUPI03/OrderType",
                new { action = "OrderType", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_Add",
                "SUPI03/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );

            context.MapRoute(
                "SUPI03_Update",
                "SUPI03/Update",
                new { action = "Update", controller = "Main" },
                namespaces: new string[] { "SUPI03.Controllers" }
            );
        }

    }
}