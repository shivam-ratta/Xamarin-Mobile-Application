using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigatePage : ContentPage
    {
        public NavigatePage()
        {
            InitializeComponent();
        }

        private async void MileageTracker_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewMileagePage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            double latitud = 40.765819;
            double longitud = -73.975866;
            string placeName = "Home";

            var supportsUri = await Launcher.CanOpenAsync("comgooglemaps://");

            if (supportsUri)
                await Launcher.OpenAsync($"comgooglemaps://?q={latitud},{longitud}({placeName})");
            else
                await Map.OpenAsync(40.765819, -73.975866, new MapLaunchOptions { Name = "Test" });
        }
    }
}