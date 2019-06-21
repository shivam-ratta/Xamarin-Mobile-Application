using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using PocketPro.iOS;
using UIKit;
using Xamarin.Forms;
using UIKit;


[assembly: Dependency(typeof(iOSBrightnessService))]
namespace PocketPro.iOS
{

    public class iOSBrightnessService : IBrightnessService
    {
        public void SetBrightness(float brightness)
        {
            UIScreen.MainScreen.Brightness = brightness;
        }
    }
}