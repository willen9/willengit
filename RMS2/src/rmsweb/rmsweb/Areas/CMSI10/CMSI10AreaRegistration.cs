using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI10AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CMSI10";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMSI10_default",
                "CMSI10",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_CUR",
                "CMSI10/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_Delete",
                "CMSI10/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SearchAddress",
                "CMSI10/SearchAddress",
                new {action = "SearchAddress", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SearchContact",
                "CMSI10/SearchContact",
                new {action = "SearchContact", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SaveNewContact",
                "CMSI10/SaveNewContact",
                new {action = "SaveNewContact", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_DeleteContact",
                "CMSI10/DeleteContact",
                new {action = "DeleteContact", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SaveContact",
                "CMSI10/SaveContact",
                new {action = "SaveContact", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SearchContactInfoAll",
                "CMSI10/SearchContactInfoAll",
                new {action = "SearchContactInfoAll", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_GetContactBlur",
                "CMSI10/GetContactBlur",
                new {action = "GetContactBlur", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_DeleteAddress",
                "CMSI10/DeleteAddress",
                new {action = "DeleteAddress", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SaveAddress",
                "CMSI10/SaveAddress",
                new {action = "SaveAddress", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SaveNewAddress",
                "CMSI10/SaveNewAddress",
                new {action = "SaveNewAddress", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_SearchVendor",
                "CMSI10/SearchVendor",
                new {action = "SearchVendor", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );

            context.MapRoute(
                "CMSI10_CURMID",
                "CMSI10/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"CMSI10.Controllers"}
                );
        }
    }
}