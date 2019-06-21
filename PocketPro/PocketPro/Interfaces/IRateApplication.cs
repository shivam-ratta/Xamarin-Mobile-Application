using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PocketPro
{
    public interface IRateApplication
    {
        void Rate();
        void OpenSupport();
        void EmailSupport();
        void SMSShare();
        void EmailShare();
        void FaceBookShare();

        byte[] GenerateThumbImage(string url, long usecond);
        string GetStorageDirectory();
    }
}
