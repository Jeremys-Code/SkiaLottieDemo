using System.Collections.Concurrent;
using System.Globalization;

namespace SkiaLottieDemo.Converters
{
    public class LottieUrlToJsonConverter : IValueConverter
    {
        static readonly ConcurrentDictionary<string, string> Cache = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var url = value is Uri u ? u.ToString() : value.ToString();

            if (Cache.TryGetValue(url, out var json))
                return json;

            var factory = App.GetService<IHttpClientFactory>() as IHttpClientFactory;
            var client = factory?.CreateClient("lottie") ?? new HttpClient();

            // synchronous call inside converter (ok for quick test); consider prefetching in VM for better UX
            json = client.GetStringAsync(url).GetAwaiter().GetResult();

            Cache.TryAdd(url, json);
            return json;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}

