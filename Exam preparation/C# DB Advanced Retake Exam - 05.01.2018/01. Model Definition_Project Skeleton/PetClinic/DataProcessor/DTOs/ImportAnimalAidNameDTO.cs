namespace PetClinic.DataProcessor.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class ImportAnimalAidNameDTO
    {
        [XmlElement("Name")]
        [Required]
        public string Name { get; set; }
    }
}
