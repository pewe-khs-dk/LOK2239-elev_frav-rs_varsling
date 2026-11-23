using System;

namespace LOK2239
{
    public class LogEntry
    {
        public string ClassId { get; set; }
        public string StudentId { get; set; }
        public string ClassShortName { get; set; }
        public string ClassLongName { get; set; }
        public DateOnly? AbsenceCalculationStartDate { get; set; } // For school year first period this is NULL. We only have week number can't be sure it starts monday. Hard to find actual date.
        public DateOnly AbsenceCalculationEndDate { get; set; } 
    }
}
