using System.Web.Mvc;

namespace rmsweb
{
    public class SUPI05AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPI05";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SUPI05_default",
                "SUPI05",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_CUR",
                "SUPI05/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_Add",
                "SUPI05/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_Exit",
                "SUPI05/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_Copy",
                "SUPI05/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_SearchOrderType",
                "SUPI05/CUR/SearchOrderType",
                new { action = "SearchOrderType", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            //context.MapRoute(
            //    "SUPI05_GetOrderTypeName",
            //    "SUPI05/GetOrderTypeName",
            //    new { action = "GetOrderTypeName", controller = "Main" },
            //    namespaces: new string[] { "SUPI05.Controllers" }
            //);

            context.MapRoute(
                "SUPI05_GetOrderTypeNo",
                "SUPI05/GetOrderTypeNo",
                new { action = "GetOrderTypeNo", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_SearchCustomer",
                "SUPI05/SearchCustomer",
                new { action = "SearchCustomer", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_SearchCustomerAddress",
                "SUPI05/SearchCustomerAddress",
                new { action = "SearchCustomerAddress", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_SearchDepartment",
                "SUPI05/SearchDepartment",
                new { action = "SearchDepartment", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_SearchCustomerContact",
                "SUPI05/SearchCustomerContact",
                new { action = "SearchCustomerContact", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_SearchSupportItem",
                "SUPI05/SearchSupportItem",
                new { action = "SearchSupportItem", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
                "SUPI05_SearchProduct",
                "SUPI05/SearchProduct",
                new { action = "SearchProduct", controller = "Main" },
                namespaces: new string[] { "SUPI05.Controllers" }
            );

            context.MapRoute(
               "SUPI05_Delete",
               "SUPI05/Delete",
               new { action = "Delete", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_Edit",
               "SUPI05/Edit",
               new { action = "Edit", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_Print",
               "SUPI05/Print",
               new { action = "Print", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_Confirmed",
               "SUPI05/Confirmed",
               new { action = "Confirmed", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_Details",
               "SUPI05/Details",
               new { action = "Details", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_CountSysc",
               "SUPI05/CountSysc",
               new { action = "CountSysc", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_Tree",
               "SUPI05/Tree",
               new { action = "Tree", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_UpFile",
               "SUPI05/UpFile",
               new { action = "UpFile", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_SearchFile",
               "SUPI05/SearchFile",
               new { action = "SearchFile", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_DelFile",
               "SUPI05/DelFile",
               new { action = "DelFile", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );

            context.MapRoute(
               "SUPI05_DownFile",
               "SUPI05/DownFile",
               new { action = "DownFile", controller = "Main" },
               namespaces: new string[] { "SUPI05.Controllers" }
           );
        }
    }
}