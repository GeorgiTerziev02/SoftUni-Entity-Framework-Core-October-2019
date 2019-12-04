namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class ImportMailDTO
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required, RegularExpression("^[A-z0-9 ]+ [s][t][r][.]$")]
        public string Address { get; set; }
    }
}
