using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesWpf.DataAccess.Interfaces;

public interface IDataUrlProvider<T> where T : class
{
    /// <summary>
    /// Get all data for 1 day from url
    /// </summary>
    Task<IEnumerable<T>> GetByDateAsync(DateTime date, string url);
}
