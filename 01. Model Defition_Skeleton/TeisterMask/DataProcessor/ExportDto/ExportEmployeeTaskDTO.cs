using Newtonsoft.Json;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class ExportEmployeeTaskDTO
    {
        //        "TaskName": "Pointed Gourd",
        //"OpenDate": "10/08/2018",
        //"DueDate": "10/24/2019",
        //"LabelType": "Priority",
        //"ExecutionType": "ProductBacklog"

            [JsonProperty("TaskName")]
        public string TaskName { get; set; }

        [JsonProperty("OpenDate")]
        public string OpenDate { get; set; }

        [JsonProperty("DueDate")]
        public string DueDate { get; set; }

        [JsonProperty("LabelType")]
        public string LabelType { get; set; }

        [JsonProperty("ExecutionType")]
        public string ExecutionType { get; set; }
    }
}
