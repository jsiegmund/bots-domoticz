using Autofac;
using Autofac.Extras.CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Repsaj.Bots.Domoticz.App.Logic.ApiConnector;
using Repsaj.Bots.Domoticz.App.Logic.Logging;
using Repsaj.Bots.Domoticz.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Register types 
            builder.RegisterType<Services.DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<ApiConnector>().As<IApiConnector>().SingleInstance();

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
