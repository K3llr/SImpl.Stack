using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Rebus.Handlers;

namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class BufferedMessageHandler<TMessage> : IHandleMessages<TMessage>
    {
        private readonly IBufferHandler<TMessage> _bufferHandler;
        private readonly Subject<TMessage> _messages;

        public BufferedMessageHandler(
            IBufferHandler<TMessage> bufferHandler,
            HandlerBufferConfig<TMessage> handlerBufferConfig)
        {
            _bufferHandler = bufferHandler;
            _messages = new Subject<TMessage>();
            
            // Buffer until x items or y millisecond has elapsed, whatever comes first.
            _messages.Buffer(handlerBufferConfig.MaxTimeSpan, handlerBufferConfig.MaxMessageCount) 
                .Subscribe(_bufferHandler.OnBufferFull);
        }

        public Task Handle(TMessage message)
        {
            _messages.OnNext(_bufferHandler.OnEnterBuffer(message));
            return Task.CompletedTask;
        }
    }
}