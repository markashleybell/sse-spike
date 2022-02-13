using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sse_spike.Infrastructure;
using sse_spike.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace sse_spike.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : Controller
    {
        private const int _reconnectionInterval = 3000;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly ILogger<ApiController> _logger;
        private readonly IMessageHub _messageHub;

        public ApiController(ILogger<ApiController> logger, IMessageHub messageHub)
        {
            _logger = logger;
            _messageHub = messageHub;
        }

        [HttpGet]
        [Route("subscribe")]
        public async Task Subscribe(CancellationToken cancellationToken)
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            async void OnMessageBroadcast(object _, MessageEventArgs eventArgs)
            {
                try
                {
                    var json = JsonSerializer.Serialize(eventArgs.Message, _jsonOptions);

                    await Response.WriteAsync($"retry: {_reconnectionInterval}\r");
                    await Response.WriteAsync($"data: {json}\r\r");

                    await Response.Body.FlushAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to send notification: " + ex.Message);
                }
            }

            _messageHub.OnMessageBroadcast += OnMessageBroadcast;

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
                _logger.LogDebug("Task cancelled in Subscribe endpoint: user disconnected");
            }
            finally
            {
                _messageHub.OnMessageBroadcast -= OnMessageBroadcast;
            }
        }

        [HttpPost]
        [Route("broadcast")]
        public IActionResult Broadcast(Broadcast broadcast)
        {
            var message = new Message(broadcast.MessageText);

            _messageHub.BroadcastMessage(message);

            var response = new {
                broadcast.MessageText
            };

            return Ok(response);
        }
    }
}
