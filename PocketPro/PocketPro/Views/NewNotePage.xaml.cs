using PocketPro.Models;
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
    public partial class NewNotePage : ContentPage
    {
        public NewNotePage()
        {
            InitializeComponent();
        }

        private async void SaveNoteButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<NewNotePage, Note>(this, "AddNote", new Note() {
                Title = Title.Text,
                Text = Text.Text
            });
            


            await Navigation.PopModalAsync();
        }

        private async void CancelNoteButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}