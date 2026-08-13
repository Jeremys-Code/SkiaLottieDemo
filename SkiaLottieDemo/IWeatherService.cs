namespace SkiaLottieDemo
{
    public interface IWeatherService
    {
        Task<HourImages[]> GetHoursAsync();
    }
}