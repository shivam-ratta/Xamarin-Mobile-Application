using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PocketPro.Models;
using PocketPro.Services;
using PocketPro.Views;
using Xamarin.Forms;

namespace PocketPro.ViewModels
{
    public class DocumentPicturesViewModel : BaseViewModel
    {      
        public string Id { get; set; }
        public ObservableCollection<Media> Pictures { get; set; }
        public Command LoadItemCommand { get; set; }

        public bool NoItems
        {
            get { return Pictures.Count == 0; }
        }

        public DocumentPicturesViewModel()
        {
            Pictures = new ObservableCollection<Media>();
            LoadItemCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<MainPage, Media>(this, "AddPicture", async (obj, item) =>
            {
                var newItem = item as Media;
                Pictures.Add(newItem);
                await Datastores.Pictures.AddItemAsync(newItem);
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Pictures.Clear();
                var service = DependencyService.Get<IRateApplication>();
                var directory = service.GetStorageDirectory();
                var picturesDirectory = System.IO.Path.Combine(directory, "Pictures");
                var pictures = System.IO.Directory.GetFiles(picturesDirectory);
                foreach (var item in pictures)
                {
                    var fi = new System.IO.FileInfo(item);

                    Pictures.Add(new Media() { Path = item, Title = fi.Name, Type = MediaType.Picture });
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