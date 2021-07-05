using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using SImpl.CQRS.Commands;
using System.Threading.Tasks;
    
namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class CommandBufferDispatcher<TCommand> : ICommandBufferDispatcher<TCommand> 
        where TCommand : class, ICommand
    {
        private readonly IBufferHandler<TCommand> _bufferHandler;
        private readonly Subject<TCommand> _messages;

        public CommandBufferDispatcher(
            IBufferHandler<TCommand> bufferHandler,
            HandlerBufferConfig<TCommand> handlerBufferConfig)
        {
            _bufferHandler = bufferHandler;
            _messages = new Subject<TCommand>();
            
            // Buffer until x items or y millisecond has elapsed, whatever comes first.
            _messages.Buffer(handlerBufferConfig.MaxTimeSpan, handlerBufferConfig.MaxMessageCount) 
                .Subscribe(_bufferHandler.OnBufferFull);
        }
        
        public Task ExecuteAsync(TCommand command)
        {
            _messages.OnNext(_bufferHandler.OnEnterBuffer(command));
            return Task.CompletedTask;
        }
    }
}