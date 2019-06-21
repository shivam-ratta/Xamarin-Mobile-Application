using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PocketPro.Models;
using PocketPro.Services;
using PocketPro.Views;
using Xamarin.Forms;

namespace PocketPro.ViewModels
{
    public class DocumentAudioRecordingsViewModel : BaseViewModel
    {      
        public string Id { get; set; }
        public ObservableCollection<Media> Pictures { get; set; }
        public Command LoadItemCommand { get; set; }

        public bool NoItems
        {
            get { return Pictures.Count == 0; }
        }

        public DocumentAudioRecordingsViewModel()
        {
            Pictures = new ObservableCollection<Media>();
            LoadItemCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<MainPage, Media>(this, "AddAudio", async (obj, item) =>
            {
                var newItem = item as Media;
                Pictures.Add(newItem);
                await Datastores.Audio.AddItemAsync(newItem);
            });

        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Pictures.Clear();

                var service = DependencyService.Get<IRateApplication>();
                var directory = service.GetStorageDirectory();
                var audioRecordings = System.IO.Path.Combine(directory, "Audio");

                var movies = System.IO.Directory.GetFiles(audioRecordings);
                foreach (var item in movies)
                {
                    var fi = new System.IO.FileInfo(item);

                    var name = fi.Name;
                    if (name.Contains("PocketPro - "))
                    {
                        name = name.Substring(name.IndexOf(" - ") + 3);
                        name = name.Substring(0,name.IndexOf("."));
                        var timestamp = DateTime.TryParseExact(name, "yyyyMMdd_HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var Date);

                        Pictures.Add(new Media()
                        {
                            Path = item,
                            Title = fi.Name,
                            Type = MediaType.Audio,
                            Size = fi.Length.ToBytesCount(),
                            Date = Date.ToString("HH:mm tt, MMMM dd")
                        });
                    }
                }

                var items = await Datastores.Pictures.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Pictures.Add(item);
                }

                OnPropertyChanged("NoItems");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}