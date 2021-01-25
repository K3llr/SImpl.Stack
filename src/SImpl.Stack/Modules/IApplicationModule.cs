using System.Threading.Tasks;
using SImpl.Stack.ApplicationBuilders;

namespace SImpl.Stack.Modules
{
    public interface IApplicationModule : IApplicationModule<IDotNetStackApplicationBuilder>
    {
    }

    public interface IApplicationModule<in TApplicationBuilder> : IDotNetStackApplicationModule
        where TApplicationBuilder : IDotNetStackApplicationBuilder
    {
        void Configure(TApplicationBuilder builder);
    }

    public interface IDotNetStackApplicationModule : IDotNetStackModule
    {
        Task StartAsync();
        
        Task StopAsync();
    }
}