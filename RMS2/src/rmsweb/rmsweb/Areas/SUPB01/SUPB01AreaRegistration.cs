using System.Web.Mvc;

namespace rmsweb
{
    public class SUPB01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPB01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SUPB01_default",
                "SUPB01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPB01.Controllers" }
            );

            context.MapRoute(
                "SUPB01_GetEmployeeName",
                "SUPB01/GetEmployeeName",
                new { action = "GetEmployeeName", controller = "Main" },
                namespaces: new string[] { "SUPB01.Controllers" }
            );

            context.MapRoute(
                "SUPB01_GetOrderTypeName",
                "SUPB01/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "SUPB01.Controllers" }
            );

            context.MapRoute(
                "SUPB01_GetOrderTypeNo",
                "SUPB01/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "SUPB01.Controllers" }
            );

            context.MapRoute(
                "SUPB01_SearchOrderType",
                "SUPB01/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SUPB01.Controllers" }
            );

            context.MapRoute(
                "SUPB01_SearchPositionEmployee",
                "SUPB01/SearchPositionEmployee",
                new { action = "SearchPositionEmployee", controller = "Main" },
                namespaces: new string[] { "SUPB01.Controllers" }
            );
        }
    }
}