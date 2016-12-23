using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI05AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI05";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CMSI05_default",
                "CMSI05",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_CUR",
                "CMSI05/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_ProductNo",
                "CMSI05/ProductNo",
                new { action = "ProductNo", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_GetBrandName",
                "CMSI05/GetBrandName",
                new { action = "GetBrandName", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_GetVendorName",
                "CMSI05/GetVendorName",
                new { action = "GetVendorName", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_GetProductType",
                "CMSI05/GetProductType",
                new { action = "GetProductType", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_Delete",
                "CMSI05/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_Edit",
                "CMSI05/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_Details",
                "CMSI05/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_SearchBrand",
                "CMSI05/Add/SearchBrand",
                new { action = "SearchBrand", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_SearchProdType",
                "CMSI05/Add/SearchProdType",
                new { action = "SearchProdType", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_SearchVendor",
                "CMSI05/Add/SearchVendor",
                new { action = "SearchVendor", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_Copy",
                "CMSI05/Copy",
                new { action = "Copy", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_Add",
                "CMSI05/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );

            context.MapRoute(
                "CMSI05_Exit",
                "CMSI05/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "CMSI05.Controllers" }
            );
        }
    }
}