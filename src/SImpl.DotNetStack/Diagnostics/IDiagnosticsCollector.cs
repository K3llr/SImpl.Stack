namespace SImpl.DotNetStack.Diagnostics
{
    public interface IDiagnosticsWriter
    {
        void AppendLine(string value);
    }
}