using ExchangeRatesWpf.Presentation.ViewModels;
using MahApps.Metro.Controls;

namespace ExchangeRatesWpf.Presentation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
