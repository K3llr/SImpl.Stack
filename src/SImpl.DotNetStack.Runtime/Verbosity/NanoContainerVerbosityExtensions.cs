using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.DotNetStack.Runtime.Host;
using SImpl.NanoContainer;

namespace SImpl.DotNetStack.Runtime.Verbosity
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