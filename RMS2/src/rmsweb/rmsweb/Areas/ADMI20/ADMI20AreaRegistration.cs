using System.Web.Mvc;

namespace rmsweb
{
    public class ADMI20AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ADMI20";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "ADMI20_default",
               "ADMI20",
               new { action = "Index", controller = "Main" },
               namespaces: new string[] { "ADMI20.Controllers" }
           );
        }
    }
}