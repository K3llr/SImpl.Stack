using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

namespace SImpl.Http.CQRS.Module
{
    public class HttpCqrsModule : IServicesCollectionConfigureModule
    {
        public string Name => nameof(HttpCqrsModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}