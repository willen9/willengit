using System.Web.Mvc;

namespace rmsweb
{
    public class REPQ05AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "REPQ05";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "REPQ05_default",
                "REPQ05",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "REPQ05.Controllers" }
            );

            context.MapRoute(
               "REPQ05_Details",
               "REPQ05/Details",
               new { action = "Details", controller = "Main" },
               namespaces: new string[] { "REPQ05.Controllers" }
           );

            context.MapRoute(
                name: "REPQ05_CUR",
                url: "REPQ05/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "REPQ05.Controllers" }
            );
        }
    }
}