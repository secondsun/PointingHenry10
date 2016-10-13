using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;

namespace PointingHenry10.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public User User { get; } = new User();

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(User)] = User.Name;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                User.Name = suspensionState[nameof(User)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public async void GotoJoinSession()
        {
            if (await IsValid())
            {
                NavigationService.Navigate(typeof(Views.List), "");
            }
        }

        public async void GotoCreateSession()
        {
            if (await IsValid())
            {
                NavigationService.Navigate(typeof(Views.NewSession), "");
            }
        }

        private async Task<bool> IsValid()
        {
            if (!string.IsNullOrEmpty(User.Name)) return true;
            await new MessageDialog("Please enter your name first.").ShowAsync();
            return false;
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
    }
}