namespace TeisterMask.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeTask
    {
        //        •	EmployeeId - integer, Primary Key, foreign key(required)
        //•	Employee -  Employee
        //•	TaskId - integer, Primary Key, foreign key(required)
        //•	Task - Task
        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
}
