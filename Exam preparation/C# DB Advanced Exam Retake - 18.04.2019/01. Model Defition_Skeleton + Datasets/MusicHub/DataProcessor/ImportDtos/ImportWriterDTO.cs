namespace MusicHub.DataProcessor.ImportDtos
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class ImportWriterDTO
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Pseudonym")]
        public string Pseudonym { get; set; }
    }
}
