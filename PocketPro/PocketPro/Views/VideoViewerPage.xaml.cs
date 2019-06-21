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
    public partial class VideoViewerPage : ContentPage
    {
        public VideoViewerPage()
        {
            InitializeComponent();
        }

        public VideoViewerPage(ImageSource videoPath)
        {
            InitializeComponent();
            VideoViewer.Source = videoPath;
        }
    }
}