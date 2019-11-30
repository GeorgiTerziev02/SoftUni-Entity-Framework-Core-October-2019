namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Performer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20), MinLength(3), Required]
        public string FirstName { get; set; }

        [MaxLength(20), MinLength(3), Required]
        public string LastName { get; set; }

        [Required, Range(18, 70)]
        public int Age { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal NetWorth  { get; set; }

        public ICollection<SongPerformer> PerformerSongs { get; set; } = new HashSet<SongPerformer>();
    }
}
