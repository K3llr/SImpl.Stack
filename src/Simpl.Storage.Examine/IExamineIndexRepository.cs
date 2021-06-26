using Examine;
using Lucene.Net.Analysis;

namespace Simpl.Storage.Examine
{
    public interface IExamineIndexRepository
    {
        void Save(string name,ValueSet document);
        ISearchResults Get(string name,string key);
        ISearchResults GetMultiple(string name,params string[] keys);
        ISearchResults GetByWildcardKey(string name,string key);
        void Delete(string name,string key);
        ISearcher GetSearcher(string name);



        void CreateIndex(string name, FieldDefinitionCollection fields, Analyzer analyzer = null);
        IIndex GetIndex(string name);
    }
}