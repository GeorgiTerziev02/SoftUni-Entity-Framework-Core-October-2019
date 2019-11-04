namespace EFCodeFirstTest.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataValidations.Student;

    public class Student
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public StudentType Type { get; set; }
    }
}
