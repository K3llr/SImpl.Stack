using System.Collections.Generic;
using Examine;
using Examine.Lucene;
using Examine.Lucene.Providers;
using Lucene.Net.Analysis;
using Lucene.Net.Store;

namespace Simpl.Storage.Examine
{
    public class ExamineIndex : LuceneIndex
    {
        public ExamineIndex(string name, Directory luceneDirectory, FieldDefinitionCollection fieldDefinitions = null, Analyzer analyzer  = null, IValueSetValidator validator = null, IReadOnlyDictionary<string, IFieldValueTypeFactory> indexValueTypesFactory = null) : base(name, luceneDirectory, fieldDefinitions, analyzer, validator, indexValueTypesFactory)
        {
        }
    }
}