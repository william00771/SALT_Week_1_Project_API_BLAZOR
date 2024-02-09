using FluentAssertions;
using Salt.Stars.API.Services;
using Xunit;

namespace Salt.Stars.Greeter.Tests
{
  public class StarFileReaderIntegrationTests
  {
    [Fact]
    public void should_create_star_file_client()
    {
      // act
      var client = new StarFileClient();

      // assert
      client.Should().NotBe(null);
    }

    [Fact]
    public async void should_write_star_with_empty_file()
    {
      // arrange
      var client = new StarFileClient();
      client.EmptyFile();

      // act
      var stars = await client.AddStarsForHero(143, 3);

      // assert
      stars.Should().Be(3);
    }

    [Fact]
    public async void should_write_star_with_file_with_other_data()
    {
      // arrange
      var client = new StarFileClient();
      client.EmptyFile();
      await client.AddStarsForHero(143, 3);

      // act
      var stars = await client.AddStarsForHero(1, 5);

      // assert
      stars.Should().Be(5);
    }

    [Fact]
    public async void should_get_stars()
    {
      // arrange
      var client = new StarFileClient();
      client.EmptyFile();
      await client.AddStarsForHero(143, 3);

      // act
      var stars = await client.GetStarsForHero(143);

      // assert
      stars.Should().Be(3);
    }

    [Fact]
    public async void should_get_stars_when_more_than_one_hero()
    {
      // arrange
      var client = new StarFileClient();
      client.EmptyFile();
      await client.AddStarsForHero(141, 1);
      await client.AddStarsForHero(5, 5);
      await client.AddStarsForHero(143, 3);

      // act
      var stars = await client.GetStarsForHero(5);

      // assert
      stars.Should().Be(5);
    }

    [Fact]
    public async void should_get_minus1_for_non_existing_hero()
    {
      // arrange
      var client = new StarFileClient();
      client.EmptyFile();
      await client.AddStarsForHero(143, 3);

      // act
      var stars = await client.GetStarsForHero(1);

      // assert
      stars.Should().Be(-1);
    }

    [Fact]
    public async void should_get_minus1_for_empty_file()
    {
      // arrange
      var client = new StarFileClient();
      client.EmptyFile();

      // act
      var stars = await client.GetStarsForHero(1);

      // assert
      stars.Should().Be(-1);
    }

    [Fact]
    public async void should_not_add_a_lot_of_newlines()
    {
      // arrange
      var client = new StarFileClient();
      client.EmptyFile();
      await client.AddStarsForHero(1, 3);
      await client.AddStarsForHero(2, 3);
      await client.AddStarsForHero(3, 3);
      await client.AddStarsForHero(4, 3);
      await client.AddStarsForHero(5, 1);
      await client.AddStarsForHero(5, 2);
      await client.AddStarsForHero(5, 3);
      await client.AddStarsForHero(5, 4);
      await client.AddStarsForHero(5, 5);
      await client.AddStarsForHero(6, 3);

      // act
      var stars = await client.GetStarsForHero(5);

      // assert
      stars.Should().Be(1);
    }
  }
}
