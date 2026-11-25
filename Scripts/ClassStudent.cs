using System;

namespace LOK2239
{
    public class ClassStudent
    {
        public string Id { get; set; }
        public string ClassShortName { get; set; }
        public string Name { get; set; }
        public StudentState State { get; set; } = StudentState.NoRemarks;
        public float CurrentPercentage { get; set; }
        public float ModuleCountSincePeriodStart { get; set; }
        public DateOnly? DateLatestletter { get; set; }
        public DateOnly? DateAbsenceCheckStartDate { get; set; }
        public DateOnly? DateOfLatestProcessing { get; set; }
        public StudentLatestCheck LatestCheck { get; set; }
        public StudentLatestCheck DateCreation { get; set; }
    }
}
