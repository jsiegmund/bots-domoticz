using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Repsaj.Bots.Domoticz.App.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowDialogAsync(string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            MessageDialog dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(buttonText));
            await dialog.ShowAsync();

            afterHideCallback.Invoke();
        }

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            MessageDialog dialog = new MessageDialog(error.Message);
            dialog.Commands.Add(new UICommand(buttonText));
            await dialog.ShowAsync();

            afterHideCallback.Invoke();
        }

        public async Task ShowMessage(string message, string title)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            MessageDialog dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand(buttonText));
            await dialog.ShowAsync();

            afterHideCallback.Invoke();
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            var cancelCommand = new UICommand(buttonCancelText);
            var confirmCommand = new UICommand(buttonConfirmText);

            MessageDialog dialog = new MessageDialog(message);
            dialog.CancelCommandIndex = 0;
            dialog.DefaultCommandIndex = 1;
            dialog.Commands.Add(cancelCommand);
            dialog.Commands.Add(confirmCommand);
            var resultCommand = await dialog.ShowAsync();

            bool result = resultCommand.Id == confirmCommand.Id;

            afterHideCallback.Invoke(result);

            return result;
        }

        public async Task ShowMessageBox(string message, string title)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
