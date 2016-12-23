using System.Web.Mvc;

namespace rmsweb
{
    public class ADMI02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ADMI02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ADMI02_default",
                "ADMI02",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"ADMI02.Controllers"}
                );

            context.MapRoute(
                "ADMI02_Delete",
                "ADMI02/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"ADMI02.Controllers"}
                );

            context.MapRoute(
                "ADMI02_CUR",
                "ADMI02/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"ADMI02.Controllers"}
                );

            context.MapRoute(
                "ADMI02_ADD",
                "ADMI02/ADD",
                new {action = "ADD", controller = "Main"},
                namespaces: new string[] {"ADMI02.Controllers"}
                );

            context.MapRoute(
                "ADMI02_CURMID",
                "ADMI02/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"ADMI02.Controllers"}
                );
        }
    }
}