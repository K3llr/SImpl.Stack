using Microsoft.Extensions.Hosting;
using Novicell.App.Console.Extensions.Configuration;

namespace Novicell.App.Console.Extensions.HostBuilders
{
    public interface IConsoleHostBuilder
    {
        IHostBuilder HostBuilder { get; }
        
        IStartupConfiguration CompositeStartupConfiguration { get; }
    }
}