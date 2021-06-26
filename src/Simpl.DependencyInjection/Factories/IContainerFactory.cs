using Microsoft.Extensions.DependencyInjection;
using SImpl.Factories;

namespace Simpl.DependencyInjection.Factories
{
    public interface IContainerFactory<out T> : IFactory<T>
    {
        // NOTE: in .net 5 scope is based on Service so we need pass new scope service if we create new scope
        // TODO: @Bielu What happens if resolve is called without a scope?
        // TODO: Might want to remove the IFactory inheritance if T new() can't sensible be used anyway
        T New(IServiceScope scope);
    }
}