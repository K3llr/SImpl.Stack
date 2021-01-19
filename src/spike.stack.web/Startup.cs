using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.WebHost.Extensions;
using SImpl.DotNetStack.ApplicationBuilders;
using spike.stack.app.Application;
using spike.stack.app.Domain;
using spike.stack.module;

namespace spike.stack.web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Works
            /*services.AddSingleton<IGreetingAppService, GreetingAppService>();
            services.AddSingleton<IGreetingService, SpanishGreetingService>();
            services.AddHostedService<GreetingHostedService>();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.UseDotNetStackWebApp(builder =>
            {
                builder.Use<TestStackedApplicationModule>();
            });
        }
    }
}
