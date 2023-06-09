using ExchangeRatesWpf.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesWpf.BusinessLogic.Services;

public class ConverterService : IConverterService
{
    public async Task<double> Convert(double rateFrom, double rateTo, double value)
    {
        return rateFrom / rateTo * value;
    }
}
