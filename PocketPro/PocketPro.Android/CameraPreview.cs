using System;
using System.Collections.Generic;
using Android;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using Java.Security;
using Sentry;
using Permission = Android.Content.PM.Permission;

namespace PocketPro.Droid
{
    public sealed class CameraPreview : ViewGroup, ISurfaceHolderCallback 
    {
        private SurfaceView surfaceView;
        public ISurfaceHolder holder;
        private Camera.Size previewSize;
        private IList<Camera.Size> supportedPreviewSizes;
        private IWindowManager windowManager;

        public Camera camera;
        public bool IsPreviewing { get; set; }
        public byte[] LastPhotoBytes;

        public EventHandler OnPictureReturn;
        public EventHandler OnVideoStarted;
        public EventHandler OnVideoFinished;

        public Camera Preview
        {
            get { return camera; }
            set
            {
                camera = value;

                if (camera != null)
                {
                    supportedPreviewSizes = Preview.GetParameters().SupportedPreviewSizes;
                    RequestLayout();
                }
            }
        }


        public CameraPreview(Context context) : base(context)
        {
            surfaceView = new SurfaceView(context);
            AddView(surfaceView);

            windowManager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            IsPreviewing = false;
            holder = surfaceView.Holder;
            holder.AddCallback(this);
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            try
            {
                if (Preview != null)
                {
                    var parameters = Preview.GetParameters();
                    parameters.PictureFormat = Android.Graphics.ImageFormatType.Jpeg;
                    Preview.SetParameters(parameters);
                    Preview.SetPreviewDisplay(holder);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }
        }


        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int width = ResolveSize(SuggestedMinimumWidth, widthMeasureSpec);
            int height = ResolveSize(SuggestedMinimumHeight, heightMeasureSpec);
            SetMeasuredDimension(width, height);

            if (supportedPreviewSizes != null)
            {
                previewSize = GetOptimalPreviewSize(supportedPreviewSizes, width, height);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            surfaceView.Measure(msw, msh);
            surfaceView.Layout(0, 0, r - l, b - t);
        }

 
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            if (holder == null) return;

            if (Preview != null)
            {
                Preview.StopPreview ();
                IsPreviewing = false;
            }
        }
 
        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
        {
            if (holder == null) return; 
            
            if (Preview != null && !IsPreviewing) {
                //TODO: Reconnect is failing when switching back into the application.
                Preview = camera;
                Preview.Reconnect();
                Preview.Lock();
                Preview.EnableShutterSound(false);
                Preview.StartPreview();
                
                IsPreviewing = true;
            }

            if(Preview != null) {

                try
                {
                    //var parameters = Preview.GetParameters();
                    //var previewSize = Preview.GetParameters().SupportedPreviewSizes[0];
                    //parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
                    //parameters.PictureFormat = Android.Graphics.ImageFormatType.Jpeg;
                    //Preview.SetParameters(parameters);
                }
                catch(Exception exception)
                {
                    SentrySdk.CaptureException(exception);
                }

                RequestLayout();

                switch (windowManager.DefaultDisplay.Rotation)
                {
                    case SurfaceOrientation.Rotation0:
                        camera.SetDisplayOrientation(90);
                        break;
                    case SurfaceOrientation.Rotation90:
                        camera.SetDisplayOrientation(0);
                        break;
                    case SurfaceOrientation.Rotation270:
                        camera.SetDisplayOrientation(180);
                        break;
                }

            }
        }

        private Camera.Size GetOptimalPreviewSize(IList<Camera.Size> sizes, int w, int h)
        {
            const double AspectTolerance = 0.1;
            double targetRatio = (double)w / h;

            if (sizes == null)
            {
                return null;
            }

            Camera.Size optimalSize = null;
            double minDiff = double.MaxValue;

            int targetHeight = 720;
            foreach (Camera.Size size in sizes)
            {
                double ratio = (double)size.Width / size.Height;

                if (Math.Abs(ratio - targetRatio) > AspectTolerance)
                    continue;
                if (Math.Abs(size.Height - targetHeight) < minDiff)
                {
                    optimalSize = size;
                    minDiff = Math.Abs(size.Height - targetHeight);
                }
            }

            if (optimalSize == null)
            {
                minDiff = double.MaxValue;
                foreach (Camera.Size size in sizes)
                {
                    if (Math.Abs(size.Height - targetHeight) < minDiff)
                    {
                        optimalSize = size;
                        minDiff = Math.Abs(size.Height - targetHeight);
                    }
                }
            }

            Console.WriteLine($"Camera Optimizal Size: {optimalSize?.Height??0} {optimalSize?.Width??0}");
            return optimalSize;
        }
    }

 

}