using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.Repository.Services;

namespace SImpl.Storage.Repository.Module
{
    public class DapperRepositoryModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(DapperRepositoryModule);

        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}