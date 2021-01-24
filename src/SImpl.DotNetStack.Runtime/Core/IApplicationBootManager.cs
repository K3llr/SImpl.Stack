using System.Collections.Generic;
using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Runtime.Core
{
    public interface IApplicationBootManager
    {
        IEnumerable<TApplicationModule> Configure<TApplicationModule, TApplicationBuilder>(TApplicationBuilder appBuilder)
            where TApplicationModule : class, IApplicationModule<TApplicationBuilder>
            where TApplicationBuilder : IDotNetStackApplicationBuilder;
        
        Task StartAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule;

        Task StopAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule;
    }
}