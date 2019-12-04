namespace SoftJail.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Officer")]
    public class ImportOfficerDTO
    {
        [XmlElement("Name")]
        [Required, MinLength(3), MaxLength(30)]
        public string FullName { get; set; }

        [XmlElement("Money")]
        [Required, Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [XmlElement("Position")]
        [Required]
        public string Position { get; set; }

        [XmlElement("Weapon")]
        [Required]
        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]
        [Required]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public List<ImportPrisonerIdDTO> Prisoners { get; set; }
    }
}
