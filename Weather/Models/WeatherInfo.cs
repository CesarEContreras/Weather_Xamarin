using System;
namespace Weather.Models
{
    public class WeatherInfo
    {
        public string City { get; set; }
        public string CurrentIcon { get; set; }
        public int CurrentCelsius { get; set; }
        public int CurrentFahrenheit { get; set; }
    }
}
