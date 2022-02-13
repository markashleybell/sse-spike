using System;

namespace sse_spike.Infrastructure
{
    public interface IMessageHub
    {
        event EventHandler<MessageEventArgs> OnMessageBroadcast;

        void BroadcastMessage(Message notification);
    }
}
