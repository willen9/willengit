using System.Web.Mvc;

namespace rmsweb
{
    public class REPQ04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "REPQ04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "REPQ04_default",
                "REPQ04",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"REPQ04.Controllers"}
                );

            context.MapRoute(
                "REPQ04_CUR",
                "REPQ04/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] { "REPQ04.Controllers" }
                );

            context.MapRoute(
                "REPQ04_DetailsP",
                "REPQ04/DetailsP",
                new { action = "DetailsP", controller = "Main" },
                namespaces: new string[] { "REPQ04.Controllers" }
                );

            context.MapRoute(
               "REPQ04_DetailsC",
               "REPQ04/DetailsC",
               new { action = "DetailsC", controller = "Main" },
               namespaces: new string[] { "REPQ04.Controllers" }
               );
        }
    }
}