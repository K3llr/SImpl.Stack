using Serilog;
using Serilog.Extensions.Logging;

namespace SImpl.Http.Logging.Serilog.Services
{
    public static class LoggerExtensions
    {
        public static Microsoft.Extensions.Logging.ILogger AsDotnetCoreLogger(this ILogger logger, string name)
        {
            return new SerilogLoggerProvider(logger).CreateLogger(name);
        } 
    }
}