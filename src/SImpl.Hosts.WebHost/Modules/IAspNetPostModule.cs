using SImpl.Modules;

namespace SImpl.Hosts.WebHost.Modules
{
    public interface IAspNetPostModule : IAspNetPostConfigureModule, IServicesCollectionConfigureModule
    {
        
    }
}