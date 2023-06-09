using ExchangeRatesWpf.Presentation.ViewModels;

namespace ExchangeRatesWpf.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
