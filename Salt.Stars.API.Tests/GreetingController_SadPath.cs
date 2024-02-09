using Salt.Stars.API.Controllers;
using Xunit;
using FluentAssertions;
using Salt.Stars.API.Models;

namespace Salt.Stars.API.Tests
{

  public class GreetingController_SadPath
  {
    GreetingResponse response;

    public GreetingController_SadPath()
    {
      // Arrange
      var mockGreeter = new MockGreeter();
      var controller = new GreetingController(mockGreeter);

      // act
      response = controller.Get(null, null);
    }

    [Fact]
    public void should_give_no_null_response_for_no_name()
    {
      // Assert
      response.Should().NotBe(null);
    }

    [Fact]
    public void should_give_error_message_for_no_name()
    {
      // Assert
      response.ErrorMessage.Should().NotBe(null);
    }

    [Fact]
    public void should_not_have_greeting_for_no_name()
    {
      // Assert
      response.Greeting.Should().Be(null);
    }

    [Fact]
    public void should_not_have_name_for_no_name()
    {
      // Assert
      response.Name.Should().Be(null);
    }

    [Fact]
    public void should_not_have_greeted_date_for_no_name()
    {
      // Assert
      response.GreetedAt.Should().Be(null);
    }
  }
}
