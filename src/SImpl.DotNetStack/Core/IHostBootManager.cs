using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Core
{
    public interface IHostBootManager
    {
        IEnumerable<IDotNetStackModule> Modules { get; }
        
        void PreInit();

        void ConfigureServices(IHostBuilder hostBuilder);

        void ConfigureHostBuilder(IHostBuilder hostBuilder);

        void ConfigureHost(IHost host);

        // TODO: Start + stop
    }
}