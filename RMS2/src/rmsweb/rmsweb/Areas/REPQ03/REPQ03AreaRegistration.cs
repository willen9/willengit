using System.Web.Mvc;

namespace rmsweb
{
    public class REPQ03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "REPQ03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "REPQ03_default",
                "REPQ03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "REPQ03.Controllers" }
            );

            context.MapRoute(
            "REPQ03_CUR",
            "REPQ03/CUR",
            new { action = "CUR", controller = "Main" },
            namespaces: new string[] { "REPQ03.Controllers" }
            );

            context.MapRoute(
                "REPQ03_DetailsP",
                "REPQ03/DetailsP",
                new { action = "DetailsP", controller = "Main" },
                namespaces: new string[] { "REPQ03.Controllers" }
                );

            context.MapRoute(
               "REPQ03_DetailsC",
               "REPQ03/DetailsC",
               new { action = "DetailsC", controller = "Main" },
               namespaces: new string[] { "REPQ03.Controllers" }
               );
        }
    }
}