using System.Web.Mvc;

namespace rmsweb
{
    public class CONI02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CONI02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CONI02_default",
                "CONI02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_CUR",
                "CONI02/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_Add",
                "CONI02/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_Exit",
                "CONI02/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchOrderType",
                "CONI02/CUR/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_GetOrderTypeName",
                "CONI02/GetOrderTypeName",
                new { action = "GetOrderTypeName", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchCustomer",
                "CONI02/CUR/SearchCustomer",
                new { action = "SearchCustomer", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchCustomerInfo",
                "CONI02/SearchCustomerInfo",
                new { action = "SearchCustomerInfo", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchCustomerContact",
                "CONI02/SearchCustomerContact",
                new { action = "SearchCustomerContact", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchVendorContact",
                "CONI02/SearchVendorContact",
                new { action = "SearchVendorContact", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchCustomerAddress",
                "CONI02/SearchCustomerAddress",
                new { action = "SearchCustomerAddress", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchAddressInfo",
                "CONI02/SearchAddressInfo",
                new { action = "SearchAddressInfo", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchContractProduct",
                "CONI02/SearchContractProduct",
                new { action = "SearchContractProduct", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchContactInfo",
                "CONI02/SearchContactInfo",
                new { action = "SearchContactInfo", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
               "CONI02_Edit",
               "CONI02/Edit",
               new { action = "Edit", controller = "Main" },
               namespaces: new string[] { "CONI02.Controllers" }
           );

            context.MapRoute(
               "CONI02_Details",
               "CONI02/Details",
               new { action = "Details", controller = "Main" },
               namespaces: new string[] { "CONI02.Controllers" }
           );

            context.MapRoute(
               "CONI02_Delete",
               "CONI02/Delete",
               new { action = "Delete", controller = "Main" },
               namespaces: new string[] { "CONI02.Controllers" }
           );

            context.MapRoute(
               "CONI02_Confirmed",
               "CONI02/Confirmed",
               new { action = "Confirmed", controller = "Main" },
               namespaces: new string[] { "CONI02.Controllers" }
           );

            context.MapRoute(
                "CONI02_SearchProduct",
                "CONI02/SearchProduct",
                new { action = "SearchProduct", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_GetProductName",
                "CONI02/GetProductName",
                new { action = "GetProductName", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchContract",
                "CONI02/SearchContract",
                new { action = "SearchContract", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_SearchContractSerial",
                "CONI02/SearchContractSerial",
                new { action = "SearchContractSerial", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );

            context.MapRoute(
                "CONI02_Tree",
                "CONI02/Tree",
                new { action = "Tree", controller = "Main" },
                namespaces: new string[] { "CONI02.Controllers" }
            );
        }
    }
}