using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;
using FHSDK;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
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
            var socket = IO.Socket("http://localhost:8001/");
            socket.On("sessions", data =>
            {
                Dispatcher.Dispatch(() =>
                {
                    var session = JsonConvert.DeserializeObject<Session>((string) data);
                    Sessions.Add(session);
                });
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

