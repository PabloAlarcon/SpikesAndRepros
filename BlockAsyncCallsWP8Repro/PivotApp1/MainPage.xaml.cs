using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PivotApp1.Resources;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace PivotApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonConfigureAwaitClick(object sender, RoutedEventArgs e)
        {
            this.textblockConfigureAwait.Text = GetRemoteDataConfigureAwaitAsync().Result;
        }

        private void ButtonTaskRun(object sender, RoutedEventArgs e)
        {
           this.textblockTask.Text = Task.Run(() => GetRemoteDataAsync()).Result;
        }

        private void ButtonTaskRunAsyncAwait(object sender, RoutedEventArgs e)
        {
            this.textblockTaskRunAsyncAwait.Text = Task.Run(async () => await GetRemoteDataAsync()).Result;
        }
      
        private void ButtonTaskRunWaitTimeout(object sender, RoutedEventArgs e)
        {
            Task t = Task.Run(() => GetRemoteDataAsync());
            bool finished = t.Wait(10000);
            this.textblockTaskWaitTimeout.Text = finished ? "finished" : "timeoutted";
        }

        private void ButtonAutoResetEvent(object sender, RoutedEventArgs e)
        {
            using (AutoResetEvent resetEvent = new AutoResetEvent(false))
            {
                Task<string> t = Task.Run(async () =>
                {
                    string text = await GetRemoteDataAsync();
                    resetEvent.Set();

                    return text;
                });

                resetEvent.WaitOne();

                this.textblockAutoResetEvent.Text = t.Result;
            }
        }

        public async Task<string> GetRemoteDataAsync()
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://www.google.com");
            using (var response = await request.GetResponseAsync())
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var text = await streamReader.ReadToEndAsync();
                return text;
            }
        }


        public async Task<string> GetRemoteDataConfigureAwaitAsync()
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://www.microsoft.com");
            using (var response = await request.GetResponseAsync().ConfigureAwait(false))
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var text = await streamReader.ReadToEndAsync().ConfigureAwait(false);

                return text;
            }
        }
    }
}