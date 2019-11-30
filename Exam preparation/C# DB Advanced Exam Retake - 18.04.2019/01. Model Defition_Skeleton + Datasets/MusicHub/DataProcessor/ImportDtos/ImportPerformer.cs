namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Performer")]
    public class ImportPerformer
    {
        [XmlElement("FirstName")]
        [MaxLength(20), MinLength(3), Required]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        [MaxLength(20), MinLength(3), Required]
        public string LastName { get; set; }

        [XmlElement("Age")]
        [Required, Range(18, 70)]
        public int Age { get; set; }

        [XmlElement("NetWorth")]
        [Required, Range(0, double.MaxValue)]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public List<ImportSongPerformerDTO> PerformersSongsIds { get; set; } = new List<ImportSongPerformerDTO>();
    }
}
