using Salt.Stars.API.Controllers;
using Xunit;
using FluentAssertions;
using System;

namespace Salt.Stars.API.Tests
{

  public class GreetingController_HappyPath
  {
    const string TEST_NAME = "Marcus";
    GreetingController controller;

    public GreetingController_HappyPath()
    {
      // Arrange
      var mockGreeter = new MockGreeter();
      this.controller = new GreetingController(mockGreeter);
    }

    [Fact]
    public void should_return_a_response()
    {
      // Act
      var response = controller.Get(TEST_NAME, null);

      // Assert
      response.Should().NotBe(null);
    }

    [Fact]
    public void should_return_a_response_with_name()
    {
      // Act
      var response = controller.Get(TEST_NAME, null);

      // Assert
      response.Name.Should().Be(TEST_NAME);
    }
    [Fact]
    public void should_return_a_response_with_greeting_date()
    {
      // Act
      var response = controller.Get(TEST_NAME, null);

      // Assert
      response.GreetedAt.Should().NotBe(null);
    }

    [Fact]
    public void should_return_a_response_with_a_greeting()
    {
      // Act
      var response = controller.Get(TEST_NAME, null);

      // Assert
      response.Greeting.Length.Should().NotBe(0);
      response.Greeting.Should().Contain(TEST_NAME);
    }

    [Fact]
    public void should_pass_date_and_get_same_date_back()
    {
      // Arrange
      var dt = new DateTime(1972, 10, 9, 0, 0, 0, DateTimeKind.Local);

      // Act
      var response = controller.Get(TEST_NAME, dt);

      // Assert
      response.GreetedAt.Value.Day.Should().Be(9);
    }
  }
}
