using Autofac;
using ExchangeRatesWpf.DataAccess.Entities;
using ExchangeRatesWpf.DataAccess.Interfaces;
using ExchangeRatesWpf.DataAccess.XML;

namespace ExchangeRatesWpf.BusinessLogic.Module;

public class BusinessLogicModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<XMLProvider>().As<IDataUrlProvider<Valute>>();
        base.Load(builder);
    }

}
