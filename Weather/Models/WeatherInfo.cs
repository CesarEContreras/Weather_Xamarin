using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Weather.Models
{
    public class WeatherInfo
    {
        public string City { get; set; }
        public string WeatherDesc { get; set; }
        public string CurrentIcon { get; set; }
        public int CurrentCelsius { get; set; }
        public int CurrentFahrenheit { get; set; }
        public ObservableCollection<ForecastInfo> DailyForecast { get; set; }

    }

    public class ForecastInfo
    {
        public string WeekDay { get; set; }
        public string WeatherIcon { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
    }
}
