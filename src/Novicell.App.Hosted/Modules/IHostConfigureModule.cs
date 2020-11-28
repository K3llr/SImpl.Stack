using Microsoft.Extensions.Hosting;

namespace Novicell.App.Hosted.Modules
{
    public interface IHostConfigureModule
    {
        void ConfigureHost(IHostBuilder hostBuilder);
    }
}