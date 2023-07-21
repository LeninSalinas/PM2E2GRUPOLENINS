using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E2GRUPOLENINS.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListsSitios : ContentPage
    {
        public PageListsSitios()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Modelo.Sitio> sitio = new List<Modelo.Sitio>();
            sitio = await Controlador.SitioController.GetSitio();
            listsitio.ItemsSource = sitio;
        }
    }
}