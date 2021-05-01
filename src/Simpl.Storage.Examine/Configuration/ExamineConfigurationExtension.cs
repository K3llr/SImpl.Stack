using System;
using SImpl.Host.Builders;

namespace Novicell.App.Examine.Configuration
{ 
    public static class ExamineConfigurationExtension
    {
        public static void UseExamine(this ISImplHostBuilder novicellAppBuilder, Action<ExamineConfig> configurer)
        {
            
            var existingExamineModule = novicellAppBuilder.GetConfiguredModule<ExamineModule>();
            var examineConfig = existingExamineModule != null
                ? existingExamineModule.Config
                : AttachModule(novicellAppBuilder);
            configurer?.Invoke(examineConfig);
        }
        private static ExamineConfig AttachModule(ISImplHostBuilder novicellAppBuilder)
        {
            var config = new ExamineConfig();
            var module = new ExamineModule(config);

            novicellAppBuilder.AttachNewOrGetConfiguredModule<ExamineModule>(()=>module);

            return config;
        }
    }
}