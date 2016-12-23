using System.Web.Mvc;

namespace rmsweb
{
    public class REPQ01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "REPQ01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "REPQ01_default",
                "REPQ01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "REPQ01.Controllers" }
            );

            context.MapRoute(
                "REPQ01_SearchPartLifetime",
                "REPQ01/SearchPartLifetime",
                new { action = "SearchPartLifetime", controller = "Main" },
                namespaces: new string[] { "REPQ01.Controllers" }
            );

            context.MapRoute(
                "REPQ01_SearchPartLifetimeCover",
                "REPQ01/SearchPartLifetimeCover",
                new { action = "SearchPartLifetimeCover", controller = "Main" },
                namespaces: new string[] { "REPQ01.Controllers" }
            );

            context.MapRoute(
                "REPQ01_DetailSum",
                "REPQ01/DetailSum",
                new { action = "DetailSum", controller = "Main" },
                namespaces: new string[] { "REPQ01.Controllers" }
            );

            context.MapRoute(
                "REPQ01_Detailw",
                "REPQ01/Detailw",
                new { action = "Detailw", controller = "Main" },
                namespaces: new string[] { "REPQ01.Controllers" }
            );

            context.MapRoute(
                "REPQ01_Detailc",
                "REPQ01/Detailc",
                new { action = "Detailc", controller = "Main" },
                namespaces: new string[] { "REPQ01.Controllers" }
            );
        }
    }
}