using System;
using Windows.Foundation;
using System.Collections.Generic;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PointingHenry10.ViewModels;
using PointingHenry10.Models;
using Windows.UI.Popups;
using Template10.Mvvm;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Newtonsoft.Json;
using Template10.Services.SerializationService;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PointingHenry10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class List : Page
    {
        public List()
        {
            this.InitializeComponent();
            listSessions.ItemsSource = this.ListViewModel.Sessions;
            textBox.Text = "";
        }

        private async void listSessions_ItemClick(object sender, ItemClickEventArgs e)
        {
            Session selectedItem = (Session)e.ClickedItem;
            if (textBox.Text == "")
            {
                var dialog = new MessageDialog($"Before entering session: {selectedItem.Name} you should enter a name.");
                await dialog.ShowAsync();
            }
            else
            {
                ListViewModel.GotoJoinDetailSession(selectedItem);
            }
        }
    }

}
