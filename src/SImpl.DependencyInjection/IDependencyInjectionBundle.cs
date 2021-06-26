using Microsoft.Extensions.DependencyInjection;

namespace SImpl.DependencyInjection
{
    public interface IDependencyInjectionBundle
    {
        void Configure(IServiceCollection serviceCollection);
    }
}