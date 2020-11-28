using Microsoft.Extensions.Hosting;

namespace Novicell.App.Hosted.Modules
{
    public interface IHostObjectConfigureModule
    {
        void ConfigureHost(IHost host);
    }
}