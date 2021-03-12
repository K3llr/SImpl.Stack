using System.Collections.Generic;
using System.Threading;

namespace SImpl.Queue
{
    public class PoolThreadQueue<T>
    {
        private readonly IDequeueAction<T> _dequeueAction;
        private readonly Queue<T> _jobs = new();
        private bool _delegateQueuedOrRunning = false;

        public PoolThreadQueue(IDequeueAction<T> dequeueAction)
        {
            _dequeueAction = dequeueAction;
        }

        public void Enqueue(T job)
        {
            lock (_jobs)
            {
                _jobs.Enqueue(job);
                if (!_delegateQueuedOrRunning)
                {
                    _delegateQueuedOrRunning = true;
                    ThreadPool.UnsafeQueueUserWorkItem(ProcessQueuedItems, null);
                }
            }
        }
 
        private void ProcessQueuedItems(object ignored)
        {
            while (true)
            {
                T item;
                lock (_jobs)
                {
                    if (_jobs.Count == 0)
                    {
                        _delegateQueuedOrRunning = false;
                        break;
                    }
 
                    item = (T)_jobs.Dequeue();
                }
 
                try
                {
                    //do job
                    _dequeueAction.DequeueAction(item);
                }
                catch
                {
                    ThreadPool.UnsafeQueueUserWorkItem(ProcessQueuedItems, null);
                    throw;
                }
            }
        }
    }
}