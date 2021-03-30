using System;
using System.Collections.ObjectModel;
using Weather.Models;

namespace Weather.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        private ObservableCollection<WeatherInfo> weather = new ObservableCollection<WeatherInfo>();

        public WeatherViewModel()
        {
            Title = "Weather";

            Weather.Add(new WeatherInfo()
            {
                City = "Nutley",
                CurrentIcon = "https://openweathermap.org/img/wn/10d@2x.png",
                CurrentTemp = 27,
                TomorrowIcon = "https://openweathermap.org/img/wn/10d@2x.png",
                TomorrowTemp = 19
            });
            Weather.Add(new WeatherInfo()
            {
                City = "Passaic",
                CurrentIcon = "https://openweathermap.org/img/wn/10d@2x.png",
                CurrentTemp = 27,
                TomorrowIcon = "https://openweathermap.org/img/wn/10d@2x.png",
                TomorrowTemp = 19
            });
        }

        public ObservableCollection<WeatherInfo> Weather
        {
            get => weather;
            set => weather = value;
        }
    }
}
