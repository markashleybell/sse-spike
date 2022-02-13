using System;

namespace sse_spike.Infrastructure
{
    public class Message
    {
        public Message(string content) =>
            Content = content ?? throw new ArgumentNullException(nameof(content));

        public string Content { get; }
    }
}
