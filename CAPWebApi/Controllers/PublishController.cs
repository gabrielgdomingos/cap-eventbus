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

        public PublishController(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        [Route("sample")]
        public async Task<IActionResult> Sample()
        {
            await _capPublisher.PublishAsync("helloWorld", "CodeOpinion");

            return Ok();
        }
    }
}
