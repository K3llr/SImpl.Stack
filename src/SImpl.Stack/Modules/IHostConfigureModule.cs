using Microsoft.Extensions.Hosting;

namespace SImpl.Stack.Modules
{
    public interface IHostConfigureModule : IDotNetStackModule
    {
        void ConfigureHost(IHost host);
    }
}