using FluentAssertions;
using Moq;
using Salt.Stars.Web.Services;
using Index = Salt.Stars.Web.Pages.Index;

namespace Salt.Stars.Web.Tests
{
  public class IndexPageTests
  {
    private IRenderedComponent<Index> _pageUnderTest;
    private TestContext _ctx;

    public IndexPageTests()
    {
      _ctx = new TestContext();
      _ctx.Services.AddSingleton<IGreetingApiClient>(new Mock<IGreetingApiClient>().Object);
      _pageUnderTest = _ctx.RenderComponent<Index>();
    }

    [Fact]
    public void should_create_page()
    {
      // Assert
      _pageUnderTest.Should().NotBeNull();
      _pageUnderTest.Instance.Should().BeOfType<Index>();
    }
  }
}
