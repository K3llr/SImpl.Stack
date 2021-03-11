using System;

namespace SImpl.JobQueues
{
    public interface IQueue<T> : IQueue
    {
        void Enqueue(T job);
    }

    public interface IQueue
    {
        
    }
}