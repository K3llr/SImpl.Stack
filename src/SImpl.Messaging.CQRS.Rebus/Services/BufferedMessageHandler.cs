using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Rebus.Handlers;

namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class BufferedMessageHandler<TMessage> : IHandleMessages<TMessage>
    {
        private readonly Subject<TMessage> _messages;

        public BufferedMessageHandler(
            IBufferHandler<TMessage> bufferHandler,
            MessageBufferConfig<TMessage> messageBufferConfig)
        {
            _messages = new Subject<TMessage>();
            
            // Buffer until x items or y millisecond has elapsed, whatever comes first.
            _messages.Buffer(messageBufferConfig.MaxTimeSpan, messageBufferConfig.MaxMessageCount) 
                .Subscribe(bufferHandler.Handle);
        }

        public Task Handle(TMessage message)
        {
            _messages.OnNext(message);
            return Task.CompletedTask;
        }
    }
}