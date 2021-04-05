using System;
using RestSharp;
using Weather.Helpers;
using Weather.Models;

namespace Weather.Services
{
    public static class WeatherService
    {
        private static RestClient client = new RestClient("https://api.openweathermap.org/data/2.5");

        public static async void GetCurrentWeatherByCity(string city)
        {
            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", city);
            request.AddParameter("appid", Settings.Instance.OpeanWeatherSecret);

            try
            {
                var response = await client.GetAsync<Root>(request);
            }
            catch (Exception ex)
            {

            }
        }

        public static async void GetCurrentAndForecastWeather()
        {
            throw new NotImplementedException();
        }

    }
}