using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Verbosity
{
    public class ModuleLoggingDecorator<T> : DispatchProxy
        where T : ISImplModule
    {
        private T _decorated;
        private readonly ILogger _logger = RuntimeServices.Current.BootContainer.Resolve<ILogger>();

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                if (typeof(T) != typeof(ISImplModule))
                {
                    LogBefore(targetMethod, args);
                }

                var result = targetMethod.Invoke(_decorated, args);

                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                LogException(ex.InnerException ?? ex, targetMethod);
                throw ex.InnerException ?? ex;
            }
        }

        public static T Create(T decorated)
        {
            object proxy = Create<T, ModuleLoggingDecorator<T>>();
            ((ModuleLoggingDecorator<T>)proxy).SetParameters(decorated);

            return (T)proxy;
        }

        private void SetParameters(T decorated)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            _decorated = decorated;
        }

        private void LogException(Exception exception, MethodInfo methodInfo = null)
        {
            _logger.LogDebug($"{typeof(T).Name} > {_decorated.Name} > {methodInfo?.Name}  threw exception:\n{exception}");
        }

        private void LogBefore(MethodInfo methodInfo, object[] args)
        {
            _logger.LogDebug($"{typeof(T).Name} > {_decorated.Name} > {methodInfo.Name} > Started");
        }
    }
}