using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using FHSDK;
using FHSDK.Config;
using Newtonsoft.Json;
using PointingHenry10.Models;
using PointingHenry10.Views;
using Quobject.SocketIoClientDotNet.Client;
using Template10.Mvvm;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace PointingHenry10.ViewModels
{
    public class ListViewModel : ViewModelBase
    {
        public ListViewModel()
        {
            RetrieveListOfSessions();
            OpenWebsocketsConnection();
        }

        public ObservableCollection<Session> Sessions { get; } = new ObservableCollection<Session>();
        public User LoggedUser;
        private void OpenWebsocketsConnection()
        {

            var socket = IO.Socket(FHConfig.GetInstance().GetHost());
            socket.On("sessions", data =>
            {
                Dispatcher.Dispatch(() =>
                {
                    var session = JsonConvert.DeserializeObject<Session>((string) data);
                    Sessions.Add(session);
                });
            });
        }

        private async void RetrieveListOfSessions()
        {
            Busy.SetBusy(true, "Getting active Sessions...");

            var response = await FH.Cloud("poker", "GET", null, null);
            if (response.Error == null)
            {
                var sessions = JsonConvert.DeserializeObject<List<Session>>(response.RawResponse);
                sessions.ForEach(item => Sessions.Add(item));
            }
            else
            {
                await new MessageDialog(response.Error.Message).ShowAsync();
            }

            Busy.SetBusy(false);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            LoggedUser = (User)parameter;
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GotoJoinDetailSession(Session session)
        {
            var response = await FH.Cloud("poker/join", "POST", null, new Dictionary<string, string>() { { "session", session.Name}, { "user", LoggedUser.Name} });
            if (response.Error == null)
            {
                var updatedSession = JsonConvert.DeserializeObject<Session>(response.RawResponse);
                NavigationService.Navigate(typeof(Views.DetailSession), new Dictionary<string, object>() { { "session", updatedSession }, { "user", LoggedUser } });
            }
            else
            {
                // TODO display error msg
            }
            
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);
    }
}