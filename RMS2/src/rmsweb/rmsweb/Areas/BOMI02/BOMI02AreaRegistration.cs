using System.Web.Mvc;

namespace rmsweb
{
    public class BOMI02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BOMI02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BOMI02_default",
                "BOMI02",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                name: "BOMI02_CUR",
                url: "BOMI02/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                name: "BOMI02_Add",
                url: "BOMI02/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                name: "BOMI02_Exit",
                url: "BOMI02/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_delete",
                "BOMI02/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers"}
            );

            context.MapRoute(
                "BOMI02_Details",
                "BOMI02/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_Edit",
                "BOMI02/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_SearchBOMDInfo",
                "BOMI02/SearchBOMDInfo",
                new { action = "SearchBOMDInfo", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_SearchBOMHInfo",
                "BOMI02/SearchBOMHInfo",
                new { action = "SearchBOMHInfo", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_SearchBOMD",
                "BOMI02/SearchBOMD",
                new { action = "SearchBOMD", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_SearchBOMH",
                "BOMI02/SearchBOMH",
                new { action = "SearchBOMH", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_IsSubstitutes_H",
                "BOMI02/IsSubstitutes_H",
                new { action = "IsSubstitutes_H", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_IsBOM_D",
                "BOMI02/IsBOM_D",
                new { action = "IsBOM_D", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_Confirmed",
                "BOMI02/Confirmed",
                new { action = "Confirmed", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );

            context.MapRoute(
                "BOMI02_Sure",
                "BOMI02/Sure",
                new { action = "Sure", controller = "Main" },
                namespaces: new string[] { "BOMI02.Controllers" }
            );
        }
    }
}