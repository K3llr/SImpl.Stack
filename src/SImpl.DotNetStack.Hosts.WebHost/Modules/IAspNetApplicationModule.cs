using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace SImpl.DotNetStack.Hosts.WebHost.Modules
{
    public interface IAspNetApplicationModule : IWebHostApplicationModule
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}