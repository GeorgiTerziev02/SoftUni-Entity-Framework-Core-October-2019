using System.ComponentModel.DataAnnotations;

namespace PetClinic.DataProcessor.DTOs
{
    public class ImportAnimalAidDTO
    {
        [MinLength(3), MaxLength(30), Required]
        public string Name { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
