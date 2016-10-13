using System;
using PointingHenry10.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

namespace PointingHenry10.Views
{
    public sealed partial class DetailSession : Page
    {

        public DetailSession()
        {
            InitializeComponent();
            listUsers.ItemsSource = DetailSessionViewModel.Users;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
    }
}
