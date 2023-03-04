using System.Net;
using System.Web.Http;
using Newtonsoft.Json;

namespace DotNetCoreWebApiShim.Test.Controllers
{
    [TestClass]
    public class WeatherForecastControllerTest
    {
        private static HttpClient client = null!;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var factory = new TestWebApplicationFactory();
            client = factory.CreateClient();
        }

        [TestMethod]
        public async Task GetWeatherForecast_Ok()
        {
            // Arrange・Act
            var response = await client.GetAsync("WeatherForecast/GetWeatherForecast");

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(json);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetWeatherForecastLegacy_Ok()
        {
            // Arrange・Act
            var response = await client.GetAsync("WeatherForecast/GetWeatherForecastLegacy");

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(json);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task Error_Ok_BadRequest()
        {
            // Arrange・Act
            var response = await client.GetAsync("WeatherForecast/Error");

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<HttpError>(json);

            Assert.AreEqual("This is Error Sample.", result.Message);
        }

        [TestMethod]
        public async Task ErrorLegacy_Ok_BadRequest()
        {
            // Arrange・Act
            var response = await client.GetAsync("WeatherForecast/ErrorLegacy");

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<HttpError>(json);

            Assert.AreEqual("This is ErrorLegacy Sample.", result.Message);
        }
    }
}
