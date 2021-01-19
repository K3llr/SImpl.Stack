using System.Collections.Generic;
using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Application
{
    public interface IApplicationBootManager
    {
        IEnumerable<IApplicationModule> Configure(IDotNetStackApplicationBuilder appBuilder);

        Task StartAsync();

        Task StopAsync();
    }
}