using System;

namespace elasticsearch
{
    public class WeakConnectionException : Exception
    {
        public WeakConnectionException() : 
            base("Connection is weak\n" +
                 "Try:\n" +
                 "1)Checking the connection\n" +
                 "2)Checking the proxy, firewall, and DNS configuration")
        {
        }
    }
}