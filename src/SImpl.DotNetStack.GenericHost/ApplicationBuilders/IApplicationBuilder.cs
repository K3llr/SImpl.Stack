using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.Core;

namespace SImpl.DotNetStack.GenericHost.ApplicationBuilders
{
    public interface IApplicationBuilder
    {
        IDotNetStackRuntime Runtime { get; }

        IDotNetStackApplication Build();
    }
}