using System;

namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class MessageBufferConfig<TMessage>
    {
        public MessageBufferConfig(TimeSpan maxTimeSpan, int maxMessageCount)
        {
            MaxTimeSpan = maxTimeSpan;
            MaxMessageCount = maxMessageCount;
        }
        
        public TimeSpan MaxTimeSpan { get; }
        public int MaxMessageCount { get; }
    }
}