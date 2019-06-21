using PocketPro.Models;
using PocketPro.ViewModels;
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
    public partial class DocumentVideosView : ContentView
    {
        public DocumentVideosViewModel viewModel;
        public DocumentVideosView()
        {
            InitializeComponent();
             
            BindingContext = viewModel = new DocumentVideosViewModel();
            viewModel.LoadItemCommand.Execute(null);
        }

        private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var media = ((Image)sender).BindingContext as Media;

            await Navigation.PushModalAsync(new VideoViewerPage(media.Path));
        }
    }
}