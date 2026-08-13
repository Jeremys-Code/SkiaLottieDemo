using SkiaLottieDemo.ViewModels;

namespace SkiaLottieDemo
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _vm;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = vm;
        }
    }
}
