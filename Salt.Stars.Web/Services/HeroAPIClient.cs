using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Salt.Stars.Web.Models;

namespace Salt.Stars.Web.Services
{
  public class HeroApiClient : IHeroApiClient
  {
    private IConfiguration _configuration;

    public HeroApiClient(IConfiguration configuration)
    {
       _configuration = configuration;
    }

    public async Task<HeroResponse?> GetHero(int id)
    {
      var client = getClient();
      var url = $"{createHeroesUrl()}{id}";

      var heroTask = client.GetStreamAsync(url);
      // var jsonTask = client.GetStringAsync(url);
      // Console.WriteLine(await jsonTask);

      return await JsonSerializer.DeserializeAsync<HeroResponse>(await heroTask);
    }

    public async Task<HeroesResponse?> GetHeroes()
    {
      var client = getClient();
      var url = createHeroesUrl();

      var heroesTask = client.GetStreamAsync(url);

      return await JsonSerializer.DeserializeAsync<HeroesResponse>(await heroesTask);
    }

    public Task<StarUpdateResponse> UpdateStars(int id, int numberOfStars)
    {
      throw new NotImplementedException();
    }

    private string createHeroesUrl()
    {
      var baseUrlForApi = _configuration["ApiBaseUrl"];
      return $"{baseUrlForApi}/heroes/";
    }

    private HttpClient getClient()
    {
      var client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      return client;
    }
  }
}
