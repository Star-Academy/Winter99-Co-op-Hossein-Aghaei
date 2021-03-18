using System;

namespace elasticsearch
{
    public class BadAuthenticationException : Exception
    {
        public BadAuthenticationException() :
            base("You mess in security things!")
        {}
    }
}