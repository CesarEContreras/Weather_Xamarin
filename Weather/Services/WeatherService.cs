using System;
using RestSharp;
using Weather.Models;

namespace Weather.Services
{
    public static class WeatherService
    {
        private static RestClient client = new RestClient("https://api.openweathermap.org/data/2.5");

        public static async void GetCurrentWeather()
        {
            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", "");
            request.AddParameter("appid", "");

            try
            {
                var response = await client.GetAsync<Root>(request);
            }
            catch (Exception ex)
            {

            }
        }

    }
}