using System;
using System.Collections.Generic;
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
        private readonly DispatchBufferConfig<TCommand> _handlerBufferConfig;
        private Subject<TCommand> _messages;

        public CommandBufferDispatcher(
            IBufferHandler<TCommand> bufferHandler,
            DispatchBufferConfig<TCommand> handlerBufferConfig)
        {
            _bufferHandler = bufferHandler;
            _handlerBufferConfig = handlerBufferConfig;
            
            _messages = NewSubject(true);
        }
        
        private Subject<TCommand> NewSubject(bool firstStart = false)
        {
            var messages = new Subject<TCommand>();

            // Buffer until x items or y millisecond has elapsed, whatever comes first.
            messages.Buffer(_handlerBufferConfig.MaxTimeSpan, _handlerBufferConfig.MaxMessageCount)
                .Subscribe(OnBufferFull, OnError, OnCompleted);

            if (firstStart)
            {
                _bufferHandler.OnStart();
            }
            else
            {
                _bufferHandler.OnRestart();    
            }
            
            return messages;
        }
        
        private void OnBufferFull(IList<TCommand> messages)
        {
            _bufferHandler.OnBufferFull(messages);
        }

        private void OnError(Exception e)
        {
            _bufferHandler.OnError(e);
            // Restart
            _messages = NewSubject();
        }

        private void OnCompleted()
        {
            _bufferHandler.OnCompleted();
            // Restart
            _messages = NewSubject();   
        }
        
        public Task ExecuteAsync(TCommand command)
        {
            _messages.OnNext(_bufferHandler.OnEnterBuffer(command));
            return Task.CompletedTask;
        }
    }
}