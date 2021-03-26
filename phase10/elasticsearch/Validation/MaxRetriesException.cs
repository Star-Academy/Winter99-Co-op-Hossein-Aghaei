using System;

namespace elasticsearch
{
    public class MaxRetriesException : Exception
    {
        public MaxRetriesException() :
            base("Mashti fek kon server khodete, kamtar request dede khoda vakili")
        {
        }
    }
}