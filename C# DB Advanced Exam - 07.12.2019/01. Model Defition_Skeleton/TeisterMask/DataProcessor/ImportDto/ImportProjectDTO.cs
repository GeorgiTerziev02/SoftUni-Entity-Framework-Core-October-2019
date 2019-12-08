namespace TeisterMask.DataProcessor.ImportDto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    
    [XmlType("Project")]
    public class ImportProjectDTO
    {
        //      <Project>
        //<Name>S</Name>
        //<OpenDate>25/01/2018</OpenDate>
        //<DueDate>16/08/2019</DueDate>
        //<Tasks>
        [Required, MinLength(2), MaxLength(40)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        //TODO: Maybe replace with dt
        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public List<ImportTaskDTO> Tasks { get; set; }
    }
}
