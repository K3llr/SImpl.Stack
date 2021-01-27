namespace SImpl.Runtime.Diagnostics
{
    public interface IDiagnosticsSection
    {
        string Headline { get; }
        void Append(IDiagnosticsWriter writer);
    }
}