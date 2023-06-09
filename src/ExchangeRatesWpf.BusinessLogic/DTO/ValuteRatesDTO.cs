using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesWpf.BusinessLogic.DTO
{
    public class ValuteRatesDTO
    {
        public string? Name { get; set; }
        public string? Nominal { get; set; }
        public string? ValueRub { get; set; }
        public string? ValueUsd { get; set; }
        public string? DifferenceRubInPercent { get; set; }
        public string? DifferenceUsdInPercent { get; set; }
    }
}
