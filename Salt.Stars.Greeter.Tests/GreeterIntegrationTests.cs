using FluentAssertions;
using Xunit;

namespace Salt.Stars.Greeter.Tests
{
  public class GreeterIntegrationTests
  {

    IGreeter _greeter;
    public GreeterIntegrationTests()
    {
      _greeter = new GreeterService();
    }

    [Fact]
    public void should_greet_me_nicely()
    {
      // act
      var greeting = _greeter.greetNicely("Ossian");

      // assert
      greeting.ToLower()
        .Should()
        .Contain("ossian");
    }

    [Fact]
    public void should_greet_me_impolitely()
    {
      // act
      var greeting = _greeter.greetImpolitely("Marcus");

      // assert
      greeting.ToLower()
        .Should()
        .Contain("marcus");
    }
  }
}
