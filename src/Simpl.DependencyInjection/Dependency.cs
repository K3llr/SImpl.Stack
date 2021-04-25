using System;

namespace Simpl.DependencyInjection
{
    public enum DependencyType
    {
        Default,
        Decorator
    }

    public class Dependency : Dependency<dynamic, dynamic>
    {
        public Dependency()
        {
            Type = DependencyType.Default;
            Lifestyle = Lifestyle.Transient;
        }
    }

    public class Dependency<TAbstraction> : Dependency<TAbstraction, dynamic>
        where TAbstraction : class
    {
        public Dependency()
        {
            Type = DependencyType.Default;
            Lifestyle = Lifestyle.Transient;
            Abstraction = typeof(TAbstraction);
        }
    }

    public class Dependency<TAbstraction, TImplementation> : IDependency
        where TAbstraction : class
    {
        public Dependency()
        {
            Type = DependencyType.Default;
            Lifestyle = Lifestyle.Transient;
            Abstraction = typeof(TAbstraction);
            Implementation = typeof(TImplementation);
        }

        public Type Abstraction { get; set; }
        public Type Implementation { get; set; }
        public Func<TAbstraction> Factory { get; set; }
        public TAbstraction Instance { get; set; }
        public DependencyType Type { get; set; }
        public Lifestyle Lifestyle { get; set; }

        public virtual string ErrorMessage => $"A required dependency is missing: {Abstraction}";

        public virtual bool IsInvalid()
        {
            return Abstraction == null || (Implementation == null && Factory == null && Instance != null);
        }
    }

    public enum Lifestyle
    {
        Scope,
        Singleton,
        Transient
    }
}