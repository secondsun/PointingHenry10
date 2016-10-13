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
            await FHClient.Init();
            var response = await FH.Cloud("poker", "GET", null, null);
            if (response.Error == null)
            {
                var sessions = JsonConvert.DeserializeObject<List<Session>>(response.RawResponse);
                sessions.ToList().ForEach(item => Sessions.Add(item));
            }
            else
            {
                await new MessageDialog(response.Error.Message).ShowAsync();
            }
        }

        public void GotoJoinDetailSession(Session session) => 
            NavigationService.Navigate(typeof(DetailSession), session);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);
    }
}