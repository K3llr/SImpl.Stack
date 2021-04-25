using System;
using Microsoft.Extensions.DependencyInjection;

namespace Simpl.DependencyInjection
{
    public interface IDependencyInjectionBundle
    {
        void Configure(IServiceCollection serviceCollection);
    }
}