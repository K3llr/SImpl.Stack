namespace SImpl.Domain
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}