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
        private ObservableCollection<Daily> dailyWeather = new ObservableCollection<Daily>();

        public WeatherViewModel()
        {
            Title = "Weather";

            Weather.Add(new WeatherInfo()
            {
                City = "Nutley",
                CurrentIcon = "https://openweathermap.org/img/wn/10d@2x.png",
                CurrentCelsius = 27,
                CurrentFahrenheit = 70
            });
            Weather.Add(new WeatherInfo()
            {
                City = "Passaic",
                CurrentIcon = "https://openweathermap.org/img/wn/10d@2x.png",
                CurrentCelsius = 27,
                CurrentFahrenheit = 71
            });

           LoadWeatherCommand = new Command<WeatherInfo>(async (item) => await LoadWeatherData(item));
        }

        public ObservableCollection<WeatherInfo> Weather
        {
            get => weather;
            set => weather = value;
        }

        public ObservableCollection<Daily> DailyWeather
        {
            get => dailyWeather;
            set => dailyWeather = value;
        }

        private async Task LoadWeatherData(WeatherInfo item)
        {
            IsBusy = true;
            

            try
            {
                var cityInfo = await WeatherService.GetCurrentWeatherByCity(item.City);
                var currentCityWeather = await WeatherService.GetCurrentAndForecastWeather(cityInfo.Coord.Lat, cityInfo.Coord.Lon);

                item.CurrentCelsius = (int)ConvertToCelsius(currentCityWeather.Current.Temp);
                item.CurrentFahrenheit = (int)ConvertToFahrenheit(currentCityWeather.Current.Temp);
                var replace = this.Weather.FirstOrDefault(i => i.City == item.City);
                var index = this.Weather.IndexOf(replace);
                this.Weather[index] = item;

                // Load Daily Weather
                currentCityWeather.Daily.ForEach(this.DailyWeather.Add);
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
            return kelvin - KELVIN;
        }

        private double ConvertToFahrenheit(double kelvin)
        {
            // (K − 273.15) × 9/5 + 32 = 53.33 °F
            return ((kelvin - KELVIN) * 9) / 5 + 32;
        }
    }
}
