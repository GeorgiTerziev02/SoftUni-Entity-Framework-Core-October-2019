namespace MusicHub.DataProcessor.ImportDtos
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ImportProducerDTO
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Pseudonym")]
        public string Pseudonym { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Albums")]
        public List<ImportAlbumDTO> Albums { get; set; }
    }
}
