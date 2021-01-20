using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Modules
{
    public interface IApplicationModule : IApplicationModule<IDotNetStackApplicationBuilder>
    {
        Task StartAsync();
        Task StopAsync();
    }

    public interface IApplicationModule<in TApplicationBuilder> : IDotNetStackModule
        where TApplicationBuilder : IDotNetStackApplicationBuilder
    {
        void Configure(TApplicationBuilder builder);
    }
}