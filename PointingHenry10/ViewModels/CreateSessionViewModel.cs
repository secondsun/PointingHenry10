using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;

namespace PointingHenry10.ViewModels
{
    class CreateSessionViewModel : ViewModelBase
    {
        public Session Session;

        public CreateSessionViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
            }
            Session = new Session();
            Session.Name = "default";
        }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                //                Value = suspensionState[nameof(Value)]?.ToString();
                
            }
            //Session = new Session();
            Session.Name = "wwwwwwww";// parameter["sessionName"];
            // TODO cloud call to create session

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
