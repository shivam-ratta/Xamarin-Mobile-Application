using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using PocketPro;
using PocketPro.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidBrightnessService))]

namespace PocketPro.Droid
{
    class AndroidBrightnessService : IBrightnessService
    {
        public void SetBrightness(float brightness)
        {
            var window = CrossCurrentActivity.Current.Activity.Window;
            var attributesWindow = new WindowManagerLayoutParams();

            attributesWindow.CopyFrom(window.Attributes);
            attributesWindow.ScreenBrightness = brightness;
            attributesWindow.Flags = WindowManagerFlags.KeepScreenOn;

            window.Attributes = attributesWindow;
        }
    }
}