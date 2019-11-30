namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Xml.Serialization;

    [XmlType("Song")]
    public class ImportSongDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Duration")]
        public string Duration { get; set; }

        [XmlElement("CreatedOn")]
        public string CreatedOn { get; set; }

        [XmlElement("Genre")]
        public string Genre { get; set; }

        [XmlElement("AlbumId")]
        public int AlbumId { get; set; }

        [XmlElement("WriterId")]
        public int WriterId { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
