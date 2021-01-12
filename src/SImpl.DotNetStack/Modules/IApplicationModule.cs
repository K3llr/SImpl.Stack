using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Modules
{
    public interface IApplicationModule : IApplicationModule<IDotNetStackApplicationBuilder>
    {
    }
    
    public interface IApplicationModule<in TApplicationBuilder> : IDotNetStackModule
        where TApplicationBuilder : IDotNetStackApplicationBuilder
    {
        void Configure(TApplicationBuilder builder);

        Task StartAsync();
        
        Task StopAsync();
    }
}