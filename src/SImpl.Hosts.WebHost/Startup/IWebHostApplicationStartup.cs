using SImpl.Application.Builders;

namespace SImpl.Hosts.WebHost.Startup
{
    public interface IWebHostApplicationStartup
    {
        void ConfigureStackApplication(IWebHostApplicationBuilder configureDelegate);
    }
}