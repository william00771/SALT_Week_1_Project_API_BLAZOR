using System;

namespace Salt.Stars.API.Models
{
  public class GreetingResponse
  {
    public string Name { get; set; }
    public DateTime? GreetedAt { get; set; }
    public string Greeting { get; set; }
    public string ErrorMessage { get; set; }
  }

}