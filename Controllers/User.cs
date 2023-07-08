using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class User : ControllerBase
    {

        [HttpGet(Name = "GetWeatherForecast")]
        public JsonContent Post()
        {
            return null;
        }
    }
}