using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupportPage : ContentPage
    {
        public SupportPage()
        {
            InitializeComponent();
        }

        private void RateThisApp_Clicked(object sender, EventArgs e)
        {
            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(new Uri("pocketpro"));
                    break;

                case TargetPlatform.Android:
                    var rating = DependencyService.Get<IRateApplication>();
                    rating.Rate();
                    break;
            }
        }

        private void ContactSupport_Clicked(object sender, EventArgs e)
        {
            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(new Uri("mailto:support@exqsd.com"));
                    break;

                case TargetPlatform.Android:
                    var rating = DependencyService.Get<IRateApplication>();
                    rating.EmailSupport();
                    break;
            }
        }

        private void WatchTutorial_Clicked(object sender, EventArgs e)
        {

        }
    }
}