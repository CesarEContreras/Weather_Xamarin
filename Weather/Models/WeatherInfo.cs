using System;
namespace Weather.Models
{
    public class WeatherInfo
    {
        public string City { get; set; }
        public string CurrentIcon { get; set; }
        public int CurrentTemp { get; set; }
        public string TomorrowIcon { get; set; }
        public int TomorrowTemp { get; set; }
    }
}
