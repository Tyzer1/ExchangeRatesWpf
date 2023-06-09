using Autofac;
using Autofac.Features.ResolveAnything;
using ExchangeRatesWpf.BusinessLogic.Module;
using ExchangeRatesWpf.Presentation.Module;
using System.Windows;

namespace ExchangeRatesWpf.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }
        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);
            var builder = new ContainerBuilder();
            builder.RegisterModule<PresentationModule>();
            Container = builder.Build();
        }
    }
}
