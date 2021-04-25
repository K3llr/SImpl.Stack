using System;

namespace Simpl.DependencyInjection.Exceptions
{
    public class InvalidDependencyException : Exception
    {
        public IDependency Dependency { get; set; }

        public InvalidDependencyException(string message, IDependency dependency) : base(message)
        {
            Dependency = dependency;
        }

        public InvalidDependencyException(string message, IDependency dependency, Exception innerException) : base(
            message, innerException)
        {
            Dependency = dependency;
        }
    }
}