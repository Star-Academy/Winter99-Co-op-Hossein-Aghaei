using System;

namespace elasticsearch
{
    public class BadResponseException : Exception
    {
        public BadResponseException() : 
            base("Response isn't you expected honey!")
        {
        }
    }
}