namespace Cinema.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    [XmlType("Customer")]
    public class ImportCustomerDTO
    {
        [XmlElement("FirstName")]
        [Required, MinLength(3), MaxLength(20)]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        [Required, MinLength(3), MaxLength(20)]
        public string LastName { get; set; }

        [XmlElement("Age")]
        [Required, Range(12, 110)]
        public int Age { get; set; }

        [XmlElement("Balance")]
        [Required, Range(0.01, double.MaxValue)]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public List<ImportTicketDTO> Tickets { get; set; }

    }
}
