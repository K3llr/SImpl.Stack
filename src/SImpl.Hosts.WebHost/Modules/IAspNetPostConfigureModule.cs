using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost.Modules
{
    public interface IAspNetPreConfigureModule : ISImplModule
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}