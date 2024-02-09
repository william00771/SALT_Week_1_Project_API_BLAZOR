using System.Collections.Generic;

namespace Salt.Stars.Greeter
{
  public interface IGreetingFileReader
  {
    IEnumerable<string> GetPoliteGreetings();
    string[] GetImpoliteGreetings();
  }
}