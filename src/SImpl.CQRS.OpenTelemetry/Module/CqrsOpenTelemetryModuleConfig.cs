namespace SImpl.CQRS.OpenTelemetry.Module;

public sealed class CqrsOpenTelemetryModuleConfig
{
    public bool EnableCommandsInstrumentation { get; set; }
    public bool EnableEventsInstrumentation { get; set; }
    public bool EnableQueriesInstrumentation { get; set; }
    public bool EnableOnlyForInMemoryImplementations { get; set; }
}