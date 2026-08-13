using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Collections.Concurrent;
using Microsoft.Maui.Controls;

namespace SkiaLottieDemo.Converters
{
    public class LottieUrlToStreamConverter : IValueConverter
    {
        static readonly ConcurrentDictionary<string, byte[]> _cache = new();
        static readonly HttpClient _httpClient = CreateClient();

        private static HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("WeatherApp/3.0");
            client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
            return client;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string url = value is Uri u ? u.ToString() : value.ToString();
            if (string.IsNullOrWhiteSpace(url))
                return null;

            try
            {
                var bytes = _cache.GetOrAdd(url, key => _httpClient.GetByteArrayAsync(key).GetAwaiter().GetResult());
                return new MemoryStream(bytes);
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
