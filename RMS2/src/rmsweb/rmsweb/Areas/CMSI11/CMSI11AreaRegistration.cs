using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI11AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI11";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "CMSI11_default",
               "CMSI11",
               new { action = "Index", controller = "Main" },
               namespaces: new string[] { "CMSI11.Controllers" }
           );

            context.MapRoute(
                 "CMSI11_CUR",
                 "CMSI11/CUR",
                 new { action = "CUR", controller = "Main" },
                 namespaces: new string[] { "CMSI11.Controllers" }
            );

            context.MapRoute(
                 "CMSI11_IsPositionNo",
                 "CMSI11/IsPositionNo",
                 new { action = "IsPositionNo", controller = "Main" },
                 namespaces: new string[] { "CMSI11.Controllers" }
            );

            context.MapRoute(
                 "CMSI11_Add",
                 "CMSI11/Add",
                 new { action = "Add", controller = "Main" },
                 namespaces: new string[] { "CMSI11.Controllers" }
            );

            context.MapRoute(
                 "CMSI11_Exit",
                 "CMSI11/Exit",
                 new { action = "Exit", controller = "Main" },
                 namespaces: new string[] { "CMSI11.Controllers" }
            );

            context.MapRoute(
                 "CMSI11_Edit",
                 "CMSI11/Edit",
                 new { action = "Edit", controller = "Main" },
                 namespaces: new string[] { "CMSI11.Controllers" }
            );

            context.MapRoute(
                 "CMSI11_Details",
                 "CMSI11/Details",
                 new { action = "Details", controller = "Main" },
                 namespaces: new string[] { "CMSI11.Controllers" }
            );

            context.MapRoute(
               "CMSI11_Delete",
               "CMSI11/Delete",
               new { action = "Delete", controller = "Main" },
               namespaces: new string[] { "CMSI11.Controllers" }
            );

            context.MapRoute(
               "CMSI11_Copy",
               "CMSI11/Copy",
               new { action = "Copy", controller = "Main" },
               namespaces: new string[] { "CMSI11.Controllers" }
            );
        }
    }
}