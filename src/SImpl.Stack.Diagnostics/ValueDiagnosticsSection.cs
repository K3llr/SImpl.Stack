using System.Text;

namespace SImpl.Stack.Diagnostics
{
    public class ValueDiagnosticsSection : IDiagnosticsSection
    {
        public string Headline { get; set; }

        public StringBuilder Value { get; } = new StringBuilder();
        
        public void Append(IDiagnosticsWriter writer)
        {
            writer.AppendLine($"{Value}");
        }
    }
}