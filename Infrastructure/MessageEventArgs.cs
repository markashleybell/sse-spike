using System;

namespace sse_spike.Infrastructure
{
    public class MessageEventArgs
    {
        public MessageEventArgs(Message message) =>
            Message = message ?? throw new ArgumentNullException(nameof(message));

        public Message Message { get; }
    }
}
