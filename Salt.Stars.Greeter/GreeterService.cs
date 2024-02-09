using System;
using System.Linq;

namespace Salt.Stars.Greeter
{

  public class GreeterService : IGreeter
  {
    private readonly IGreetingFileReader _reader;
    private const string NAME_KEY = "{name}";

    public GreeterService(IGreetingFileReader reader)
    {
      _reader = reader;
    }

    public GreeterService() : this(new GreetingFileReader("./data")) { }

    private string getRandomGreetingString(string[] greetings)
    {
      var randIndex = new Random().Next(greetings.Length);
      return greetings[randIndex];
    }
    public string greetNicely(string name)
    {
      var niceGreetings = _reader.GetPoliteGreetings().ToArray();
      var greetingTemplate = getRandomGreetingString(niceGreetings);
      return greetingTemplate.Replace(NAME_KEY, name);
    }

    public string greetImpolitely(string name)
    {
      var impoliteGreetings = _reader.GetImpoliteGreetings();
      var greetingTemplate = getRandomGreetingString(impoliteGreetings);
      return greetingTemplate.Replace(NAME_KEY, name);
    }

    public string greet(string name, DateTime dt) =>
      dt.Second % 2 == 0 ?
        greetNicely(name) :
        greetImpolitely(name);
  }
}
