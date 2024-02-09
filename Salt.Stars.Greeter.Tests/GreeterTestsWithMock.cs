using Xunit;
using FluentAssertions;
using System;

namespace Salt.Stars.Greeter.Tests
{
  public class GreeterTestsWithMock
  {
    MockGreetingFileReader mockFileReader = new MockGreetingFileReader();

    [Fact]
    public void should_greet_me_nicely_by_name_when_one_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "You are nice, Marcus" };
      var greeter = new GreeterService(mockFileReader);

      // act
      var greeting = greeter.greetNicely("Marcus");

      // assert
      greeting.ToLower()
        .Should()
        .Contain("marcus").And
        .Contain("nice");
    }

    [Fact]
    public void should_greet_me_nicely_by_name_when_more_than_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "You are fine, Marcus", "You da bomb, Marcus" };
      var greeter = new GreeterService(mockFileReader);

      // act
      var greeting = greeter.greetNicely("Marcus");

      // assert
      greeting
        .Should()
        .BeOneOf("You are fine, Marcus", "You da bomb, Marcus");
    }

    [Fact]
    public void should_greet_name_nicely_by_name_when_more_than_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "You are fine, {name}", "You da bomb, {name}" };
      var greeter = new GreeterService(mockFileReader);

      // act
      var greeting = greeter.greetNicely("Anders");

      // assert
      greeting
        .Should()
        .BeOneOf("You are fine, Anders", "You da bomb, Anders");
    }

    [Fact]
    public void should_flip_me_off_when_one_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "Ossian - suck a pickle!" };
      var greeter = new GreeterService(mockFileReader);

      // act
      var greeting = greeter.greetImpolitely("Ossian");

      // assert
      greeting.ToLower()
              .Should()
              .Contain("ossian").And
              .Contain("pickle");
    }

    [Fact]
    public void should_flip_me_off_when_more_than_one_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "Ossian - suck a pickle!", "Ossian - go away!", "Ossian - no thanks." };
      var greeter = new GreeterService(mockFileReader);

      // act
      var greeting = greeter.greetImpolitely("Ossian");

      // assert
      greeting
        .Should()
        .BeOneOf("Ossian - suck a pickle!", "Ossian - go away!", "Ossian - no thanks.");
    }

    [Fact]
    public void should_flip_name_off_when_more_than_one_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "Brendan - suck a pickle!", "Brendan - go away!", "Brendan - no thanks." };
      var greeter = new GreeterService(mockFileReader);

      // act
      var greeting = greeter.greetImpolitely("Brendan");

      // assert
      greeting
        .Should()
        .BeOneOf("Brendan - suck a pickle!", "Brendan - go away!", "Brendan - no thanks.");
    }

    [Fact]
    public void should_greet_nicely_if_even_seconds_in_time_when_one_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "You are nice Ossian" };
      var greeter = new GreeterService(mockFileReader);
      var dt = new DateTime(1972, 10, 9, 03, 32, 2, DateTimeKind.Local);

      // act
      var greeting = greeter.greet("Ossian", dt);

      // assert
      greeting.ToLower()
              .Should()
              .Contain("ossian").And
              .Contain("nice");
    }

    [Fact]
    public void should_greet_impolitely_if_odd_seconds_in_time_when_one_response()
    {
      // arrange
      mockFileReader.Responses = new string[] { "Marcus - suck a pickle!" };
      var greeter = new GreeterService(mockFileReader);
      var dt = new DateTime(1972, 10, 9, 03, 32, 21, DateTimeKind.Local);

      // act
      var greeting = greeter.greet("Marcus", dt);

      // assert
      greeting.ToLower()
              .Should()
              .Contain("marcus").And
              .Contain("pickle");
    }
  }
}
