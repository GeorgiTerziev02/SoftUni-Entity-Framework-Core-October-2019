namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HallId { get; set; }

        public Hall Hall { get; set; }
                                           //        •	Id – integer, Primary Key
                                           //•	HallId – integer, foreign key(required)
                                           //•	Hall – the seat’s hall

    }
}
