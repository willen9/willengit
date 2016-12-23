using System.Web.Mvc;

namespace rmsweb
{
    public class SERB03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERB03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERB03_default",
                "SERB03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERB03.Controllers" }
            );

            context.MapRoute(
                "SERB03_Details",
                "SERB03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERB03.Controllers" }
            );

            context.MapRoute(
                "SERB03_CUR",
                "SERB03/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERB03.Controllers" }
            );

        }
    }
}