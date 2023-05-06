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
        /// <param name="number">you can use any type of number</param>
        /// <returns></returns>
        [HttpGet("{number:int}")]
        public IActionResult Index(int number)
        {

            var textNumber = number.ToKurdishText();
            return Ok(textNumber);
        }
    }
}
