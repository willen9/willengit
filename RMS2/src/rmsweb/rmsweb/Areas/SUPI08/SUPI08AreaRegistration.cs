using System.Web.Mvc;

namespace rmsweb
{
    public class SUPI08AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPI08";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SUPI08_default",
                "SUPI08",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPI08.Controllers" }
            );

            context.MapRoute(
                "SUPI08_Edit",
                "SUPI08/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SUPI08.Controllers" }
            );

            context.MapRoute(
                "SUPI08_Exit",
                "SUPI08/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SUPI08.Controllers" }
            );
        }
    }
}