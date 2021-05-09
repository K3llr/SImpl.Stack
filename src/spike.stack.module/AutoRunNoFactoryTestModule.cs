using SImpl.AutoRun;
using SImpl.Modules;

[assembly:AutoRunDiscoverable]
namespace spike.stack.module
{
    [AutoRun]
    public class AutoRunNoFactoryTestModule : ISImplModule
    {
        public string Name { get; } = nameof(AutoRunNoFactoryTestModule);
    }
}