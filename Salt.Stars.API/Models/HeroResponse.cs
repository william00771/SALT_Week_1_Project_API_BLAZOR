using System;

namespace Salt.Stars.API.Models
{
  public class HeroResponse
  {
    public Hero Hero { get; set; }
    public DateTime RequestedAt { get; set; }
  }
}