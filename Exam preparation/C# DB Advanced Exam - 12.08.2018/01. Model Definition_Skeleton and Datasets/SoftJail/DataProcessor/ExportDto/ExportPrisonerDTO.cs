namespace SoftJail.DataProcessor.ExportDto
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class ExportPrisonerDTO
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public List<ExportMessageDTO> EncryptedMessages { get; set; }
    }
}
