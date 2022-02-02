using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Rebus.Handlers;

namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class BufferedMessageHandler<TMessage> : IHandleMessages<TMessage>
    {
        private readonly IBufferHandler<TMessage> _bufferHandler;
        private readonly HandlerBufferConfig<TMessage> _handlerBufferConfig;
        private Subject<TMessage> _messages;

        public BufferedMessageHandler(
            IBufferHandler<TMessage> bufferHandler,
            HandlerBufferConfig<TMessage> handlerBufferConfig)
        {
            _bufferHandler = bufferHandler;
            _handlerBufferConfig = handlerBufferConfig;
            
            _messages = NewSubject(true);
        }

        private Subject<TMessage> NewSubject(bool firstStart = false)
        {
            var messages = new Subject<TMessage>();

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

        private void OnBufferFull(IList<TMessage> messages)
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
        
        public Task Handle(TMessage message)
        {
            _messages.OnNext(_bufferHandler.OnEnterBuffer(message));
            return Task.CompletedTask;
        }
    }
}