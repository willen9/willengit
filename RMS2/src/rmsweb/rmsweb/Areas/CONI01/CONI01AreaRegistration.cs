using System.Web.Mvc;

namespace rmsweb
{
    public class CONI01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CONI01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CONI01_default",
                "CONI01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_CUR",
                "CONI01/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_Add",
                "CONI01/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_Update",
                "CONI01/Update",
                new { action = "Update", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_Delete",
                "CONI01/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_Details",
                "CONI01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_Edit",
                "CONI01/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_Show",
                "CONI01/Show",
                new { action = "Show", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_SearchOrderType",
                "CONI01/CUR/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );

            context.MapRoute(
                "CONI01_GetOrderTypeName",
                "CONI01/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "CONI01.Controllers" }
            );
        }
    }
}