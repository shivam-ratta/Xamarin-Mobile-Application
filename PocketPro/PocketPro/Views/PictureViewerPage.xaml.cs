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
    public partial class PictureViewer : ContentPage
    {
        public PictureViewer()
        {
            InitializeComponent();
        }
        public PictureViewer(ImageSource fileImageSource)
        {
            InitializeComponent();

            Image.Source = fileImageSource;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}