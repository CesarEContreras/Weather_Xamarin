using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Weather.Helpers
{
    public class Settings
    {
        private static readonly Lazy<Settings> lazy = new Lazy<Settings>(() => new Settings());

        private Settings()
        {
        }

        public static Settings Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public string OpeanWeatherSecret
        {
            get
            {
                var token = GetSettingFile["openweather-appid"].ToString();
                return ValidateToken(token, "#{openweather-appid}#") ? string.Empty : token;
            }
        }

        public string AppCenterTokenIOS
        {
            get
            {
                var token = GetSettingFile["appcenter-ios-token"].ToString();
                return ValidateToken(token, "#{appcenter-ios-token}#") ? string.Empty : token;
            }
        }

        public string AppCenterTokenAndroid
        {
            get
            {
                var token = GetSettingFile["appcenter-android-token"].ToString();
                return ValidateToken(token, "#{appcenter-android-token}#") ? string.Empty : token;
            }
        }

        private JObject GetSettingFile
        {
            get
            {
                return JObject.Parse(File.ReadAllText(@"settings.json"));
            }
        }

        private bool ValidateToken(string token, string validator)
        {
            return token == validator || string.IsNullOrEmpty(token);
        }
    }
}
