using FHSDK;
using PointingHenry10.Models;
using Template10.Mvvm;

namespace PointingHenry10.ViewModels
{
    public class NewSessionViewModel: ViewModelBase
    {
        public Session Session { get; set; } = new Session();

        public async void SaveSession()
        {
            await FH.Cloud("poker", "POST", null, Session);
            NavigationService.Navigate(typeof(Views.DetailSession), Session);
        }
    }
}