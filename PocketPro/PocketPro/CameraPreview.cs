using System;
using Xamarin.Forms;

namespace PocketPro
{
    public class CameraPreview : View
    {
        public static readonly BindableProperty CameraProperty = BindableProperty.Create(
            propertyName: "Camera",
            returnType: typeof(CameraOptions),
            declaringType: typeof(CameraPreview),
            defaultValue: CameraOptions.Rear);

        public bool IsPreviewing { get; set; }
        public EventHandler OnPictureReturn;
        public EventHandler OnVideoStarted;
        public EventHandler OnVideoFinished;

        public event EventHandler TakePhotoEvent;
        public event EventHandler StartRecordingEvent;
        public event EventHandler StopRecordingEvent;

        public event EventHandler AutoFocusEvent;
        public event EventHandler ToggleCamera;
        public event EventHandler EnableTorchEvent;
        public event EventHandler DisableTorchEvent;
        public byte[] LastPhotoBytes;


        public void TakePhoto()
        {
            EventHandler eventHandler = this.TakePhotoEvent;
            eventHandler?.Invoke((object)this, new CustomEventArgs() { OnPictureReturn = OnPictureReturn });
        }

        public void StartRecording()
        {
            EventHandler eventHandler = this.StartRecordingEvent;
            eventHandler?.Invoke((object)this, new CustomEventArgs() { OnVideoStarted = OnVideoStarted });
        }

        public void StopRecording()
        {
            EventHandler eventHandler = this.StopRecordingEvent;
            eventHandler?.Invoke((object)this, new CustomEventArgs() { OnVideoFinished = OnVideoFinished });
        }

        public void AutoFocus()
        {
            EventHandler eventHandler = this.AutoFocusEvent;
            eventHandler?.Invoke((object)this, EventArgs.Empty);
        }


        public void EnableTorch()
        {
            EventHandler eventHandler = this.EnableTorchEvent;
            eventHandler?.Invoke((object)this, EventArgs.Empty);
        }        
        public void DisableTorch()
        {
            EventHandler eventHandler = this.DisableTorchEvent;
            eventHandler?.Invoke((object)this, EventArgs.Empty);
        }

         
        public void Toggle()
        {
            EventHandler eventHandler = this.ToggleCamera;
            eventHandler?.Invoke((object)this, EventArgs.Empty);
        }


        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create<CameraPreview, bool>(p => p.IsChecked, true, propertyChanged: (s, o, n) => { (s as CameraPreview).OnChecked(new EventArgs()); });
        public static readonly BindableProperty ColorProperty = BindableProperty.Create<CameraPreview, Color>(p => p.Color, Color.Default);


        public CameraOptions Options {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        
        public CameraOptions Camera {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public event EventHandler Checked;

        protected virtual void OnChecked(EventArgs e)
        {
            if (Checked != null)
                Checked(this, e);
        }


    }
}
