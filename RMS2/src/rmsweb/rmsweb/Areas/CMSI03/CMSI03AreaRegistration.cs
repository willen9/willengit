using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMSI03_default",
                "CMSI03",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"CMSI03.Controllers"}
                );

            context.MapRoute(
                "CMSI03_CUR",
                "CMSI03/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"CMSI03.Controllers"}
                );

            context.MapRoute(
                "CMSI03_Delete",
                "CMSI03/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"CMSI03.Controllers"}
                );

            context.MapRoute(
                "CMSI03_SearchDepartment",
                "CMSI03/SearchDepartment",
                new {action = "SearchDepartment", controller = "Main"},
                namespaces: new string[] {"CMSI03.Controllers"}
                );

            context.MapRoute(
                "CMSI03_CURMID",
                "CMSI03/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"CMSI03.Controllers"}
                );
        }
    }
}