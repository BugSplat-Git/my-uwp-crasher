using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyUwpCrasher
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

        private void UnhandledExceptionButton_Click(object sender, RoutedEventArgs e)
        {
            ThrowExceptionWithStackFrames();
        }

        private async void HandledExceptionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThrowExceptionWithStackFrames();
            }
            catch (Exception ex)
            {
                this.ExceptionText.Text = "Sending to BugSplat...";

                await Task.Run(() => App.BugSplat.Post(ex));

                this.ExceptionText.Text = "Sent!";
                this.ExceptionText.Text += Environment.NewLine;
                this.ExceptionText.Text += ex.ToString();
            }
        }

        private async void AsyncExceptionButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => throw new Exception("BugSplat!"));
        }

        private void UnobservedTaskExceptionButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                ThrowExceptionWithStackFrames();
            });

            Thread.Sleep(100);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void ThrowExceptionWithStackFrames()
        {
            new Foo(new Bar(new Baz())).Crash();
        }
    }
}
