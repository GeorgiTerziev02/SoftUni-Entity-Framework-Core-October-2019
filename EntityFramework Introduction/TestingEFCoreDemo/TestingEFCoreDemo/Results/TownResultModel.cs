using System.Collections.Generic;

namespace TestingEFCoreDemo.Results
{
    public class TownResultModel
    {
        public string Name { get; set; }

        public IEnumerable<string> Address { get; set; }
    }
}
