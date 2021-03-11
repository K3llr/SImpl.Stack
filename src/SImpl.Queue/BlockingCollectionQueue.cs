using System.Collections.Concurrent;
using System.Threading;

namespace SImpl.Queue
{
    public class BlockingCollectionQueue<T> : IQueue<T>
    {
        private readonly IDequeueAction<T> _dequeueAction;

        private readonly BlockingCollection<T> _jobs = new();

        public BlockingCollectionQueue(IDequeueAction<T> dequeueAction)
        {
            _dequeueAction = dequeueAction;
            
            var thread = new Thread(OnStart)
            {
                IsBackground = true
            };
            
            thread.Start();
        }

        public void Enqueue(T job)
        {
            _jobs.Add(job);
        }

        private void ProcessQueuedItem(T item)
        {
            _dequeueAction.DequeueAction(item);
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