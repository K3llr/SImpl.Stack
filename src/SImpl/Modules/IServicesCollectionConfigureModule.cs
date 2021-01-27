using Microsoft.Extensions.DependencyInjection;

namespace SImpl.Modules
{
    public interface IServicesCollectionConfigureModule : ISImplModule
    {
        void ConfigureServices(IServiceCollection services);
    }
}