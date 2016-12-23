using System.Web.Mvc;

namespace rmsweb
{
    public class CONI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CONI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CONI03_default",
                "CONI03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "CONI03.Controllers" }
            );

            context.MapRoute(
                "CONI03_CUR",
                "CONI03/CUR",
                new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "CONI03.Controllers" }
            );

            context.MapRoute(
                "CONI03_SearchContract",
                "CONI03/CUR/SearchContract",
                new { action = "SearchContract", controller = "Main" },
                namespaces: new string[] { "CONI03.Controllers" }
            );

            context.MapRoute(
                "CONI03_SearchContractInfo",
                "CONI03/CUR/SearchContractInfo",
                new { action = "SearchContractInfo", controller = "Main" },
                namespaces: new string[] { "CONI03.Controllers" }
            );

            context.MapRoute(
                "CONI03_SearchContractProductDInfo",
                "CONI03/CUR/SearchContractProductDInfo",
                new { action = "SearchContractProductDInfo", controller = "Main" },
                namespaces: new string[] { "CONI03.Controllers" }
            );

            context.MapRoute(
                "CONI03_Add",
                "CONI03/Add",
                new { action = "Add", controller = "Main" },
                namespaces: new string[] { "CONI03.Controllers" }
            );

            context.MapRoute(
                "CONI03_Exit",
                "CONI03/Exit",
                new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "CONI03.Controllers" }
            );

            context.MapRoute(
               "CONI03_Delete",
               "CONI03/Delete/{ContractType}/{ContractNo}/{Version}",
               new { action = "Delete", controller = "Main" },
               namespaces: new string[] { "CONI03.Controllers" }
           );

            context.MapRoute(
               "CONI03_Edit",
               "CONI03/Edit/{ContractType}/{ContractNo}/{Version}",
               new { action = "Edit", controller = "Main" },
               namespaces: new string[] { "CONI03.Controllers" }
           );

            context.MapRoute(
               "CONI03_Details",
               "CONI03/Details/{ContractType}/{ContractNo}/{Version}",
               new { action = "Details", controller = "Main" },
               namespaces: new string[] { "CONI03.Controllers" }
           );

            context.MapRoute(
               "CONI03_Confirmed",
               "CONI03/Confirmed/{ContractType}/{ContractNo}/{Version}",
               new { action = "Confirmed", controller = "Main" },
               namespaces: new string[] { "CONI03.Controllers" }
           );

        }
    }
}