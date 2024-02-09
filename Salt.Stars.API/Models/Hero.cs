using System;
using System.Text.Json.Serialization;

namespace Salt.Stars.API.Models
{
  public class Hero
  {
    [JsonPropertyName("url")]
    public string Url { get; set; }

    public int Id
    {
      get
      {
        var urlString = Url.EndsWith('/') ? Url.Remove(Url.Length - 1, 1) : Url;
        var idString = urlString.Remove(0, urlString.LastIndexOf('/') + 1);

        int id;
        bool success = int.TryParse(idString, out id);
        if (!success)
        {
          throw new Exception($"No numeric id found in url '{Url}'");
        }
        return id;
      }
    }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("height")]
    public string Height { get; set; }

    public short HeightInCm
    {
      get
      {
        short result;
        if (short.TryParse(Height, out result))
          return result;
        return 0;
      }
    }

    [JsonPropertyName("mass")]
    public string Mass { get; set; }

    public short Weight
    {
      get
      {
        short result;
        if (short.TryParse(Mass, out result))
          return result;
        return 0;
      }
    }

    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; }

    [JsonPropertyName("StarRating")]
    public int StarRating { get; set; }
  }
}