using System.Web.Mvc;

namespace rmsweb
{
    public class CMSI01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMSI01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CMSI01_default",
                "CMSI01",
                new { action = "Index", controller = "Main" },
                namespaces: new string[] { "CMSI01.Controllers" }
            );

            context.MapRoute(
                "CMSI01_SaveCompany",
                "CMSI01/SaveCompany",
                new { action = "SaveCompany", controller = "Main" },
                namespaces: new string[] { "CMSI01.Controllers" }
            );
        }
    }
}