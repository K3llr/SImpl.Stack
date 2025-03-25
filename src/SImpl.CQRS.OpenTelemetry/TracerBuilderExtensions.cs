using OpenTelemetry.Trace;

namespace SImpl.CQRS.OpenTelemetry;

public static class TracerBuilderExtensions
{
    public static TracerProviderBuilder AddSimpleStackInstrumentation(this TracerProviderBuilder builder)
    {
        builder.AddSource(Constants.SourceName);
        
        return builder;
    }
}