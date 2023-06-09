using System.Xml.Serialization;

namespace ExchangeRatesWpf.DataAccess.Entities;

public class Valute
{
    [XmlElement(ElementName = "NumCode")]
    public string? NumCode { get; set; }
    [XmlElement(ElementName = "CharCode")]
    public string? CharCode { get; set; }
    [XmlElement(ElementName = "Nominal")]
    public string? Nominal { get; set; }
    [XmlElement(ElementName = "Name")]
    public string? Name { get; set; }
    [XmlElement(ElementName = "Value")]
    public string? Value { get; set; }
}
