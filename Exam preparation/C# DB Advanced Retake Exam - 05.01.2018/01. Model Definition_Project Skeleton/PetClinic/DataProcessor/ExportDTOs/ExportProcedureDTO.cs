namespace PetClinic.DataProcessor.ExportDTOs
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Procedure")]
    public class ExportProcedureDTO
    {
        [XmlElement("Password")]
        public string Passport { get; set; }

        [XmlElement("OwnerNumber")]
        public string OwnerNumber { get; set; }

        [XmlElement("DateTime")]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public List<ExportAnimalAidDTO> AnimalAids { get; set; }

        [XmlElement("TotalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
