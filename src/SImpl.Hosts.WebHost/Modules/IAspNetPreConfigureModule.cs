using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost.Modules
{
    public interface IAspNetConfigureModule : ISImplModule
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}