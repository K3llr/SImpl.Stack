using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Hosts.WebHost.Modules
{
    public interface IWebHostApplicationModule : IApplicationModule<IWebHostApplicationBuilder>
    {
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}