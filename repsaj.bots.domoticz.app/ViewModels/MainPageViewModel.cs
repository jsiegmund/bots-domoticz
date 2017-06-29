using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Repsaj.Bots.Domoticz.Logic.ApiConnector;
using Repsaj.Bots.Domoticz.App.Logic.Helpers;
using Repsaj.Bots.Domoticz.App.Logic.Logging;
using Repsaj.Bots.Domoticz.App.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Repsaj.Bots.Domoticz.App.ViewModels
{
    public class MainPageViewModel : NotificationBase
    {
        private IApiConnector _apiConnector;
        private IDialogService _dialogService;
        private INavigationService _navigationService;

        string _command;
        public string Command
        {
            get { return _command; }
            set { SetProperty(ref _command, value); }
        }

        TrulyObservableCollection<LightSwitchViewModel> _lightsSwitches = new TrulyObservableCollection<LightSwitchViewModel>();
        public TrulyObservableCollection<LightSwitchViewModel> LightsSwitches
        {
            get { return _lightsSwitches; }
            set { SetProperty(ref _lightsSwitches, value); }
        }

        TrulyObservableCollection<SceneViewModel> _scenes = new TrulyObservableCollection<SceneViewModel>();
        public TrulyObservableCollection<SceneViewModel> Scenes
        {
            get { return _scenes; }
            set { SetProperty(ref _scenes, value); }
        }

        #region Commands
        private ICommand _runCommand;
        public ICommand RunCommand
        {
            get
            {
                return _runCommand ??
                    (_runCommand = new RelayCommand(async () =>
                    {
                        await ProcessCommand();
                    }));
            }
        }
        #endregion

        public MainPageViewModel(
            IApiConnector apiConnector,
            IDialogService dialogService,
            INavigationService navigationService)
        {
            _apiConnector = apiConnector;
            _navigationService = navigationService;
            _dialogService = dialogService;

            Initialize();
        }

        public MainPageViewModel() : this(
            new Design.ApiConnector(),
            new DialogService(),
            new NavigationService())
        {
#if DEBUG
            if (ViewModelBase.IsInDesignModeStatic)
            {
                Initialize();
            }
#endif
        }

        private async Task Initialize()
        {
            try
            {
                await RefreshLightsSwitches();
                await RefreshScenes();
            }
            catch (Exception ex)
            {
                LogEventSource.Log.Error($"Failure initializing the Main Page View Model: {ex}");
            }
        }

        private async Task RefreshLightsSwitches()
        {
            var lightsSwitches = await _apiConnector.GetLightSwitches();

            foreach (var lightSwitch in lightsSwitches)
            {
                var viewModel = new LightSwitchViewModel(lightSwitch);

                if (!_lightsSwitches.Any(ls => ls.Name == viewModel.Name))
                {
                    _lightsSwitches.Add(viewModel);
                }
            }

            RaisePropertyChanged(nameof(LightsSwitches));
        }

        private async Task RefreshScenes()
        {
            var scenes = await _apiConnector.GetScenes();

            foreach (var scene in scenes)
            {
                var viewModel = new SceneViewModel(scene);

                if (!_scenes.Any(ls => ls.Name == viewModel.Name))
                {
                    _scenes.Add(viewModel);
                }
            }

            RaisePropertyChanged(nameof(LightsSwitches));
        }

        private Task ProcessCommand()
        {
            return _apiConnector.RunCommand(_command);
        }
    }
}
