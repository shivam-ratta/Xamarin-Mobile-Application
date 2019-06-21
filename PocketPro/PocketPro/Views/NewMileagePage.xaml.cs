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
    public partial class NewMileagePage : ContentPage
    {
        public NewMileagePage()
        {
            InitializeComponent();
        }

        private async void SaveMilageButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<NewMileagePage, Mileage>(this, "AddMileage", new Mileage() {
                Miles = decimal.Parse(Miles.Text)
            });
            
            await Navigation.PopModalAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}