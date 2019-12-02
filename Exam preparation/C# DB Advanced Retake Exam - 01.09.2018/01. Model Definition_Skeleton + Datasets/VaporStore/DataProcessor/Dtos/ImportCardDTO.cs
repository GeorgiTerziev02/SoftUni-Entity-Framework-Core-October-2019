namespace VaporStore.DataProcessor.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class ImportCardDTO
    {
        //        "Number": "1111 1111 1111 1111",
        //"CVC": "111",
        //"Type": "Debit"
        [JsonProperty("Number")]
        [Required]
        [RegularExpression("^[0-9]{4}[ ][0-9]{4}[ ][0-9]{4}[ ][0-9]{4}$")]
        public string Number { get; set; }

        [JsonProperty("CVC")]
        [Required]
        [RegularExpression("^[0-9]{3}$")]
        public string CVC { get; set; }

        [JsonProperty("Type")]
        [Required]
        public string Type { get; set; }
    }
}
