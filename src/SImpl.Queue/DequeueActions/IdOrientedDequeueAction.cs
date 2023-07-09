using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SImpl.Queue
{
    public class IdOrientedDequeueAction :  IDequeueAction<(Guid Id, Func<IServiceScopeFactory,Task> Job)>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IdOrientedDequeueAction> _logger;

        public IdOrientedDequeueAction(IServiceProvider serviceProvider, ILogger<IdOrientedDequeueAction> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void DequeueAction((Guid Id, Func<IServiceScopeFactory,Task> Job) action)
        {
            ExecutionContext.SuppressFlow();
            Task.Run(async () =>
            {
                try
                {
                    var startTime = DateTime.Now;
                    _logger.LogInformation($"{startTime.ToLongTimeString()} starting task {action.Id}");
                    await action.Job.Invoke(
                        _serviceProvider.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory);
                    var endTime = DateTime.Now;

                    TimeSpan diff = endTime - startTime;
                    _logger.LogInformation(
                        $"{endTime.ToLongTimeString()} finishing task {action.Id} - took {diff.TotalSeconds} seconds");

                }
                catch (Exception e)
                {
                    _logger.LogInformation(
                        $" task {action.Id}  failed");
                }
            });
                
                
                
            ExecutionContext.RestoreFlow();

        }
    }
}