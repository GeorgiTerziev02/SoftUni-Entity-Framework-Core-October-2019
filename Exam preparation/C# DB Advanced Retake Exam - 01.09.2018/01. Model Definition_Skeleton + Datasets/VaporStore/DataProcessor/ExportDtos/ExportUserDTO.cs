namespace VaporStore.DataProcessor.ExportDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ExportUserDTO
    {
        [XmlArray("Purchases")]
        public List<ExportPurchaseDTO> Purchases { get; set; }

        [XmlElement("TotalSpent")]
        public decimal TotalSpent { get; set; }
    }
}
