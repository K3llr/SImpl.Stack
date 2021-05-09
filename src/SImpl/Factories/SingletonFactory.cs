namespace SImpl.Factories
{
    public class SingletonFactory<T> : IFactory<T>
    {
        private readonly T _service;

        public SingletonFactory(T service)
        {
            _service = service;
        }

        public T New()
        {
            return _service;
        }
    }
}
