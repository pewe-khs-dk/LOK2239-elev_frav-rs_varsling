using System.Collections.Generic;

namespace LOK2239
{
    public class Subject
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string LinkToClasses { get; set; }
        public List<Class> Classes { get; set; } = [];
    }
}
