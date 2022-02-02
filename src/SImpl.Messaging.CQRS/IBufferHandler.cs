using System;
using System.Collections.Generic;

namespace SImpl.Messaging.CQRS
{
    public interface IBufferHandler<TMessage>
    {
        void OnBufferFull(IList<TMessage> commands);

        void OnError(Exception e);

        void OnCompleted();

        void OnStart();
        
        void OnRestart();
        
        TMessage OnEnterBuffer(TMessage message);
    }
}