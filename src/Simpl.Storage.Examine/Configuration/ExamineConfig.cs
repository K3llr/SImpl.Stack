namespace Simpl.Storage.Examine.Configuration
{
    public class ExamineConfig
    {
        public void SetIndexType(string indexType) => IndexType = indexType;

        public string IndexType { get;  private set; }

       
    }
}