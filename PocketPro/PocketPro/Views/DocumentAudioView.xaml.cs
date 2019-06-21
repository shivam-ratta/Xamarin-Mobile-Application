using MediaManager;
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
	public partial class DocumentAudioView : ContentView
	{
		public DocumentAudioView()
		{
			InitializeComponent ();

		    var viewModel = new DocumentAudioRecordingsViewModel();
		    BindingContext = viewModel = new DocumentAudioRecordingsViewModel();
		    viewModel.LoadItemCommand.Execute(null);
        }

        private void ImageButton_OnClicked(object sender, EventArgs e)
	    {

	    }

	    private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
            var media = e.SelectedItem as Media;
            await CrossMediaManager.Current.Play(media.Path);
            ((ListView)sender).SelectedItem = null;
        }  
	}
}