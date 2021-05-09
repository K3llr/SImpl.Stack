using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;

namespace SImpl.Reflect.Providers
{
    public class ForceLoadAssemblyProvider : IForceLoadAssemblyProvider
    {
        private readonly string[] _blacklistedAssemblyPrefixes = new string[]
        {
            "mscorlib",
            "System",
            "Microsoft"
        };

        private IEnumerable<Assembly> _assemblies = null;
        public IEnumerable<Assembly> GetAssemblies()
        {
            return _assemblies ??= LoadAssemblies();
        }

        private IEnumerable<Assembly> LoadAssemblies()
        {
            var appDomainAssemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => !IsBlacklisted(assembly.FullName));

            var binFolder = string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath)
                ? AppDomain.CurrentDomain.BaseDirectory // Base directory for non-web apps
                : AppDomain.CurrentDomain.RelativeSearchPath; // Base directory for ASP.NET web apps

            var binAssemblyFiles = Directory.GetFiles(binFolder)
                .Where(assemblyFile =>
                    !string.IsNullOrWhiteSpace(assemblyFile)
                    && assemblyFile.EndsWith(".dll")
                    && !IsBlacklisted(assemblyFile));

            var loadedAssemblies = new List<Assembly>();

            foreach (var filePath in binAssemblyFiles)
            {
                try
                {
                    var ass = Assembly.LoadFrom(filePath);
                    loadedAssemblies.Add(ass);
                }
                catch (Exception e)
                {
                    if (e is SecurityException || e is BadImageFormatException)
                    {
                        // Swallow exceptions
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return appDomainAssemblies
                .Union(loadedAssemblies)
                .Distinct();
        }

        private bool IsBlacklisted(string assemblyName)
        {
            return _blacklistedAssemblyPrefixes.Any(
                blacklistedPrefix => assemblyName
                    ?.StartsWith(blacklistedPrefix) ?? false);
        }
    }
}