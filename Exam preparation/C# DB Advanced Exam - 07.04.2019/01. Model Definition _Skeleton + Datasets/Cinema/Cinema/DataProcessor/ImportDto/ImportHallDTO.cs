namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class ImportHallDTO
    {
        //    "Name": "Methocarbamol",
        //"Is4Dx": false,
        //"Is3D": true,
        //"Seats": 52
        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        [Range(1, int.MaxValue)]
        public int Seats { get; set; }

    }
}
