using System.Web.Mvc;

namespace rmsweb
{
    public class SUPB03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SUPB03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SUPB03_default",
                "SUPB03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SUPB03.Controllers" }
            );
        }
    }
}