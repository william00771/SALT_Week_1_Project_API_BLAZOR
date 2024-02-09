using System;
using System.Text.Json.Serialization;

namespace Salt.Stars.Web.Models
{
  public class GreetingResponse
  {
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("greetedAt")]
    public DateTime? GreetedAtUtc { get; set; }
    public DateTime? GreetedAt => GreetedAtUtc.HasValue ? GreetedAtUtc.Value.ToLocalTime() : null;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("greeting")]
    public string? Greeting { get; set; }
  }
}
