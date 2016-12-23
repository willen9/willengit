using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI02AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CMSI02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMSI02_default",
                "CMSI02",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"CMSI02.Controllers"}
                );

            context.MapRoute(
                "CMSI02_CUR",
                "CMSI02/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"CMSI02.Controllers"}
                );

            context.MapRoute(
                "CMSI02_Delete",
                "CMSI02/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"CMSI02.Controllers"}
                );

            context.MapRoute(
                "CMSI02_CURMID",
                "CMSI02/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"CMSI02.Controllers"}
                );

        }
    }
}