using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;
using PointingHenry10.Services.PokerServices;
using PointingHenry10.Views;
using Template10.Mvvm;

namespace PointingHenry10.ViewModels
{
    public class DetailSessionViewModel : ViewModelBase
    {
        private readonly PokerService _pokerService = PokerService.Instance;

        private ICommand _clickCommand;
        private User _loggedUser;

        public DetailSessionViewModel()
        {
            _pokerService.SessionUpdatedEvent += (sender, args) =>
                Dispatcher.Dispatch(() =>
                {
                    var found = SelectedSession.Users.FirstOrDefault(user => user.Name.Equals(args.User.Name));
                    if (found != null && args.User.Vote != null)
                    {
                        found.Vote = args.User.Vote;
                    }
                    else
                    {
                        SelectedSession.Users.Add(args.User);
                    }
                });
        }

        public Session SelectedSession { get; } = new Session { Users = new ObservableCollection<User>()};

        public ICommand VoteOnClick
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new DelegateCommand<string>(vote =>
                {
                    _loggedUser.Vote = vote;
                    _pokerService.CastVote(SelectedSession.Name, _loggedUser);
                }));
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode,
            IDictionary<string, object> suspensionState)
        {
            var dict = parameter as Dictionary<string, object>;
            var session = dict?["session"] as Session;
            var user = dict?["user"] as User;
            if (session != null)
            {
                var users = session.Users.ToList();
                SelectedSession.Name = session.Name;
                SelectedSession.CreatedBy = session.CreatedBy;
                SelectedSession.Users.Clear();
                users.ForEach(item => SelectedSession.Users.Add(item));
            }
            if (user != null)
            {
                _loggedUser = user;
            }
            await Task.CompletedTask;
        }

        public void ShareSession() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);


        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);
    }
}