using System.Collections.Generic;

namespace search
{
    public class DocContainer
    {
        public HashSet<string> NoSignWords { get; set; }
        public HashSet<string> PlusSignWords { get; set; }
        public HashSet<string> MinusSignWords { get; set; }
        
        
    }
}