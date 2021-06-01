using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CAPWebApi.Controllers
{
    [ApiController]
    [Route("publish")]
    public class PublishController : ControllerBase
    {
        private readonly ICapPublisher _capPublisher;

        private static int _counter = 1;

        public PublishController(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        public async Task<IActionResult> Publish()
        {
            await _capPublisher.PublishAsync("hello", _counter.ToString());

            _counter++;

            return Ok();
        }
    }
}
