using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.DynamicProxy;
using Castle.Windsor;
using DocumentService.Repository;
using DocumentService.Interceptors;

namespace DocumentService.Installers
{
    public class RepositoryInstaller:IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().Where(type => typeof(IInterceptor).IsAssignableFrom(type)).LifestyleTransient());
            container.Register(Classes.FromThisAssembly().Where(type => typeof(IRepository).IsAssignableFrom(type)).WithServiceDefaultInterfaces().Configure(c => c.LifestyleTransient().Interceptors<AspectInterceptor>()));
        }
    }
}
