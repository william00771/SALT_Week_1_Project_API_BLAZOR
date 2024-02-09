using System;
using System.Text.Json.Serialization;

namespace Salt.Stars.Web.Models
{
  public class HeroResponse
  {
    [JsonPropertyName("hero")]
    public Hero? Hero { get; set; }

    [JsonPropertyName("requestedAt")]
    public DateTime? RequestedAt { get; set; }
  }
}
