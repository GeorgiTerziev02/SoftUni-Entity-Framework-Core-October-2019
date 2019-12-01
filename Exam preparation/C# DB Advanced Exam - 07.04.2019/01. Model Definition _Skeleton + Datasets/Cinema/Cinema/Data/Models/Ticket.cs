namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Ticket
    {
        //•	Id – integer, Primary Key
        //•	Price – decimal (non-negative, minimum value: 0.01) (required)
        //•	CustomerId – integer, foreign key(required)
        //•	Customer – the customer’s ticket 
        //•	ProjectionId – integer, foreign key(required)
        //•	Projection – the projection’s ticket

        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public int ProjectionId { get; set; }

        public Projection Projection { get; set; }
    }
}
