using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;

namespace SImpl.Hosts.GenericHost.Startup
{
    public interface IGenericHostApplicationStartup
    {
        void ConfigureServices(IServiceCollection services);

        void ConfigureApplication(IGenericHostApplicationBuilder builder);
    }
}