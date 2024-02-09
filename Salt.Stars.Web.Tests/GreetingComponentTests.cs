using System;
using FluentAssertions;
using Moq;
using Salt.Stars.Web.Models;
using Salt.Stars.Web.Services;
using Salt.Stars.Web.Shared;

namespace Salt.Stars.Web.Tests
{
  public class GreetingComponentTests
  {

    private GreetingResponse MOCK_RESPONSE = new GreetingResponse { Name = "Marcus", Greeting = "Jora" };

    private Mock<IGreetingApiClient> _apiClientMock;
    private IRenderedComponent<Greeting> _component;
    private TestContext _ctx;

    public GreetingComponentTests()
    {
      _apiClientMock = new Mock<IGreetingApiClient>();
      _apiClientMock.Setup(apiClient => apiClient.GetGreeting(It.IsAny<string>()).Result).Returns(MOCK_RESPONSE);

      _ctx = new TestContext();
      _ctx.Services.AddSingleton<IGreetingApiClient>(_apiClientMock.Object);
      _component = _ctx.RenderComponent<Greeting>();
    }

    [Fact]
    public void should_create_component()
    {
      // Assert
      _component.Should().NotBeNull();
      _component.Instance.Should().BeOfType<Greeting>();
    }

    [Fact]
    public void should_get_name()
    {
      // Arrange
      _component = _ctx.RenderComponent<Greeting>(builder =>
      {
        builder.Add(c => c.Name, "APA");
      });

      // Act
      _apiClientMock.Verify(p => p.GetGreeting("APA"));

      // Assert
      _component.Should().NotBeNull();
      _component.Instance.GreetingResponse!.Name.Should().Be(MOCK_RESPONSE.Name);
    }

    [Fact]
    public void should_get_greeting()
    {
      // Arrange
      _component = _ctx.RenderComponent<Greeting>(builder =>
      {
        builder.Add(c => c.Name, "APA");
      });

      // Act
      _apiClientMock.Verify(p => p.GetGreeting("APA"));

      // Assert
      _component.Should().NotBeNull();
      _component.Instance.GreetingResponse!.Greeting.Should().Be(MOCK_RESPONSE.Greeting);
    }

    [Fact]
    public void should_handle_exception_from_API()
    {
      // Arrange
      var apiClientMock = new Mock<IGreetingApiClient>(MockBehavior.Strict);
      apiClientMock.Setup(apiClient => apiClient.GetGreeting(It.IsAny<string>()).Result).Throws<Exception>();

      var ctx = new TestContext();
      ctx.Services.AddSingleton<IGreetingApiClient>(apiClientMock.Object);

      _component = ctx.RenderComponent<Greeting>(builder =>
      {
        builder.Add(c => c.Name, "APA");
      });

      // Assert
      _component.Instance.GreetingResponse.Should().Be(null);
      _component.Instance.ErrorMessage.Should().NotBe(string.Empty);
    }
  }
}
