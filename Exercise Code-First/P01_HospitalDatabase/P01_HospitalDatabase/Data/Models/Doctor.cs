namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataValidations.Doctor;

    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(DoctorMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DoctorMaxLength)]
        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; }
    }
}
