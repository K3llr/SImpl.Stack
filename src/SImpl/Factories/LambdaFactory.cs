using System;

namespace SImpl.Factories
{
    public class LambdaFactory<T> : IFactory<T>
    {
        private readonly Func<T> _factoryLambda;

        public LambdaFactory(Func<T> factoryLambda)
        {
            _factoryLambda = factoryLambda;
        }

        public T New()
        {
            return _factoryLambda.Invoke();
        }
    }
}
