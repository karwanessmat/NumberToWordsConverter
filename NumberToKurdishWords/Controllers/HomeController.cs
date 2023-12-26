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
        [HttpGet( "kurdish/{number:long}")]
        public IActionResult WordsToKurdish(long number)
        {

            var textNumber = number.NumberWordsInKurdish();
            return Ok(textNumber);
        }


        [HttpGet( "arabic/{number:long}")]
        public IActionResult WordsToArabic(long number)
        {
            var textNumber = number.NumberWordsInArabic();
            return Ok(textNumber);
        }
    }
}
