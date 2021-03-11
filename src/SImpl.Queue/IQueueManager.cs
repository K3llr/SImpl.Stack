namespace SImpl.Queue
{
    public interface IQueueManager
    {
        void Enqueue<T>(T job);
    }
}