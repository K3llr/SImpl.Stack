using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using Microsoft.Extensions.DependencyInjection;

namespace Simpl.DependencyInjection
{
    public interface IContainerService
    {
        T Resolve<T>() where T : class;

        T Resolve<T>(Type t) where T : class;

        IList<T> ResolveCollection<T>() where T : class;

        IList<T> ResolveCollection<T>(Type t) where T : class;

        bool IsRegistered<T>();

        bool IsRegistered(Type t);

        IServiceScope BeginScope();
    }
}