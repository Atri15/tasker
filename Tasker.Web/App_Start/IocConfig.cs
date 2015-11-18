using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Tasker.Data.DAL;
using Tasker.Services.Intefaces;
using Tasker.Services.Services;

namespace Tasker.Web
{
    public class IocConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //register custom types
            builder.RegisterType<TaskService>().As<ITaskService>();
            builder.RegisterType<TaskerDbContext>().AsSelf();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}