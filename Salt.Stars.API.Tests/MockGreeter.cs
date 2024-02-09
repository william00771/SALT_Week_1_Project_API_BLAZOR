using System;
using Salt.Stars.Greeter;

namespace Salt.Stars.API.Tests
{
  public class MockGreeter : IGreeter
  {
    public string greet(string name, DateTime dt) => $"Greeting for {name}";
    public string greetNicely(string name) => $"Nice greeting for {name}";
    public string greetImpolitely(string name) => $"Impolite greeting for {name}";

  }
}
