using System;
using System.IO;
using System.Text;
using System.Xml.Xsl.Runtime;
using Android.Content;
using Android.Graphics;
using Android.Icu.Text;
using Android.Media;
using Android.Text.Format;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using PocketPro.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Camera = Android.Hardware.Camera;
using Environment = Android.OS.Environment;
using Exception = Java.Lang.Exception;
using File = Java.IO.File;
using Image = Android.Media.Image;
using Stream = System.IO.Stream;

[assembly: ExportRenderer(typeof(PocketPro.CameraPreview), typeof(CameraPreviewRenderer))]
namespace PocketPro.Droid
{
    public class CameraPreviewRenderer : ViewRenderer<PocketPro.CameraPreview, PocketPro.Droid.CameraPreview>
    {
        private CameraPreview cameraPreview;
        private CameraOptions cameraOptions;
        private MediaRecorder mRecorder;
        private string videoFilePath;

        public CameraPreviewRenderer(Context context) :
            base(context)
        { 

        } 

        protected override void OnElementChanged(ElementChangedEventArgs<PocketPro.CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                cameraPreview = new CameraPreview(Context);
                cameraPreview.OnPictureReturn = e.NewElement.OnPictureReturn;
                cameraPreview.OnVideoStarted = e.NewElement.OnVideoStarted;
                cameraPreview.OnVideoFinished = e.NewElement.OnVideoFinished;

                SetNativeControl(cameraPreview);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe
                cameraPreview.Click -= OnCameraPreviewClicked;
            }

            if (e.NewElement != null)
            {
                Camera camera = null;
                 
                e.NewElement.TakePhotoEvent += (sender, args) =>
                {
                    if (cameraPreview.Preview != null)
                    {
                        cameraPreview.OnPictureReturn = ((PocketPro.CameraPreview)(sender)).OnPictureReturn;
                        cameraPreview.OnVideoStarted = ((PocketPro.CameraPreview)(sender)).OnVideoStarted;
                        cameraPreview.OnVideoFinished = ((PocketPro.CameraPreview)(sender)).OnVideoFinished;
                        //cameraPreview.Preview.AutoFocus(new AutofocusCallBack(cameraPreview)
                        //{
                        //    isTakePhoto = true
                        //});


                        cameraPreview.camera.EnableShutterSound(false);
                        cameraPreview.camera.TakePicture(new ShutterCallback(), null, new JpegCallback(this.cameraPreview));

                        //}
                        //else camera.AutoFocus(new AutofocusCallBack(cameraPreview)); // TODO: Verify No Memory Leak On Failure

                    }
                    else
                    {
                    }
                };

                e.NewElement.StartRecordingEvent += (sender, args) => {
                    cameraPreview.Preview.StopPreview();
                    cameraPreview.Preview.Release();

                    var cameraOption = cameraOptions.Equals(CameraOptions.Front) ? CameraOptions.Front : CameraOptions.Rear;
                    var dcimPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Environment.DirectoryMovies).Path;

                    var sdf = new SimpleDateFormat("yyyyMMdd_HHmmss");
                    var currentDateandTime = sdf.Format(new Date());
                    var filename = $"PocketPro - {currentDateandTime}.mp4";
                    var dcimFilePath = System.IO.Path.Combine(dcimPath, filename);

                    videoFilePath = dcimFilePath;

                    camera = Camera.Open((int)cameraOption);
                    cameraOptions = cameraOption;
                    cameraPreview.Preview = camera;
                    camera.Unlock();

                    try
                    {
                        mRecorder = new MediaRecorder();
                        mRecorder.SetCamera(camera);
                        mRecorder.SetVideoSource(VideoSource.Camera);
                        mRecorder.SetAudioSource(AudioSource.Mic);
                        mRecorder.SetOutputFormat(OutputFormat.Mpeg4);
                        mRecorder.SetVideoEncoder(VideoEncoder.H264);
                        mRecorder.SetAudioEncoder(AudioEncoder.Aac);
                        mRecorder.SetOutputFile(dcimFilePath);
                        mRecorder.SetOrientationHint(0);
                        mRecorder.SetVideoFrameRate(30);
                        // mRecorder.SetVideoEncodingBitRate(512 * 1000);
                        mRecorder.SetVideoSize(1920, 1080);
                        mRecorder.SetPreviewDisplay(cameraPreview.holder.Surface);
                        mRecorder.Prepare();
                        mRecorder.Start();
                    }
                    catch (Exception exception)
                    {

                        Console.WriteLine(exception.Message);
                    }
                };

                e.NewElement.StopRecordingEvent += (sender, args) => {
                   
                    mRecorder.Stop();
                    mRecorder.Reset();
                    mRecorder.Release();

                    //camera = Camera.Open((int)cameraOption);
                    //cameraOptions = cameraOption;
                    //camera.Unlock();

                    var dcimPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Environment.DirectoryMovies).Path;
                    var fileBase = System.IO.Path.GetFileNameWithoutExtension(videoFilePath);
                    var dcimFilePath = System.IO.Path.Combine(dcimPath, $"{fileBase}.png");

                    var brightnessService = DependencyService.Get<IRateApplication>();
                    var thumb = brightnessService.GenerateThumbImage(videoFilePath, 1000);
                    System.IO.File.WriteAllBytes(dcimFilePath, thumb);
                };

                e.NewElement.ToggleCamera += (sender, args) =>
                { 
                    cameraPreview.Preview.StopPreview();
                    cameraPreview.Preview.Release();
                    
                    var cameraOption =  cameraOptions.Equals(CameraOptions.Front) ? CameraOptions.Front : CameraOptions.Rear;

                    camera = Camera.Open( (int)cameraOption );
                    cameraOptions = cameraOption;

                    cameraPreview.Preview = camera;
                    cameraPreview.Preview.StartPreview();

                    cameraPreview = new CameraPreview(Context); 
                    cameraPreview.OnPictureReturn = e.OldElement.OnPictureReturn;
                    cameraPreview.OnVideoStarted = e.OldElement.OnVideoStarted;
                    cameraPreview.OnVideoFinished = e.OldElement.OnVideoFinished;

                    SetNativeControl(cameraPreview);

                    Control.Preview = camera;
                    Control.Preview.StartPreview();

                };

                e.NewElement.AutoFocusEvent += (sender, args) =>
                {
                    cameraPreview.Preview.AutoFocus(new AutofocusCallBack(cameraPreview));
                };

                e.NewElement.EnableTorchEvent += (sender, args) =>
                {
                    var parameters = cameraPreview.Preview.GetParameters();
                    parameters.FlashMode = Camera.Parameters.FlashModeTorch;
                    //cameraPreview.Preview.StopPreview();
                    cameraPreview.Preview.SetParameters(parameters);
                };

                e.NewElement.DisableTorchEvent += (sender, args) =>
                {
                    var parameters = cameraPreview.Preview.GetParameters();
                    parameters.FlashMode = Camera.Parameters.FlashModeOff;
                    //cameraPreview.Preview.StartPreview();
                    cameraPreview.Preview.SetParameters(parameters);
                };

                // Subscribe
                cameraPreview.Click += OnCameraPreviewClicked;
                try
                {
                    if (camera == null)
                        Control.Preview = Camera.Open((int) e.NewElement.Camera);
                }
                catch (Exception cameraException)
                {
                    Console.WriteLine(cameraException.Message);

                }


            }
            
        }

        private void AutoFocusEvent(object sender, EventArgs e)
        {
            cameraPreview.Preview.AutoFocus(new AutofocusCallBack(cameraPreview));
        }
 
        void OnCameraPreviewClicked(object sender, EventArgs e)
        {
            if(cameraPreview.Preview != null) cameraPreview.Preview.AutoFocus(new AutofocusCallBack(cameraPreview));
            //cameraPreview.Preview.TakePicture(null, null, new JpegCallback());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(Control!=null && Control.Preview!=null) Control.Preview.Release();
            }
            base.Dispose(disposing);
        }
    }

    public class AutofocusCallBack : Java.Lang.Object, Camera.IAutoFocusCallback
    {
        public CameraPreview cameraPreview { get; set; }
        public bool isTakePhoto { get; set; }

        public AutofocusCallBack(CameraPreview cameraPreview)
        {
            this.isTakePhoto = true;
            this.cameraPreview = cameraPreview;
        }

        public void OnAutoFocus(bool autoFocused, Camera camera)
        {
            if (isTakePhoto && autoFocused)
            {
                camera.TakePicture(new ShutterCallback(), null, new JpegCallback(this.cameraPreview));
            }
            else camera.AutoFocus(new AutofocusCallBack(cameraPreview)); // TODO: Verify No Memory Leak On Failure
        }
    }

    public class ShutterCallback : Java.Lang.Object, Camera.IShutterCallback
    {
        public void OnShutter()
        {
             Console.WriteLine("Shutter ....");
        }
    }

    public class JpegCallback : Java.Lang.Object, Camera.IPictureCallback
    {
        public CameraPreview cameraPreview { get; set; }
        
        public JpegCallback(CameraPreview cameraPreview)
        {
            this.cameraPreview = cameraPreview;
        }
        
        public void OnPreviewFrame(byte[] data, Camera camera)
        {

            
        }

        public void OnPictureTaken(byte[] data, Camera camera)
        {
            var sdf = new SimpleDateFormat("yyyyMMdd_HHmmss");
            var currentDateandTime = sdf.Format(new Date());
             
            var dcimPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).Path;
            var filename = $"{currentDateandTime}.jpg";
            var dcimFilePath = System.IO.Path.Combine(dcimPath, filename);

            System.IO.File.WriteAllBytes(dcimFilePath, data);

            camera.StartPreview();

            this.cameraPreview.LastPhotoBytes = data;
            this.cameraPreview.OnPictureReturn?.Invoke((object)this, EventArgs.Empty);

            
        }
    }

}
