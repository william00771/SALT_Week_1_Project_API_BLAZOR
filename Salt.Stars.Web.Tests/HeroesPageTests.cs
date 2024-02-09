using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Salt.Stars.Web.Models;
using Salt.Stars.Web.Services;
using Salt.Stars.Web.Shared;
using HeroesPage = Salt.Stars.Web.Pages.HeroesPage;

namespace Salt.Stars.Web.Tests;

public class HeroesPageTests
{
  private Mock<IHeroApiClient> _apiClientMock;
  private IRenderedComponent<HeroesPage> _pageUnderTest;
  private readonly TestContext _ctx;

  HeroesResponse MOCK_RESPONSE = new HeroesResponse
  {
    Count = 143,
    PageSize = 2,
    Heroes = new List<Hero> { new Hero { Name = "Captain Kirk" }, new Hero { Name = "Kosh Naranek" } }
  };

  public HeroesPageTests()
  {
    _apiClientMock = new Mock<IHeroApiClient>(); //Mocking dependency
    _apiClientMock.Setup(apiClient => apiClient.GetHeroes().Result).Returns(MOCK_RESPONSE);

    _ctx = new TestContext();
    _ctx.Services.AddSingleton<IHeroApiClient>(_apiClientMock.Object);
    _pageUnderTest = _ctx.RenderComponent<HeroesPage>();
  }

  [Fact]
  public void should_render_page()
  {
    // Assert
    _pageUnderTest.Should().NotBeNull();
    _pageUnderTest.Instance.Should().BeOfType<HeroesPage>();
  }

  [Fact]
  public void should_get_heroes()
  {
    // Act
    _apiClientMock.Verify(p => p.GetHeroes());

    // Assert
    _pageUnderTest.Instance.HeroesResponse.Should().NotBeNull();
    _pageUnderTest.Instance.HeroesResponse!.Heroes.Should().NotBeNull();

    _pageUnderTest.Instance.HeroesResponse!.Heroes!.Count.Should().NotBe(0);
    _pageUnderTest.Instance.HeroesResponse!.Heroes!.Count.Should().Be(MOCK_RESPONSE.Heroes!.Count);

    _pageUnderTest.Instance.HeroesResponse.Heroes.Should().AllBeOfType<Hero>();
    _pageUnderTest.Instance.HeroesResponse.Heroes![0].Name.Should().Be(MOCK_RESPONSE.Heroes![0].Name);
  }

  [Fact]
  public void should_handle_exception_from_api()
  {
    // Arrange
    _apiClientMock = new Mock<IHeroApiClient>(MockBehavior.Strict);
    _apiClientMock.Setup(apiClient => apiClient.GetHero(It.IsAny<int>()).Result).Throws<Exception>();

    var ctx = new TestContext();
    ctx.Services.AddSingleton<IHeroApiClient>(_apiClientMock.Object);
    _pageUnderTest = ctx.RenderComponent<HeroesPage>();

    // Act
    _apiClientMock.Verify(p => p.GetHeroes());

    // Assert
    _pageUnderTest.Instance.HeroesResponse.Should().BeNull();
    _pageUnderTest.Instance.ErrorMessage.Should().NotBe(string.Empty);
  }
}