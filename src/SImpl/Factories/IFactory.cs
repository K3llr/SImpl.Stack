namespace SImpl.Factories
{
    public interface IFactory<out T>
    {
        T New();
    }
}
