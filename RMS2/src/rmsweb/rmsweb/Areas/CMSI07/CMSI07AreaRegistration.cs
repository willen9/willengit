using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI07AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI07";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMSI07_default",
                "CMSI07",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"CMSI07.Controllers"}
                );

            context.MapRoute(
                "CMSI07_CUR",
                "CMSI07/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"CMSI07.Controllers"}
                );

            context.MapRoute(
                "CMSI07_Delete",
                "CMSI07/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"CMSI07.Controllers"}
                );

            context.MapRoute(
                "CMSI07_CURMID",
                "CMSI07/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"CMSI07.Controllers"}
                );

        }
    }
}