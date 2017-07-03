using Autofac;
using Microsoft.Practices.ServiceLocation;
using Repsaj.Bots.Domoticz.Bot.Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repsaj.Bots.Domoticz.Bot
{
    public class ServiceLocator
    {
        public ServiceLocator()
        {
            BuildContainer();
        }

        public IServiceLocator Instance { get; private set; }

        private void BuildContainer()
        {
            try
            {
                var builder = new Autofac.ContainerBuilder();

                // Register dependencies.
                builder.RegisterType<IntentHandler>().As<IIntentHandler>().InstancePerDependency();
                //builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();

                var container = builder.Build();

                // Create service locator.
                var csl = new Autofac.Extras.CommonServiceLocator.AutofacServiceLocator(container);

                // Set the service locator created.
                Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => csl);

                // Use the service locator.
                this.Instance = Microsoft.Practices.ServiceLocation.ServiceLocator.Current;
            } catch (Exception ex)
            {

            }
        }
    }
}