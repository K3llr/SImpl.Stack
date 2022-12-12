using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SImpl.Queue
{
    public class IdOrientedJobQueue : IQueue<(Guid Id, Func<IServiceScopeFactory,Task> Job)>
    {
        private readonly IDequeueAction<(Guid Id, Func<IServiceScopeFactory,Task> Job)> _dequeueAction;
        private readonly ILogger<IdOrientedJobQueue> _logger;
        private readonly BlockingCollection<(Guid Id, Func<IServiceScopeFactory,Task> Job)> _jobs = new BlockingCollection<(Guid Id, Func<IServiceScopeFactory,Task> Job)>();

        public IdOrientedJobQueue(IDequeueAction<(Guid Id, Func<IServiceScopeFactory,Task> Job)> dequeueAction, ILogger<IdOrientedJobQueue> logger)
        {
            this._dequeueAction = dequeueAction;
            _logger = logger;
            new Thread(new ThreadStart(this.OnStart))
            {
                IsBackground = false,
                
            }.Start();
            new Thread(new ThreadStart(this.OnStart))
            {
                IsBackground = false,
                
            }.Start();
        }

        public static IdOrientedJobQueue CreateInstance(IDequeueAction<(Guid Id, Func<IServiceScopeFactory,Task> Job)> dequeueAction, ILogger<IdOrientedJobQueue> logger)
        {
            return new IdOrientedJobQueue(dequeueAction, logger);
        }


        private void ProcessQueuedItem((Guid Id, Func<IServiceScopeFactory,Task> Job) item) => this._dequeueAction.DequeueAction(item);

        private void OnStart()
        {
            var pause = TimeSpan.FromSeconds(1);
            var lastrun = DateTime.Now;
            while (true) // some criteria to abort or even true works here
            {
                if (_jobs.Count == 0)
                {
                    if (lastrun + TimeSpan.FromMinutes(1) < DateTime.Now)
                    {
                        _logger.LogInformation("background Queue is empty");
                        lastrun = DateTime.Now;

                    }
                    // no pending actions available. pause
                    continue;
                }

                foreach ((Guid Id, Func<IServiceScopeFactory,Task> Job) consuming in this._jobs.GetConsumingEnumerable(
                             CancellationToken.None))
                {
                    _logger.LogInformation($"background Queue has {_jobs.Count}");
                    try
                    {
                        this.ProcessQueuedItem(consuming);

                    }
                    catch (Exception e)
                    {
                        _logger.LogError("background task failed",e);
                    }
                }
            }
        
        }

        public void Enqueue((Guid Id, Func<IServiceScopeFactory,Task> Job) job)
        {
            _logger.LogInformation($"adding job with id {job.Id}");
            this._jobs.Add(job);
        }
    }
}