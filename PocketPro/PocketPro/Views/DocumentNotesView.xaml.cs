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
	public partial class DocumentNotesView : ContentView
	{
		public DocumentNotesView()
		{
			InitializeComponent ();

		    var viewModel = new DocumentNotesViewModel();
		    BindingContext = viewModel = new DocumentNotesViewModel();
		    viewModel.LoadItemCommand.Execute(null);
        }

        private void ImageButton_OnClicked(object sender, EventArgs e)
	    {
	        var note = (((ImageButton) sender).BindingContext) as Note;
	        note.isVisible = !note.isVisible;
	    }

	    private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem == null) return;    
	        var note = e.SelectedItem as Note;
	        note.isVisible = !note.isVisible;
            ((ListView) sender).SelectedItem = null;
	    }

	    private async void AddNewNote_OnClicked(object sender, EventArgs e)
	    {
            await Navigation.PushModalAsync(new NewNotePage());
	    } 
	}
}