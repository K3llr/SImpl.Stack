using System;
using System.Collections.Generic;
using Novicell.App.DependencyInjection.Configuration;
using SimpleInjector.Integration.ServiceCollection;

namespace SImpl.DotNetStack.GenericHost.DependencyInjection.Configuration
{
    public class GenericHostDependencyInjectionConfig
    {
        public IList<Action<SimpleInjectorAddOptions>> OptionConfigures { get; } = new List<Action<SimpleInjectorAddOptions>>();
        public DependencyInjectionConfig DependencyInjectionConfig { get; }

        public GenericHostDependencyInjectionConfig(DependencyInjectionConfig config)
        {
            DependencyInjectionConfig = config;
        }
        
        public void AddOptions(Action<SimpleInjectorAddOptions> optionConfigure)
        {
            OptionConfigures.Add(optionConfigure);
        }
    }
}