namespace MusicHub.DataProcessor.ImportDtos
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ImportProducerDTO
    {
        [MaxLength(30), MinLength(3), Required]
        public string Name { get; set; }

        [RegularExpression(@"[A-Z]{1}[a-z]+[ ][A-Z]{1}[a-z]+$")]
        public string Pseudonym { get; set; }

        [RegularExpression(@"[+]{1}[0-9]{3}[ ][0-9]{3}[ ][0-9]{3}[ ][0-9]{3}$")]
        public string PhoneNumber { get; set; }

        public List<ImportAlbumDTO> Albums { get; set; }
    }
}
