using SImpl.AutoRun;
using SImpl.Hosts.WebHost;
using SImpl.Runtime;
using spike.stack.module;

var builder = WebApplication.CreateBuilder(args);
var simplify = builder.SImplify(args, simpl =>
{
    simpl.Use<GreetingsWebModule>();
    simpl.Use<RootModule>();
    simpl.UseAutoRunModules();
    simpl.ConfigureWebHostStackApp(webHost =>
    {
        webHost.ConfigureApplication(app =>
        {
            app.UseAppModule<TestStackedApplicationModule>();
            app.UseAppModule<ApplicationTestModule>();
        });
    });
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.Run();