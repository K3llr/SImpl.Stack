using Newtonsoft.Json;

namespace SImpl.CQRS.OpenTelemetry.Helpers;

internal static class SerializationHelper
{
    private const string SerializationError = "Serialization error.";

    internal static string SerializeActivityObject<T>(T obj)
    {
        try
        {
            return JsonConvert.SerializeObject(obj,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
        }
        catch
        {
            return SerializationError;
        }
    }
}