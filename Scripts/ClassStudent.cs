using System;

namespace LOK2239
{
    public class ClassStudent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public StudentState State { get; set; } = StudentState.NoRemarks;
        public DateOnly? DateLatestletter { get; set; }
        public DateOnly? DateAbsenceCheckStartDate { get; set; }
    }
}
