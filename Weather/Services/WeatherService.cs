using System;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using RestSharp;
using Weather.Helpers;
using Weather.Models;

namespace Weather.Services
{
    public static class WeatherService
    {
        private static RestClient client = new RestClient("https://api.openweathermap.org/data/2.5");

        public static async Task<Root> GetCurrentWeatherByCity(string city)
        {
            var request = new RestRequest("weather", DataFormat.Json);
            request.AddParameter("q", city);
            request.AddParameter("appid", Settings.Instance.OpeanWeatherSecret);

            try
            {
                var response = await client.GetAsync<Root>(request);
                return response;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return null;
            }
        }

        public static async Task<OneCall> GetCurrentAndForecastWeather(double lat, double lon)
        {
            var request = new RestRequest("onecall", DataFormat.Json);
            request.AddParameter("lat", lat);
            request.AddParameter("lon", lon);
            request.AddParameter("appid", Settings.Instance.OpeanWeatherSecret);

            try
            {
                var response = await client.GetAsync<OneCall>(request);
                return response;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return null;
            }
        }

    }
}