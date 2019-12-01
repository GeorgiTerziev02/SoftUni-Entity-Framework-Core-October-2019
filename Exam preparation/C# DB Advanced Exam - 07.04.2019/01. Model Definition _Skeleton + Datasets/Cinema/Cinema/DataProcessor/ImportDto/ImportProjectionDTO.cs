namespace Cinema.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System;

    [XmlType("Projection")]
    public class ImportProjectionDTO
    {
        [XmlElement("MovieId")]
        [Required]
        public int MovieId { get; set; }

        [XmlElement("HallId")]
        [Required]
        public int HallId { get; set; }

        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }
    }
}
