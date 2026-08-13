using SkiaSharp.Extended.UI.Controls;
using System.Net.Http.Json;

namespace SkiaLottieDemo;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _client;
    string api = "https://api.open-meteo.com/v1/forecast?latitude=42.35&longitude=-71.09&hourly=temperature_2m,dew_point_2m,weather_code,relative_humidity_2m,is_day&models=ncep_gfs_seamless&timezone=auto&forecast_days=3&temporal_resolution=native&cell_selection=nearest";
    public WeatherService(IHttpClientFactory client)
    {
        _client = client.CreateClient("lottie");
    }

    public async Task<HourImages[]> GetHoursAsync()
    {
        var apiResult = await _client.GetFromJsonAsync<WeatherData.Rootobject>(api);
        if (apiResult == null)
            return null;

        HourImages[] hours = new HourImages[7];
        for (int i = 0; i < 7; i++)
        {
            hours[i] = new HourImages
            {
                Time = apiResult.hourly.time[i],
                IsDay = apiResult.hourly.is_day[i] == 1 ? true : false,
                Temp = apiResult.hourly.temperature_2m[i],
                TempUnit = apiResult.hourly_units.temperature_2m,
                Dewpoint = apiResult.hourly.dew_point_2m[i],
                DewpointUnit = apiResult.hourly_units.dew_point_2m,
                RelativeHumidity = apiResult.hourly.relative_humidity_2m[i],
                RelativeHumidityUnit = apiResult.hourly_units.relative_humidity_2m,
                WmoCode = apiResult.hourly.weather_code[i],
                SkHourImage = new SKFileLottieImageSource { File = await MeteoconsResolver.ResolveIcon(apiResult.hourly.weather_code[i], apiResult.hourly.is_day[i] == 1 ? true : false) }
            };
        }
        return hours;
    }
}