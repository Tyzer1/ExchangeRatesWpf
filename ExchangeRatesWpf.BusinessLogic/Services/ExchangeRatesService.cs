using Autofac;
using ExchangeRatesWpf.BusinessLogic.DTO;
using ExchangeRatesWpf.BusinessLogic.Infrastructure;
using ExchangeRatesWpf.BusinessLogic.Interfaces;
using ExchangeRatesWpf.DataAccess.Entities;
using ExchangeRatesWpf.DataAccess.Interfaces;
using System.Configuration;
using System.Security.Policy;

namespace ExchangeRatesWpf.BusinessLogic.Services;

public class ExchangeRatesService : IExchangeRatesService
{
    private IDataUrlProvider<Valute> _provider;
    public ExchangeRatesService()
    {
        _provider = IocContainer.Container.Resolve<IDataUrlProvider<Valute>>();
    }

    public async Task<IEnumerable<ValuteDTO>> GetRatesByDateAsync(DateTime date)
    {
        try
        {
            string? url = ConfigurationManager.AppSettings.Get("SourceUrl");
            var rates = await _provider.GetByDateAsync(date, url);
            
            var currenciesDTO = rates.Select(x =>
            new ValuteDTO
            {
                NumCode = x.NumCode,
                CharCode = x.CharCode,
                Nominal = x.Nominal,
                Name = x.Name,
                Value = x.Value
            }).ToList();
            return currenciesDTO;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<ValuteRatesDTO>> GetRelatedRatesByDateAsync(DateTime date)
    {
        try
        {
            string? url = ConfigurationManager.AppSettings.Get("SourceUrl");

            var todayRates = await _provider.GetByDateAsync(DateTime.Now, url);
            var ratesByDate = await _provider.GetByDateAsync(date, url);

            string? usdRateByDate = ratesByDate?.FirstOrDefault(x => x.CharCode == "USD")?.Value;
            double.TryParse(usdRateByDate, out var usdToRubByDate);
            string? usdTodayRate = todayRates?.FirstOrDefault(x => x.CharCode == "USD")?.Value;
            double.TryParse(usdTodayRate, out var usdToRubToday);
            var valuteRatesDTO =
                ratesByDate.Select(x =>
                    new ValuteRatesDTO
                    {
                        Name = x.Name,
                        Nominal = x.Nominal,
                        ValueRub = String.Format("{0:0.##}", double.Parse(x.Value) / double.Parse(x.Nominal)),
                        ValueUsd = String.Format("{0:0.##}", double.Parse(x.Value) / double.Parse(x.Nominal) / usdToRubByDate),
                        DifferenceRubInPercent = GetDifferenceRub(todayRates.FirstOrDefault(y => y.CharCode == x.CharCode),
                            x),
                        DifferenceUsdInPercent = GetDifferenceUsd(todayRates.FirstOrDefault(y => y.CharCode == x.CharCode),
                            x,
                            usdToRubByDate,
                            usdToRubToday)
                    }).ToList();
            return valuteRatesDTO;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string GetDifferenceRub(Valute todayValute, Valute ValuteByDate)
    {
        var RateByDate = double.Parse(ValuteByDate.Value) / double.Parse(ValuteByDate.Nominal);
        var RateToday = double.Parse(todayValute.Value) / double.Parse(todayValute.Nominal);
        return String.Format("{0:0.##}", (RateByDate - RateToday) / RateByDate * 100);
    }
    private string GetDifferenceUsd(Valute todayValute,
        Valute ValuteByDate,
        double usdToRubByDate,
        double usdToRubToday)
    {
        var RateByDate = (double.Parse(ValuteByDate.Value) / double.Parse(ValuteByDate.Nominal)) 
            / usdToRubByDate;
        var RateToday = (double.Parse(todayValute.Value) / double.Parse(todayValute.Nominal)) 
            / usdToRubToday;
        return String.Format("{0:0.##}", (RateByDate - RateToday) / RateByDate * 100);
    }
}