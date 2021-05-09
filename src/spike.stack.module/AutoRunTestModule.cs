using SImpl.AutoRun;
using SImpl.Factories;
using SImpl.Modules;

namespace spike.stack.module
{
    [AutoRun(typeof(AutoRunTestModuleFactory))]
    public class AutoRunTestModule : ISImplModule
    {
        public string Name { get; }

        public AutoRunTestModule(string name)
        {
            Name = name;
        }
    }

    public class AutoRunTestModuleFactory : IModuleFactory
    {
        public ISImplModule New()
        {
            return new AutoRunTestModule(nameof(AutoRunTestModule));
        }
    }
}