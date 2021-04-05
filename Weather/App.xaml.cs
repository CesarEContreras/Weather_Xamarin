using System;
using Weather.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Weather.Helpers;

namespace Weather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new WeatherPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start($"android={Settings.Instance.AppCenterTokenAndroid};" +
                  $"ios={Settings.Instance.AppCenterTokenIOS}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
