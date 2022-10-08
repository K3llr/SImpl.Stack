using System;

namespace SImpl.Host.Builders;

public interface ISImplWebApplicationBuilder{
    void Configure(ISImplWebApplicationBuilder dotNetStackHostBuilder, Action<object> action);
}