namespace MusicHub.DataProcessor.ExportDtos
{
    using System.Collections.Generic;

    public class ExportAlbumByProducerDTO
    {
        public string AlbumName { get; set; }

        public string ReleaseDate { get; set; }

        public string ProducerName { get; set; }

        public List<ExportSongFromAlbumDTO> Songs { get; set; }

        public string AlbumPrice { get; set; }
    }
}
