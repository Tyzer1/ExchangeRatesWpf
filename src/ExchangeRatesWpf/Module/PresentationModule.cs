using Autofac;
using ExchangeRatesWpf.BusinessLogic.Interfaces;
using ExchangeRatesWpf.BusinessLogic.Services;

namespace ExchangeRatesWpf.Presentation.Module;

public class PresentationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ExchangeRatesService>().As<IExchangeRatesService>();
        builder.RegisterType<ConverterService>().As<IConverterService>();
        base.Load(builder);
    }
}
