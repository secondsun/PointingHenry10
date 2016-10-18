using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;
using PointingHenry10.Services.PokerServices;
using PointingHenry10.Views;
using Template10.Mvvm;

namespace PointingHenry10.ViewModels
{
    public class NewSessionViewModel: ViewModelBase
    {
        public Session Session { get; set; } = new Session();

        public async void SaveSession()
        {
            var session = await PokerService.Instance.CreateSession(Session);

            NavigationService.Navigate(typeof(DetailSession),
                new Dictionary<string, object>() { { "session", session }, { "user", Session.CreatedBy } });
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Session.CreatedBy = (User) parameter;
            return Task.CompletedTask;
        }
    }
}