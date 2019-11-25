namespace ProductShop.Dtos.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("SoldProducts")]
    public class ExportSoldProductsDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public List<ExportSoldProductDTO> SoldProducts { get; set; }
    }
}
