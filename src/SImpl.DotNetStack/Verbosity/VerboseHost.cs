using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Core;

namespace SImpl.DotNetStack.Diagnostics
{
    public class DiagnosticsHost : IHost
    {
        private readonly IHost _host;
        private readonly IDotNetStackRuntime _runtime;

        public DiagnosticsHost(IHost host, IDotNetStackRuntime runtime)
        {
            _host = host;
            _runtime = runtime;
        }
        
        public void Dispose()
        {
            _host.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("");
            Console.WriteLine("Diagnostics:");
            Console.WriteLine("> Flags");
            Console.WriteLine($"    --diagnostics: {_runtime.Flags.Diagnostics }");
            Console.WriteLine($"    --verbose: {_runtime.Flags.Verbose }");
            Console.WriteLine("");
            
            Console.ResetColor();
            
            await _host.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _host.StopAsync(cancellationToken);
            
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("");
            Console.WriteLine("Post diagnostics:");
            // TODO: ?
            Console.WriteLine("");
            
            Console.ResetColor();
        }

        public IServiceProvider Services => _host.Services;
    }
}