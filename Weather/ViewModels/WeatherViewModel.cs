using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Weather.Models;
using Weather.Services;
using Xamarin.Forms;

namespace Weather.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        private ObservableCollection<WeatherInfo> weather = new ObservableCollection<WeatherInfo>();
        public Command LoadWeatherCommand { get; set; }
        private const double KELVIN = 273.15;

        public WeatherViewModel()
        {
            Title = "Weather";

            Weather.Add(new WeatherInfo()
            {
                City = "Nutley",
            });
            Weather.Add(new WeatherInfo()
            {
                City = "Passaic",
            });

           LoadWeatherCommand = new Command<WeatherInfo>(async (item) => await LoadWeatherData(item));
        }

        public ObservableCollection<WeatherInfo> Weather
        {
            get => weather;
            set => weather = value;
        }

        private async Task LoadWeatherData(WeatherInfo item)
        {
            IsBusy = true;

            try
            {
                var cityInfo = await WeatherService.GetCurrentWeatherByCity(item.City);
                var currentCityWeather = await WeatherService.GetCurrentAndForecastWeather(cityInfo.Coord.Lat, cityInfo.Coord.Lon);

                // Load Current Weather
                item.WeatherDesc = currentCityWeather.Current.Weather.First().Description;
                item.CurrentCelsius = (int)ConvertToCelsius(currentCityWeather.Current.Temp);
                item.CurrentFahrenheit = (int)ConvertToFahrenheit(currentCityWeather.Current.Temp);
                item.CurrentIcon = $"https://openweathermap.org/img/wn/{currentCityWeather.Current.Weather.First().Icon}@2x.png";

                // Load Daily Forecast
                item.DailyForecast = new ObservableCollection<ForecastInfo>();
                
                for(int i = 1; i <= 5; i++)
                {
                    item.DailyForecast.Add(new ForecastInfo(){
                        WeekDay = ConvertToDateTime(currentCityWeather.Daily[i].Dt).ToString("ddd").ToUpper(),
                        WeatherIcon = $"https://openweathermap.org/img/wn/{currentCityWeather.Daily[i].Weather.First().Icon}@2x.png",
                        MaxTemp = (int)ConvertToFahrenheit(currentCityWeather.Daily[i].Temp.Max),
                        MinTemp = (int)ConvertToFahrenheit(currentCityWeather.Daily[i].Temp.Min)
                    });
                }
                

                var replace = this.Weather.FirstOrDefault(i => i.City == item.City);
                var index = this.Weather.IndexOf(replace);
                this.Weather[index] = item;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        

        private double ConvertToCelsius(double kelvin)
        {
            // (K − 273.15)
            return kelvin - KELVIN;
        }

        private double ConvertToFahrenheit(double kelvin)
        {
            // (K − 273.15) × 9/5 + 32 = 53.33 °F
            return ((kelvin - KELVIN) * 9) / 5 + 32;
        }

        private DateTime ConvertToDateTime(int unixTimestamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimestamp).ToLocalTime();
            return dtDateTime;
        }

        // TODO: Create SQLite to store city and the coordinates
        
    }
}
