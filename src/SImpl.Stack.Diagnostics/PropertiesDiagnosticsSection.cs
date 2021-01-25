using System.Collections.Specialized;

namespace SImpl.Stack.Diagnostics
{
    public class PropertiesDiagnosticsSection : IDiagnosticsSection
    {
        public string Headline { get; set; }

        public NameValueCollection Properties { get; } = new NameValueCollection();
        
        public void Append(IDiagnosticsWriter writer)
        {
            foreach (string key in Properties.Keys)
            {
                writer.AppendLine($"- {key}:{Properties[key]}");
            }
            
        }
    }
}