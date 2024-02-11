using System;
using System.Linq;
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
        var heroListRatingResponse = await _starFileClient.GetAllHeroRatings();
        foreach(var hero in heroListResponse.Heroes)
        {
            foreach (var ratingObject in heroListRatingResponse)
            {
                if(hero.Id == ratingObject.HeroId)
                {
                    hero.StarRating = ratingObject.StarRating;
                }
            }
        }
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
        var ratingListResponse = await _starFileClient.GetStarsForHero(id);
        hero.StarRating = ratingListResponse;

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

    [HttpPut("{heroId}")]
    public async Task<ActionResult<HeroStarUpdateResponse>> AddStarToHeroAsync(int heroId, HeroStarUpdateRequest request)
    {
        if (request is null)
        {
            return NotFound(new { Message = "No Body found" });
        }
        try
        {
            await _starFileClient.AddStarsForHero(heroId, request.NewStarRating);

            var response = new HeroStarUpdateResponse
            {
                HeroId = heroId,
                StarRating = request.NewStarRating,
                RequestedAt = DateTime.Now
            };

            return Ok(response);
            }
        catch (System.Exception ex)
        {
            return NotFound(new { ex.Message });
        }
    }
  }
}
