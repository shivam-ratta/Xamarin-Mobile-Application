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
    public class DocumentNotesViewModel : BaseViewModel
    {      
        public string Id { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public Command LoadItemCommand { get; set; }

        public bool NoItems
        {
            get { return Notes.Count == 0; }
        }

        public DocumentNotesViewModel()
        {
            Notes = new ObservableCollection<Note>();
            LoadItemCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewNotePage, Note>(this, "AddNote", async (obj, item) =>
            {
                var newItem = item as Note;
                Notes.Add(newItem);
                OnPropertyChanged("Notes");
            });

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Notes.Clear();
                var items = await Datastores.Notes.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Notes.Add(item);
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