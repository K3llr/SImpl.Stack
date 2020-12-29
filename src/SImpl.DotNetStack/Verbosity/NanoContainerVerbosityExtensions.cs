using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.NanoDependencyInjection;

namespace SImpl.DotNetStack.Verbosity
{
    public static class NanoContainerVerbosityExtensions
    {
        public static INanoContainer AddVerbosity(this INanoContainer container)
        {
            container.Register(() =>
            {
                using var loggerFactory = LoggerFactory.Create(logging =>
                {
                    logging
                        .AddSimpleConsole(options =>
                        {
                            options.IncludeScopes = true;
                            options.SingleLine = true;
                            options.TimestampFormat = "hh:mm:ss.fff ";
                        })
                        .SetMinimumLevel(LogLevel.Trace);
                });

                return loggerFactory.CreateLogger<VerboseHost>();
            });
                
            container.RegisterDecorator<IModuleManager, VerboseModuleManager>();
            container.RegisterDecorator<IHostBootManager, VerboseHostBootManager>();
            container.RegisterDecorator<IDotNetStackHostBuilder, VerboseHostBuilder>();
            
            return container;
        }
    }
}