using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExchangeRatesWpf.DataAccess.Entities;

[XmlRoot(ElementName = "ValCurs")]
public class ValCurs
{
    [XmlElement(ElementName = "Valute")]
    public List<Valute> Valutes { get; set; }
}
