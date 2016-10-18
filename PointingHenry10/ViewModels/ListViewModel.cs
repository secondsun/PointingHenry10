using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using FHSDK;
using Newtonsoft.Json;
using PointingHenry10.Models;
using PointingHenry10.Views;
using Template10.Mvvm;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using FHSDK.FHHttpClient;
using PointingHenry10.Services.PokerServices;

namespace PointingHenry10.ViewModels
{
    public class ListViewModel : ViewModelBase
    {
        private readonly PokerService _pokerService = PokerService.Instance;
        public ListViewModel()
        {
            RetrieveListOfSessions();
            _pokerService.SessionEvent += (sender, args) =>
            {
                Dispatcher.Dispatch(() => Sessions.Add(args.Session));
            };
        }

        public ObservableCollection<Session> Sessions { get; } = new ObservableCollection<Session>();
        private User _loggedUser;

        private async void RetrieveListOfSessions()
        {
            Busy.SetBusy(true, "Getting active Sessions...");

            try
            {
                var sessions = await _pokerService.GetSessions();
                sessions.ForEach(item => Sessions.Add(item));
            }
            catch (FHException e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }

            Busy.SetBusy(false);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _loggedUser = (User)parameter;
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GotoJoinDetailSession(Session session)
        {
            try
            {
                var updatedSession = await _pokerService.JoinSession(session.Name, _loggedUser.Name);
                NavigationService.Navigate(typeof(DetailSession),
                    new Dictionary<string, object>() { { "session", updatedSession }, { "user", _loggedUser } });
            }
            catch (FHException e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);
    }
}