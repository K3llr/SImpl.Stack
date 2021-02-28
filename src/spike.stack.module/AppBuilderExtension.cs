using SImpl.Host.Builders;
using SImpl.Runtime;

namespace spike.stack.module
{
    public static partial class AppBuilderExtension
    {
        public static void UseDotNetStackTestModule(this ISImplHostBuilder stackHostBuilders)
        {
            stackHostBuilders.Use<DotNetStackTestModule>();
        }
    }
}