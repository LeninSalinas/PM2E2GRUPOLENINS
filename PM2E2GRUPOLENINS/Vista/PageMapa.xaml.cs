using Android.Locations;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E2GRUPOLENINS.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMapa : ContentPage
    {
        public PageMapa()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        public async Task NavigateToBuilding25()
        {
            var location = new Xamarin.Essentials.Location(47.645160, -122.1306032);
            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

            await Xamarin.Essentials.Map.OpenAsync(location, options);
        }
    }
}