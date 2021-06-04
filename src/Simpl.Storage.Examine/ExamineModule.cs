using Examine;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using Simpl.Storage.Examine;
using Simpl.Storage.Examine.Configuration;

namespace Simpl.Examine
{
    public class ExamineModule : IServicesCollectionConfigureModule
    {
        public ExamineConfig Config { get; }
        
        public ExamineModule(ExamineConfig config)
        {
            Config = config;
        }

        public void Init()
        {
        }

        public string Name { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config);
            services.AddSingleton<IExamineManager, ExamineManager>();
            services.AddSingleton<IExamineIndexRepository, ExamineIndexRepository>();
        }
    }
}