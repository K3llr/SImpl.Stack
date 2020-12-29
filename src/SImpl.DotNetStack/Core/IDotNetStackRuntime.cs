using Microsoft.Extensions.Hosting;

namespace Novicell.DotNetStack.Core
{
    public interface IDotNetStackRuntime
    {
        IModuleManager ModuleManager { get; }
        IHostBuilder HostBuilder { get; }
    }
}