using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ExchangeRatesWpf.BusinessLogic.Interfaces;
using Autofac;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using ExchangeRatesWpf.Presentation.Models;

namespace ExchangeRatesWpf.Presentation.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private IExchangeRatesService _exchangeRatesService;
    private IConverterService _converterService;

    [ObservableProperty]
    private string _search;

    [ObservableProperty]
    private List<Valute> _valuteNames;

    [ObservableProperty]
    private List<Valute> _valutes;

    [ObservableProperty]
    private List<ValuteRates> _valutesRates;

    [ObservableProperty]
    private DateTime _valuteRatesDate;

    [ObservableProperty]
    private string _valuteOneName;

    [ObservableProperty]
    private string _valuteTwoName;

    [ObservableProperty]
    private string _valuteOneValue;

    [ObservableProperty]
    private string _valuteTwoValue;

    public MainWindowViewModel()
    {
        _exchangeRatesService = App.Container.Resolve<IExchangeRatesService>();
        _converterService = App.Container.Resolve<IConverterService>();
        Search = "";
        ValuteRatesDate = DateTime.Now;
        UpdateValuteCodes();
        UpdateValutes();
    }

    [RelayCommand]
    private async void UpdateRatesByDate()
    {
        var rates = await _exchangeRatesService.GetRelatedRatesByDateAsync(ValuteRatesDate);
        ValutesRates = new List<ValuteRates>(
            rates.Select(x => 
            new ValuteRates
            {
                Name = x.Name,
                Nominal = x.Nominal,
                ValueRub = x.ValueRub,
                ValueUsd = x.ValueUsd,
                DifferenceRubInPercent = x.DifferenceRubInPercent,
                DifferenceUsdInPercent = x.DifferenceUsdInPercent,
            }));
    }

    [RelayCommand]
    private async void UpdateValuteCodes()
    {
        var rates = await _exchangeRatesService.GetRatesByDateAsync(DateTime.Now);
        ValuteNames = new List<Valute>(
            rates.Where(x => x.CharCode.Contains(Search) || x.Name.Contains(Search))
            .Select(x => new Valute
            {
                Name = x.Name,
                Nominal = x.Nominal,
                CharCode = x.CharCode,
                NumCode = x.NumCode,
                Value = x.Value,
            }));
    }

    private async void UpdateValutes()
    {
        var rates = await _exchangeRatesService.GetRatesByDateAsync(DateTime.Now);
        Valutes = new List<Valute>(
            rates.Select(x => new Valute
            {
                Name = x.Name,
                Nominal = x.Nominal,
                CharCode = x.CharCode,
                NumCode = x.NumCode,
                Value = x.Value,
            }));

    }

    partial void OnValuteOneNameChanged(string value)
    {
        if (_valuteOneName != null && _valuteTwoName != null && _valuteOneValue != null)
            ConvertValueOne();
    }

    partial void OnValuteTwoNameChanged(string value)
    {
        if (_valuteOneName != null && _valuteTwoName != null && _valuteOneValue != null)
            ConvertValueOne();
    }

    partial void OnValuteOneValueChanged(string value)
    {
        if (_valuteOneName != null && _valuteTwoName != null && _valuteOneValue != null)
            ConvertValueOne();
    }

    partial void OnValuteTwoValueChanged(string value)
    {
        if (_valuteOneName != null && _valuteTwoName != null && _valuteTwoValue != null)
            ConvertValueTwo();
    }

    private async void ConvertValueOne()
    {
        var convertedValue = await Convert(_valuteOneName, _valuteTwoName, double.Parse(_valuteOneValue));
        ValuteTwoValue = String.Format("{0:0.##}", convertedValue);
    }
    
    private async void ConvertValueTwo()
    {
        var convertedValue = await Convert(_valuteTwoName, _valuteOneName, double.Parse(_valuteTwoValue));
        ValuteOneValue = String.Format("{0:0.##}", convertedValue);
    }

    private async Task<double> Convert(string valuteFromName, string valuteToName, double value)
    {
        var valuteFrom = _valutes.FirstOrDefault(x => x.Name == valuteFromName);
        var valuteTo = _valutes.FirstOrDefault(x => x.Name == valuteToName);
        var valuteFromValue = double.Parse(valuteFrom.Value) / double.Parse(valuteFrom.Nominal);
        var valuteToValue = double.Parse(valuteTo.Value) / double.Parse(valuteTo.Nominal);
        return await _converterService.Convert(valuteFromValue, valuteToValue, value);
    }
}