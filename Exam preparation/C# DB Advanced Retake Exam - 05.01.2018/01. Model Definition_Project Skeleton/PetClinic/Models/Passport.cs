namespace PetClinic.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Passport
    {
        [Key]
        [RegularExpression("^[A-z]{7}[0-9]{3}$")]
        public string SerialNumber { get; set; }

        [Required]
        public Animal Animal { get; set; }

        [RegularExpression("^([+][3][5][9]|[0])[0-9]{9}$")]
        public string OwnerPhoneNumber  { get; set; }

        [Required, MinLength(3),MaxLength(30)]
        public string OwnerName { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

    }
}
