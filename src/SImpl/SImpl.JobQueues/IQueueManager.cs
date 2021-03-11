namespace SImpl.JobQueues
{
    public interface IQueueManager
    {
        void Enqueue<T>(T job);
    }
}