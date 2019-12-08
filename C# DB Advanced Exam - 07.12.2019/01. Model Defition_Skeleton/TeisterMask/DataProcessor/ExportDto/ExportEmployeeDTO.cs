using Newtonsoft.Json;
using System.Collections.Generic;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class ExportEmployeeDTO
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Tasks")]
        public List<ExportEmployeeTaskDTO> Tasks { get; set; }
    }
}
