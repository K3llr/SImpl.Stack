using Microsoft.Extensions.Hosting;

namespace SImpl.Modules
{
    public interface IHostConfigureModule : ISImplModule
    {
        void ConfigureHost(IHost host);
    }
}