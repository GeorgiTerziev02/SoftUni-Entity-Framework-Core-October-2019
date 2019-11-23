namespace XMLDemo.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("cars")]
    public class XmlCars
    {
        [XmlArray("cars")]
        public List<Car> Cars { get; set; }
    }
}
