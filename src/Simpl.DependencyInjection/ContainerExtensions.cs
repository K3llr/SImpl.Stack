using System;

namespace Simpl.DependencyInjection
{
    public static class ContainerAwareExtension
    {
        public static T Resolve<T>(this IContainerAware me)
            where T : class
        {
            return ContainerService.Current?.Resolve<T>();
        }

        public static T Resolve<T>(this IContainerAware me, Type type)
            where T : class
        {
            return ContainerService.Current?.Resolve<T>(type);
        }
    }
}