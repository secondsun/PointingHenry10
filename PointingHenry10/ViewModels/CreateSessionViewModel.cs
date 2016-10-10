using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;
using System.ComponentModel;

namespace PointingHenry10.ViewModels
{
    class CreateSessionViewModel : ViewModelBase
    {
        public Session SelectedSession;

        public CreateSessionViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
            }
            SelectedSession = new Session();
            SelectedSession.Name = "default";
        }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                
            }

            var dict = parameter as Dictionary<string, string>;
            if (dict.Count != 0)
            {
                var sessionName = dict["session"];
                var nickName = dict["nick"];
                SelectedSession.Name = $"{sessionName} created by {nickName}";
            }
            else
            {
                SelectedSession.Name = "default";
            }
            

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                //suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void ShareSession() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);


        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);


    }
}
