using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Modules
{
    public interface IApplicationModule : IDotNetStackModule
    {
        void Configure(IDotNetStackApplicationBuilder builder);

        Task StartAsync();
        
        Task StopAsync();
    }
}