using System;
using System.Text.Json.Serialization;

namespace Salt.Stars.Web.Models
{
  public class StarUpdateResponse
  {
    [JsonPropertyName("requestedAt")]
    public DateTime RequestedAt { get; set; }

    [JsonPropertyName("heroId")]
    public int HeroId { get; set; }

    [JsonPropertyName("currentNumberOfStars")]
    public int CurrentNumberOfStars { get; set; }
  }
}