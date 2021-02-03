namespace SImpl.Runtime.Verbosity
{
    public class ModuleInfo
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string[] Implements { get; set; }
        public string[] DependentOn { get; set; }
    }
}