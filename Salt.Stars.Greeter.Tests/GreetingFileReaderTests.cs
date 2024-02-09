using System;
using FluentAssertions;
using Xunit;

namespace Salt.Stars.Greeter.Tests
{
  public class GreetingFileReaderTests
  {

    [Fact]
    public void should_create_file_reader_with_existing_directory()
    {
      // Act
      var fileReader = new GreetingFileReader("data");

      // assert
      fileReader.Should().NotBe(null);
    }

    [Fact]
    public void should_throw_with_non_existing_directory()
    {
      // act
      Action act = () => new GreetingFileReader("not_a_directory");

      // assert
      act.Should().Throw<Exception>();
    }

    [Fact]
    public void should_read_polite_greetings()
    {
      // arrange
      var fileReader = new GreetingFileReader("data");

      // Arrange
      var politeGreetings = fileReader.GetPoliteGreetings();

      // assert
      politeGreetings.Should().NotBeEmpty();
      politeGreetings.Should().NotContain("pickle");
    }

    [Fact]
    public void should_read_imppolite_greetings()
    {
      // arrange
      var fileReader = new GreetingFileReader("data");

      // Arrange
      var politeGreetings = fileReader.GetImpoliteGreetings();

      // assert
      politeGreetings.Should().NotBeEmpty();
      politeGreetings.Should().NotContain("nice");
    }
  }
}