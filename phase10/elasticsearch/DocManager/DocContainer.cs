using System;
using System.Collections.Generic;

namespace elasticsearch
{
    public class DocContainer
    {
        public string NoSignWords { get; set; }
        public string PlusSignWords { get; set; }
        public string MinusSignWords { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DocContainer b1 &&
                   NoSignWords == b1.NoSignWords &&
                   PlusSignWords == b1.PlusSignWords &&
                   MinusSignWords == b1.MinusSignWords;
        }
        
      
        public override int GetHashCode()
        {
            return HashCode.Combine(NoSignWords, PlusSignWords, MinusSignWords);
        }

    }
}