using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Host.Builders;
using SImpl.NanoContainer;
using SImpl.Runtime.Core;
using SImpl.Runtime.Diagnostics;
using SImpl.Runtime.Host.Builders;
using SImpl.Runtime.Modules;
using SImpl.Runtime.Verbosity;

namespace SImpl.Runtime
{
    public class SImply
    {
        public static IHostBuilder Boot(WebApplicationBuilder webApplicationBuilder, string[] args, ILogger logger, Action<ISImplHostBuilder> configureDelegate)
        {
            return Boot(webApplicationBuilder.Host, args, logger, configureDelegate);
        }
        public static IHostBuilder Boot(IHostBuilder hostBuilder, string[] args, ILogger logger, Action<ISImplHostBuilder> configureDelegate)
        {
            var diagnostics = DiagnosticsCollector.Create();
            
            // Write welcome to console
            ConsoleWriteNameAndVersion();
            
            // Parse command line arguments
            var flags = ParseArgs(args);
            
            // Init boot container
            var bootContainer = InitBootContainer(hostBuilder, logger ?? CreateLogger(), flags, diagnostics);
            
            // Init runtime services
            RuntimeServices.Init(bootContainer.Resolve<IRuntimeServices>());
            
            // Create .NET Stack host builder
            var dotNetStackHostBuilder = bootContainer.Resolve<ISImplHostBuilder>();
            
            // Start configuration of the host builder
            dotNetStackHostBuilder.Configure(dotNetStackHostBuilder, stack =>
            {
                // Pre-installed module
                stack.Use(() => new StackRuntimeModule(bootContainer));
                // Installed modules
                configureDelegate?.Invoke(stack);
            });
            
            return dotNetStackHostBuilder;
        }

        private static INanoContainer InitBootContainer(IHostBuilder hostBuilder, ILogger logger, RuntimeFlags flags, DiagnosticsCollector diagnostics)
        {
            var container = new NanoContainer.NanoContainer();

            // Register container
            container.Register<INanoContainer>(container);

            // Register logging
            container.Register<ILogger>(() => logger);
            
            // Register parameters
            container.Register<IHostBuilder>(hostBuilder);
            container.Register<IDiagnosticsCollector>(diagnostics);
            container.Register<RuntimeFlags>(flags);

            // Register core services
            container.Register<IModuleManager, ModuleManager>();
            container.Register<IBootSequenceFactory, BootSequenceFactory>();
            container.Register<IRuntimeServices, RuntimeServices>();

            // Register host builder and boot manager
            container.Register<IHostBootManager, HostBootManager>();
            container.Register<ISImplHostBuilder, SImplHostBuilder>();

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

            return container;
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
            Console.WriteLine($"SImpl runtime version {typeof(SImply).Assembly.GetName().Version}");
            Console.ResetColor();
        }

        private static ILogger CreateLogger()
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

            return loggerFactory.CreateLogger<SImply>();
        }
    }
}