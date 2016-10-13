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
<<<<<<< HEAD
            var socket = IO.Socket("https://cloudappdefa7pmf.osm3.feedhenry.net/"); //TODO take it from FH.init properties (back from FH.init)
            socket.On("sessions", (data) =>
=======
            var socket = IO.Socket(FHConfig.GetInstance().GetHost());
            socket.On("sessions", data =>
>>>>>>> e165a367cfb34e7bd2cefe5081df66e8c489d0ae
            {
                Dispatcher.Dispatch(() =>
                {
                    var session = JsonConvert.DeserializeObject<Session>((string) data);
                    Sessions.Add(session);
                });
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

<<<<<<< HEAD
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
=======
        public void GotoJoinDetailSession(Session session) => 
            NavigationService.Navigate(typeof(DetailSession), session);
>>>>>>> e165a367cfb34e7bd2cefe5081df66e8c489d0ae

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);
    }
}