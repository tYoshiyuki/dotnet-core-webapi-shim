using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApiShim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : CompatibleControllerBase
    {
        private static readonly string[] s_summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ASP.NET Core �W������
        /// ����n�G���h�|�C���g
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return GetWeatherForecast();
        }

        /// <summary>
        /// ASP.NET Web API ���� (<see cref="HttpResponseMessage"/>��Ԃ��p�^�[��)
        /// ����n�G���h�|�C���g
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeatherForecastLegacy")]
        public HttpResponseMessage GetLegacy()
        {
            return RequestMessage.CreateResponse(HttpStatusCode.OK, GetWeatherForecast());
        }

        /// <summary>
        /// ASP.NET Core �W������
        /// �G���[�n�G���h�|�C���g
        /// </summary>
        /// <returns></returns>
        [HttpGet("Error")]
        public IActionResult Error()
        {
            return BadRequest(new HttpError("This is Error Sample."));
        }

        /// <summary>
        /// ASP.NET Web API ���� (<see cref="HttpResponseMessage"/>��Ԃ��p�^�[��)
        /// �G���[�n�G���h�|�C���g
        /// </summary>
        /// <returns></returns>
        [HttpGet("ErrorLegacy")]
        public HttpResponseMessage ErrorLegacy()
        {
            return RequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError("This is ErrorLegacy Sample."));
        }

        private IEnumerable<WeatherForecast> GetWeatherForecast() => Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast 
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = s_summaries[Random.Shared.Next(s_summaries.Length)]
            })
            .ToArray();
    }
}
