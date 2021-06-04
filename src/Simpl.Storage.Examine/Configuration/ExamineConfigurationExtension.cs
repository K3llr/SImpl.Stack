using System;
using Simpl.Examine;
using SImpl.Host.Builders;

namespace Simpl.Storage.Examine.Configuration
{ 
    public static class ExamineConfigurationExtension
    {
        public static void UseExamine(this ISImplHostBuilder simplHostBuilder, Action<ExamineConfig> configurer)
        {
            
            var existingExamineModule = simplHostBuilder.GetConfiguredModule<ExamineModule>();
            var examineConfig = existingExamineModule != null
                ? existingExamineModule.Config
                : AttachModule(simplHostBuilder);
            configurer?.Invoke(examineConfig);
        }
        private static ExamineConfig AttachModule(ISImplHostBuilder simplHostBuilder)
        {
            var config = new ExamineConfig();
            var module = new ExamineModule(config);

            simplHostBuilder.AttachNewOrGetConfiguredModule<ExamineModule>(()=>module);

            return config;
        }
    }
}