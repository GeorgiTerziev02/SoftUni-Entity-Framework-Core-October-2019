namespace PetClinic.DataProcessor.DTOs
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ImportAnimalDTO
    {
        [MinLength(3), MaxLength(20), Required]
        public string Name { get; set; }

        [MinLength(3), MaxLength(20), Required]
        public string Type { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Age { get; set; }

        [JsonProperty("Passport")]
        public ImportPassportDTO Passport { get; set; }
    }
}
