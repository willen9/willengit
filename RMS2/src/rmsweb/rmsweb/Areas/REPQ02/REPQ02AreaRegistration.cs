using System.Web.Mvc;

namespace rmsweb
{
    public class REPQ02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "REPQ02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "REPQ02_default",
                "REPQ02",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"REPQ02.Controllers"}
                );

            context.MapRoute(
                "REPQ02_SearchPartLifetime",
                "REPQ02/SearchPartLifetime",
                new {action = "SearchPartLifetime", controller = "Main"},
                namespaces: new string[] {"REPQ02.Controllers"}
                );

            context.MapRoute(
                "REPQ02_SearchPartLifetimeCover",
                "REPQ02/SearchPartLifetimeCover",
                new {action = "SearchPartLifetimeCover", controller = "Main"},
                namespaces: new string[] {"REPQ02.Controllers"}
                );
        }
    }
}