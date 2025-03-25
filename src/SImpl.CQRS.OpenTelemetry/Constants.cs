using System.Diagnostics;

namespace SImpl.CQRS.OpenTelemetry;

public static class Constants
{
    public const string SourceName = "Simple.Stack";
    public static readonly ActivitySource ActivitySource = new(SourceName);
}