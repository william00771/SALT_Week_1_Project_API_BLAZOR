using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Salt.Stars.Web.Models;

namespace Salt.Stars.Web.Services
{
  public class GreetingApiClient : IGreetingApiClient
  {
    private IConfiguration _configuration;

    public GreetingApiClient(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    private string CreateGreetingUrl(string name)
    {
      var baseUrlForApi = _configuration["ApiBaseUrl"];
      return $"{baseUrlForApi}/greeting?name={name}";
    }

    public async Task<GreetingResponse> GetGreeting(string name)
    {
      var client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var url = CreateGreetingUrl(name);
      var greetingTask = client.GetStreamAsync(url);

      var greetingResult = await JsonSerializer.DeserializeAsync<GreetingResponse>(await greetingTask);
      return greetingResult!;
    }

  }
}
