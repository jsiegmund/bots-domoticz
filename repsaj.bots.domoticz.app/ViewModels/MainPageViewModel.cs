using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Repsaj.Bots.Domoticz.Logic.Domoticz;
using Repsaj.Bots.Domoticz.Logic.Helpers;
using Repsaj.Bots.Domoticz.Logic.Logging;
using Repsaj.Bots.Domoticz.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Repsaj.Bots.Domoticz.Logic.RequestHandler;
using Microsoft.Practices.ServiceLocation;

namespace Repsaj.Bots.Domoticz.App.ViewModels
{
    public class MainPageViewModel : NotificationBase
    {
        private IDomoticzManager _domoticzManager;
        private IRequestHandler _requestHandler;
        private IDialogService _dialogService;
        private INavigationService _navigationService;
        private IDomoticzSettingsService _settingsService;

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
            IDomoticzManager domoticzManager,
            IRequestHandler requestHandler,
            IDialogService dialogService,
            INavigationService navigationService,
            IDomoticzSettingsService settingsService)
        {
            _domoticzManager = domoticzManager;
            _requestHandler = requestHandler;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _settingsService = settingsService;

            Initialize();
        }

        public MainPageViewModel() : this(
            new Design.DomoticzManager(),
            new Design.RequestHandler(),
            new DialogService(),
            new NavigationService(),
            new DomoticzSettingsService())
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
                LoadSettings();

                await RefreshLightsSwitches();
                await RefreshScenes();
            }
            catch (Exception ex)
            {
                LogEventSource.Log.Error($"Failure initializing the Main Page View Model: {ex}");
            }
        }

        private void LoadSettings()
        {
            // initialize the settings from the application data store
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            _settingsService.FromDictionary(localSettings.Values);
        }

        private async Task SaveSetting()
        {

        }

        private async Task RefreshLightsSwitches()
        {
            var lightsSwitches = await _domoticzManager.GetLightSwitches();

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
            var scenes = await _domoticzManager.GetScenes();

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
            return _requestHandler.HandleIncomingRequest(_command);
        }
    }
}
