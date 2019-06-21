using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PocketPro.Services;
using PocketPro.Views;
using DLToolkit.Forms.Controls;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace PocketPro
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            FlowListView.Init();

            DependencyService.Register<NoteDataStore>();

            var status1 = CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera).Result;
            var status2 = CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage).Result;

            if (status1 != PermissionStatus.Granted || status2 != PermissionStatus.Granted)
            {
                MainPage = new CameraPermissions();
            }
            else
            {
                MainPage = new AppShell();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
