using System.Collections.Generic;

namespace SImpl.Messaging.CQRS.Rebus
{
    public interface IBufferHandler<TMessage>
    {
        void OnBufferFull(IList<TMessage> commands);

        TMessage OnEnterBuffer(TMessage message);
    }
}