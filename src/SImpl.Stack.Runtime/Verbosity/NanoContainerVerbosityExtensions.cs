using Microsoft.Extensions.Logging;
using SImpl.NanoContainer;
using SImpl.Stack.HostBuilders;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Runtime.Verbosity
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
            container.RegisterDecorator<IBootSequenceFactory, VerboseBootSequenceFactory>();
            container.RegisterDecorator<IHostBootManager, VerboseHostBootManager>();
            container.RegisterDecorator<IDotNetStackHostBuilder, VerboseHostBuilder>();
            container.RegisterDecorator<IApplicationBootManager, VerboseApplicationBootManager>();
            
            return container;
        }
    }
}