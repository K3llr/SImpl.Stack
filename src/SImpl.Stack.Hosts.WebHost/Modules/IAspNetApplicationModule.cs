using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.Stack.Modules;

namespace SImpl.Stack.Hosts.WebHost.Modules
{
    public interface IAspNetApplicationModule : IWebHostApplicationModule
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}