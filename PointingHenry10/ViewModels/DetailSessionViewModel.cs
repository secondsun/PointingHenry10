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
using FHSDK;
using FHSDK.Config;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using PointingHenry10.Services;
using PointingHenry10.Services.PokerServices;

namespace PointingHenry10.ViewModels
{
    public class DetailSessionViewModel : ViewModelBase
    {
        public Session SelectedSession;
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();
        public User LoggedUser;

        public DetailSessionViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
            }
            SelectedSession = new Session();

            PokerService.Instance.SessionUpdatedEvent += (sender, args) =>
                Dispatcher.Dispatch(() =>
                {
                    SelectedSession.Users.Add(args.User);
                    Users.Add(args.User);
                });
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode,
            IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
            }
            var dict = parameter as Dictionary<string, object>;
            var session = dict["session"] as Session;
            var user = dict["user"] as User;
            if (session != null)
            {
                SelectedSession = session;
                Users.Clear();
                SelectedSession.Users.ForEach(item => Users.Add(item));
            }
            if (user != null)
            {
                LoggedUser = user;
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