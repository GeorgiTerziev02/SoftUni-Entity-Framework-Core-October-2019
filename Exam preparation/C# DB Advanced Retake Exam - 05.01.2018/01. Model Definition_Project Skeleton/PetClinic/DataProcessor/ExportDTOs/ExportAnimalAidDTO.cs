namespace PetClinic.DataProcessor.ExportDTOs
{
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class ExportAnimalAidDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
