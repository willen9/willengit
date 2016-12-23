using System.Web.Mvc;

namespace rmsweb
{
    public class ADMI05AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "ADMI05"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ADMI05_default",
                "ADMI05",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"ADMI05.Controllers"}
                );

            context.MapRoute(
                "ADMI05_CUR",
                "ADMI05/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"ADMI05.Controllers"}
                );

            context.MapRoute(
                "ADMI05_Delete",
                "ADMI05/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"ADMI05.Controllers"}
                );

            context.MapRoute(
                "ADMI05_CURID",
                "ADMI05/CURID",
                new {action = "CURID", controller = "Main"},
                namespaces: new string[] {"ADMI05.Controllers"}
                );
            context.MapRoute(
                "ADMI05_CURMID",
                "ADMI05/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"ADMI05.Controllers"}
                );
            context.MapRoute(
                "ADMI05_ADD",
                "ADMI05/ADD",
                new {action = "ADD", controller = "Main"},
                namespaces: new string[] {"ADMI05.Controllers"}
                );
            context.MapRoute(
                "ADMI05_Edit",
                "ADMI05/Edit",
                new {action = "Edit", controller = "Main"},
                namespaces: new string[] {"ADMI05.Controllers"}
                );
        }
    }
}