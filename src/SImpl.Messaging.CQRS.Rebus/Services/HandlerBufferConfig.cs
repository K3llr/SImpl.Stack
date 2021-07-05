using System;

namespace SImpl.Messaging.CQRS.Rebus.Services
{
    public class HandlerBufferConfig<TMessage>
    {
        public HandlerBufferConfig(TimeSpan maxTimeSpan, int maxMessageCount)
        {
            MaxTimeSpan = maxTimeSpan;
            MaxMessageCount = maxMessageCount;
        }
        
        public TimeSpan MaxTimeSpan { get; }
        public int MaxMessageCount { get; }
    }
}