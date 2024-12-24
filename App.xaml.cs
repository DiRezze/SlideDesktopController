namespace SlideDesktopController
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            setMinimumWindowSize();
        }

        private void setMinimumWindowSize()
        {
            var window = Application.Current?.Windows?.FirstOrDefault();
            if (window != null) 
            {
                window.MinimumWidth = 320;
            }
        }
    }
}
