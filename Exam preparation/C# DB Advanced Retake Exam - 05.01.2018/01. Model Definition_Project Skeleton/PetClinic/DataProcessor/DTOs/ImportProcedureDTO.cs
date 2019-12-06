namespace PetClinic.DataProcessor.DTOs
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    [XmlType("Procedure")]
    public class ImportProcedureDTO
    {
        [XmlElement("Vet")]
        [Required]
        public string VetName { get; set; }

        [XmlElement("Animal")]
        [Required]
        public string AnimalSerialNumber { get; set; }

        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public List<ImportAnimalAidNameDTO> AnimalAids { get; set; }
    }
}
