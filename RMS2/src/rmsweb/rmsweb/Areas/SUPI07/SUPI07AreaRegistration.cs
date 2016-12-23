using System.Web.Mvc;

namespace rmsweb
{
    public class SUPI07AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPI07";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SUPI07_default",
                "SUPI07",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPI07.Controllers" }
            );

            context.MapRoute(
                "SUPI07_Add",
                "SUPI07/Add",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SUPI07.Controllers" }
            );

            context.MapRoute(
                "SUPI07_SearchSupportAplAsign",
                "SUPI07/SearchSupportAplAsign",
                new { action = "SearchSupportAplAsign", controller = "Main" },
                namespaces: new string[] { "SUPI07.Controllers" }
            );

            context.MapRoute(
                "SUPI07_Edit",
                "SUPI07/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SUPI07.Controllers" }
            );

            context.MapRoute(
                "SUPI07_Details",
                "SUPI07/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SUPI07.Controllers" }
            );

            context.MapRoute(
                "SUPI07_Delete",
                "SUPI07/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SUPI07.Controllers" }
            );
        }
    }
}