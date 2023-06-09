using Autofac;
using ExchangeRatesWpf.BusinessLogic.Module;

namespace ExchangeRatesWpf.BusinessLogic.Infrastructure;

public static class IocContainer
{
    public static readonly IContainer Container;
    static IocContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<BusinessLogicModule>();
        Container = builder.Build();
    }
}
