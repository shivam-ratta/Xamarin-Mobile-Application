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
    public partial class DocumentsPage : ContentPage
    {
        private DocumentAudioView documentAudioView;
        private DocumentVideosView documentVideosView;
        private DocumentPicturesView documentPicturesView;
        private DocumentNotesView documentNotesView;

        public DocumentsPage()
        {
            InitializeComponent();
            
            ContentView.Content = new DocumentPicturesView();

            documentAudioView = new DocumentAudioView();
            documentVideosView = new DocumentVideosView();
            documentPicturesView = new DocumentPicturesView();
            documentNotesView = new DocumentNotesView();

            SetDefaultColors();

            PicturesButton.BorderColor = (Color)Application.Current.Resources["Primary"];
            PicturesButton.TextColor = (Color)Application.Current.Resources["Primary"];
        }

        private void SetDefaultColors()
        {
            PicturesButton.BorderColor = (Color)Application.Current.Resources["LightTextColor"];
            VideoButton.BorderColor = (Color)Application.Current.Resources["LightTextColor"];
            AudioButton.BorderColor = (Color)Application.Current.Resources["LightTextColor"];
            NotesButton.BorderColor = (Color)Application.Current.Resources["LightTextColor"];

            PicturesButton.TextColor = (Color)Application.Current.Resources["LightTextColor"];
            VideoButton.TextColor = (Color)Application.Current.Resources["LightTextColor"];
            AudioButton.TextColor = (Color)Application.Current.Resources["LightTextColor"];
            NotesButton.TextColor = (Color)Application.Current.Resources["LightTextColor"];
        }

        private void PicturesButton_OnClicked(object sender, EventArgs e)
        {
            ContentView.Content = documentPicturesView;

            SetDefaultColors();

            PicturesButton.BorderColor = (Color)Application.Current.Resources["Primary"];
            PicturesButton.TextColor = (Color)Application.Current.Resources["Primary"];
        }

        private void VideoButton_OnClicked(object sender, EventArgs e)
        {
            ContentView.Content = documentVideosView;

            SetDefaultColors();

            VideoButton.BorderColor = (Color)Application.Current.Resources["Primary"];
            VideoButton.TextColor = (Color)Application.Current.Resources["Primary"];
        }

        private void AudioButton_OnClicked(object sender, EventArgs e)
        {
            ContentView.Content = documentAudioView;

            SetDefaultColors();

            AudioButton.BorderColor = (Color)Application.Current.Resources["Primary"];
            AudioButton.TextColor = (Color)Application.Current.Resources["Primary"];
        }

        private void NotesButton_OnClicked(object sender, EventArgs e)
        {
            ContentView.Content = documentNotesView;

            SetDefaultColors();

            NotesButton.BorderColor = (Color)Application.Current.Resources["Primary"];
            NotesButton.TextColor = (Color)Application.Current.Resources["Primary"];
        }
    }
}