using System.Web.Mvc;

namespace rmsweb
{
    public class SERI06AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI06";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI06_default",
                "SERI06",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                name: "SERI06_CUR",
                url: "SERI06/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                name: "SERI06_Add",
                url: "SERI06/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                name: "SERI06_Exit",
                url: "SERI06/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                name: "SERI06_Copy",
                url: "SERI06/Copy",
                defaults: new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_delete",
                "SERI06/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers"}
            );

            context.MapRoute(
                "SERI06_Details",
                "SERI06/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_Edit",
                "SERI06/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchServiceArrangeDInfo",
                "SERI06/SearchServiceArrangeDInfo",
                new { action = "SearchServiceArrangeDInfo", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchServiceArrange",
                "SERI06/CUR/SearchServiceArrange",
                new { action = "SearchServiceArrange", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchWarrantyDInfo",
                "SERI06/SearchWarrantyDInfo",
                new { action = "SearchWarrantyDInfo", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchServiceArrangeInfo",
                "SERI06/SearchServiceArrangeInfo",
                new { action = "SearchServiceArrangeInfo", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchOrderType",
                "SERI06/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchWarrantySerialNo",
                "SERI06/SearchWarrantySerialNo",
                new { action = "SearchWarrantySerialNo", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchWarrantySerialNoInfo",
                "SERI06/SearchWarrantySerialNoInfo",
                new { action = "SearchWarrantySerialNoInfo", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_Confirmed",
                "SERI06/Confirmed",
                new { action = "Confirmed", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

            context.MapRoute(
                "SERI06_SearchBOMD",
                "SERI06/SearchBOMD",
                new { action = "SearchBOMD", controller = "Main" },
                namespaces: new string[] { "SERI06.Controllers" }
            );

        }
    }
}