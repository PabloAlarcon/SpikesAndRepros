using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

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
