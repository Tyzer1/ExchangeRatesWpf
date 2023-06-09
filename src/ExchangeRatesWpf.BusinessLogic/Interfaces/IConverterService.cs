using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesWpf.BusinessLogic.Interfaces
{
    public interface IConverterService
    {
        Task<double> Convert(double rateFrom, double rateTo, double value);
    }
}
