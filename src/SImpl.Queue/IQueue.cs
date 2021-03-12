namespace SImpl.Queue
{
    public interface IQueue<T> : IQueue
    {
        void Enqueue(T job);
    }

    public interface IQueue
    {
        
    }
}