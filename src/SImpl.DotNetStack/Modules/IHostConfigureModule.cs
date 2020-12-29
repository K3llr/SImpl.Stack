using Microsoft.Extensions.Hosting;

namespace SImpl.DotNetStack.Modules
{
    public interface IHostConfigureModule : IDotNetStackModule
    {
        void ConfigureHost(IHost host);
    }
}