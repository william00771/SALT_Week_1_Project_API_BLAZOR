using System;

namespace Salt.Stars.API.Models
{
  public class HeroStarUpdateResponse
  {
    public int HeroId { get; set; }
    public int StarRating { get; set; }
    public DateTime RequestedAt { get; set; }

  }
}