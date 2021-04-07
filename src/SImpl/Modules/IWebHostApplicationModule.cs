using SImpl.Application.Builders;

namespace SImpl.Modules
{
    public interface IWebHostApplicationModule : IWebHostModule, IApplicationModule<IWebHostApplicationBuilder>
    {
        
    }
}