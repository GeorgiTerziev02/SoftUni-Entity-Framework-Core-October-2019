namespace PetClinic.DataProcessor.DTOs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImportPassportDTO
    {
        [RegularExpression("^[A-z]{7}[0-9]{3}$")]
        public string SerialNumber { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string OwnerName { get; set; }

        [RegularExpression("^([+][3][5][9]|[0])[0-9]{9}$")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        public string RegistrationDate { get; set; }
    }
}
