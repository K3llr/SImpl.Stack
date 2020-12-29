using Microsoft.Extensions.Hosting;

namespace SImpl.DotNetStack.Modules
{
    public interface IHostBuilderConfigureModule : IDotNetStackModule
    {
        void ConfigureHostBuilder(IHostBuilder hostBuilder);
    }
}