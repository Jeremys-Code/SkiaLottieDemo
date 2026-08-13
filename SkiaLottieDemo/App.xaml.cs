namespace SkiaLottieDemo
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }
        public App(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
            InitializeComponent();
        }

        public static T GetService<T>() => Services.GetService<T>();

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}