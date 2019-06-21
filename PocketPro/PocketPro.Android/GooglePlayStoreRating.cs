using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PocketPro.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Xamarin.Forms.Dependency(typeof(GooglePlayStoreRating))]
namespace PocketPro.Droid
{
    class GooglePlayStoreRating : Java.Lang.Object, IRateApplication
    {
        public void EmailSupport()
        {
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("message/rfc822");
            intent.PutExtra(Intent.ExtraEmail, new[] { "support@exqsd.com" });
            Android.App.Application.Context.StartActivity(Intent.CreateChooser(intent, "Send email"));
        }

        public void OpenSupport()
        {
            throw new NotImplementedException();
        }


        public void Rate()
        {
            string appPackageName = Android.App.Application.Context.PackageName;
            Android.App.Application.Context.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=" + appPackageName)));
        }


        public byte[] GenerateThumbImage(string url, long usecond)
        {
            try
            {
                MediaMetadataRetriever retriever = new MediaMetadataRetriever();
                //retriever.SetDataSource(url, new Dictionary<string, string>());
                retriever.SetDataSource(url);
                Bitmap bitmap = retriever.GetFrameAtTime(usecond);
                if (bitmap != null)
                {
                    MemoryStream stream = new MemoryStream();
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    byte[] bitmapData = stream.ToArray();
                    return bitmapData;// ImageSource.FromStream(() => new MemoryStream(bitmapData));
                }
            }
            catch (Exception exception)
            { 
            }
            return null;
        }


        public string GetStorageDirectory()
        {
            return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        }

        public void SMSShare()
        {
            string appPackageName = Android.App.Application.Context.PackageName;
            var intent = new Intent(Intent.ActionSend);
            //intent.SetType(contentType);
            intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=" + appPackageName));
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject,  string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(chooserIntent);
        }

        public void EmailShare()
        {
            string appPackageName = Android.App.Application.Context.PackageName;
            var intent = new Intent(Intent.ActionSend);
            //intent.SetType(contentType);
            intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=" + appPackageName));
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(chooserIntent);
        }

        public void FaceBookShare()
        {
            string appPackageName = Android.App.Application.Context.PackageName;
            var intent = new Intent(Intent.ActionSend);
            //intent.SetType(contentType);
            intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=" + appPackageName));
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(chooserIntent);
        }
    }
}