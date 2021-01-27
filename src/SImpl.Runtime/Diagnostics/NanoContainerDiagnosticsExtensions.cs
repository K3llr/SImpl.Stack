using Microsoft.Extensions.Logging;
using SImpl.NanoContainer;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime.Diagnostics
{
    public static class NanoContainerDiagnosticsExtensions
    {
        public static INanoContainer AddDiagnostics(this INanoContainer container)
        {
            container.Register(() =>
            {
                using var loggerFactory = LoggerFactory.Create(logging =>
                {
                    logging
                        .AddSimpleConsole(options =>
                        {
                            options.IncludeScopes = true;
                            options.TimestampFormat = "hh:mm:ss.fff ";
                        })
                        .SetMinimumLevel(LogLevel.Trace);
                });

                return loggerFactory.CreateLogger<DiagnosticsHost>();
            });
            
            container.RegisterDecorator<ISImplHostBuilder, DiagnosticsHostBuilder>();

            return container;
        }
    }
}