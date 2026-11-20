using System;
using System.Collections.Generic;

namespace LOK2239
{
    public class ClassStudent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClassShortName { get; set; }
        public StudentState State { get; set; } = StudentState.NoRemarks;
        public float CurrentPercentage { get; set; }
        public int ModuleCountSincePeriodStart { get; set; }
        public DateOnly? DateLatestletter { get; set; }
        public DateOnly? DateOfLatestProcessing { get; set; }
        public List<string> Log { get; set; } = [];
    }
}
