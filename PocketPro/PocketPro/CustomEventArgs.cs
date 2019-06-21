using System;
using System.Collections.Generic;
using System.Text;

namespace PocketPro
{
    class CustomEventArgs : EventArgs
    {
        public EventHandler OnPictureReturn;
        public EventHandler OnVideoStarted;
        public EventHandler OnVideoFinished;

    }
}
