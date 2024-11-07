using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        // Inject IBackgroundJobClient
        public JobController(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpGet("StartJob")]
        public IActionResult StartJob()
        {
            // Enqueue a background job using the injected service
            _backgroundJobClient.Enqueue(() => Console.WriteLine("Hello from a background job!"));

            return Ok("Background job has been started!");
        }

        [HttpGet("DelayedJob")]
        public IActionResult DelayedJob()
        {
            // Schedule a delayed job using the injected service
            _backgroundJobClient.Schedule(() => Console.WriteLine("This job runs after 10 minutes!"),
                TimeSpan.FromMinutes(10));

            return Ok("Delayed job has been scheduled!");
        }
    }
}
