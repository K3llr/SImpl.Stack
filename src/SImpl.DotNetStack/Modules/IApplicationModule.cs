using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Modules
{
    public interface IApplicationConfigureModule : IApplicationConfigureModule<IDotNetStackApplicationBuilder>
    {
    }
    
    public interface IApplicationConfigureModule<in TApplicationBuilder> : IStartableModule
        where TApplicationBuilder : IDotNetStackApplicationBuilder
    {
        void ConfigureApplication(TApplicationBuilder builder);
    }
}