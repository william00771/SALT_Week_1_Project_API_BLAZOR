using Microsoft.Extensions.DependencyInjection;
using System;
using FluentAssertions;
using Moq;
using Salt.Stars.Web.Models;
using Salt.Stars.Web.Services;
using Salt.Stars.Web.Shared;
using HeroPage = Salt.Stars.Web.Pages.HeroPage;

namespace Salt.Stars.Web.Tests;

public class HeroPageTests
{
  private Mock<IHeroApiClient> _apiClientMock;
  private IRenderedComponent<HeroPage> _pageUnderTest;
  private readonly TestContext _ctx;

  HeroResponse MOCK_RESPONSE = new HeroResponse
  {
    RequestedAt = DateTime.Now,
    Hero = new Hero { Name = "Captain Kirk", StarRating = 2 }
  };

  public HeroPageTests()
  {
    _apiClientMock = new Mock<IHeroApiClient>(); //Mocking dependency
    _apiClientMock.Setup(apiClient => apiClient.GetHero(It.IsAny<int>()).Result)
                  .Returns(MOCK_RESPONSE);

    _ctx = new TestContext();
    _ctx.Services.AddSingleton<IHeroApiClient>(_apiClientMock.Object);
    _pageUnderTest = _ctx.RenderComponent<HeroPage>();
  }

  [Fact]
  public void should_render_page()
  {
    // Assert
    _pageUnderTest.Should().NotBeNull();
    _pageUnderTest.Instance.Should().BeOfType<HeroPage>();
  }

  [Fact]
  public void should_get_hero()
  {
    // Arrange
    _pageUnderTest = _ctx.RenderComponent<HeroPage>(builder =>
    {
      builder.Add(c => c.Id, 123);
    });

    // Act
    _apiClientMock.Verify(client => client.GetHero(_pageUnderTest.Instance.Id));

    // Assert
    _pageUnderTest.Instance.HeroResponse.Should().NotBeNull();
    _pageUnderTest.Instance.HeroResponse!.RequestedAt.Should().Be(MOCK_RESPONSE.RequestedAt);
    _pageUnderTest.Instance.Hero!.Name.Should().Be(MOCK_RESPONSE.Hero!.Name);
  }

  [Fact]
  public void should_handle_exception_from_api()
  {
    // Arrange
    _apiClientMock = new Mock<IHeroApiClient>(MockBehavior.Strict);
    _apiClientMock.Setup(apiService => apiService.GetHero(It.IsAny<int>()).Result).Throws<Exception>();

    var ctx = new TestContext();
    ctx.Services.AddSingleton<IHeroApiClient>(_apiClientMock.Object);
    _pageUnderTest = ctx.RenderComponent<HeroPage>(builder =>
    {
      builder.Add(c => c.Id, 123);
    });

    // Assert
    _pageUnderTest.Instance.HeroResponse.Should().BeNull();
    _pageUnderTest.Instance.ErrorMessage.Should().NotBe(string.Empty);
  }

  [Fact]
  public void should_have_stars_on_hero()
  {
    // Arrange
    _pageUnderTest = _ctx.RenderComponent<HeroPage>(builder =>
    {
      builder.Add(c => c.Id, 123);
    });
    // Assert
    _pageUnderTest.Instance.Hero.Should().NotBeNull();
    _pageUnderTest.Instance.Hero!.StarRating.Should().Be(MOCK_RESPONSE.Hero!.StarRating);
  }

  [Fact]
  public void should_update_stars_on_hero()
  {
    // Arrange
    var response = new StarUpdateResponse { HeroId = 123, CurrentNumberOfStars = 4, RequestedAt = DateTime.Now };

    _apiClientMock = new Mock<IHeroApiClient>(MockBehavior.Strict);
    _apiClientMock.Setup(apiService => apiService.UpdateStars(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(response);
    _apiClientMock.Setup(apiClient => apiClient.GetHero(It.IsAny<int>()).Result).Returns(MOCK_RESPONSE);

    var ctx = new TestContext();
    ctx.Services.AddSingleton<IHeroApiClient>(_apiClientMock.Object);

    _pageUnderTest = ctx.RenderComponent<HeroPage>(builder =>
    {
      builder.Add(c => c.Id, 123);
    });

    // Act
    _pageUnderTest.Instance.Hero!.StarRating = 4;
    _pageUnderTest.Instance.SetStarRating();

    // Assert
    _apiClientMock.Verify(apiClient => apiClient.UpdateStars(_pageUnderTest.Instance.Id, _pageUnderTest.Instance.Hero!.StarRating));
    _pageUnderTest.Instance.HeroResponse.Should().NotBeNull();
    _pageUnderTest.Instance.Hero.Should().NotBeNull();
    _pageUnderTest.Instance.HeroResponse!.Hero!.StarRating.Should().Be(4);
    _pageUnderTest.Instance.ErrorMessage.Should().Be(null);
  }

  [Fact]
  public void should_handle_error_when_update_stars_for_hero()
  {
    // Arrange
    _apiClientMock = new Mock<IHeroApiClient>(MockBehavior.Strict);
    _apiClientMock.Setup(apiClient => apiClient.UpdateStars(It.IsAny<int>(), It.IsAny<int>()).Result).Throws<Exception>();
    _apiClientMock.Setup(apiClient => apiClient.GetHero(It.IsAny<int>()).Result).Returns(MOCK_RESPONSE);

    var ctx = new TestContext();
    ctx.Services.AddSingleton<IHeroApiClient>(_apiClientMock.Object);

    _pageUnderTest = ctx.RenderComponent<HeroPage>(builder =>
    {
      builder.Add(c => c.Id, 123);
    });

    _pageUnderTest.Instance.Hero!.StarRating = 4;
    _pageUnderTest.Instance.SetStarRating();

    // Assert;
    _pageUnderTest.Instance.HeroResponse.Should().NotBeNull();
    _pageUnderTest.Instance.Hero.Should().NotBeNull();
    _pageUnderTest.Instance.Hero!.StarRating.Should().Be(4);
    _pageUnderTest.Instance.ErrorMessage.Should().NotBe(String.Empty);
  }
}
