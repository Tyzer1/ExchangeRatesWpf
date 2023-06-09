using ExchangeRatesWpf.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesWpf.BusinessLogic.Interfaces;

public interface IExchangeRatesService
{
    Task<IEnumerable<ValuteDTO>> GetRatesByDateAsync(DateTime date);
    Task<IEnumerable<ValuteRatesDTO>> GetRelatedRatesByDateAsync(DateTime date);
}
