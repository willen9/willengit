using System.Web.Mvc;

namespace rmsweb
{
    public class PMAB01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PMAB01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PMAB01_default",
                "PMAB01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "PMAB01.Controllers" }
            );

            context.MapRoute(
                "PMAB01_GetEmployeeName",
                "PMAB01/GetEmployeeName",
                new { action = "GetEmployeeName", controller = "Main" },
                namespaces: new string[] { "PMAB01.Controllers" }
            );

            context.MapRoute(
                "PMAB01_GetOrderTypeName",
                "PMAB01/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "PMAB01.Controllers" }
            );

            context.MapRoute(
                "PMAB01_GetOrderTypeNo",
                "PMAB01/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "PMAB01.Controllers" }
            );

            context.MapRoute(
                "PMAB01_SearchOrderType",
                "PMAB01/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "PMAB01.Controllers" }
            );

            context.MapRoute(
                "PMAB01_Details",
                "PMAB01/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "PMAB01.Controllers" }
            );
        }
    }
}