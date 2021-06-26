using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using Microsoft.Extensions.DependencyInjection;
using Simpl.DependencyInjection.Models;

namespace Simpl.DependencyInjection
{
    public interface IContainerRegisterService
    {
        void Register<TAbstraction, TImplementation>(Dependency<TAbstraction, TImplementation> dependency)
            where TAbstraction : class where TImplementation : class, TAbstraction;
    }
}