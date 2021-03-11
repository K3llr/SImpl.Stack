using System;

namespace SImpl.JobQueues
{
    public interface IDeQueueAction<T>
    {
        void DeQueueAction(T action);
    }
}