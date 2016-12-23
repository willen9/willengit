using System.Web.Mvc;

namespace rmsweb
{

    public class CMSI09AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CMSI09";
            }
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CMSI09_default",
                "CMSI09",
                new {action = "Index", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_CUR",
                "CMSI09/CUR",
                new {action = "CUR", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_Delete",
                "CMSI09/Delete",
                new {action = "Delete", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchCustomer",
                "CMSI09/SearchCustomer",
                new {action = "SearchCustomer", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchAddress",
                "CMSI09/SearchAddress",
                new {action = "SearchAddress", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchContact",
                "CMSI09/SearchContact",
                new {action = "SearchContact", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_DeleteContact",
                "CMSI09/DeleteContact",
                new {action = "DeleteContact", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SaveContact",
                "CMSI09/SaveContact",
                new {action = "SaveContact", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SaveNewContact",
                "CMSI09/SaveNewContact",
                new {action = "SaveNewContact", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_GetContactBlur",
                "CMSI09/GetContactBlur",
                new {action = "GetContactBlur", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SaveNewAddress",
                "CMSI09/SaveNewAddress",
                new {action = "SaveNewAddress", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_DeleteAddress",
                "CMSI09/DeleteAddress",
                new {action = "DeleteAddress", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SaveAddress",
                "CMSI09/SaveAddress",
                new {action = "SaveAddress", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchEmployeeInfoAll",
                "CMSI09/SearchEmployeeInfoAll",
                new {action = "SearchEmployeeInfoAll", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchEmployeeInfoDetail",
                "CMSI09/SearchEmployeeInfoDetail",
                new {action = "SearchEmployeeInfoDetail", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_GetSalesInfo",
                "CMSI09/GetSalesInfo",
                new {action = "GetSalesInfo", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchContactInfoAll",
                "CMSI09/SearchContactInfoAll",
                new {action = "SearchContactInfoAll", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchCurrencyInfoAll",
                "CMSI09/SearchCurrencyInfoAll",
                new {action = "SearchCurrencyInfoAll", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchCurrencyInfoDetail",
                "CMSI09/SearchCurrencyInfoDetail",
                new {action = "SearchCurrencyInfoDetail", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_GetCurrencyInfo",
                "CMSI09/GetCurrencyInfo",
                new {action = "GetCurrencyInfo", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_SearchCustomerInfo",
                "CMSI09/SearchCustomerInfo",
                new {action = "SearchCustomerInfo", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );

            context.MapRoute(
                "CMSI09_CURMID",
                "CMSI09/CURMID",
                new {action = "CURMID", controller = "Main"},
                namespaces: new string[] {"CMSI09.Controllers"}
                );
        }
    }
}