namespace VaporStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class Developer
    {
//        •	Id – integer, Primary Key
//•	Name – text(required)
//•	Games - collection of type Game

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
