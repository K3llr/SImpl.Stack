using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SImpl.JobQueues
{
    public class BlockingCollectionQueue<T> : IQueue<T>
    {
        private Action<T> _onDeQueue;
        private BlockingCollection<T> _jobs = new();
        private bool _delegateQueuedOrRunning = false;

        public BlockingCollectionQueue()
        {
            var thread = new Thread(new ThreadStart(OnStart));
            thread.IsBackground = true;
            thread.Start();
        }

        public void DefineDeQueueAction(Action<T> onDeQueue)
        {
            _onDeQueue = onDeQueue;
        }

        public void Enqueue(T job)
        {
            _jobs.Add(job);
        }

        private void ProcessQueuedItem(T item)
        {
            _onDeQueue.Invoke(item);
        }
        private void OnStart()
        {
            foreach (var job in _jobs.GetConsumingEnumerable(CancellationToken.None))
            {
                ProcessQueuedItem(job);
            }
        }
    }
}