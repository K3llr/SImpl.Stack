using Microsoft.Extensions.Hosting;
using SImpl.Host.Builders;

namespace SImpl.Modules
{
    public interface IHostBuilderConfigureModule : ISImplModule
    {
        void ConfigureHostBuilder(ISImplHostBuilder hostBuilder);
    }
}