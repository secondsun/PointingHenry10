using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PointingHenry10.ViewModels;
using Windows.UI.Xaml.Navigation;
using PointingHenry10.Models;
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
            List<Session> items = new List<Session>();
            items.Add(new Session() { Name = "Session1", CreatedBy = "Passos" });
            items.Add(new Session() { Name = "Session2", CreatedBy = "Summers" });
            items.Add(new Session() { Name = "Session3", CreatedBy = "Julio" });
            listSessions.ItemsSource = items;
        }
    }

}
