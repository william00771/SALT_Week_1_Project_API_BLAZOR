using Salt.Stars.API.Controllers;
using Xunit;
using FluentAssertions;
using Moq;
using System;
using Salt.Stars.API.Services;

namespace Salt.Stars.API.Tests
{

  public class HeroesController_SadPath
  {
    HeroesController controller;
    Mock<ISwApiClient> _apiClientMock;

    public HeroesController_SadPath()
    {
      // Arrange
      _apiClientMock = new Mock<ISwApiClient>();
      _apiClientMock.Setup(apiClient => apiClient.getHerosFromSwapi().Result).Throws<Exception>();
      _apiClientMock.Setup(apiClient => apiClient.getHeroFromSwapi(It.IsAny<short>()).Result).Throws<Exception>();

      this.controller = new HeroesController(_apiClientMock.Object, null);
    }

    [Fact]
    public void getHeroes_should_handle_exception_from_API()
    {
      // Act
      var response = controller.GetHeroListAsync();

      // Assert
      response.Should().NotBe(null);
      response.Result.Value.Should().Be(null);
    }

    [Fact]
    public void getHero_should_handle_exception_from_API()
    {
      // Act
      var response = controller.GetHeroAsync(10);

      // Assert
      response.Should().NotBe(null);
      response.Result.Value.Should().Be(null);
    }
  }
}
