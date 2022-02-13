using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace sse_spike.Controllers
{
    [Route("/api/sse")]
    public class ApiController : Controller
    {
        public async Task Get()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");

            for (var i = 0; i < 5; i++)
            {
                await Response.WriteAsync($"data: Controller {i} at {DateTime.Now}\r\r");

                await Response.Body.FlushAsync();

                await Task.Delay(5 * 1000);
            }
        }
    }
}
