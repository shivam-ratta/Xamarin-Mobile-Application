using System;
using PocketPro;
using PocketPro.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AVFoundation;
using CoreVideo;
using CoreMedia;
using CoreFoundation;

[assembly: ExportRenderer (typeof(CameraPreview), typeof(CameraPreviewRenderer))]
namespace PocketPro.iOS
{
	public class CameraPreviewRenderer : ViewRenderer<CameraPreview, UICameraPreview>
	{
		UICameraPreview uiCameraPreview;
        private CameraPreview cameraPreview;
        private CameraOptions cameraOptions;
        private string videoFilePath;


        protected override void OnElementChanged (ElementChangedEventArgs<CameraPreview> e)
		{
			base.OnElementChanged (e);

            if (Control == null) {
				uiCameraPreview = new UICameraPreview (e.NewElement.Camera);
				SetNativeControl (uiCameraPreview);
			}
			if (e.OldElement != null) {
				// Unsubscribe
				uiCameraPreview.Tapped -= OnCameraPreviewTapped;
			}
			if (e.NewElement != null) {
				// Subscribe
				uiCameraPreview.Tapped += OnCameraPreviewTapped;
			}

            e.NewElement.StartRecordingEvent += (sender, args) => {
               // cameraPreview.Preview.StopPreview();
               // cameraPreview.Preview.Release();

                var cameraOption = cameraOptions.Equals(CameraOptions.Front) ? CameraOptions.Front : CameraOptions.Rear;
                var dcimPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                
                var filename = $"PocketPro - Test.mp4";
                var dcimFilePath = System.IO.Path.Combine(dcimPath, filename);

                videoFilePath = dcimFilePath;


            };

        }

		void OnCameraPreviewTapped (object sender, EventArgs e)
		{
			if (uiCameraPreview.IsPreviewing) {
				uiCameraPreview.CaptureSession.StopRunning ();
				uiCameraPreview.IsPreviewing = false;
			} else {
				uiCameraPreview.CaptureSession.StartRunning ();
				uiCameraPreview.IsPreviewing = true;
			}
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing) {
				Control.CaptureSession.Dispose ();
				Control.Dispose ();
			}
			base.Dispose (disposing);
		}
	}
}
