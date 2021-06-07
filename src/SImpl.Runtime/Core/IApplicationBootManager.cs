using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Application.Builders;
using SImpl.Modules;

namespace SImpl.Runtime.Core
{
    public interface IApplicationBootManager
    {
        IEnumerable<TApplicationModule> Configure<TApplicationModule, TApplicationBuilder>(TApplicationBuilder appBuilder)
            where TApplicationModule : class, IApplicationModule<TApplicationBuilder>
            where TApplicationBuilder : ISImplApplicationBuilder;
        
        Task StartAsync<TApplicationModule>(IHost host)
            where TApplicationModule : IDotNetStackApplicationModule;

        Task StopAsync<TApplicationModule>(IHost host)
            where TApplicationModule : IDotNetStackApplicationModule;
    }
}