using System.Text;

namespace SImpl.DotNetStack.Diagnostics
{
    public class DiagnosticsWriter : IDiagnosticsWriter
    {
        private readonly StringBuilder _builder;
        private readonly string _prefix;

        public DiagnosticsWriter(StringBuilder builder, string prefix = "")
        {
            _builder = builder;
            _prefix = prefix;
        }
        public void AppendLine(string value)
        {
            _builder.AppendLine($"{_prefix}{value}");
        }
    }
}