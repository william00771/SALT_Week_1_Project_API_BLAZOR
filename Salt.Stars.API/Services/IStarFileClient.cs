using System.Threading.Tasks;

namespace Salt.Stars.API.Services
{
  public interface IStarFileClient
  {
    Task<int> AddStarsForHero(int heroId, int stars);
    Task<int> GetStarsForHero(int heroId);
  }
}
