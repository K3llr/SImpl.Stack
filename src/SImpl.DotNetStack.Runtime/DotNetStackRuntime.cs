using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Runtime.ApplicationBuilders;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.DotNetStack.Runtime.Diagnostics;
using SImpl.DotNetStack.Runtime.Host;
using SImpl.DotNetStack.Runtime.HostBuilders;
using SImpl.DotNetStack.Runtime.Verbosity;
using SImpl.NanoContainer;

namespace SImpl.DotNetStack.Runtime
{
    public class DotNetStackRuntime
    {
        public static IHostBuilder Init(IHostBuilder hostBuilder, string[] args, Action<IDotNetStackHostBuilder> configureDelegate)
        {
            var diagnostics = DiagnosticsCollector.Create();
            
            // Write welcome to console
            ConsoleWriteNameAndVersion();
            
            // Parse command line arguments
            var flags = ParseArgs(args);

            // Create .NET Stack host builder
            var dotNetStackHostBuilder = CreateDotNetStackHostBuilder(hostBuilder, flags, diagnostics);
            
            // Start configuration of the host builder
            dotNetStackHostBuilder.Configure(dotNetStackHostBuilder, configureDelegate);
            
            return dotNetStackHostBuilder;
        }
        
        private static IDotNetStackHostBuilder CreateDotNetStackHostBuilder(IHostBuilder hostBuilder, RuntimeFlags flags, DiagnosticsCollector diagnostics)
        {
            var container = new NanoContainer.NanoContainer();
            
            // Register container
            container.Register<INanoContainer>(container);
            
            // Register parameters
            container.Register<IHostBuilder>(hostBuilder);
            container.Register<IDiagnosticsCollector>(diagnostics);
            container.Register<RuntimeFlags>(flags);
            
            // Register core services
            container.Register<IModuleManager, ModuleManager>();
            container.Register<IBootSequenceFactory, BootSequenceFactory>();
            container.Register<IDotNetStackRuntimeServices, DotNetStackRuntimeServices>();
            
            // Register host builder and boot manager
            container.Register<IHostBootManager, HostBootManager>();
            container.Register<IDotNetStackHostBuilder, DotNetStackHostBuilder>();
            
            // register application builder and boot manager
            container.Register<IApplicationBootManager, ApplicationBootManager>();
            
            // Handle cli flags
            if (flags.Diagnostics)
            {
                container.AddDiagnostics();
            }
            
            if (flags.Verbose)
            {
                container.AddVerbosity();
            }

            // Init runtime servcies
            DotNetStackRuntimeServices.Init(container.Resolve<IDotNetStackRuntimeServices>());
            
            return container.Resolve<IDotNetStackHostBuilder>();
        }

        private static RuntimeFlags ParseArgs(string[] args)
        {
            var flags = new RuntimeFlags();
            
            // Define and parse commands
            var cmd = new RootCommand("SImpl .NET Stack runtime")
            {
                new Option<bool>(
                    new[] {"--diagnostics", "-d"},
                    getDefaultValue: () => false,
                    description: "Enable stack diagnostics"),
                new Option<bool>(
                    new[] {"--verbose", "-v"},
                    getDefaultValue: () => false,
                    description: "Enable verbose logging")
            };

            var requiresBoot = false;
            cmd.Handler = CommandHandler.Create<bool, bool>((diagnostics, verbose) =>
            {
                // Parse args into flags
                flags.Diagnostics = diagnostics;
                flags.Verbose = verbose;

                // Args has been parsed
                requiresBoot = true;
            });

            cmd.Invoke(args);

            if (!requiresBoot)
            {
                Environment.Exit(0);
            }

            return flags;
        }

        private static void ConsoleWriteNameAndVersion()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"SImpl .NET Stack runtime version {typeof(DotNetStackRuntime).Assembly.GetName().Version}");
            Console.ResetColor();
        }
    }
}