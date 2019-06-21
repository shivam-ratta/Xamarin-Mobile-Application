using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
    public partial class CameraPermissions : ContentPage
    {
        public CameraPermissions()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var permissions = new[] { Permission.Camera, Permission.Storage, Permission.Microphone, Permission.Photos };

            foreach (var permission in permissions)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if ( status != PermissionStatus.Granted )
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(permissions);
                    //Best practice to always check that the key exists
                    if ((!results.ContainsKey(permission) || results[permission] != PermissionStatus.Granted))
                    {
                        await DisplayAlert("Error", "Permission to access your Camera and Storage must be authorized.", "OK");
                        break;
                    }
                }
            }

            App.Current.MainPage = new AppShell();
        }
    }
}