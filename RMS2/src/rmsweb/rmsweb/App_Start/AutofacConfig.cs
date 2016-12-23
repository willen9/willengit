using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace rmsweb
{
    public class AutofacConfig
    {
        public static void Register() {
            ContainerBuilder builder = new ContainerBuilder();


            //register individual controlllers manually.
            //builder.RegisterType<HomeController>().InstancePerRequest();

            //register objects
            //builder.RegisterType<APIHeader>().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}