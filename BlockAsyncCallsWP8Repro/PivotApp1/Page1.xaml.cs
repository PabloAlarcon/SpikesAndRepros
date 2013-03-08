using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PivotApp1
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": " + message);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Log("OnNavigatedTo");

            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Log("OnNavigatedFrom");

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Log("OnNavigatingFrom");

            base.OnNavigatingFrom(e);
        }

    }
}