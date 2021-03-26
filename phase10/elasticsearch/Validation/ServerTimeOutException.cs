using System;

namespace elasticsearch
{
    [Serializable]
    public class ServerTimeOutException : Exception
    {
        public ServerTimeOutException() :
            base("Server is busy\nPlease Try again in a few Seconds")
        {
        }
    }
}