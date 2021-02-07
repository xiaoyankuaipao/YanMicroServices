using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace Yan.Admin.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public class WebModule: Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var controllerTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t)).ToArray();

            builder.RegisterTypes(controllerTypesInAssembly).PropertiesAutowired(new AutowiredPropertySelector());
        }
    }
}
