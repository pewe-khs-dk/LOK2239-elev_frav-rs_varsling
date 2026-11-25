using System;

namespace LOK2239
{
    public class ClassStudent
    {
        public string Id { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public float CurrentPercentage { get; set; }
        public float ModuleCountSincePeriodStart { get; set; }
        public DateOnly DateCreation { get; set; }
        public DateOnly? DateLatestLetter { get; set; }
        public StudentState State { get; set; } = StudentState.NoRemarks;
        public bool ViolationHandlingPending { get; set; } // Will be set in first pass (ProcessNewStudent and ProcessStudentWithHistory) and reset after violation is handled
    }
}
