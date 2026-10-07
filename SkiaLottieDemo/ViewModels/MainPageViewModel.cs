using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SkiaLottieDemo.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial List<Meteocon> Meteocons { get; set; }
        [ObservableProperty]
        public partial Meteocon FirstSelectedAnimation { get; set; }
        [ObservableProperty]
        public partial Meteocon SecondSelectedAnimation { get; set; }
        public MainPageViewModel()
        {
            var files = Directory.GetFiles("meteocons/", "*.json", SearchOption.TopDirectoryOnly);

            Meteocons = new List<Meteocon>();

            foreach (var file in files)
            {
                var m = new Meteocon
                {
                    ImageName = new FileInfo(file).Name,
                    Animation = new SkiaSharp.Extended.UI.Controls.SKFileLottieImageSource { File = new FileInfo(file).FullName }
                };

                Meteocons.Add(m);
            }

            FirstSelectedAnimation = Meteocons.First() ?? new Meteocon();
            SecondSelectedAnimation = Meteocons.Last() ?? new Meteocon();
        }

        [RelayCommand]
        private async Task GoToMeteocons()
        {
            await Browser.OpenAsync("https://meteocons.com/");
        }
        [RelayCommand]
        private async Task GoToBostonFromAbove()
        {
            await Browser.OpenAsync("https://bostonfromabove.com/");
        }
    }
}
