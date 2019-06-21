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
	public partial class DocumentPicturesView : ContentView
	{
		public DocumentPicturesView()
		{
			InitializeComponent ();

		    var viewModel = new DocumentPicturesViewModel();
		    BindingContext = viewModel = new DocumentPicturesViewModel();
		    viewModel.LoadItemCommand.Execute(null);

        }

        private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var image = ((Image)sender);
            await Navigation.PushModalAsync(new PictureViewer(image.Source));
        }
    }
}