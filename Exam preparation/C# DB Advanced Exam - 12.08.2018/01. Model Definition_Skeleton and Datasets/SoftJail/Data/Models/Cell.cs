namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Cell
    {
        [Key]
        public int Id { get; set; }

        [Required, Range(1, 1000)]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Prisoner> Prisoners { get; set; } = new HashSet<Prisoner>();
    }
}
