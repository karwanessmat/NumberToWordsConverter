using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberToKurdishWords.Extensions;

namespace NumberToKurdishWords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet("{number:long}")]
        public IActionResult Index(long number)
        {
            var textNumber = number.NumberToKurdishText();
            return Ok(textNumber);
        }
    }
}
