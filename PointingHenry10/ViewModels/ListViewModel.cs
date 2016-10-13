using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;
using FHSDK;
using Newtonsoft;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Quobject.SocketIoClientDotNet.Client;

namespace PointingHenry10.ViewModels
{
    public class ListViewModel : ViewModelBase
    {
        public ObservableCollection<Session> Sessions;
        public ListViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
            }
            Sessions = new ObservableCollection<Session>();
            RetrieveListOfSessions();
            OpenWebsocketsConnection();
        }

        private void OpenWebsocketsConnection()
        {
            var socket = IO.Socket("https://cloudappdefa7pmf.osm3.feedhenry.net/"); //TODO take it from FH.init properties (back from FH.init)
            socket.On("sessions", (data) =>
            {
                Sessions.Add((Session) data);
            });
            socket.On("sessionUpdated", (data) =>
            {
               
                //var sessionUpdated = Sessions.ToList().Find(theSession => theSession.Name == ((User)data).Name);
               // if (sessionUpdated != null)
               // {
               //     sessionUpdated.Users = ((Session)data).Users;
               // }
            });
        }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                //                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        private async void RetrieveListOfSessions()
        {
            await FHClient.Init();
            var response = await FH.Cloud("poker", "GET", null, null);
            if (response.Error == null)
            {
                var sessions = JsonConvert.DeserializeObject<List<Session>>(response.RawResponse);
                sessions.ForEach(item => Sessions.Add(item));
            }
            else
            {
                // TODO display error msg
            }
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

        public async void GotoJoinDetailSession(Session session)
        {
            // todo ask erik where he stores logged in user
            var response = await FH.Cloud("poker/join", "POST", null, new Dictionary<string, string>() { { "session", session.Name}, { "user", "eeeeee"} });
            if (response.Error == null)
            {
                var updatedSession = JsonConvert.DeserializeObject<Session>(response.RawResponse);
                NavigationService.Navigate(typeof(Views.DetailSession), updatedSession);
            }
            else
            {
                // TODO display error msg
            }
            
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

