using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;
using System.Collections.ObjectModel;
using PointingHenry10.Services.PokerServices;

namespace PointingHenry10.ViewModels
{
    public class DetailSessionViewModel : ViewModelBase
    {
        public Session SelectedSession;
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();
        private User _loggedUser;

        public DetailSessionViewModel()
        {
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
            var dict = parameter as Dictionary<string, object>;
            var session = dict?["session"] as Session;
            var user = dict?["user"] as User;
            if (session != null)
            {
                SelectedSession = session;
                Users.Clear();
                SelectedSession.Users.ForEach(item => Users.Add(item));
            }
            if (user != null)
            {
                _loggedUser = user;
            }
            await Task.CompletedTask;
        }

        public void ShareSession() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);


        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
    }
}