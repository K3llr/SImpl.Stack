using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;

namespace SImpl.Hosts.WebHost.Startup
{
    public interface IInternalWebHostApplicationStartup
    {
        void ConfigureServices(IServiceCollection services);

        void ConfigureStackApplication(IWebHostApplicationBuilder builder);
    }
}