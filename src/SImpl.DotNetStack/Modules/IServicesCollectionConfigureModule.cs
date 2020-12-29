using Microsoft.Extensions.DependencyInjection;

namespace SImpl.DotNetStack.Modules
{
    public interface IServicesCollectionConfigureModule : IDotNetStackModule
    {
        void ConfigureServices(IServiceCollection services);
    }
}