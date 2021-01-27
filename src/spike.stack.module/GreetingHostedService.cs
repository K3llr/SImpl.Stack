using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using spike.stack.application;

namespace spike.stack.module
{
    public class GreetingHostedService : IHostedService
    {
        private readonly IGreetingAppService _greetingAppService;

        public GreetingHostedService(IGreetingAppService greetingAppService)
        {
            _greetingAppService = greetingAppService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(_greetingAppService.SayHi("Keller"));
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(_greetingAppService.SayBye("Keller"));
            
            return Task.CompletedTask;
        }
    }
}