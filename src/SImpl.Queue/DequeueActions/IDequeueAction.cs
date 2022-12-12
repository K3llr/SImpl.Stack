namespace SImpl.Queue
{
    public interface IDequeueAction<T>
    {
        void DequeueAction(T action);
    }
}