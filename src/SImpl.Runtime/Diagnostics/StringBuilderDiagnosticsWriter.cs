using System.Text;

namespace SImpl.Runtime.Diagnostics
{
    public class StringBuilderDiagnosticsWriter : IDiagnosticsWriter
    {
        private readonly StringBuilder _builder;

        public StringBuilderDiagnosticsWriter(StringBuilder builder)
        {
            _builder = builder;
        }

        public void AppendLine(string value)
        {
            _builder.AppendLine(value);
        }
    }
}