using System;

namespace Simpl.DependencyInjection
{
    public interface IDependency
    {
        string ErrorMessage { get; }
        Type Abstraction { get; set; }
        Type Implementation { get; set; }
        DependencyType Type { get; set; }
        bool IsInvalid();
    }
}