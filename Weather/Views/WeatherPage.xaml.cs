using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Weather.Views
{
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();
            var list = new List<string>
            {
                "Hey",
                "Did you check the",
                "The CarouselView",
                "In Xamarin.Forms?"
            };
            TheCarousel.ItemsSource = list;
        }
    }
}
