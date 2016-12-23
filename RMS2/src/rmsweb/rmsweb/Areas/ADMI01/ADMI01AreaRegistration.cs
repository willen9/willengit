using System.Web.Mvc;

namespace rmsweb
{
    public class ADMI01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ADMI01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "ADMI01_default",
               "ADMI01",
               new { action = "Index", controller = "Main" },
               namespaces: new string[] { "ADMI01.Controllers" }
           );

            context.MapRoute(
                 "ADMI01_CUR",
                 "ADMI01/CUR",
                 new { action = "CUR", controller = "Main" },
                 namespaces: new string[] { "ADMI01.Controllers" }
            );

            context.MapRoute(
               "ADMI01_Delete",
               "ADMI01/Delete",
               new { action = "Delete", controller = "Main" },
               namespaces: new string[] { "ADMI01.Controllers" }
            );
        }
    }
}