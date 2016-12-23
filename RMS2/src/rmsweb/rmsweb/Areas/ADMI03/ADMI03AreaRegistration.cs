using System.Web.Mvc;

namespace rmsweb
{
    public class ADMI03AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ADMI03";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "ADMI03_default",
               "ADMI03",
               new { action = "Index", controller = "Main" },
               namespaces: new string[] { "ADMI03.Controllers" }
           );

            context.MapRoute(
                 "ADMI03_CUR",
                 "ADMI03/CUR",
                 new { action = "CUR", controller = "Main" },
                 namespaces: new string[] { "ADMI03.Controllers" }
            );

            context.MapRoute(
               "ADMI03_Delete",
               "ADMI03/Delete",
               new { action = "Delete", controller = "Main" },
               namespaces: new string[] { "ADMI03.Controllers" }
            );
        }
    }
}