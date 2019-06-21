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
    public partial class SocialPage : ContentPage
    {
        public SocialPage()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
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

        private void ImageButton_Clicked_1(object sender, EventArgs e)
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

        private void ImageButton_Clicked_2(object sender, EventArgs e)
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
    }
}