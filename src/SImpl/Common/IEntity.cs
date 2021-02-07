namespace SImpl.Common
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}