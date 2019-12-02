namespace VaporStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GameTag
    {
        //        •	GameId – integer, Primary Key, foreign key(required)
        //•	TagId – integer, Primary Key, foreign key(required)
        //•	Game – Game
        //•	Tag – Tag
        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [Required]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
