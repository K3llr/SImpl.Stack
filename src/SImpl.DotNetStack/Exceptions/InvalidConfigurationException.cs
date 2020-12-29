using System;

namespace Novicell.DotNetStack.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException() { }

        public InvalidConfigurationException(string message) : base(message) { }

        public InvalidConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}