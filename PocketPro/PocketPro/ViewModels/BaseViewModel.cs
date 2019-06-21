using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using PocketPro.Models;
using PocketPro.Services;
using System.Windows.Input;
using System.Threading.Tasks;

namespace PocketPro.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ICommand BrowseDigitalDocuments;
        public ICommand TestCommand2;

        public BaseViewModel()
        {
            BrowseDigitalDocuments = new Command(_BrowseDigitalDocuments);
        }
        
        private async void _BrowseDigitalDocuments()
        {
            await App.Current.MainPage.DisplayAlert("OK", "OK", "OK");         
        }

        
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
