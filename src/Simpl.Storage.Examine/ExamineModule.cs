using Examine;
using Microsoft.Extensions.DependencyInjection;
using Novicell.App.Examine.Configuration;
using SImpl.Modules;

namespace Novicell.App.Examine
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