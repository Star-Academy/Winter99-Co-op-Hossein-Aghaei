using System;

namespace elasticsearch
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : 
            base("there is some bugs in request, fix then request again\n" +
                 "Bikar nistim ma ke golam alaki mizani")
        {
        }
    }
}