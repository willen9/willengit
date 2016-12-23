using System.Web.Mvc;

namespace rmsweb
{
    public class ADMI06AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ADMI06";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ADMI06_default",
                "ADMI06",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"ADMI06.Controllers"}
                );

            context.MapRoute(
                "ADMI06_CURMID",
                "ADMI06/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"ADMI06.Controllers"}
                );

            context.MapRoute(
                "ADMI06_CUR",
                "ADMI06/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"ADMI06.Controllers"}
                );

            context.MapRoute(
                "ADMI06_Delete",
                "ADMI06/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"ADMI06.Controllers"}
                );

            context.MapRoute(
                "ADMI06_CURID",
                "ADMI06/CURID",
                new {action = "CURID", controller = "Main"},
                namespaces: new string[] {"ADMI06.Controllers"}
                );
        }
    }
}