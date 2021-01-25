using Microsoft.Extensions.Hosting;

namespace SImpl.Stack.Modules
{
    public interface IHostBuilderConfigureModule : IDotNetStackModule
    {
        void ConfigureHostBuilder(IHostBuilder hostBuilder);
    }
}