namespace Cinema.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    [XmlType("Ticket")]
    public class ImportTicketDTO
    {
        [XmlElement("ProjectionId")]
        [Required]
        public int ProjectionId { get; set; }

        [XmlElement("Price")]
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
