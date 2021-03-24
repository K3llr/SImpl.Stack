using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost.Modules
{
    public interface IAspNetPostConfigureModule : IWebHostModule
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}