using System.Web.Mvc;

namespace rmsweb
{
    public class REPQ06AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "REPQ06";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "REPQ06_default",
                "REPQ06",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "REPQ06.Controllers" }
            );

            context.MapRoute(
                "REPQ06_CUR",
                "REPQ06/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "REPQ06.Controllers" }
            );

            context.MapRoute(
                "REPQ06_Detail",
                "REPQ06/Detail",
                new { action = "Detail", controller = "Main" },
                namespaces: new string[] { "REPQ06.Controllers" }
            );

        }
    }
}