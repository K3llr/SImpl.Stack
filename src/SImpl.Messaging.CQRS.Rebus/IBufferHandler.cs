using System.Collections.Generic;

namespace SImpl.Messaging.CQRS.Rebus
{
    public interface IBufferHandler<TMessage>
    {
        void Handle(IList<TMessage> commands);
    }
}