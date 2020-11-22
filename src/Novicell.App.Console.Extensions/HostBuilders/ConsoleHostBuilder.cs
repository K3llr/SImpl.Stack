using Microsoft.Extensions.Hosting;
using Novicell.App.Console.Extensions.Configuration;

namespace Novicell.App.Console.Extensions.HostBuilders
{
    public class ConsoleHostBuilder : IConsoleHostBuilder
    {
        public IHostBuilder HostBuilder { get; }
        public IStartupConfiguration CompositeStartupConfiguration { get; }

        public ConsoleHostBuilder(IHostBuilder hostBuilder, IStartupConfiguration compositeStartupConfiguration)
        {
            HostBuilder = hostBuilder;
            CompositeStartupConfiguration = compositeStartupConfiguration;
        }
    }
}