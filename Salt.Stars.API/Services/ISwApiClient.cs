using System.Threading.Tasks;
using Salt.Stars.API.Models;

namespace Salt.Stars.API.Services
{
  public interface ISwApiClient
  {
    Task<HeroListResponse> getHerosFromSwapi();
    Task<Hero> getHeroFromSwapi(int id);
  }
}
