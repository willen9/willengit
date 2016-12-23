using System.Web.Mvc;

namespace rmsweb
{
    public class SUPI09AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPI09";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SUPI09_default",
                "SUPI09",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_Add",
                "SUPI09/Add",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_SearchOrderType",
                "SUPI09/Add/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_GetOrderTypeName",
                "SUPI09/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_GetOrderTypeNo",
                "SUPI09/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_SearchSupportApl_H",
                "SUPI09/SearchSupportApl_H",
                new { action = "SearchSupportApl_H", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_GetSupportApl_ProductD",
                "SUPI09/GetSupportApl_ProductD",
                new { action = "GetSupportApl_ProductD", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                 "SUPI09_Edit",
                 "SUPI09/Edit/{PickingOrderType}/{PickingOrderNo}",
                 new { action = "Edit", controller = "Main" },
                 namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                 "SUPI09_Delete",
                 "SUPI09/Delete/{PickingOrderType}/{PickingOrderNo}",
                 new { action = "Delete", controller = "Main" },
                 namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                 "SUPI09_Details",
                 "SUPI09/Details/{PickingOrderType}/{PickingOrderNo}",
                 new { action = "Details", controller = "Main" },
                 namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_SearchPicking_ProductSerial",
                "SUPI09/SearchPicking_ProductSerial",
                new { action = "SearchPicking_ProductSerial", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );

            context.MapRoute(
                "SUPI09_AddSerial",
                "SUPI09/AddSerial",
                new { action = "AddSerial", controller = "Main" },
                namespaces: new string[] { "SUPI09.Controllers" }
            );
        }
    }
}