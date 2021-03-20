using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Yan.Autofac;

namespace Yan.SystemService.API.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public class WebModule:Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var controllerTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t)).ToArray();

            //将控制器注入到容器，方便属性注入
            builder.RegisterTypes(controllerTypesInAssembly).PropertiesAutowired(new AutowiredPropertySelector());
        }
    }
}
