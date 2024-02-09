using System.Collections.Generic;

namespace Salt.Stars.Greeter.Tests
{
  public class MockGreetingFileReader : IGreetingFileReader
  {
    public string[] Responses { get; set; }

    public string[] GetImpoliteGreetings()
    {
      return Responses;
    }

    public IEnumerable<string> GetPoliteGreetings()
    {
      return Responses;
    }
  }
}