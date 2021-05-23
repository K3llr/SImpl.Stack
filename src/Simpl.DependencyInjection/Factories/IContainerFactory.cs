using Microsoft.Extensions.DependencyInjection;
using SImpl.Factories;

namespace Novicell.App.Headless.Core.Factories
{
    public interface IContainerFactory<out T> : IFactory<T>
    {
        //in .net 5 scope is based on Service so we need pass new scope service if we create new scope
        T New(IServiceScope scope);
    }
}