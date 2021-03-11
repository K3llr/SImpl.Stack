using System;
using System.Collections.Generic;
using System.Threading;

namespace SImpl.JobQueues
{
    public class PoolThreadQueue<T>
    {
        private readonly IDeQueueAction<T> _deQueueAction;
        private Queue<T> _jobs = new();
        private bool _delegateQueuedOrRunning = false;

        public PoolThreadQueue(IDeQueueAction<T> deQueueAction)
        {
            _deQueueAction = deQueueAction;
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
                    _deQueueAction.DeQueueAction(item);
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