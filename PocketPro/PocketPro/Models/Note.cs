using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PocketPro.Models
{
    public class Note     : INotifyPropertyChanged
    {
        private bool _isVisible;
        private bool _isNotVisible;

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public bool isNotVisible => !isVisible;
        public bool isVisible
        {
            get { return _isVisible; }
            set {
                _isVisible = value;
                OnPropertyChanged();
                OnPropertyChanged("isNotVisible");
            }
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