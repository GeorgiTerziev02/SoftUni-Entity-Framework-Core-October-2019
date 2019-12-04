namespace SoftJail.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ImportDepartmentDTO
    {
        [Required, MinLength(3), MaxLength(25)]
        public string Name { get; set; }

        public List<ImportCellDTO> Cells { get; set; }
    }
}
