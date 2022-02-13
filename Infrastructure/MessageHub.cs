using Microsoft.Extensions.Logging;
using System;

namespace sse_spike.Infrastructure
{
    public class MessageHub : IMessageHub
    {
        private readonly ILogger<MessageHub> _logger;

        public MessageHub(ILogger<MessageHub> logger) =>
            _logger = logger;

        public event EventHandler<MessageEventArgs> OnMessageBroadcast;

        public void BroadcastMessage(Message message)
        {
            _logger.LogDebug($"Broadcasting message '{message.Content}' to all listeners");

            OnMessageBroadcast?.Invoke(this, new MessageEventArgs(message));
        }
    }
}
