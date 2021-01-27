using System.Collections.Generic;
using System.Threading.Tasks;
using SImpl.Application.Builders;
using SImpl.Modules;

namespace SImpl.Runtime.Core
{
    public interface IApplicationBootManager
    {
        IEnumerable<TApplicationModule> Configure<TApplicationModule, TApplicationBuilder>(TApplicationBuilder appBuilder)
            where TApplicationModule : class, IApplicationModule<TApplicationBuilder>
            where TApplicationBuilder : ISImplApplicationBuilder;
        
        Task StartAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule;

        Task StopAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule;
    }
}