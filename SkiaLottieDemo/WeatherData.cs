using SkiaSharp.Extended.UI.Controls;

namespace SkiaLottieDemo;

public class WeatherData
{

    public class Rootobject
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public Hourly_Units hourly_units { get; set; }
        public Hourly hourly { get; set; }
    }

    public class Hourly_Units
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string dew_point_2m { get; set; }
        public string weather_code { get; set; }
        public string relative_humidity_2m { get; set; }
    }

    public class Hourly
    {
        public DateTime[] time { get; set; }
        public double[] temperature_2m { get; set; }
        public double[] dew_point_2m { get; set; }
        public int[] weather_code { get; set; }
        public int[] relative_humidity_2m { get; set; }
        public int[] is_day { get; set; }
    }

}

public class HourImages
{
    public DateTime Time { get; set; }
    public bool IsDay { get; set; }
    public double Temp { get; set; }
    public string TempUnit { get; set; }
    public double Dewpoint { get; set; }
    public string DewpointUnit { get; set; }
    public int WmoCode { get; set; }
    public string MeteoconResource { get; set; }
    public int RelativeHumidity { get; set; }
    public string RelativeHumidityUnit { get; set; }
    public SKFileLottieImageSource SkHourImage { get; set; }
}
