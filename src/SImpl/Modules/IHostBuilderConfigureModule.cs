using Microsoft.Extensions.Hosting;

namespace SImpl.Modules
{
    public interface IHostBuilderConfigureModule : ISImplModule
    {
        void ConfigureHostBuilder(IHostBuilder hostBuilder);
    }
}