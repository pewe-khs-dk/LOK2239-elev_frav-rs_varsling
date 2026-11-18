using System.Collections.Generic;

namespace LOK2239
{
    public static class Messages
    {
        public static List<string> List { get; set; } = [];
        
        public static List<string> Add(string message)
        {
            List.Add(message);
            return List;
        }
    }
}
