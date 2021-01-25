using System;

namespace SImpl.Stack.Runtime.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException() { }

        public InvalidConfigurationException(string message) : base(message) { }

        public InvalidConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}