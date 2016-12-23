using System.Web.Mvc;

namespace rmsweb
{
    public class SUPB02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPB02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SUPB02_default",
                "SUPB02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );

            context.MapRoute(
                "SUPB02_GetOrderTypeNo",
                "SUPB02/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );

            context.MapRoute(
                "SUPB02_SearchOrderType",
                "SUPB02/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );

            context.MapRoute(
                "SUPB02_GetOrderTypeName",
                "SUPB02/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );

            context.MapRoute(
                "SUPB02_Issue",
                "SUPB02/Issue",
                new { action = "Issue", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );

            context.MapRoute(
                "SUPB02_CUR",
                "SUPB02/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );

            context.MapRoute(
                "SUPB02_Details",
                "SUPB02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );

            context.MapRoute(
                "SUPB02_Add",
                "SUPB02/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SUPB02.Controllers" }
            );
        }
    }
}