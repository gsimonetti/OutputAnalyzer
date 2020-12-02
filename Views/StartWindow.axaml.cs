using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OutputAnalyzer.Views
{
    public class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
