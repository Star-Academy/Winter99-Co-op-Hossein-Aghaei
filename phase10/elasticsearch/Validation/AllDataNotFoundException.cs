using System;

namespace elasticsearch
{
    [Serializable]
    public class AllDataNotFoundException : Exception
    {
        public AllDataNotFoundException()
            : base("Some data is missing on\nTry again in a few minutes for actual result")
        {
        }
        
    }
}