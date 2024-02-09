using System;
using FluentAssertions;
using Salt.Stars.API.Models;
using Xunit;

namespace Salt.Stars.API.Tests
{

  public class HeroModelTests
  {
    [Fact]
    public void should_correctly_translate_swapiurl_to_id()
    {
      // act
      var hero = new Hero
      {
        Url = "https://swapi.py4e.com/api/people/10/"
      };

      // assert
      hero.Id.Should().Be(10);
    }

    [Fact]
    public void should_correctly_handle_no_trailing_slash()
    {
      // act
      var hero = new Hero
      {
        Url = "https://swapi.py4e.com/api/people/10"
      };

      // assert
      hero.Id.Should().Be(10);
    }

    [Fact]
    public void should_throw_for_no_id()
    {
      // arrange
      const string URL_TO_TEST = "https://swapi.py4e.com/api/people/";

      // act
      Action act = () =>
      {
        var hero = new Hero
        {
          Url = URL_TO_TEST
        };

        // This line to exercise the id-property
        Console.WriteLine(hero.Id);
      };

      // assert
      act.Should().Throw<Exception>().WithMessage($"No numeric id found in url '{URL_TO_TEST}'");
    }
  }
}