using System;

namespace SImpl.JobQueues
{
    public interface IQueue<T> : IQueue
    {
        void DefineDeQueueAction(Action<T> onDeQueue);
        void Enqueue(T job);
    }

    public interface IQueue
    {
        
    }
}