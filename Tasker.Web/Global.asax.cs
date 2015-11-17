using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Tasker.Common.Intefaces;
using Tasker.Common.Services;
using Tasker.Data.DAL;

namespace Tasker.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //autofac initialization
            IocInitializer();

            Database.SetInitializer(new TaskerDbInitializer());
        }

        private static void IocInitializer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //register types
            builder.RegisterType<TaskService>().As<ITaskService>();
            builder.RegisterType<TaskerDbContext>().AsSelf();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}