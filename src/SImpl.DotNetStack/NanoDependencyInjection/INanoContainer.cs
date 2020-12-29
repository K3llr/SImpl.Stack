using System;

namespace SImpl.DotNetStack.DI
{
    public interface INanoContainer
    {
        INanoContainer Register<TService, TImplementation>()
            where TImplementation : TService;

        INanoContainer Register<TService>(Func<TService> factory);
        
        INanoContainer Register<TService>(TService instance);
        
        INanoContainer RegisterDecorator<TService, TDecorator>()
            where TDecorator : TService;

        TService Resolve<TService>();
        
        object Resolve(Type serviceType);
    }
}