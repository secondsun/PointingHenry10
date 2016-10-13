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
            var socket = IO.Socket("https://cloudappdefa7pmf.osm3.feedhenry.net/");
            socket.On("sessions", (data) =>
            {
                Sessions.Add((Session) data);
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
                sessions.ToList().ForEach(item => Sessions.Add(item));
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

        public void GotoJoinDetailSession(Session session)
        {
            NavigationService.Navigate(typeof(Views.DetailSession), session);
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

