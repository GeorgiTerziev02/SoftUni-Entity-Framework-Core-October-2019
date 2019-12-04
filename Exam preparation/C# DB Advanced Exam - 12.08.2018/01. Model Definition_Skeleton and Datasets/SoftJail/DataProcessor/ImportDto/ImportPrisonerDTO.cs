namespace SoftJail.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ImportPrisonerDTO
    {
        [Required, MinLength(3), MaxLength(20)]
        public string FullName { get; set; }

        [Required, RegularExpression("^[T][h][e][ ][A-Z][a-z]+$")]
        public string Nickname { get; set; }

        [Required, Range(18, 65)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public List<ImportMailDTO> Mails { get; set; }

    }
}
