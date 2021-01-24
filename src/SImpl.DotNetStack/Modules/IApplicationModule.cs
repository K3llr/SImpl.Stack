using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Modules
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