namespace TeisterMask.DataProcessor.ExportDto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Project")]
    public class ExportProjectDTO
    {
        //      <Project TasksCount = "10" >
        //< ProjectName > Hyster - Yale </ ProjectName >
        //< HasEndDate > No </ HasEndDate >
        //< Tasks >
        //  < Task >
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public List<ExportTaskDTO> Tasks { get; set; }
    }
}
