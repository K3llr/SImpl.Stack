namespace SImpl.CQRS.Queries
{
    public interface IQuery
    {
    }
    
    public interface IQuery<TResult> : IQuery
    {
    }
}