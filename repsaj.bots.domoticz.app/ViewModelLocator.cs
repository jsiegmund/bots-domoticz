using Autofac;
using Autofac.Extras.CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Repsaj.Bots.Domoticz.Logic.Domoticz;
using Repsaj.Bots.Domoticz.Logic.Logging;
using Repsaj.Bots.Domoticz.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repsaj.Bots.Domoticz.Logic.RequestHandler;

namespace Repsaj.Bots.Domoticz.App
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            // Register the navigation service
            var nav = new NavigationService();
            builder.RegisterInstance<INavigationService>(nav);

            // Register the settings object
            var settings = new DomoticzSettingsService();
            builder.RegisterInstance<IDomoticzSettingsService>(settings);

            // Register types 
            builder.RegisterType<Services.DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<ApiConnector>().As<IApiConnector>().SingleInstance();
            builder.RegisterType<RequestHandler>().As<IRequestHandler>().SingleInstance();
            builder.RegisterType<DomoticzManager>().As<IDomoticzManager>().SingleInstance();

            // Register ViewModels
            builder.RegisterType<MainPageViewModel>().SingleInstance();

            try
            {
                // Perform registrations and build the container.
                _container = builder.Build();
            }
            catch (Exception ex)
            {
                LogEventSource.Log.Error($"Could not compile the Autofac container: {ex}");
            }

            AutofacServiceLocator csl = new AutofacServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainPageViewModel MainPageViewModel
        {
            get
            {
                return _container.Resolve<MainPageViewModel>();
            }
        }
    }
}
