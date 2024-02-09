using System.Threading.Tasks;
using Salt.Stars.Web.Models;

namespace Salt.Stars.Web.Services
{
  public interface IGreetingApiClient
  {
    Task<GreetingResponse> GetGreeting(string name);
  }
}
