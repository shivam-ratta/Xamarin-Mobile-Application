using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PocketPro
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.FlyoutBackgroundColor = new Color(0, 0, 0, 0.85);
            this.BackgroundColor = new Color(0, 0, 0, 0.85);



            Routing.RegisterRoute("documents", typeof(Views.DocumentsPage));
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}
