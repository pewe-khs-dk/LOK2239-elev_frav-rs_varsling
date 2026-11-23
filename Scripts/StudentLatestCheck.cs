using System;

namespace LOK2239
{
    public class StudentLatestCheck
    {
        public string Id { get; set; }
        public string ClassShortName { get; set; }
        public string Name { get; set; }
        public float CurrentPercentage { get; set; }
        public int ModuleCountSincePeriodStart { get; set; }
        public DateOnly? PeriodStartDate { get; set; }
        public DateOnly PeriodDate { get; set; }
        public DateOnly DateOfCheck { get; set; }
    }
}
