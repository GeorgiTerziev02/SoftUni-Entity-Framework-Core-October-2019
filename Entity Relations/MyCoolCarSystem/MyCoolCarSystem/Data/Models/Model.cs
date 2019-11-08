﻿namespace MyCoolCarSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataValidations.Model;

    public class Model
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxModificationLength)]
        public string Modification { get; set; }

        public int Year { get; set; }

        public int MakeId { get; set; }

        public Make Make { get; set; }

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
