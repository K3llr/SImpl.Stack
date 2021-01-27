using System;
using System.Collections.Generic;

namespace SImpl.NanoContainer
{
    public interface INanoContainer
    {
        INanoContainer Register<TService>(IServiceResolver serviceResolver);
        
        INanoContainer Register<TService, TImplementation>()
            where TImplementation : TService;

        INanoContainer Register<TService>(Func<TService> factory);
        
        INanoContainer Register<TService>(TService instance);
        
        INanoContainer RegisterDecorator<TService, TDecorator>()
            where TDecorator : TService;

        TService Resolve<TService>();
        
        object Resolve(Type serviceType);

        TService New<TService>();

        TService New<TService>(IDictionary<Type,object> overrideScope);
    }
}