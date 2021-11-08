namespace SImpl.Common
{
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }

    public interface IEntity
    {
        
    }
}