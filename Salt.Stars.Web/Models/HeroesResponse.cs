using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Salt.Stars.Web.Models
{
  public class HeroesResponse
  {
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("results")]
    public IList<Hero>? Heroes { get; set; }
  }
}