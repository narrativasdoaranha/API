using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

       

        [HttpPost]
        public ActionResult Post([FromBody] StringModel input)
        {
            if (input?.Value == null)
            {
                return BadRequest("O corpo da requisição não pode ser nulo.");
            }

            var response = new
            {
                IsPalindrome = IsPalindrome(input.Value),
                CharacterCount = CountCharacterOccurrences(input.Value)
            };

            return Ok(response);
        }

        private bool IsPalindrome(string value)
        {
            var processedValue = value.ToLowerInvariant().Replace(" ", "");
            var reverse = new string(processedValue.Reverse().ToArray());
            return processedValue == reverse;
        }

        private Dictionary<char, int> CountCharacterOccurrences(string value)
        {
            return value.GroupBy(c => c)
                        .ToDictionary(grp => grp.Key, grp => grp.Count());
        }
    }
}
