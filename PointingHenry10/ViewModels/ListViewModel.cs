using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;

namespace PointingHenry10.ViewModels
{
    public class ListViewModel : ViewModelBase
    {
        public List<Session> Sessions;
        public ListViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
            }
            Sessions = new List<Session>();
            Sessions.Add(new Session() { Name = "Session1", CreatedBy = new User() { Name = "Passos" } });
            Sessions.Add(new Session() { Name = "Session2", CreatedBy = new User() { Name = "Summers" } });
            Sessions.Add(new Session() { Name = "Session3", CreatedBy = new User { Name = "Julio" } });
        }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                //                Value = suspensionState[nameof(Value)]?.ToString();
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

        public void GotoJoinDetailSession(Session session)
        {
            NavigationService.Navigate(typeof(Views.DetailSession), session);
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

