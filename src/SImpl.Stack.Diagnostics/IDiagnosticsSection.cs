namespace SImpl.Stack.Diagnostics
{
    public interface IDiagnosticsSection
    {
        string Headline { get; }
        void Append(IDiagnosticsWriter writer);
    }
}