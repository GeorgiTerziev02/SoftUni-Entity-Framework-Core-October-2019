namespace VaporStore.DataProcessor.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Purchase")]
    public class ImportPurchaseDTO
    {
        //      <Purchase title = "Dungeon Warfare 2" >
        //< Type > Digital </ Type >
        //< Key > ZTZ3 - 0D2S-G4TJ</Key>
        //<Card>1833 5024 0553 6211</Card>
        //<Date>07/12/2016 05:49</Date>

        [XmlAttribute("title")]
        [Required]
        public string GameName { get; set; }

        [XmlElement("Type")]
        [Required]
        public string Type { get; set; }

        [XmlElement("Key")]
        [Required]
        [RegularExpression("^[0-9A-Z]{4}[-][0-9A-Z]{4}[-][0-9A-Z]{4}$")]
        public string ProductKey { get; set; }

        [XmlElement("Card")]
        [Required]
        [RegularExpression("^[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}[ ][0-9]{4}$")]
        public string CardNumber { get; set; }

        [XmlElement("Date")]
        [Required]
        public string Date { get; set; }
    }
}
