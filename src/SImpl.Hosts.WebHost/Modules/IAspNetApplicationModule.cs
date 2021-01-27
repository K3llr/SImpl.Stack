using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost.Modules
{
    public interface IAspNetApplicationModule : IWebHostApplicationModule
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}