using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using wmsapi.Models;
using wmsapi.Repositories;

namespace wmsapi
{
    public class AutofacConfig
    {
        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            //register controllers all at once using assembly scanning
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //register individual controlllers manually
            //builder.RegisterType<ValuesController>().InstancePerRequest();

            //register DbContext
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            builder.Register<WMSADMDbContext>(x => new WMSADMDbContext(connectionString)).InstancePerRequest();
            builder.RegisterType<WMSDbContext>().InstancePerRequest();

            //register repositories
            builder.RegisterType<CustomerRepository>().InstancePerRequest();
            builder.RegisterType<DepartmentRepository>().InstancePerRequest();
            builder.RegisterType<DocumentRepository>().InstancePerRequest();
            builder.RegisterType<MaterialRepository>().InstancePerRequest();
            builder.RegisterType<PlantRepository>().InstancePerRequest();
            builder.RegisterType<StoreLocationRepository>().InstancePerRequest();
            builder.RegisterType<StoreBinRepository>().InstancePerRequest();
            builder.RegisterType<SupplierRepository>().InstancePerRequest();
            builder.RegisterType<TransactionRepository>().InstancePerRequest();

            //register objects
            builder.RegisterType<APIHeader>().InstancePerRequest();
            builder.RegisterType<APIDatabaseConfiguration>().InstancePerRequest();

            //Create container
            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}