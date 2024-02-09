using System;

namespace Salt.Stars.Greeter
{
  public interface IGreeter
  {
    string greet(string name, DateTime dt);
    string greetImpolitely(string name);
    string greetNicely(string name);
  }
}
