using Microsoft.Extensions.DependencyInjection;

namespace Novicell.DotNetStack.Modules
{
    public interface IServicesCollectionConfigureModule : IDotNetStackModule
    {
        void ConfigureServices(IServiceCollection services);
    }
}