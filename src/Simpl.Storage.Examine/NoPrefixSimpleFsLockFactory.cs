using System.IO;
using Lucene.Net.Store;

namespace Novicell.App.Examine
{
    public class NoPrefixSimpleFsLockFactory : SimpleFSLockFactory
    {
        public NoPrefixSimpleFsLockFactory(DirectoryInfo lockDir) : base(lockDir)
        {
        }

        public override string LockPrefix
        {
            get => base.LockPrefix;
            set => base.LockPrefix = null; //always set to null
        }
        
    }
}