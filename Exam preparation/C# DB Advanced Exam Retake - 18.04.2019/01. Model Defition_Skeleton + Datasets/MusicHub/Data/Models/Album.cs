namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Album
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(40), MinLength(3), Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public decimal Price => this.Songs.Sum(p => p.Price);

        public int? ProducerId { get; set; }

        public Producer Producer { get; set; }

        public ICollection<Song> Songs { get; set; } = new HashSet<Song>();
    }
}
