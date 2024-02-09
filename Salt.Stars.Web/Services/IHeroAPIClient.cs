using System.Threading.Tasks;
using Salt.Stars.Web.Models;

namespace Salt.Stars.Web.Services
{
  public interface IHeroApiClient
  {
    Task<HeroesResponse?> GetHeroes();
    Task<HeroResponse?> GetHero(int id);
    Task<StarUpdateResponse> UpdateStars(int id, int numberOfStars);
  }
}
