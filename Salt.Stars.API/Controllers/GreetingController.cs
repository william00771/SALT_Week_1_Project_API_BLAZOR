using System;
using Microsoft.AspNetCore.Mvc;
using Salt.Stars.API.Models;
using Salt.Stars.Greeter;

namespace Salt.Stars.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GreetingController : ControllerBase
  {
    private readonly IGreeter _greeter;

    public GreetingController(IGreeter greeter)
    {
      this._greeter = greeter;
    }

    [HttpGet]
    public GreetingResponse Get(string name, DateTime? clientDate)
    {
      var greetingTime = clientDate.HasValue ? clientDate.Value : DateTime.Now;

      if (string.IsNullOrEmpty(name))
      {
        return new GreetingResponse
        {
          ErrorMessage = "No name passed in",
          GreetedAt = null
        };
      }

      var greeting = this._greeter.greet(name, greetingTime);

      return new GreetingResponse
      {
        Name = name,
        GreetedAt = greetingTime,
        Greeting = greeting,
      };
    }
  }
}
