using Microsoft.Extensions.DependencyInjection;

namespace SImpl.Stack.Modules
{
    public interface IServicesCollectionConfigureModule : IDotNetStackModule
    {
        void ConfigureServices(IServiceCollection services);
    }
}