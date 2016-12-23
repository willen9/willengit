using System.Web.Mvc;

namespace rmsweb
{
    public class SERI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SERI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SERI03_default",
                "SERI03",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                name: "SERI03_CUR",
                url: "SERI03/CUR",
                defaults: new { action = "CUR", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                name: "SERI03_Add",
                url: "SERI03/Add",
                defaults: new { action = "Add", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                name: "SERI03_Exit",
                url: "SERI03/Exit",
                defaults: new { action = "Exit", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                "SERI03_delete",
                "SERI03/Delete",
                new { action = "Delete", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers"}
            );

            context.MapRoute(
                "SERI03_Details",
                "SERI03/Details",
                new { action = "Details", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                "SERI03_Edit",
                "SERI03/Edit",
                new { action = "Edit", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                "SERI03_IsQuestionNo",
                "SERI03/IsQuestionNo",
                new { action = "IsQuestionNo", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                "SERI03_Show",
                "SERI03/Show",
                new { action = "Show", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

            context.MapRoute(
                "SERI03_SearchQuestionList",
                "SERI03/SearchQuestionList",
                new { action = "SearchQuestionList", controller = "Main" },
                namespaces: new string[] { "SERI03.Controllers" }
            );

        }
    }
}