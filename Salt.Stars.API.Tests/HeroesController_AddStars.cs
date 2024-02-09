using Salt.Stars.API.Controllers;
using Xunit;
using Moq;
using Salt.Stars.API.Services;
using Salt.Stars.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Salt.Stars.API.Tests
{

  public class HeroesController_AddStars
  {
    HeroesController controller;
    Mock<IStarFileClient> _starFileMockClient;

    public HeroesController_AddStars()
    {
      // Arrange
      _starFileMockClient = new Mock<IStarFileClient>();
      this.controller = new HeroesController(null, _starFileMockClient.Object);
    }

    [Fact]
    public void should_add_stars_for_hero()
    {
      // Arrange
      var heroId = 14;
      var newNumberOfStars = 3;
      var request = new HeroStarUpdateRequest { NewStarRating = newNumberOfStars };

      _starFileMockClient.Setup(fileMock => fileMock.AddStarsForHero(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(3);

      // Act
      var response = controller.AddStarToHeroAsync(heroId, request);

      // Assert
      _starFileMockClient.Verify(fileMock => fileMock.AddStarsForHero(heroId, newNumberOfStars));

      var responseValue = (response.Result.Result as OkObjectResult).Value.As<HeroStarUpdateResponse>();
      responseValue.HeroId.Should().Be(heroId);
      responseValue.StarRating.Should().Be(3);
    }

    [Fact]
    public void should_handle_error()
    {
      // Arrange
      _starFileMockClient.Setup(fileMock => fileMock.AddStarsForHero(It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();

      // Act
      var response = controller.AddStarToHeroAsync(-1, null);

      // Assert
      var responseValue = (response.Result.Result as NotFoundObjectResult);
      responseValue.Should().NotBe(null);
      responseValue.Value.Should().NotBe(string.Empty);
    }

  }
}
