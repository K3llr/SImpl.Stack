using Microsoft.Extensions.DependencyInjection;

namespace Novicell.App.Hosted.Modules
{
    public interface IServicesCollectionConfigureModule
    {
        void ConfigureServices(IServiceCollection services);
    }
}