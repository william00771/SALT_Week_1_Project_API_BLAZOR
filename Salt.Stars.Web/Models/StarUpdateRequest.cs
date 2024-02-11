using System.Text.Json.Serialization;

namespace Salt.Stars.Web.Models
{
  public class StarUpdateRequest
  {
    public int NewStarRating { get; set; }
  }
}
