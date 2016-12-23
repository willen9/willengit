using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI06AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI06";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CMSI06_default",
                "CMSI06",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "CMSI06.Controllers" }
            );

            context.MapRoute(
                "CMSI06_GetEventsByMonth",
                "CMSI06/GetEventsByMonth",
                new { action = "GetEventsByMonth", controller = "Main" },
                namespaces: new string[] { "CMSI06.Controllers" }
                );

            context.MapRoute(
                "CMSI06_DelEvents",
                "CMSI06/DelEvents",
                new { action = "DelEvents", controller = "Main" },
                namespaces: new string[] { "CMSI06.Controllers" }
                );
        }
    }
}