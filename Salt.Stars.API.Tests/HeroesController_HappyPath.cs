using Salt.Stars.API.Controllers;
using Xunit;
using FluentAssertions;
using Moq;
using Salt.Stars.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Salt.Stars.API.Services;

namespace Salt.Stars.API.Tests
{

  public class HeroesController_HappyPath
  {
    HeroesController controller;
    Mock<ISwApiClient> _apiClientMock;
    Mock<IStarFileClient> _starFileClientMock;

    HeroListResponse MOCK_HEROES_DATA = new HeroListResponse
    {
      Count = 143,
      PageSize = 2,
      Heroes = new List<Hero> { new Hero { Name = "Captain Kirk" }, new Hero { Name = "Kosh Naranek" } }
    };

    Hero MOCK_HERO_DATA = new Hero { Name = "Captain Kirk", Url = "https://trekkiapi.dev/api/people/3232/" };
    int MOCK_NUMBER_STARS = 5;

    public HeroesController_HappyPath()
    {
      // Arrange
      _apiClientMock = new Mock<ISwApiClient>();
      _apiClientMock.Setup(apiClient => apiClient.getHerosFromSwapi().Result).Returns(MOCK_HEROES_DATA);
      _apiClientMock.Setup(apiClient => apiClient.getHeroFromSwapi(It.IsAny<int>()).Result).Returns(MOCK_HERO_DATA);

      _starFileClientMock = new Mock<IStarFileClient>();
      _starFileClientMock.Setup(fileClient => fileClient.GetStarsForHero(It.IsAny<int>()).Result).Returns(MOCK_NUMBER_STARS);

      this.controller = new HeroesController(_apiClientMock.Object, _starFileClientMock.Object);
    }

    [Fact]
    public void get_heroes_should_return_a_response()
    {
      // Act
      var response = controller.GetHeroListAsync();

      // Assert
      response.Should().NotBe(null);
      response.Status.Should().Be(TaskStatus.RanToCompletion);
    }

    [Fact]
    public void get_heroes_should_return_a_mocked_data()
    {
      // Act
      var response = controller.GetHeroListAsync();

      // Assert
      var data = response.Result.Value;
      data.Count.Should().Be(MOCK_HEROES_DATA.Count);
      data.PageSize.Should().Be(MOCK_HEROES_DATA.PageSize);
      data.Heroes.Count.Should().Be(MOCK_HEROES_DATA.Heroes.Count);
    }

    [Fact]
    public void get_hero_should_return_a_response()
    {
      // Act
      var response = controller.GetHeroAsync(123);

      // Assert
      response.Should().NotBe(null);
      response.Status.Should().Be(TaskStatus.RanToCompletion);
    }

    [Fact]
    public void get_hero_should_return_a_mocked_data()
    {
      // Act
      var response = controller.GetHeroAsync(234);

      // Assert
      var hero = response.Result.Value.Hero;
      hero.Name.Should().Be(MOCK_HERO_DATA.Name);
      hero.Id.Should().Be(3232);
      hero.StarRating.Should().Be(MOCK_NUMBER_STARS);
    }
  }
}
