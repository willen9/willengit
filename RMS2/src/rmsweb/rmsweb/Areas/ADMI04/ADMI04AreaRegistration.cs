using System.Web.Mvc;

namespace rmsweb
{
    public class ADMI04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ADMI04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ADMI04_default",
                "ADMI04",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"ADMI04.Controllers"}
                );

            context.MapRoute(
                "ADMI04_CURMID",
                "ADMI04/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"ADMI04.Controllers"}
                );

            context.MapRoute(
                "ADMI04_CUR",
                "ADMI04/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"ADMI04.Controllers"}
                );

            context.MapRoute(
                "ADMI04_Delete",
                "ADMI04/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"ADMI04.Controllers"}
                );

            context.MapRoute(
                "ADMI04_ADD",
                "ADMI04/ADD",
                new {action = "ADD", controller = "Main"},
                namespaces: new string[] {"ADMI04.Controllers"}
                );
            context.MapRoute(
                "ADMI04_Edit",
                "ADMI04/Edit",
                new {action = "Edit", controller = "Main"},
                namespaces: new string[] {"ADMI04.Controllers"}
                );
            context.MapRoute(
                "ADMI04_EditDetails",
                "ADMI04/EditDetails",
                new {action = "EditDetails", controller = "Main"},
                namespaces: new string[] {"ADMI04.Controllers"}
                );
        }
    }
}