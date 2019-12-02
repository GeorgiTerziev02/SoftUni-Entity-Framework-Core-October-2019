namespace VaporStore.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Game")]
    public class ExportGameDTO
    {
        //      <Game title = "Counter-Strike: Global Offensive" >
        //< Genre > Action </ Genre >
        //< Price > 12.49 </ Price >
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlElement("Genre")]
        public string Genre { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
