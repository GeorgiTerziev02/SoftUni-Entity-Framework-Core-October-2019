namespace MusicHub.Data.Models
{
    using MusicHub.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public int? AlbumId { get; set; }

        public Album Album { get; set; }

        [Required]
        public int WriterId { get; set; }

        public Writer Writer { get; set; }

        public ICollection<SongPerformer> SongPerformers { get; set; } = new HashSet<SongPerformer>();
    }
}
