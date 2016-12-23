using System.Web.Mvc;

namespace rmsweb
{
    public class SERI10AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI10";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI10_default",
                "SERI10",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                name: "SERI10_CUR",
                url: "SERI10/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_delete",
                "SERI10/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers"}
            );

            context.MapRoute(
                "SERI10_Details",
                "SERI10/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_Edit",
                "SERI10/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_Copy",
                "SERI10/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_CURID",
                "SERI10/CURID",
                new { action = "CURID", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_Add",
                "SERI10/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_Exit",
                "SERI10/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_Confirmed",
                "SERI10/Confirmed",
                new { action = "Confirmed", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
              "SERI10_SearchServiceArrangeDInfo",
              "SERI10/SearchServiceArrangeDInfo",
              new { action = "SearchServiceArrangeDInfo", controller = "Main" },
              namespaces: new string[] { "SERI10.Controllers" }
          );

            context.MapRoute(
                "SERI10_SearchServiceArrange",
                "SERI10/CUR/SearchServiceArrange",
                new { action = "SearchServiceArrange", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_SearchWarrantyDInfo",
                "SERI10/SearchWarrantyDInfo",
                new { action = "SearchWarrantyDInfo", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_SearchServiceArrangeInfo",
                "SERI10/SearchServiceArrangeInfo",
                new { action = "SearchServiceArrangeInfo", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_SearchOrderType",
                "SERI10/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_SearchWarrantySerialNo",
                "SERI10/SearchWarrantySerialNo",
                new { action = "SearchWarrantySerialNo", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_SearchWarrantySerialNoInfo",
                "SERI10/SearchWarrantySerialNoInfo",
                new { action = "SearchWarrantySerialNoInfo", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );

            context.MapRoute(
                "SERI10_SearchBOMD",
                "SERI10/SearchBOMD",
                new { action = "SearchBOMD", controller = "Main" },
                namespaces: new string[] { "SERI10.Controllers" }
            );
        }
    }
}