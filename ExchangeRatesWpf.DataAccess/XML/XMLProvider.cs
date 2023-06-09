using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ExchangeRatesWpf.DataAccess.Interfaces;
using ExchangeRatesWpf.DataAccess.Entities;
using System.Net;
using System;
using System.Security.Policy;
using System.Text;
using System.Linq;
using System.Globalization;

namespace ExchangeRatesWpf.DataAccess.XML;

public class XMLProvider : IDataUrlProvider<Valute>
{
    public async Task<IEnumerable<Valute>> GetByDateAsync(DateTime date, string url)
    {
        string dateRequest = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        try
        {
            HttpClient client = new HttpClient();
            var bytes = await client.GetByteArrayAsync(url + dateRequest);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            string responseString = encoding.GetString(bytes, 0, bytes.Length);
            var responseStream = new StringReader(responseString);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.AutomaticDecompression = DecompressionMethods.GZip;
            //var response = await request.GetResponseAsync();
            //Stream responseStream = response.GetResponseStream();

            //var client = new HttpClient();
            //var response = await client.GetAsync(url + dateRequest);
            //var responseStream = response.Content.ReadAsStream();

            //Convert from XML to C# model:
            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));
            ValCurs? dailyExRates = (ValCurs?)serializer.Deserialize(responseStream);
            return dailyExRates.Valutes;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
