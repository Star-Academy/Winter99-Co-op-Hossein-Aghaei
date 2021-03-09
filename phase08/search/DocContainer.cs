using System;
using System.Collections.Generic;
namespace search
{
    public class DocContainer
    {
        public HashSet<string> NoSignWords { get; set; }
        public HashSet<string> PlusSignWords { get; set; }
        public HashSet<string> MinusSignWords { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DocContainer b1 &&
                   NoSignWords.ToString() == b1.NoSignWords.ToString() &&
                   PlusSignWords.ToString() == b1.PlusSignWords.ToString() &&
                   MinusSignWords.ToString() == b1.MinusSignWords.ToString();
        }
        
      
        public override int GetHashCode()
        {
            return HashCode.Combine(NoSignWords, PlusSignWords, MinusSignWords);
        }

    }
}