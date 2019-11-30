namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Producer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30), MinLength(3), Required]
        public string Name { get; set; }

        [RegularExpression(@"[A-Z]{1}[a-z]+[ ][A-Z]{1}[a-z]+$")]
        public string Pseudonym { get; set; }

        [RegularExpression(@"[+]{1}[0-9]{3}[ ][0-9]{3}[ ][0-9]{3}[ ][0-9]{3}$")]
        public string PhoneNumber  { get; set; }

        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}
