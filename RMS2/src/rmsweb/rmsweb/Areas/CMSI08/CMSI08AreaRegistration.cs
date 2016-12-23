using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI08AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI08";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMSI08_default",
                "CMSI08",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"CMSI08.Controllers"}
                );

            context.MapRoute(
                "CMSI08_Add",
                "CMSI08/Add",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"CMSI08.Controllers"}
                );

            context.MapRoute(
                "CMSI08_Delete",
                "CMSI08/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"CMSI08.Controllers"}
                );

            context.MapRoute(
                "CMSI08_Edit",
                "CMSI08/Edit",
                new {action = "Edit", controller = "Main"},
                namespaces: new string[] {"CMSI08.Controllers"}
                );

            context.MapRoute(
                "CMSI08_Details",
                "CMSI08/Details",
                new {action = "Details", controller = "Main"},
                namespaces: new string[] {"CMSI08.Controllers"}
                );

            context.MapRoute(
                "CMSI08_CURMID",
                "CMSI08/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"CMSI08.Controllers"}
                );

        }
    }
}