using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Salt.Stars.API.Models;
using Salt.Stars.API.Services;

namespace Salt.Stars.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public partial class HeroesController : ControllerBase
  {
    private readonly ISwApiClient _swApiClient;
    private readonly IStarFileClient _starFileClient;

    public HeroesController(ISwApiClient swApiClient, IStarFileClient starFileClient)
    {
      _swApiClient = swApiClient;
      _starFileClient = starFileClient;
    }

    [HttpGet]
    public async Task<ActionResult<HeroListResponse>> GetHeroListAsync()
    {
      try
      {
        var heroListResponse = await _swApiClient.getHerosFromSwapi();
        heroListResponse.PageSize = heroListResponse.Heroes.Count;
        heroListResponse.CurrentPage = 1;
        heroListResponse.RequestedAt = DateTime.Now;

        return heroListResponse;

      }
      catch (System.Exception ex)
      {
        return NotFound(ex.ToString());
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HeroResponse>> GetHeroAsync(int id)
    {
      try
      {
        var hero = await _swApiClient.getHeroFromSwapi(id);

        // TODO: Get the star ratings from the service

        return new HeroResponse
        {
          Hero = hero,
          RequestedAt = DateTime.Now
        };
      }
      catch (System.Exception ex)
      {
        return NotFound(ex.ToString());
      }
    }

    [HttpPatch("{heroId}")]
    public Task<ActionResult<HeroStarUpdateResponse>> AddStarToHeroAsync(int heroId, HeroStarUpdateRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
