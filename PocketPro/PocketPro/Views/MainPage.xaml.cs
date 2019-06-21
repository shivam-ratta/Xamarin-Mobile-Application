using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PocketPro.Models;
using PocketPro.Views;
using PocketPro.ViewModels;
using Plugin.Media;
using PocketPro.Services;
using SQLite;
using System.IO;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.AudioRecorder;

namespace PocketPro.Views
{
    public partial class MainPage : ContentPage
    {
        //public string ToHexString(Color c) => $"{c.R:X2}";
        private AudioRecorderService recorder;

        public MainPage()
        {
            InitializeComponent();
            

            recorder = new AudioRecorderService();

            Datastores.SetDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "pocketpro.db"));
            
            MessagingCenter.Subscribe<NewNotePage, Note>(this, "AddNote", async (obj, item) =>
            {
                var newItem = item as Note;
                await Datastores.Notes.AddItemAsync(newItem); 
            });

            MessagingCenter.Subscribe<NewMileagePage, Mileage>(this, "AddMileage", async (obj, item) =>
            {
                var newItem = item as Mileage;
                await Datastores.Mileage.AddItemAsync(newItem); 
            });
 
        }

     
        private async Task Zoom(ImageButton button)
        {
            uint timeout = 500;
            //await (button).ScaleTo(1.6, timeout, Easing.BounceIn);
            //await (button).ScaleTo(1, timeout, Easing.BounceOut);
        }


        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var shellItem = (this.Parent.Parent.Parent) as ShellItem;
            shellItem.CurrentItem = shellItem.Items[1];
        }

        private void NavigateButton_Clicked(object sender, EventArgs e)
        {
            var shellItem = (this.Parent.Parent.Parent) as ShellItem;
            shellItem.CurrentItem = shellItem.Items[2];
        }

        private void SocialButton_Clicked(object sender, EventArgs e)
        {
            var shellItem = (this.Parent.Parent.Parent) as ShellItem;
            shellItem.CurrentItem = shellItem.Items[3];
        }

        private void SupportButton_Clicked(object sender, EventArgs e)
        {
            var shellItem = (this.Parent.Parent.Parent) as ShellItem;
            shellItem.CurrentItem = shellItem.Items[4];
        }

        private async void CreateNewNoteButton_OnClicked(object sender, EventArgs e)
        {
            await Zoom((ImageButton)sender);
            await Navigation.PushModalAsync(new NewNotePage());           
        }

        private bool isRecordingVideo = false;
        
        private async void ShowCamera_Clicked(object sender, EventArgs e)
        {


            await Zoom((ImageButton)sender);

            if (!isRecordingVideo)
            {
                isRecordingVideo = true;
                ShowCameraVideo.Source = "green_video_white_lg.png";

                //Camera.TakePhoto();
                Camera.StartRecording();

                await Task.Delay(5000);
                var brightnessService = DependencyService.Get<IBrightnessService>();
                brightnessService.SetBrightness(0f);
            }
            else
            {
                ShowCameraVideo.Source = "green_video_lg.png";

                isRecordingVideo = false;
                Camera.StopRecording();
               
                var brightnessService = DependencyService.Get<IBrightnessService>();
                brightnessService.SetBrightness(-1f);

            }


        }

        private async void ShowCameraPicture_Clicked(object sender, EventArgs e)
        {
            await Zoom((ImageButton)sender);
            Camera.TakePhoto();
        }

        private async void RecordAudioButton_Clicked(object sender, EventArgs e)
        {
            // await Zoom((ImageButton)sender);

            RecordAudioButton.Source = "green_audio_lg.png";

            // await RecordAudio();

            //var brightnessService = DependencyService.Get<IRateApplication>();
            //var service = DependencyService.Get<IRateApplication>();
            //var directory = service.GetStorageDirectory();
            //var audioRecordingsPath = System.IO.Path.Combine(directory, "Audio");
            //Directory.CreateDirectory(audioRecordingsPath);

            //var currentDateandTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            //var filename = $"PocketPro - {currentDateandTime}.wav";
            //var recordingFilePath = System.IO.Path.Combine(audioRecordingsPath, filename);


            //recorder.FilePath = recordingFilePath;
             

        }

        public bool isAudioRecording;

        async Task RecordAudio()
        {
            try
            {
                if (!isAudioRecording)
                {
                    RecordAudioButton.Source = "green_audio_lg.png";
                    isAudioRecording = true;
                    
                    //await recorder.StartRecording();
                    
                }
                else
                {
                    RecordAudioButton.Source = "green_audio_lg.png";
                    isAudioRecording = false;

                    //await recorder.StopRecording();
                }
            }
            catch (Exception ex)
            {
	        }
        }
    }
}