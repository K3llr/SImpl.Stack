using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Hosts.WebHost.Modules
{
    public interface IAspNetApplicationModule : IWebHostApplicationModule
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}