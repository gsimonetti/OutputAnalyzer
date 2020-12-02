using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OutputAnalyzer.ViewModels;
using OutputAnalyzer.Views;

namespace OutputAnalyzer
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new StartWindow() { DataContext = new StartWindowViewModel() };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
