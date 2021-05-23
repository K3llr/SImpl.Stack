using System;
using System.IO;
using System.Linq;
using Examine;
using Examine.LuceneEngine.Directories;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Microsoft.Extensions.Configuration;
using Simpl.DependencyInjection;
using Simpl.Storage.Examine.Configuration;
using Directory = Lucene.Net.Store.Directory;

namespace Simpl.Storage.Examine
{
    public class ExamineIndexRepository : IExamineIndexRepository, IContainerAware
    {
        private readonly ExamineConfig _examineConfig;
        private readonly IExamineManager _examineManager;

        private readonly IConfiguration _configuration;
        // private readonly ILoggingService _loggingService;


        public ExamineIndexRepository(ExamineConfig examineConfig, IExamineManager examineManager, IConfiguration configuration
           // ,  ILoggingService loggingService
            )
        {
            _examineConfig = examineConfig;
            _examineManager = examineManager;
            _configuration = configuration;
            //  _loggingService = loggingService;
            DirectoryFactory.DefaultLockFactory = d =>
            {
                var simpleFsLockFactory = new NoPrefixSimpleFsLockFactory(d);
                return simpleFsLockFactory;
            };
            foreach (var luceneIndexer in examineManager.Indexes.OfType<ExamineIndex>())
            {
               // loggingService.Info("Forcing index {IndexerName} to be unlocked since it was left in a locked state", luceneIndexer.Name);

                luceneIndexer.WaitForIndexQueueOnShutdown = false;
                var dir = luceneIndexer.GetLuceneDirectory();
                if (!string.IsNullOrEmpty(dir.GetLockID()))
                {
                    dir.ClearLock("write.lock");
                }

                if (IndexWriter.IsLocked(dir))
                {
                    IndexWriter.Unlock(dir);
                }
            }
        }

        public void CreateIndex(string name, FieldDefinitionCollection fields, Analyzer analyzer = null)
        {
            if (analyzer == null) analyzer = new KeywordAnalyzer();
            var examineIndex = new ExamineIndex(name,
                CreateFileSystemLuceneDirectory(name),
                fields, analyzer);
            examineIndex.WaitForIndexQueueOnShutdown = false;
            _examineManager.TryGetIndex(name, out var index);
            if (index == null)
            {
                _examineManager.AddIndex(examineIndex);
                var dir = examineIndex.GetLuceneDirectory();

                if (!string.IsNullOrEmpty(dir.GetLockID()))
                {
                  //  _loggingService.Info("Forcing index {IndexerName} to be unlocked since it was left in a locked state", examineIndex.Name);

                    dir.ClearLock("write.lock");
                }

                if (IndexWriter.IsLocked(dir))
                {
                    IndexWriter.Unlock(dir);
                }
            }
        }

        public IIndex GetIndex(string name)
        {
            _examineManager.TryGetIndex(name, out var indexDefinition);

            return indexDefinition;
        }


        public void Save(string name, ValueSet document)
        {
            var index = GetIndex(name);
            if (!index.IndexExists())
            {
                index.CreateIndex();
            }

            index.IndexItem(document);
        }

        public ISearchResults Get(string name, string key)
        {
            var index = GetIndex(name);
            if (!index.IndexExists())
            {
                return EmptySearchResults.Instance;
            }

            var searcher = index.GetSearcher();
            var query = searcher.CreateQuery(_examineConfig.IndexType).Field(ExamineFieldNames.ItemIdFieldName, key);
            var result = query.Execute();
            return result;
        }

        public ISearchResults GetMultiple(string name, params string[] keys)
        {
            var index = GetIndex(name);
            if (!index.IndexExists())
            {
                return EmptySearchResults.Instance;
            }

            var searcher = index.GetSearcher();
            var query = searcher.CreateQuery(_examineConfig.IndexType)
                .GroupedOr(new[] {ExamineFieldNames.ItemIdFieldName}, keys);
            var result = query.Execute();
            return result;
        }

        public ISearchResults GetByWildcardKey(string name, string key)
        {
            var index = GetIndex(name);
            if (!index.IndexExists())
            {
                return EmptySearchResults.Instance;
            }

            var searcher = index.GetSearcher();
            var query = searcher.CreateQuery(_examineConfig.IndexType)
                .Field(ExamineFieldNames.ItemIdFieldName, key.MultipleCharacterWildcard());
            var result = query.Execute();
            return result;
        }

        public void Delete(string name, string key)
        {
            var index = GetIndex(name);
            index.DeleteFromIndex(key);
        }

        public ISearcher GetSearcher(string name)
        {
            var index = GetIndex(name);
            if (!index.IndexExists())
            {
                return null;
            }

            return index.GetSearcher();
        }

        public virtual Directory CreateFileSystemLuceneDirectory(string folderName)
        {
            var combinedPath = Path.Combine("App_Data/TEMP", "ExamineIndexes", folderName);
            var baseDirectory = AppContext.BaseDirectory;
            var currentPath = Path.Combine(AppContext.BaseDirectory,combinedPath);

            var dirInfo = new DirectoryInfo(currentPath);
            if (!dirInfo.Exists)
                System.IO.Directory.CreateDirectory(dirInfo.FullName);

            string configuredDirectoryFactory =
                _configuration["Novicell.Examine.LuceneDirectoryFactory"];

            if (!string.IsNullOrWhiteSpace(configuredDirectoryFactory))
            {
                /*  TODO: Check with Mikkel if is better way for TypeFinder */
                var factoryType = ExamineTypeFinder.GetTypeByName(configuredDirectoryFactory);
                if (factoryType == null)
                    throw new NullReferenceException("No directory type found for value: " +
                                                     configuredDirectoryFactory);
                var directoryFactory = (IDirectoryFactory) this.Resolve<IDirectoryFactory>(factoryType);
                return directoryFactory.CreateDirectory(dirInfo);
            }

            //no dir factory, just create a normal fs directory
            var luceneDir = new SimpleFSDirectory(dirInfo);

            //we want to tell examine to use a different fs lock instead of the default NativeFSFileLock which could cause problems if the appdomain
            //terminates and in some rare cases would only allow unlocking of the file if IIS is forcefully terminated. Instead we'll rely on the simplefslock
            //which simply checks the existence of the lock file
            // The full syntax of this is: new NoPrefixSimpleFsLockFactory(dirInfo)
            // however, we are setting the DefaultLockFactory in startup so we'll use that instead since it can be managed globally.
            luceneDir.SetLockFactory(DirectoryFactory.DefaultLockFactory(dirInfo));
            return luceneDir;
        }
    }
}