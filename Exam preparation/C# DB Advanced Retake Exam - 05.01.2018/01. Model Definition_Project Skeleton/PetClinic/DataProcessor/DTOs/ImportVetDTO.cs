namespace PetClinic.DataProcessor.DTOs
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    [XmlType("Vet")]
    public class ImportVetDTO
    {
        [XmlElement("Name")]
        [Required, MinLength(3), MaxLength(40)]
        public string Name { get; set; }

        [XmlElement("Profession")]
        [Required, MinLength(3), MaxLength(50)]
        public string Profession { get; set; }

        [XmlElement("Age")]
        [Required, Range(22, 65)]
        public int Age { get; set; }

        [XmlElement("PhoneNumber")]
        [Required, RegularExpression("^([+][3][5][9]|[0])[0-9]{9}$")]
        public string PhoneNumber { get; set; }
    }
}
