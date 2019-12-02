namespace VaporStore.DataProcessor.Dtos
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Models;

    public class ImportUserDTO
    {
        //    "FullName": "",
        //"Username": "invalid",
        //"Email": "invalid@invalid.com",
        //"Age": 20,
        //"Cards": [
        
        [JsonProperty("FullName")]
        [Required]
        [RegularExpression("^[A-Z][a-z]+[ ][A-Z][a-z]+$")]
        public string FullName { get; set; }

        [JsonProperty("Username")]
        [Required, MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [JsonProperty("Email")]
        [Required]
        public string Email { get; set; }

        [JsonProperty("Age")]
        [Required, Range(3, 103)]
        public int Age { get; set; }

        [JsonProperty("Cards")]
        public ICollection<ImportCardDTO> Cards { get; set; }
    }
}
