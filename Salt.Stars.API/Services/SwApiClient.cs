using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Salt.Stars.API.Models;

namespace Salt.Stars.API.Services
{
  public class SwApiClient : ISwApiClient
  {

    public async Task<HeroListResponse> getHerosFromSwapi()
    {
      var client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var url = $"https://swapi.py4e.com/api/people/";
      var heroesTask = client.GetStreamAsync(url);

      return await JsonSerializer.DeserializeAsync<HeroListResponse>(await heroesTask);
    }

    public async Task<Hero> getHeroFromSwapi(int id)
    {
      var client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var url = $"https://swapi.py4e.com/api/people/{id}";
      var heroTask = client.GetStreamAsync(url);

      return await JsonSerializer.DeserializeAsync<Hero>(await heroTask);
    }

  }
}
