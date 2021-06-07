using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Application.Builders;

namespace SImpl.Modules
{
    public interface IApplicationModule : IApplicationModule<ISImplApplicationBuilder>
    {
    }

    public interface IApplicationModule<in TApplicationBuilder> : IDotNetStackApplicationModule
        where TApplicationBuilder : ISImplApplicationBuilder
    {
        void Configure(TApplicationBuilder builder);
    }

    public interface IDotNetStackApplicationModule : ISImplModule
    {
        Task StartAsync(IHost host);
        
        Task StopAsync(IHost host);
    }
}