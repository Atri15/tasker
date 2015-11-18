using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Tasker.Data.DAL;
using Tasker.Data.Interfaces;
using Tasker.Data.Services;

namespace Tasker.Web
{
    public class IocConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //register custom types
            builder.RegisterType<TaskerDbContext>().AsSelf();

            //register custom services
            builder.RegisterType<TaskService>().As<ITaskService>();
            builder.RegisterType<DateService>().As<IDateService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}