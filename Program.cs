using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace sse_spike
{
    public static class Program
    {
        public static void Main(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(wb => wb.UseStartup<Startup>())
                .Build()
                .Run();
    }
}
