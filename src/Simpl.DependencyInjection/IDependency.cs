using System;
using Simpl.DependencyInjection.Models;

namespace Simpl.DependencyInjection
{
    public interface IDependency
    {
        string ErrorMessage { get; }
        Type Abstraction { get; set; }
        Type Implementation { get; set; }
        Lifestyle Lifestyle { get; set; }
        DependencyType Type { get; set; }
        bool IsInvalid();
    }
}